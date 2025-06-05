using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using TaskManager.Models;


    namespace TaskManager.Data
    {
        public static class TaskRepository
        {
            private static string dbPath = "Data/tasks.db";

            public static void AddTask(TaskModel task)
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    var query = "INSERT INTO Tasks (Title, Description, DueDate, IsCompleted) VALUES (@Title, @Description, @DueDate, @IsCompleted)";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", task.Title);
                        command.Parameters.AddWithValue("@Description", task.Description);
                        command.Parameters.AddWithValue("@DueDate", task.DueDate);
                        command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted ? 1 : 0);
                        command.ExecuteNonQuery();
                    }
                }
            }

            public static List<TaskModel> GetAllTasks()
            {
                var tasks = new List<TaskModel>();
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    var query = "SELECT * FROM Tasks";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                DueDate = reader.GetString(3),
                                IsCompleted = reader.GetInt32(4) == 1
                            });
                        }
                    }
                }

                return tasks;
            }

            public static void UpdateTask(TaskModel task)
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    var query = "UPDATE Tasks SET Title=@Title, Description=@Description, DueDate=@DueDate, IsCompleted=@IsCompleted WHERE Id=@Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", task.Title);
                        command.Parameters.AddWithValue("@Description", task.Description);
                        command.Parameters.AddWithValue("@DueDate", task.DueDate);
                        command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted ? 1 : 0);
                        command.Parameters.AddWithValue("@Id", task.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            public static void DeleteTask(int taskId)
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    var query = "DELETE FROM Tasks WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", taskId);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }

