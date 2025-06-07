using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using TaskManager.Models;


namespace TaskManager.Data
{
    public static class TaskRepository
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "tasks.db");


        public static void AddTask(TaskModel task)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas dodawania zadania: " + ex.Message);
            }
        }

        public static List<TaskModel> GetAllTasks()
        {
            var tasks = new List<TaskModel>();
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd ładowania zadań: " + ex.Message);
            }

            return tasks;
        }

        public static void UpdateTask(TaskModel task)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas aktualizowania zadania: " + ex.Message);
            }
        }

        public static void DeleteTask(int taskId)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas usuwania zadania: " + ex.Message);
            }
        }
    }
}

