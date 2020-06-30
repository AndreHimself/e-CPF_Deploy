using System.Windows;
using System.Windows.Input;

namespace e_CPF_Deploy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void B_Import_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            OutputManager.Display( await Actions.ImportCertAsync(), "Your certificate has been imported!");
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void B_Export_Click(object sender, RoutedEventArgs e)
        {
            var ExportWindow = new ExportWindow();
            ExportWindow.ShowDialog();
        }
    }
}
