using System;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace e_CPF_Deploy
{
    public class Actions
    {
        #region Constants declaration

        private const string ISSUER = "CN=AC Certisign RFB G5, OU=Secretaria da Receita Federal do Brasil - RFB, O=ICP-Brasil, C=BR";
        public static readonly string certDir = string.Format(@"\\pmibrhqtfnp01\ctest$\{0}\", Environment.UserName);
        public static readonly string certPath = certDir + Environment.UserName + ".pfx";

        public static UserPrincipal User { get; private set; }
        
        #endregion


        #region Import/Export certificate

        public static async Task<Answer> ImportCertAsync()
        {
            try
            {
                await Task.Run(() => {

                    X509Certificate2 cert = new X509Certificate2(certPath, string.Empty, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.UserKeySet);

                    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadWrite);
                    store.Add(cert);
                    store.Close();
                });

                return Answer.Success;
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                //if folder exists, returns CertificateNotFound, otherwise folder not created.

                return FolderExists() ? Answer.CertificateNotFound : Answer.FolderNotCreated;
            }

        }


        public static Answer ExportCert(string filePath, System.Security.SecureString pwd)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(filePath, pwd, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.Exportable);

                SetCurrentUser();

                /*
                if (cert.Issuer != ISSUER)
                {
                    return Answer.InvalidCert;
                }
                if (!IsCertOwnedByLoggedOnUser(cert.FriendlyName))
                {
// return Answer.InvalidUser;
                }
                if (!cert.HasPrivateKey)
                {
                    return Answer.NoPrivateKey;
                }
                if (!FolderExists())
                {
                    return Answer.FolderNotCreated;
                }
                */

                Runspace rs = RunspaceFactory.CreateRunspace();
                rs.ThreadOptions = PSThreadOptions.UseCurrentThread;
                rs.Open();

                PowerShell ps = PowerShell.Create();
                ps.Runspace = rs;

                ps.AddCommand("Export-PfxCertificate");
                ps.AddParameter("-Cert", cert);
                ps.AddParameter("-FilePath", certPath);
                ps.AddParameter("-ProtectTo", $"PMI\\{Environment.UserName}");
                ps.AddParameter("-ChainOption", "BuildChain");

                ps.Invoke();
                rs.Close();

                if (CertExists())
                {
                    File.Delete(filePath);
                }

                return Answer.Success;
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return Answer.InvalidPassword;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
                //Trocar nome da folder e mudar permissionamento para validar error handling que falta.
            }

        }

        #endregion


        #region Secondary checks

        private static bool FolderExists()
        {
            return Directory.Exists(certDir);
        }

        public static bool CertExists()
        {
            return File.Exists(certPath);
        }

        
        private static bool IsCertOwnedByLoggedOnUser(string certFriendlyName)
        {
            if (!string.IsNullOrWhiteSpace(certFriendlyName))
            {
                var firstNameUpper = certFriendlyName.Contains(" ") ?
                certFriendlyName.Substring(0, certFriendlyName.IndexOf(' ')).ToUpper() : certFriendlyName.ToUpper();

                return User.Name.ToUpper().Contains(firstNameUpper);
            }

            return false;
        }


        private static void SetCurrentUser()
        {
            try
            {
                User = UserPrincipal.Current;
            }
            catch (Exception)
            {
                OutputManager.Display(Answer.NoNetwork, "");
            }
        }

        #endregion
    }
}
