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

        //Import button actions

        private async void B_Import_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            OutputManager.Display( await Actions.ImportCertAsync(), "Your certificate has been imported!");
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        //Export button actions
        private void B_Export_Click(object sender, RoutedEventArgs e)
        {
            var ExportWindow = new ExportWindow();
            ExportWindow.Owner = this;
            ExportWindow.ShowDialog();
        }



        #region Generic Event Handlers

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
