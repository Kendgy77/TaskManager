using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Data;
using TaskManager.Models;
using System.IO;
using System;


namespace TaskManager.Tests
{
    [TestClass]
    public class TaskRepositoryTests
    {
        private string testDbPath;

        [TestInitialize]
        public void Setup()
        {
           
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            testDbPath = Path.Combine(baseDir, "test_tasks.db");

            if (File.Exists(testDbPath))
                File.Delete(testDbPath);

            SQLite.SQLiteConnection.CreateFile(testDbPath);
            DatabaseInitializer.Initialize(); 
        }

        [TestMethod]
        public void AddTask_ShouldInsertTask()
        {
            var task = new TaskModel
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.Now.ToShortDateString(),
                IsCompleted = false
            };

            TaskRepository.AddTask(task);
            var tasks = TaskRepository.GetAllTasks();

            Assert.IsTrue(tasks.Exists(t => t.Title == "Test Task"));
        }
    }
}
