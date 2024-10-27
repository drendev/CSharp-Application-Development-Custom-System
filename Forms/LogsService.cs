using System;
using System.Text.Json;


namespace MainActivity
{
    public class LogsService: ILogs
    {
        public string Username { get; set; }
        public string Log { get; set; }
        public string LogDate { get; set; }

        public LogsService(string username, string log, string logDate)
        {
            Username = username;
            Log = log;
            LogDate = logDate;
        }

        public async Task SaveToFile(string jsonPath)
        {
            List<LogsService> logs;

            if (File.Exists(jsonPath))
            {
                // Read and deserialize existing data
                var existingData = await File.ReadAllTextAsync(jsonPath);
                logs = JsonSerializer.Deserialize<List<LogsService>>(existingData) ?? new List<LogsService>();
            }
            else
            {
                logs = new List<LogsService>();
            }

            // Add the new log and serialize the list
            logs.Add(this);
            string jsonString = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(jsonPath, jsonString);
        }
    }
}
