using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using IBM.Data.DB2.iSeries;
using SpritzDotNet;

namespace Chase_IRF
{
    public class Report
    {

        [Serializable]
        public sealed class MyReportServerCredentials :
    IReportServerCredentials
        {
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use the default Windows user.  Credentials will be
                    // provided by the NetworkCredentials property.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    // Read the user information from the Web.config file.  
                    // By reading the information on demand instead of 
                    // storing it, the credentials will not be stored in 
                    // session, reducing the vulnerable surface area to the
                    // Web.config file, which can be secured with an ACL.

                    //  Set Global Key
                    string encryptionkey = "ChaseIRFKey";

                    // User name -----------------------------------------------------------------------
                    string reportUserencrypted = ConfigurationManager.AppSettings["ReportViewerUser"];
                    //var userStringEnc = Spritz.EPIEncrypt(reportUserencrypted, encryptionkey);
                    var userString = Spritz.EPIDecrypt(reportUserencrypted, encryptionkey);

                    string userName = userString.ToString();
                    //    ConfigurationManager.AppSettings
                    //        ["ReportViewerUser"];

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception(
                            "Missing user name from web.config file");

                    // Password ------------------------------------------------------------------------
                    string reportPasswordencrypted = ConfigurationManager.AppSettings["ReportViewerPassword"];
                    //var passStringEnc = Spritz.EPIEncrypt(reportPasswordencrypted, encryptionkey);
                    var passwordString = Spritz.EPIDecrypt(reportPasswordencrypted, encryptionkey);

                    string password = passwordString.ToString();
                    //    ConfigurationManager.AppSettings
                    //        ["ReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception(
                            "Missing password from web.config file");

                    // Domain --------------------------------------------------------------------------
                    string reportDomainencrypted = ConfigurationManager.AppSettings["ReportViewerDomain"];
                    //var DomStringEnc = Spritz.EPIEncrypt(reportDomainencrypted, encryptionkey);
                    var domainString = Spritz.EPIDecrypt(reportDomainencrypted, encryptionkey);

                    string domain = domainString.ToString();
                    //    ConfigurationManager.AppSettings
                    //        ["ReportViewerDomain"];

                    if (string.IsNullOrEmpty(domain))
                        throw new Exception(
                            "Missing domain from web.config file");

                    return new NetworkCredential(userName, password, domain);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie,
                        out string userName, out string password,
                        out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;

                // Not using form credentials
                return false;
            }
        }

    }
}