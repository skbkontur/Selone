using System;

namespace Kontur.Selone.Tests.Integration
{
    public static class TeamcityHelper
    {
        public static bool IsTeamCity()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
        }

        public static void PublishArtifacts(string filePath, string to)
        {
            Console.WriteLine($"##teamcity[publishArtifacts '{filePath} => {to}']");
        }
    }
}