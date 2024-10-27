using System;
using System.Text.Json;

namespace MainActivity
{
    public class ViewMyLogs
    {
        string UsersFilePath = EnvFilePath.logs;

        public async Task LogsAsync(string username)
        {
            Header header = new Header("View Logs");

            // Check if log file exists
            if (!File.Exists(UsersFilePath))
            {
                Console.WriteLine("User data file not found.");
            }

            // Read and deserialize the log data
            var usersData = await File.ReadAllTextAsync(UsersFilePath);
            var logs = JsonSerializer.Deserialize<List<LogsService>>(usersData) ?? new List<LogsService>();

            // Filter logs for the given username
            var userLogs = logs.Where(log => log.Username == username).ToList();

            // Display logs
            if (userLogs.Count == 0)
            {
                Console.WriteLine($"No logs found for user: {username}");
            }
            else
            {
                Console.WriteLine($"Logs for {username}:");
                foreach (var log in userLogs)
                {
                    Console.WriteLine($"Action: {log.Log}, Date: {log.LogDate}");
                }

                Console.WriteLine();
                Console.Write("Generate a report file? (y/n)");
                string response = Console.ReadLine();

                Console.Clear();
                if (response.ToLower() == "y")
                {
                    // Generate the report
                    await GenerateReportAsync(username, userLogs);
                    Console.WriteLine("Report generated successfully.");
                }
                else
                {
                    Console.WriteLine("Report generation skipped.");
                }
            }

        }

        private async Task GenerateReportAsync(string username, List<LogsService> userLogs)
        {
            // Define the report file path
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HH:mm");

            string reportFilePath = Path.Combine(EnvFilePath.reports, $"{username}_logs_report_{timestamp}.txt");

            // Create the report content
            using (StreamWriter writer = new StreamWriter(reportFilePath))
            {
                await writer.WriteLineAsync($"Logs Report for {username}");
                await writer.WriteLineAsync("--------------------------------------");
                foreach (var log in userLogs)
                {
                    await writer.WriteLineAsync($"Action: {log.Log}, Date: {log.LogDate}");
                }
                await writer.WriteLineAsync("--------------------------------------");
                await writer.WriteLineAsync($"Report generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
            }

            // Optional: Notify where the report is saved
            Console.WriteLine($"The report has been saved {username}_logs_report_{timestamp}.txt");
        }
    }
}
