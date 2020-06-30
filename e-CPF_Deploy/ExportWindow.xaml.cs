using Microsoft.Win32;
using System;
using System.Windows;


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
            OpenFileDialog filePath = new OpenFileDialog();
            filePath.Title = "Select new .pfx certificate";
            filePath.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            filePath.Filter = "Certificate (*.pfx)| *.pfx";
            filePath.FilterIndex = 1;
            filePath.RestoreDirectory = true;

            filePath.ShowDialog();

            txt_filePath.Text = !string.IsNullOrWhiteSpace(filePath.FileName) ? filePath.FileName : txt_filePath.Text;
        }

        private void B_exportCertificate_Click(object sender, RoutedEventArgs e)
        {
            if (pwdbox.Password == string.Empty)
            {
                OutputManager.Display(Answer.MissingPassword, "");
            }
            else
            {
                OutputManager.Display(Actions.ExportCert(txt_filePath.Text, pwdbox.SecurePassword), "Your certificate has been exported!");
            }
        }

        public void CloseExportWindow()
        {
            this.Close();
        }
    }
}
