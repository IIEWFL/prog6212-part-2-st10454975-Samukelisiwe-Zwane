using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace ClaimManagement
{
    public partial class MainWindow : Window
    {
        // ObservableCollection so UI updates automatically
        public ObservableCollection<Claim> ClaimsList { get; set; } = new ObservableCollection<Claim>();

        // store uploaded file names for the current submission
        private ObservableCollection<string> uploadedFiles = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            // sample data to test the DataGrid UI
            ClaimsList.Add(new Claim { ClaimID = 1, Lecturer = "John Doe", ContractorID = 101, ClaimMonth = DateTime.Now.AddMonths(-1), HoursWorked = 40, SubmissionDate = DateTime.Now.AddDays(-7), Status = "Pending", CalculatedAmount = 40m * 200m });
            ClaimsList.Add(new Claim { ClaimID = 2, Lecturer = "Jane Smith", ContractorID = 102, ClaimMonth = DateTime.Now.AddMonths(-2), HoursWorked = 35, SubmissionDate = DateTime.Now.AddDays(-10), Status = "Approved", CalculatedAmount = 35m * 200m });
            ClaimsList.Add(new Claim { ClaimID = 3, Lecturer = "Bob Johnson", ContractorID = 103, ClaimMonth = DateTime.Now.AddMonths(-1), HoursWorked = 45, SubmissionDate = DateTime.Now.AddDays(-5), Status = "Rejected", CalculatedAmount = 45m * 200m });

            // bind collections to the grids / listbox
            ClaimsDataGrid.ItemsSource = ClaimsList;
            ViewClaimsDataGrid.ItemsSource = ClaimsList;
            UploadedFilesList.ItemsSource = uploadedFiles;
        }


        public void UpdateClaimStatus(int claimID, string newStatus)
        {
            var claim = ClaimsList.FirstOrDefault(c => c.ClaimID == claimID);
            if (claim != null)
            {
                claim.Status = newStatus;
                RefreshGrids();
            }
        }


        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Supported Files (*.pdf;*.docx;*.xlsx)|*.pdf;*.docx;*.xlsx",
                Multiselect = false
            };

            if (dlg.ShowDialog() == true)
            {
                var fi = new FileInfo(dlg.FileName);

                // 5 MB limit
                if (fi.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("File size exceeds 5 MB.", "Upload error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string uploadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles");
                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                string dest = Path.Combine(uploadFolder, fi.Name);
                File.Copy(fi.FullName, dest, true);

                uploadedFiles.Add(fi.Name);
                MessageBox.Show("File uploaded.", "Upload", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Submit claim - creates new Claim and adds to list
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClaimantNameTextBox.Text))
            {
                MessageBox.Show("Please enter the claimant name.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(HoursWorkedTextBox.Text) || string.IsNullOrWhiteSpace(HourlyRateTextBox.Text))
            {
                MessageBox.Show("Please fill required fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(HoursWorkedTextBox.Text, out decimal hours) || !decimal.TryParse(HourlyRateTextBox.Text, out decimal rate))
            {
                MessageBox.Show("Please enter valid numbers for hours and rate.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newClaim = new Claim
            {
                ClaimID = GenerateNewClaimID(),
                Lecturer = ClaimantNameTextBox.Text,
                ContractorID = 1, // placeholder
                ContractID = 1,
                ClaimMonth = ClaimMonthPicker.SelectedDate ?? DateTime.Now,
                HoursWorked = hours,
                SubmissionDate = DateTime.Now,
                Status = "Pending",
                CalculatedAmount = hours * rate,
                Comments = NotesTextBox.Text
            };

            ClaimsList.Add(newClaim);
            MessageBox.Show($"Claim submitted. Total: R{newClaim.CalculatedAmount}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // clear inputs
            ClearForm();
        }

        private int GenerateNewClaimID()
        {
            return ClaimsList.Any() ? ClaimsList.Max(c => c.ClaimID) + 1 : 1;
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ClaimantNameTextBox.Clear();
            ClaimMonthPicker.SelectedDate = null;
            HoursWorkedTextBox.Clear();
            HourlyRateTextBox.Clear();
            NotesTextBox.Clear();
            uploadedFiles.Clear();
        }

        // Approve selected claim 
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            // get the claim associated with the clicked row
            var claim = (sender as FrameworkElement)?.DataContext as Claim;
            if (claim == null) return;

            claim.Status = "Approved";
            MessageBox.Show($"Claim #{claim.ClaimID} approved.", "Approved", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshGrids();
        }

        // Reject selected claim 
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            var claim = (sender as FrameworkElement)?.DataContext as Claim;
            if (claim == null) return;

            claim.Status = "Rejected";
            MessageBox.Show($"Claim #{claim.ClaimID} rejected.", "Rejected", MessageBoxButton.OK, MessageBoxImage.Warning);
            RefreshGrids();
        }

        // View details - opens a details window 
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var claim = (sender as FrameworkElement)?.DataContext as Claim;
            if (claim == null) return;


            ClaimDetailsWindow detailsWindow = new ClaimDetailsWindow(claim, this);
            detailsWindow.Owner = this;
            detailsWindow.ShowDialog();
        }

        private void RefreshGrids()
        {
            ClaimsDataGrid.Items.Refresh();
            ViewClaimsDataGrid.Items.Refresh();
        }
    }
}

// CODE ATTRIBUTIONS:
// I got this code structure and ideas from Stack Overflow
// Link to website:https://stackoverflow.com/questions/11624298/how-do-i-use-openfiledialog-to-select-a-folder
// Author name: LarsTech
// Profile: https://stackoverflow.com/users/719186/larstech

// I got this code structure and ideas from Stack Overflow
// Link to website:https://stackoverflow.com/questions/10659347/bind-an-observablecollection-to-a-listview
// Author name: bryan walker
// Profile: https://stackoverflow.com/users/685140/bryan-walker

// I got this code structure and ideas from Stack Overflow
// Link to website: https://stackoverflow.com/questions/40400600/how-can-i-reference-the-listview-datacontext-from-within-the-listview-itemtempla
// Author name: lup silviu
// Profile: https://stackoverflow.com/users/658394/lupu-silviu


// I got this code structure and ideas from Stack Overflow
// Link to website: https://stackoverflow.com/questions/35686577/how-to-refresh-a-datagrid-in-wpf-after-updating-the-data-source
// Author name: mm8
// Profile: https://stackoverflow.com/users/1560275/mm8

// I got this code structure and ideas from Stack Overflow
// Link to website: https://stackoverflow.com/questions/2227380/openfiledialog-to-select-a-file-and-also-show-a-preview-of-the-file-in-winforms
// Author name: LarsTech
// Profile: https://stackoverflow.com/users/719186/larstech

// I got this code structure and ideas from Stack Overflow
// Link to website: https://stackoverflow.com/questions/11324688/how-to-refresh-datagrid-in-wpf
// Author name: mm8
// Profile: https://stackoverflow.com/users/1560275/mm8
//also consulted ai for implementation ideas
//openai
// https://openai.com/