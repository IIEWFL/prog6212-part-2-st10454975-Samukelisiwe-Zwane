using System;
using System.Windows;

namespace ClaimManagement
{
    public partial class ClaimDetailsWindow : Window
    {
        private Claim _claim;
        private MainWindow _mainWindow;

        public ClaimDetailsWindow(Claim claim, MainWindow mainWindow)
        {
            InitializeComponent();

            _claim = claim ?? throw new ArgumentNullException(nameof(claim));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));

            // load claim details to UI elements (if you have named controls, set their .Text/.Content here)
            LoadClaimDetails();
        }

        private void LoadClaimDetails()
        {
            // If your XAML has TextBlocks or Labels with names, set them here.
            // Example (uncomment if you have those controls):
            // ClaimIdTextBlock.Text = _claim.ClaimID.ToString();
            // ClaimMonthTextBlock.Text = _claim.ClaimMonth.ToString("yyyy-MM");
            // HoursTextBlock.Text = _claim.HoursWorked.ToString();
            // StatusTextBlock.Text = _claim.Status;

            // For now, if no named UI elements, just set window title
            this.Title = $"Claim Details - ID {_claim.ClaimID} ({_claim.Status})";
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            // call the public method on MainWindow
            _mainWindow.UpdateClaimStatus(_claim.ClaimID, "Approved");
            MessageBox.Show("Claim approved.", "Approved", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.UpdateClaimStatus(_claim.ClaimID, "Rejected");
            MessageBox.Show("Claim rejected.", "Rejected", MessageBoxButton.OK, MessageBoxImage.Warning);
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
