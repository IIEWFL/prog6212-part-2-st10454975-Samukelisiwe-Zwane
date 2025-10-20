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
            ClaimsList.Add(new Claim { ClaimID = 1, ContractorID = 101, ClaimMonth = DateTime.Now.AddMonths(-1), HoursWorked = 40, SubmissionDate = DateTime.Now.AddDays(-7), Status = "Pending", CalculatedAmount = 40m * 200m });
            ClaimsList.Add(new Claim { ClaimID = 2, ContractorID = 102, ClaimMonth = DateTime.Now.AddMonths(-2), HoursWorked = 35, SubmissionDate = DateTime.Now.AddDays(-10), Status = "Approved", CalculatedAmount = 35m * 200m });
            ClaimsList.Add(new Claim { ClaimID = 3, ContractorID = 103, ClaimMonth = DateTime.Now.AddMonths(-1), HoursWorked = 45, SubmissionDate = DateTime.Now.AddDays(-5), Status = "Rejected", CalculatedAmount = 45m * 200m });

            // bind collections to the grids / listbox
            ClaimsDataGrid.ItemsSource = ClaimsList;
            ViewClaimsDataGrid.ItemsSource = ClaimsList;
            UploadedFilesList.ItemsSource = uploadedFiles;
        }

        // -------------------------
        // PUBLIC API used by other windows
        // -------------------------
        // Make this public so ClaimDetailsWindow (or other windows) can call it
        public void UpdateClaimStatus(int claimID, string newStatus)
        {
            var claim = ClaimsList.FirstOrDefault(c => c.ClaimID == claimID);
            if (claim != null)
            {
                claim.Status = newStatus;
                RefreshGrids();
            }
        }

        // -------------------------
        // Upload file button - stores file locally and displays filename
        // -------------------------
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
            ClaimMonthPicker.SelectedDate = null;
            HoursWorkedTextBox.Clear();
            HourlyRateTextBox.Clear();
            NotesTextBox.Clear();
            uploadedFiles.Clear();
        }

        // Approve selected claim (row button click)
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            // get the claim associated with the clicked row
            var claim = (sender as FrameworkElement)?.DataContext as Claim;
            if (claim == null) return;

            claim.Status = "Approved";
            MessageBox.Show($"Claim #{claim.ClaimID} approved.", "Approved", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshGrids();
        }

        // Reject selected claim (row button click)
        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            var claim = (sender as FrameworkElement)?.DataContext as Claim;
            if (claim == null) return;

            claim.Status = "Rejected";
            MessageBox.Show($"Claim #{claim.ClaimID} rejected.", "Rejected", MessageBoxButton.OK, MessageBoxImage.Warning);
            RefreshGrids();
        }

        // View details - opens a details window (uses DataContext from row)
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var claim = (sender as FrameworkElement)?.DataContext as Claim;
            if (claim == null) return;

            // open details window and pass 'this' so details window can call back
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
