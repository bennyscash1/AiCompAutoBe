﻿using ComprehensiveAutomation.MobileTest.Inital;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService
{
    public class AppiumMenegar
    {

        #region appium server run
        public async Task RunAppiumServer()
        {
            bool IsAppiumRunning()
            {
                try
                {
                    string baseUrlAppium = MobileAiDriverFactory.baseAppiumUrl; // 
                    string statusUrl = $"{baseUrlAppium}/status";

                    using var client = new HttpClient();
                    var response = client.GetAsync(statusUrl).Result;
                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    return false;
                }
            }

            // Check if already running
            if (IsAppiumRunning())
            {
                Console.WriteLine("Appium server is already running.");
                return;
            }
            string appiumPath = GetAppiumCmdPath();
            Console.WriteLine($"Appium path install: {appiumPath}");
            string appiumArg = $"--address 127.0.0.1 --port {MobileAiDriverFactory.appiumPort}";
            if (!File.Exists(appiumPath))
                throw new FileNotFoundException("Appium path not being found: " + appiumPath);
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = appiumPath,
                    Arguments = appiumArg,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(appiumPath) // ✅ חשוב שיהיה נכון עבור cmd
                }
            };

            process.OutputDataReceived += (sender, args) => Console.WriteLine("OUT: " + args.Data);
            process.ErrorDataReceived += (sender, args) => Console.WriteLine("ERR: " + args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Retry until it's available
            int maxRetries = 10;
            for (int i = 0; i < maxRetries; i++)
            {
                if (IsAppiumRunning())
                {
                    Console.WriteLine("Appium server is ready.");
                    return;
                }

                await Task.Delay(1000);
                Console.WriteLine($"Waiting for Appium server... retry {i + 1}/{maxRetries}");
            }

            throw new Exception("Appium server failed to start after multiple retries.");
        }
        #endregion


        public static string GetAppiumCmdPath()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null && dir.Exists)
            {
                string potential = Path.Combine(dir.FullName, "MobileTools", "AppiumService", "appium.cmd");
                Console.WriteLine( $"Search appium file on {potential}");
                if (File.Exists(potential))
                    return potential;

                dir = dir.Parent;
            }
            Console.WriteLine("Appium folder not being found, make sure the folder exsist");
            throw new FileNotFoundException("appium.cmd not found by recursive search.");
        }
        

    }
}
