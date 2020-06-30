using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_CPF_Deploy
{
    public enum Answer
    {
        Success,            //Default
        InvalidUser,        //When user is trying to export someone else's certificate
        InvalidCert,        //Certificate issued by someone else
        NoPrivateKey,
        NoNetwork,
        FolderNotCreated,    //When user has no folder in the server
        InsufficientRights,   //user doesn't have modify rights in his folder
        CertificateNotFound,
        InvalidPassword,
        MissingPassword
    }
}
