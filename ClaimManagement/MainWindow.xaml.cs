using System;
using System.Windows;

namespace ClaimManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // SUBMIT button logic
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // basic validation
            if (string.IsNullOrWhiteSpace(HoursWorkedTextBox.Text) ||
                string.IsNullOrWhiteSpace(HourlyRateTextBox.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Info",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // create a new Claim object
                Claim newClaim = new Claim
                {
                    ClaimID = new Random().Next(1000, 9999),
                    ContractorID = 1,  // placeholder for lecturer
                    ContractID = 1,
                    ClaimMonth = ClaimMonthPicker.SelectedDate ?? DateTime.Now,
                    HoursWorked = decimal.Parse(HoursWorkedTextBox.Text),
                    SubmissionDate = DateTime.Now,
                    Status = "Pending",
                    CalculatedAmount = decimal.Parse(HoursWorkedTextBox.Text) * decimal.Parse(HourlyRateTextBox.Text),
                    Comments = NotesTextBox.Text
                };

                // show success message
                MessageBox.Show($"Claim submitted successfully!\nTotal: R{newClaim.CalculatedAmount}",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // clear form
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting claim: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // CLEAR FORM button logic
        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        // helper method to clear all fields
        private void ClearForm()
        {
            ClaimMonthPicker.SelectedDate = null;
            HoursWorkedTextBox.Clear();
            HourlyRateTextBox.Clear();
            NotesTextBox.Clear();
        }

        // temporary placeholder for Approve Claims tab
        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Details view coming soon!");
        }
    }
}
