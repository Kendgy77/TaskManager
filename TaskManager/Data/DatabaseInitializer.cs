using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;



namespace TaskManager.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "tasks.db");

            
            string folderPath = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); 
            }

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                string createTableQuery = @"
        CREATE TABLE IF NOT EXISTS Tasks (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            Description TEXT,
            DueDate TEXT NOT NULL,
            IsCompleted INTEGER NOT NULL CHECK (IsCompleted IN (0, 1))
        );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
