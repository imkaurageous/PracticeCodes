using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EmployeeApplication
{
    public class Logger
    {
        private readonly string _logDirectory;

        public Logger(string logDirectory)
        {
            _logDirectory = logDirectory;

            // Ensure the directory exists
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }


        public void LogMessage(string message)
        {
            // Generate file name based on current date
            string fileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
            string filePath = Path.Combine(_logDirectory, fileName);

            // Prepare log entry
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}";

            // Append log entry to the file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (IOException ex)
            {
                // Handle file IO exceptions if necessary
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }


    }
}