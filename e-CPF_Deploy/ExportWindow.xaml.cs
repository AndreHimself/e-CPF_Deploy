using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace e_CPF_Deploy
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        public ExportWindow()
        {
            InitializeComponent();

        }

        private void B_selectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filePath = new OpenFileDialog
            {
                Title = "Select new .pfx certificate",
                InitialDirectory = Environment.SpecialFolder.Desktop.ToString(),
                Filter = "Certificate (*.pfx)| *.pfx",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            filePath.ShowDialog();

            txt_filePath.Text = !string.IsNullOrWhiteSpace(filePath.FileName) ? filePath.FileName : txt_filePath.Text;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private async void B_exportCertificate_Click(object sender, RoutedEventArgs e)
        {
            if (pwdbox.Password == string.Empty)
            {
                OutputManager.Display(Answer.MissingPassword, "");
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Wait;
                OutputManager.Display(await Actions.ExportCertAsync(txt_filePath.Text, pwdbox.SecurePassword), "Your certificate has been exported!");
                Mouse.OverrideCursor = Cursors.Arrow;
                Application.Current.Shutdown();
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
