using System.Windows;

namespace e_CPF_Deploy
{
    public static class OutputManager
    {
        
        public static void Display(Answer type, string message)
        {
            switch (type)
            {
                case Answer.Success:
                    MessageBox.Show(message, "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Shutdown();
                    break;

                case Answer.InvalidUser:
                    MessageBox.Show("This certificate is not meant for you.", "Invalid User", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.InvalidCert:
                    MessageBox.Show("This is not a personal certificate.", "Invalid Certificate", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.NoPrivateKey:
                    MessageBox.Show("Selected certificate has no private key.", "Invalid Certificate", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.NoNetwork:
                    MessageBox.Show("Please check your internet and VPN connection.", "Network Unavailable", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.FolderNotCreated:
                    MessageBox.Show("Check your network connection or ask IT to create your personal storage in the server.", "Network or storage issue", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.InsufficientRights:
                    MessageBox.Show("Please ask IS to check your access.", "Insufficient rights", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.CertificateNotFound:
                    MessageBox.Show("You do not have a certificate yet, please create a backup copy first.", "Certificate not found", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.InvalidPassword:
                    MessageBox.Show("Incorrect password, please recheck.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case Answer.MissingPassword:
                    MessageBox.Show("Missing password", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;

                    
                default:
                    break;
            }


        }


    }
}
