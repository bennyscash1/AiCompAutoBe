﻿using CliWrap;
using CliWrap.Buffered;
using ComprehensiveAutomation.MobileTest.Inital;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService
{
    public class MobileEmulatorMenegar
    {
        #region Check if any device connect
        public bool IsAnyDeviceConnected()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = "devices",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var lines = output.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Skip header and check if any line ends with "device" (online)
            return lines.Skip(1).Any(line => line.EndsWith("\tdevice"));
        }
        #endregion

        #region Kill adb and restart it
        public void RestartAdb()
        {
            Console.WriteLine("Restarting ADB...");
            Process.Start("adb", "kill-server")?.WaitForExit();
            Process.Start("adb", "start-server")?.WaitForExit();
        }
        #endregion

        #region get device id 
        public string GetFirstConnectedDeviceId()
        {
            int retries = 0;
            while (retries++ < 20)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "adb",
                        Arguments = "devices",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var lines = output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                  .Skip(1)
                                  .Where(line => line.Contains("\tdevice"))
                                  .ToList();

                if (lines.Any())
                {
                    return lines.First().Split('\t')[0];
                }

                Console.WriteLine("Waiting for device to connect...");
                Thread.Sleep(1000);
            }

            throw new Exception("No connected device found after waiting.");
        }
        #endregion

        #region Check if device is ready to use
        public bool IsDeviceBootCompleted(string deviceId)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "adb",
                        Arguments = $"-s {deviceId} shell getprop sys.boot_completed",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                return output == "1";
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Run the emulator and make sure it ready to use
        public string  EnsureDeviceIsRunning(string emulatorName = "Small_Phone_API_35")
        {
            if (!IsAnyDeviceConnected())
            {
                RestartAdb();

                if (!IsAnyDeviceConnected())
                {
                    Console.WriteLine("No devices found. Starting emulator...");
                    //This is the locator for the emulator path - need to add it to cloud
                    string emulatorPath = GetEmulatorExePath();
                    if (!File.Exists(emulatorPath))
                        throw new FileNotFoundException("emulator.exe not found at: " + emulatorPath);

                    string commandAvd = $"-avd {emulatorName}";
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = emulatorPath,
                        Arguments = commandAvd,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    });

                    // Wait for emulator to appear in adb
                    int retries = 0;
                    while (!IsAnyDeviceConnected() && retries++ < 40)
                    {
                        Console.WriteLine("Waiting for emulator to appear in adb...");
                        Thread.Sleep(2000);
                    }

                    if (!IsAnyDeviceConnected())
                        throw new Exception("Emulator failed to connect to ADB.");
                }
            }

            Console.WriteLine("Device is connected via ADB.");

            // ✅ Wait for device to finish booting
            
            string deviceId = GetFirstConnectedDeviceId();
            int bootCheckRetries = 0;
            while (!IsDeviceBootCompleted(deviceId) && bootCheckRetries++ < 60)
            {
                Console.WriteLine("Waiting for device to complete boot...");
                Thread.Sleep(2000);
            }

            if (!IsDeviceBootCompleted(deviceId))
                throw new Exception("Device did not finish booting in time.");
            Console.WriteLine("Device is ready for use.");
            return deviceId;
        }
        #endregion

        #region Get app opened name 
        public static string GetForegroundAppName()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "adb",
                        Arguments = "shell \"dumpsys window | grep mCurrentFocus\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (string.IsNullOrWhiteSpace(output) || !output.Contains("mCurrentFocus"))
                    return "No app found.";

                var index = output.IndexOf("mCurrentFocus=");
                if (index == -1)
                    return "Parsing error.";

                var line = output.Substring(index + "mCurrentFocus=".Length).Trim();
                var parts = line.Split('/');

                if (parts.Length < 2)
                    return "Package info not found.";

                var package = parts[0];
                var appName = package.Split('.').Last().Trim();

                return appName;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        #endregion

        #region Test if the app is emlulator
        public static bool IsRuningDeviceEmulator()
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "adb",
                        Arguments = "shell getprop ro.product.model",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                output = output.ToLowerInvariant();

                // Common emulator model names: 'sdk', 'emulator', 'generic'
                return output.Contains("sdk") || output.Contains("emulator") || output.Contains("generic");
            }
            catch
            {
                return false;
            }
        }
        #endregion

        public enum EmulatorEnumList
        {
            Pixel_2_API_35,
            Pixel_4_API_35,
            Pixel_5_API_36,
            Pixel_API_35,
            Pixel_API_35_2,
            Pixel_XL_API_35,
            Small_Phone_API_35
        }

        public static string GetEmulatorExePath()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null && dir.Exists)
            {
                string potential = Path.Combine(dir.FullName, "MobileTools", "emulator", "emulator.exe");
                if (File.Exists(potential))
                    return potential;

                dir = dir.Parent;
            }

            throw new FileNotFoundException("emulator.exe not found by recursive search.");
        }


    }
}
