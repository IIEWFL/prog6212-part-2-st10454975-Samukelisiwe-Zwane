using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ClaimManagement
{
    public partial class MainWindow : Window
    {
        // store all claims
        private static List<Claim> claimsList = new List<Claim>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshDataGrids();
        }

        // ==============================
        //  SUBMIT CLAIM
        // ==============================
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HoursWorkedTextBox.Text) ||
                string.IsNullOrWhiteSpace(HourlyRateTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Info",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Claim newClaim = new Claim
                {
                    ClaimID = new Random().Next(1000, 9999),
                    ContractorID = 1,  // example lecturer
                    ContractID = 1,
                    ClaimMonth = ClaimMonthPicker.SelectedDate ?? DateTime.Now,
                    HoursWorked = decimal.Parse(HoursWorkedTextBox.Text),
                    SubmissionDate = DateTime.Now,
                    Status = "Pending",
                    CalculatedAmount = decimal.Parse(HoursWorkedTextBox.Text) * decimal.Parse(HourlyRateTextBox.Text),
                    Comments = NotesTextBox.Text
                };

                claimsList.Add(newClaim);
                MessageBox.Show($"Claim submitted successfully!\nTotal: R{newClaim.CalculatedAmount}",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearForm();
                RefreshDataGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting claim: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        }

        // ==============================
        //  APPROVE OR REJECT CLAIMS
        // ==============================
        private void ApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (PendingClaimsGrid.SelectedItem is Claim selectedClaim)
            {
                selectedClaim.Status = "Approved";
                MessageBox.Show($"Claim #{selectedClaim.ClaimID} approved successfully!",
                    "Approved", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshDataGrids();
            }
        }

        private void RejectClaim_Click(object sender, RoutedEventArgs e)
        {
            if (PendingClaimsGrid.SelectedItem is Claim selectedClaim)
            {
                selectedClaim.Status = "Rejected";
                MessageBox.Show($"Claim #{selectedClaim.ClaimID} has been rejected.",
                    "Rejected", MessageBoxButton.OK, MessageBoxImage.Warning);
                RefreshDataGrids();
            }
        }

        // ==============================
        //  REFRESH GRIDS
        // ==============================
        private void RefreshDataGrids()
        {
            ViewClaimsDataGrid.ItemsSource = null;
            ViewClaimsDataGrid.ItemsSource = claimsList;

            PendingClaimsGrid.ItemsSource = null;
            PendingClaimsGrid.ItemsSource = claimsList.Where(c => c.Status == "Pending").ToList();
        }
    }
}
