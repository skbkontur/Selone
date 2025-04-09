using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kontur.Selone.Tests.Integration;
using OpenQA.Selenium;

namespace Kontur.Selone.Tests.Extensions
{
    public static class ScreenshotExtensions
    {
        private static readonly HashSet<char> invalidPathChars = new HashSet<char>(Path.GetInvalidPathChars());

        public static void Save(this Screenshot screenshot, string dir, string file)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var filename = $"{DateTime.Now:yyyy.MM.dd-HH.mm.ss.fff}-0x{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}-{EscapePath(file, 100)}.png";
            var screenshotPath = Path.Combine(dir, filename);
            try
            {
                screenshot.SaveAsFile(screenshotPath);
                Console.WriteLine($"Screenshot saved to '{screenshotPath}'");
                if (TeamcityHelper.IsTeamCity())
                {
                    TeamcityHelper.PublishArtifacts(screenshotPath, ".screenshots");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot save screenshot to '{screenshotPath}'");
                Console.WriteLine(e);
            }
        }

        private static string EscapePath(string testName, int maxLength)
        {
            var resultLength = Math.Min(maxLength, testName.Length);
            var result = new StringBuilder(resultLength);

            foreach (var symbol in testName.Take(resultLength))
            {
                var value = !IsUrlSafeChar(symbol) || invalidPathChars.Contains(symbol) ? '_' : symbol;
                result.Append(value);
            }

            return result.ToString();
        }

        //copied from System.Web.Util.HttpEncoderUtility
        private static bool IsUrlSafeChar(char ch)
        {
            if ((ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90) || (ch >= 48 && ch <= 57) || ch == 33)
                return true;
            switch (ch)
            {
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                    return true;
                default:
                    return false;
            }
        }
    }
}