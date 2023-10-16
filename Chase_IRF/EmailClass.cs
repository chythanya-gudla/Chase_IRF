using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chase_IRF
{
    public class EmailClass
    {
        public int SendEmail(string body, string ToAddress,string ccAddress, string bccAddress, string Subject, bool HTML)
        {
            try
            {
                //Initilize Email
                MailMessage newmail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "10.0.21.10";
                //client.Host = "smtpa.epiinc.com";
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.Credentials = new System.Net.NetworkCredential("NoReply@epiinc.com", "bB20131220B");
                client.UseDefaultCredentials = true;


                //Add Fields and Send
                newmail.From = (new MailAddress("NoReply@epiinc.com"));
                foreach (var address in ToAddress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    newmail.To.Add(new MailAddress(address));
                }
                foreach (var ccaddress in ccAddress.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    newmail.CC.Add(new MailAddress(ccaddress));
                }
                if ((bccAddress != "") && (bccAddress != null))
                {
                    newmail.Bcc.Add(new MailAddress(bccAddress));
                }

                //newmail.To.Add(new MailAddress("e_ours@epiinc.com"));
                newmail.Subject = Subject;
                newmail.Body = body;
                newmail.IsBodyHtml = HTML;
                client.Send(newmail);

                return 1;
            }
            catch (Exception ex)
            {
                return 2;
            }

        }
    }
}
