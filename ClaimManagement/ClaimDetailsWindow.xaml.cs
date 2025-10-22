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
            LoadClaimDetails();
        }
        private void LoadClaimDetails()
        {
            this.Title = $"Claim Details - ID {_claim.ClaimID} ({_claim.Status})";
            ClaimantTextBlock.Text = $"Claimant: {_claim.Lecturer}";
            ClaimMonthTextBlock.Text = $"Claim Month: {_claim.ClaimMonth:MMMM yyyy}";
            HoursWorkedTextBlock.Text = $"Hours Worked: {_claim.HoursWorked}";
            StatusTextBlock.Text = $"Status: {_claim.Status}";
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
//Code attribution for window
// Ireferenced Microsoft Learn for window interaction examples
// https://learn.microsoft.com/en-us/dotnet/desktop/wpf/get-started/create-app-visual-studio
// Author: Microsoft Documentation Team
// https://learn.microsoft.com/en-us/users/
// The message box implementation idea was adapted from C# Corner
// https://www.c-sharpcorner.com/UploadFile/mahesh/messagebox-in-wpf/
// Author: Mahesh Chand
// https://www.c-sharpcorner.com/members/mahesh-chand
// CODE ATTRIBUTIONS:
// I got this code structure and ideas from Stack Overflow
// Link to website: https://stackoverflow.com/questions/30063550/how-should-i-pass-data-between-wpf-windows-involving-mainwindow-c
// Author name: minkus
// Profile: https://https://stackoverflow.com/users/12872270/minkus
// I got this code structure and ideas from Stack Overflow  
// Link to website: https://stackoverflow.com/questions/16172462/close-window-from-viewmodel
// Author name: wpfnoob
// Profile: https://stackoverflow.com/users/1990445/wpfnoob
// I got this code structure and ideas from Stack Overflow
// Link to website: https://stackoverflow.com/questions/368742/throwing-argumentnullexception
// Author name: tvfosson
// Profile: https://stackoverflow.com/users/12950/tvanfosson