using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace MatrixOperationsApp
{
    public partial class MainWindow : Window
    {
        private MatrixOperations matrixOps;
        private FileOperations fileOps;
        
        public MainWindow()
        {
            InitializeComponent();
            matrixOps = new MatrixOperations();
            fileOps = new FileOperations();
            
            
            for (int i = 1; i <= 30; i++)
            {
                TaskComboBox.Items.Add($"Task {i}");
            }
        }
        
        private void TaskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputPanel.Children.Clear();
            
            int selectedTask = TaskComboBox.SelectedIndex + 1;
            
            
            AddInputControl("k (number of matrices in File 1):", "kTextBox");
            AddInputControl("m (rows):", "mTextBox");
            AddInputControl("n (columns):", "nTextBox");
            
            
            switch (selectedTask)
            {
                case 3:
                case 15:
                    AddInputControl("l (columns for second matrix):", "lTextBox");
                    break;
                case 4:
                case 7:
                case 14:
                case 19:
                case 25:
                case 28:
                case 30:
                    AddInputControl("l (number of matrices in File 2):", "lTextBox");
                    break;
                case 5:
                case 17:
                    
                    break;
                case 10:
                case 22:
                    
                    break;
                case 23:
                    AddInputControl("l (columns for second matrix):", "lTextBox");
                    break;
            }
            
            
            AddInputControl("File 1 path:", "file1TextBox");
            AddInputControl("File 2 path:", "file2TextBox");
            
            if (selectedTask == 2 || selectedTask == 5 || selectedTask == 10 || selectedTask == 12 || 
                selectedTask == 14 || selectedTask == 17 || selectedTask == 22 || selectedTask == 23 || 
                selectedTask == 24 || selectedTask == 26 || selectedTask == 27 || selectedTask == 28)
            {
                AddInputControl("File 3 path:", "file3TextBox");
            }
        }
        
        private void AddInputControl(string label, string name)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0,5,0,0) };
            stackPanel.Children.Add(new TextBlock { Text = label, Width = 150 });
            stackPanel.Children.Add(new TextBox { Name = name, Width = 200 });
            InputPanel.Children.Add(stackPanel);
        }
        
        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int taskNumber = TaskComboBox.SelectedIndex + 1;
                if (taskNumber < 1 || taskNumber > 30)
                {
                    MessageBox.Show("Please select a valid task (1-30)");
                    return;
                }
                
               
                int k = int.Parse(FindTextBox("kTextBox").Text);
                int m = int.Parse(FindTextBox("mTextBox").Text);
                int n = int.Parse(FindTextBox("nTextBox").Text);
                
                string file1Path = FindTextBox("file1TextBox").Text;
                string file2Path = FindTextBox("file2TextBox").Text;
                string file3Path = taskNumber == 2 || taskNumber == 5 || taskNumber == 10 || taskNumber == 12 || 
                                  taskNumber == 14 || taskNumber == 17 || taskNumber == 22 || taskNumber == 23 || 
                                  taskNumber == 24 || taskNumber == 26 || taskNumber == 27 || taskNumber == 28 ? 
                                  FindTextBox("file3TextBox").Text : string.Empty;
                
                
                int l = 0;
                if (taskNumber == 3 || taskNumber == 4 || taskNumber == 7 || taskNumber == 14 || 
                    taskNumber == 15 || taskNumber == 19 || taskNumber == 23 || taskNumber == 25 || 
                    taskNumber == 28 || taskNumber == 30)
                {
                    l = int.Parse(FindTextBox("lTextBox").Text);
                }
                
               
                string result = string.Empty;
                switch (taskNumber)
                {
                    case 1:
                        result = matrixOps.Task1(k, m, n, file1Path, file2Path);
                        break;
                    case 2:
                        result = matrixOps.Task2(k, m, n, file1Path, file2Path, file3Path);
                        break;
                    case 3:
                        result = matrixOps.Task3(k, m, n, l, file1Path, file2Path);
                        break;
                   
                    case 30:
                        result = matrixOps.Task30(k, m, n, file1Path, file2Path);
                        break;
                    default:
                        result = "Task not implemented yet";
                        break;
                }
                
               
                OutputTextBox.Text = result;
                UpdateFileContents(file1Path, file2Path, file3Path);
                StatusText.Text = $"Task {taskNumber} completed successfully";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
                MessageBox.Show($"Error executing task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private TextBox FindTextBox(string name)
        {
            return (TextBox)InputPanel.Children
                .OfType<StackPanel>()
                .SelectMany(sp => sp.Children.OfType<TextBox>())
                .FirstOrDefault(tb => tb.Name == name);
        }
        
        private void UpdateFileContents(string file1Path, string file2Path, string file3Path)
        {
            try
            {
                File1TextBox.Text = File.Exists(file1Path) ? File.ReadAllText(file1Path) : "File not found";
                File2TextBox.Text = File.Exists(file2Path) ? File.ReadAllText(file2Path) : "File not found";
                File3TextBox.Text = !string.IsNullOrEmpty(file3Path) && File.Exists(file3Path) ? 
                                    File.ReadAllText(file3Path) : "File not found or not used";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error reading files: {ex.Message}";
            }
        }
    }
}
