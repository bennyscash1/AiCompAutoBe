using AiCompAutoBe.MobileTest.MobileServices;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators
{
    public class RecordLocatoreService
    {
        public static string screenHandler = "#SCREEN";
        public string CreateRecordFile(string fileName = "")
        {
            string chromeGeneralPath = Path.Combine(Directory.GetCurrentDirectory(), "MobileTest", "MobileServices", "RecordLocators", "LocatorsFiles");
            Directory.CreateDirectory(chromeGeneralPath);

            if (string.IsNullOrEmpty(fileName))
                fileName = Guid.NewGuid().ToString() + ".txt";
            else
                fileName = fileName + ".txt";

            return Path.Combine(chromeGeneralPath, fileName);
        }
        public static string GetRecordFileFullPath(string fileName)
        {
            string chromeGeneralPath = Path.Combine(Directory.GetCurrentDirectory(), "MobileTest", "MobileServices", "RecordLocators", "LocatorsFiles");
            Directory.CreateDirectory(chromeGeneralPath);
            fileName = fileName + ".txt";
            return Path.Combine(chromeGeneralPath, fileName);
        }
        public static (int x, int y) GetDevicesSize()
        {
            string? screenSizeLine = RunShell("adb shell wm size")
                .Split('\n')
                .FirstOrDefault(x => x.Contains("Physical size"));

            if (screenSizeLine == null)
                throw new Exception("Unable to get device screen size.");

            // Extract "1080x1920" from "Physical size: 1080x1920"
            string sizePart = screenSizeLine.Split(":")[1].Trim();
            var parts = sizePart.Split("x");

            int width = int.Parse(parts[0]);
            int height = int.Parse(parts[1]);

            return (width, height);
        }
        public static string GetDeviceScreenSizeString()
        {
            string output = RunShell("adb shell wm size");
            return $"The current device screen size is: {output.Trim()}";
        }

        public Process StartAdbRecordingToFile(string fullFilePath)
        {
            (int deviceX, int deviceY) screenSizeLine = GetDevicesSize();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = "shell getevent -t",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            Task.Run(async () =>
            {
                using var writer = new StreamWriter(fullFilePath);
                if (!string.IsNullOrEmpty(screenSizeLine.deviceX.ToString()))
                    await writer.WriteLineAsync($"{screenHandler} Physical size: {screenSizeLine.deviceX}x{screenSizeLine.deviceY}");

                while (!process.StandardOutput.EndOfStream)
                {
                    var line = await process.StandardOutput.ReadLineAsync();
                    if (line != null)
                        await writer.WriteLineAsync(line);
                }
            });

            return process;
        }

        private static string RunShell(string cmd)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {cmd}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return output;
        }

        public void StopAdbRecording(Process process)
        {
            if (process != null && !process.HasExited)
            {
                Thread.Sleep(500); // wait for I/O flush (you can bump this if needed)

                process.Kill();
                Thread.Sleep(1000); // wait for I/O flush (you can bump this if needed)
                process.Dispose();
            }
        }
      
        public static List<(int x, int y)> ExtractTouchCoordinates(string eventFilePath)
        {
            
            bool isDeviceEmulator = MobileEmulatorMenegar.IsRuningDeviceEmulator();
            if (isDeviceEmulator)
            {
                var coordinates = ExtractTouchCoordinatesForEmulator(eventFilePath);
                return coordinates;
            }
            else
            {
                var coordinates = ExtractTouchCoordinatesForRealDevice(eventFilePath);
                return coordinates;
            }
        }
        public static List<(int x, int y)> ExtractTouchCoordinatesForRealDevice(
                                        string eventFilePath)
        {
            var lines = File.ReadAllLines(eventFilePath);

            // 1️⃣  physical screen size of the device you will replay on
            var (screenW, screenH) = GetDevicesSize();

            // 2️⃣  true raw-axis range
            var (maxRawX, maxRawY) = GetTouchAxisRange();   // ~16383 on Xiaomi/Redmi

            var pts = new List<(int x, int y)>();
            int? rx = null, ry = null;
            bool finger = false;

            foreach (var l in lines)
            {
                if (l.Contains(" 0039 "))            // tracking-ID
                    finger = !l.Contains("ffffffff"); // up / down

                if (!finger) continue;

                if (l.Contains(" 0035 ")) rx = Convert.ToInt32(l.Split().Last(), 16);
                if (l.Contains(" 0036 ")) ry = Convert.ToInt32(l.Split().Last(), 16);

                if (rx.HasValue && ry.HasValue)
                {
                    int px = rx.Value * screenW / maxRawX;
                    int py = ry.Value * screenH / maxRawY;
                    pts.Add((px, py));

                    rx = ry = null;                  // one tap per contact
                    finger = false;
                }
            }
            return pts;
        }



        public static List<(int x, int y)> ExtractTouchCoordinatesForEmulator(string eventFilePath)
        {
            var allLines = File.ReadAllLines(eventFilePath).ToList();

            (int currentWidth, int currentHeight) = GetDevicesSize();
            var coordinates = new List<(int x, int y)>();

            int? rawX = null, rawY = null;
            bool touchStarted = false;
            bool coordinateCaptured = false;

            // Set assumed max raw touch range
            const int maxRaw = 32768; // << This is the real missing thing!

            foreach (var line in allLines)
            {
                if (line.Contains("0039"))
                {
                    if (line.Contains("ffffffff"))
                    {
                        touchStarted = false;
                        coordinateCaptured = false;
                    }
                    else
                    {
                        touchStarted = true;
                        coordinateCaptured = false;
                        rawX = rawY = null;
                    }
                }

                if (!touchStarted || coordinateCaptured)
                    continue;

                if (line.Contains("0035"))
                    rawX = Convert.ToInt32(line.Trim().Split(' ').Last(), 16);

                if (line.Contains("0036"))
                    rawY = Convert.ToInt32(line.Trim().Split(' ').Last(), 16);

                if (rawX.HasValue && rawY.HasValue)
                {
                    int scaledX = rawX.Value * currentWidth / maxRaw;
                    int scaledY = rawY.Value * currentHeight / maxRaw;

                    (scaledX, scaledY) = AddSmallRandomOffset(scaledX, scaledY);

                    coordinates.Add((scaledX, scaledY));
                    rawX = rawY = null;
                    coordinateCaptured = true;
                }

            }

            return coordinates;
        }


        private static Random _rand = new Random();

        private static (int x, int y) AddSmallRandomOffset(int x, int y)
        {
            // רק מוסיף 0 או 1 לפיקסל ב-X
            int offsetX = _rand.Next(0, 2); // 0 או 1
            return (x + offsetX, y);        // y נשאר זהה
        }

        private static (int maxX, int maxY) GetTouchAxisRange()
        {
            string output = CommandServices.RunAdbCommand("shell getevent -pl");

            int Extract(string axis)
            {
                foreach (var line in output.Split('\n'))
                {
                    if (!line.Contains(axis, StringComparison.OrdinalIgnoreCase))
                        continue;

                    // “… max 8640”   or   “… max    19200”
                    var m = Regex.Match(line, @"max\s+(\d+)");
                    if (m.Success) return int.Parse(m.Groups[1].Value);
                }
                return 4095;   // last-resort fallback
            }

            return (Extract("ABS_MT_POSITION_X"),   // → 8640
                    Extract("ABS_MT_POSITION_Y"));  // → 19200
        }














    }
}
