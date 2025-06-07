using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.Models;
using TaskManager.Data;

namespace TaskManager
{
    public partial class AddEditForm : Form
    {
        private TaskModel currentTask;

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void dueDatePicker_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void completedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            
        }

        public AddEditForm(TaskModel task = null)
        {
            InitializeComponent();
            currentTask = task;

            
            if (currentTask != null)
            {
                
                titleTextBox.Text = currentTask.Title;
                descriptionTextBox.Text = currentTask.Description;
                dueDatePicker.Value = DateTime.TryParse(currentTask.DueDate, out DateTime parsedDate)
                    ? parsedDate
                    : DateTime.Now;
                completedCheckBox.Checked = currentTask.IsCompleted;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text;
            string description = descriptionTextBox.Text;
            string dueDate = dueDatePicker.Value.ToString("yyyy-MM-dd");
            bool isCompleted = completedCheckBox.Checked;

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Tytuł nie może być pusty!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (currentTask == null)
                {
                    var newTask = new TaskModel
                    {
                        Title = title,
                        Description = description,
                        DueDate = dueDate,
                        IsCompleted = isCompleted
                    };

                    TaskRepository.AddTask(newTask);
                }
                else
                {
                    currentTask.Title = title;
                    currentTask.Description = description;
                    currentTask.DueDate = dueDate;
                    currentTask.IsCompleted = isCompleted;

                    TaskRepository.UpdateTask(currentTask);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas zapisywania zadania: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}