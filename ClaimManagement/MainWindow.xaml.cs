using System.Windows;

namespace ClaimManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            // For the prototype, this simply opens a new window.
            // In a functional app, this would pass the selected claim data to the new window.
            ClaimDetailsWindow detailsWindow = new ClaimDetailsWindow();
            detailsWindow.Show();
        }
    }
}