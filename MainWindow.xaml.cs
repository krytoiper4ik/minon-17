using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MatrixOperations
{
    public partial class MainWindow : Window
    {
        private List<Matrix> file1Matrices = new List<Matrix>();
        private List<Matrix> file2Matrices = new List<Matrix>();
        private List<Matrix> file3Matrices = new List<Matrix>();
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitializeTaskComboBox();
        }

        private void InitializeTaskComboBox()
        {
            for (int i = 1; i <= 30; i++)
            {
                taskComboBox.Items.Add($"Task {i}");
            }
            taskComboBox.SelectedIndex = 0;
        }

        private void TaskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int taskNumber = taskComboBox.SelectedIndex + 1;
            lTextBox.IsEnabled = taskNumber != 9 && taskNumber != 18 && taskNumber != 21 && taskNumber != 29;
        }

        private void GenerateMatrices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int k = int.Parse(kTextBox.Text);
                int m = int.Parse(mTextBox.Text);
                int n = int.Parse(nTextBox.Text);
                int l = lTextBox.IsEnabled ? int.Parse(lTextBox.Text) : 0;

                file1Matrices.Clear();
                file2Matrices.Clear();

                
                for (int i = 0; i < k; i++)
                {
                    file1Matrices.Add(Matrix.GenerateRandomMatrix(m, n, random));
                }

               
                if (l > 0)
                {
                    for (int i = 0; i < l; i++)
                    {
                        file2Matrices.Add(Matrix.GenerateRandomMatrix(m, n, random));
                    }
                }

                DisplayMatrices();
                MessageBox.Show("Matrices generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating matrices: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayMatrices()
        {
            file1Matrices.Items.Clear();
            file2Matrices.Items.Clear();

            foreach (var matrix in file1Matrices)
            {
                file1Matrices.Items.Add(CreateMatrixDisplayItem(matrix));
            }

            foreach (var matrix in file2Matrices)
            {
                file2Matrices.Items.Add(CreateMatrixDisplayItem(matrix));
            }
        }

        private FrameworkElement CreateMatrixDisplayItem(Matrix matrix)
        {
            var stackPanel = new StackPanel { Margin = new Thickness(0, 0, 0, 10) };
            var grid = new Grid();
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                var rowPanel = new StackPanel { Orientation = Orientation.Horizontal };
                
                for (int j = 0; j < matrix.Cols; j++)
                {
                    rowPanel.Children.Add(new TextBlock 
                    { 
                        Text = matrix.Data[i, j].ToString("F2"), 
                        Width = 50, 
                        Margin = new Thickness(2),
                        TextAlignment = TextAlignment.Center
                    });
                }
                
                Grid.SetRow(rowPanel, i);
                grid.Children.Add(rowPanel);
            }
            
            stackPanel.Children.Add(grid);
            return stackPanel;
        }

        private void ExecuteTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int taskNumber = taskComboBox.SelectedIndex + 1;
                file3Matrices.Clear();

                switch (taskNumber)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                    case 5: Task5(); break;
                    case 6: Task6(); break;
                    case 7: Task7(); break;
                    case 8: Task8(); break;
                    case 9: Task9(); break;
                    case 10: Task10(); break;
                    case 11: Task11(); break;
                    case 12: Task12(); break;
                    case 13: Task13(); break;
                    case 14: Task14(); break;
                    case 15: Task15(); break;
                    case 16: Task16(); break;
                    case 17: Task17(); break;
                    case 18: Task18(); break;
                    case 19: Task19(); break;
                    case 20: Task20(); break;
                    case 21: Task21(); break;
                    case 22: Task22(); break;
                    case 23: Task23(); break;
                    case 24: Task24(); break;
                    case 25: Task25(); break;
                    case 26: Task26(); break;
                    case 27: Task27(); break;
                    case 28: Task28(); break;
                    case 29: Task29(); break;
                    case 30: Task30(); break;
                }

                DisplayResults();
                MessageBox.Show($"Task {taskNumber} executed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayResults()
        {
            file1Contents.Text = "File 1:\n" + string.Join("\n\n", file1Matrices.Select(m => m.ToString()));
            file2Contents.Text = "File 2:\n" + string.Join("\n\n", file2Matrices.Select(m => m.ToString()));
            file3Contents.Text = "File 3:\n" + string.Join("\n\n", file3Matrices.Select(m => m.ToString()));
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            file1Matrices.Clear();
            file2Matrices.Clear();
            file3Matrices.Clear();
            file1Matrices.Items.Clear();
            file2Matrices.Items.Clear();
            file1Contents.Text = "";
            file2Contents.Text = "";
            file3Contents.Text = "";
        }

        
        private void Task1()
        {
            // Move matrices from file1 where a[0,0] = 0 to end of file2
            var toMove = file1Matrices.Where(m => m.Data[0, 0] == 0).ToList();
            foreach (var matrix in toMove)
            {
                file2Matrices.Add(matrix);
                file1Matrices.Remove(matrix);
            }
        }

        private void Task2()
        {
           
            if (file1Matrices.Count > file2Matrices.Count)
            {
                int diff = file1Matrices.Count - file2Matrices.Count;
                file3Matrices.AddRange(file1Matrices.TakeLast(diff));
                file1Matrices = file1Matrices.Take(file2Matrices.Count).ToList();
            }
            else if (file2Matrices.Count > file1Matrices.Count)
            {
                int diff = file2Matrices.Count - file1Matrices.Count;
                file3Matrices.AddRange(file2Matrices.TakeLast(diff));
                file2Matrices = file2Matrices.Take(file1Matrices.Count).ToList();
            }
        }

        private void Task3()
        {
            
            if (file1Matrices.Count != file2Matrices.Count)
                throw new InvalidOperationException("Number of matrices in both files must be equal");
            
            file3Matrices.Clear();
            for (int i = 0; i < file1Matrices.Count; i++)
            {
                file3Matrices.Add(Matrix.Multiply(file1Matrices[i], file2Matrices[i]));
            }
        }

       
        
        private void Task29()
        {
            
            file2Matrices.Clear();
            foreach (var matrix in file1Matrices)
            {
                double product = matrix.ScalarProductOfDiagonals();
                if (product > 15)
                {
                    file2Matrices.Add(matrix);
                }
            }
        }

        private void Task30()
        {
            
            var eligibleMatrices = file1Matrices.Where(m => m.Data[0, 0] == 5).ToList();
            int minCount = Math.Min(eligibleMatrices.Count, file2Matrices.Count);
            
            for (int i = 0; i < minCount; i++)
            {
                file2Matrices[i].ReplaceDiagonals(eligibleMatrices[i]);
            }
        }
    }
}
