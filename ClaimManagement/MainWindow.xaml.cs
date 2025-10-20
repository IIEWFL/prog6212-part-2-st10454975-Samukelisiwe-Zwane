using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ClaimManagement
{
    public partial class MainWindow : Window
    {
        private List<string> uploadedFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Supported Files|*.pdf;*.docx;*.xlsx",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                // File size limit: 5 MB
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("File size exceeds 5 MB limit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create folder for uploads if it doesn’t exist
                string uploadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                // Copy file to upload folder
                string destinationPath = Path.Combine(uploadFolder, fileInfo.Name);
                File.Copy(fileInfo.FullName, destinationPath, true);

                // Add to list
                uploadedFiles.Add(fileInfo.Name);
                UploadedFilesList.Items.Add(fileInfo.Name);

                MessageBox.Show("File uploaded successfully.", "Upload Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HoursWorkedTextBox.Text) ||
                string.IsNullOrWhiteSpace(HourlyRateTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal hours = decimal.Parse(HoursWorkedTextBox.Text);
            decimal rate = decimal.Parse(HourlyRateTextBox.Text);
            decimal total = hours * rate;

            MessageBox.Show($"Claim submitted successfully!\nTotal Amount: R{total}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear fields after submission
            ClaimMonthPicker.SelectedDate = null;
            HoursWorkedTextBox.Clear();
            HourlyRateTextBox.Clear();
            NotesTextBox.Clear();
            UploadedFilesList.Items.Clear();
            uploadedFiles.Clear();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClaimMonthPicker.SelectedDate = null;
            HoursWorkedTextBox.Clear();
            HourlyRateTextBox.Clear();
            NotesTextBox.Clear();
            UploadedFilesList.Items.Clear();
            uploadedFiles.Clear();
        }
    }
}
