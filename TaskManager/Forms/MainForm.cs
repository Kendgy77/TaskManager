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
            var form = new AddEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTasks(); // оновлення списку після додавання
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (tasksGridView.CurrentRow == null)
            {
                MessageBox.Show("Wybierz zadanie do edycji.");
                return;
            }

            var selectedTask = (TaskModel)tasksGridView.CurrentRow.DataBoundItem;

            var form = new AddEditForm(selectedTask); // Передаємо обране завдання у форму
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTasks(); // оновлення списку після редагування
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (tasksGridView.CurrentRow == null)
            {
                MessageBox.Show("Wybierz zadanie do usunięcia.");
                return;
            }

            var selectedTask = (TaskModel)tasksGridView.CurrentRow.DataBoundItem;

            var confirm = MessageBox.Show($"Czy na pewno chcesz usunąć zadanie '{selectedTask.Title}'?", "Potwierdzenie", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                TaskRepository.DeleteTask(selectedTask.Id); // видаляємо
                LoadTasks(); // оновлюємо
            }
        }
    }
}
