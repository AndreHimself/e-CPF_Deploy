using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;

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
            B_selectFile_Click(this, null);

        }

        private void B_selectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filePath = new OpenFileDialog
            {
                Title = "Select new .pfx certificate",
                InitialDirectory = Environment.SpecialFolder.Desktop.ToString(),
                Filter = "Certificate (*.pfx)| *.pfx",
                FilterIndex = 1,
                //RestoreDirectory = true
            };

            filePath.ShowDialog();

            if (string.IsNullOrWhiteSpace(filePath.FileName) == false)
            {
                txt_filePath.Text = filePath.FileName.Split('\\').Last();
                pwdbox.Focus();
                pwdbox_PasswordChanged(this, null);
            }
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
                var result = await Actions.ExportCertAsync(txt_filePath.Text, pwdbox.SecurePassword);
                
                if (result == Answer.InvalidPassword)
                {
                    OutputManager.Display(result, "Wrong password, try again.");
                    Mouse.OverrideCursor = Cursors.Arrow;
                }
                else
                {
                    OutputManager.Display(result, "Your certificate has been exported!");
                    Mouse.OverrideCursor = Cursors.Arrow;
                    Application.Current.Shutdown();
                }
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pwdbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwdbox.Password.Length > 0 &&
                string.IsNullOrEmpty(txt_filePath.Text) == false)
            {
                b_exportCertificate.IsEnabled = true;
                b_exportCertificate.Foreground = Application.Current.TryFindResource("HighLightColor") as Brush;
            }
            else
            {
                b_exportCertificate.Foreground = Application.Current.TryFindResource("MediumGreyColor") as Brush;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && (b_exportCertificate.IsEnabled == true))
                B_exportCertificate_Click(this, null);
        }
    }
}
