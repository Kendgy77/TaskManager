using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadTasks(); 
        }

        
        private void LoadTasks()
        {
            var tasks = TaskRepository.GetAllTasks(); 
            tasksGridView.DataSource = null;          
            tasksGridView.DataSource = tasks;

            tasksGridView.Columns["Id"].Visible = false;
            tasksGridView.Columns["Title"].HeaderText = "Tytuł";
            tasksGridView.Columns["Description"].HeaderText = "Opis";
            tasksGridView.Columns["DueDate"].HeaderText = "Termin";
            tasksGridView.Columns["IsCompleted"].HeaderText = "Zakończone";
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadTasks(); 
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // TODO: відкриття форми додавання завдання
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // TODO: відкриття форми редагування вибраного завдання
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // TODO: видалення вибраного завдання
        }
    }
}
