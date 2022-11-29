using System;
using System.Data;
using IBM.Data.DB2.iSeries;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chase_IRF
{
    public class Emails
    {
        //test comment
        public int CreateEmailNotification(string itemnumber, string sendto)
        {
            int clientid = 1;
            int emailresult = 0;
            string email = sendto;

            String NullTest = String.Empty;


            DataClass DC = new DataClass();
            DataTable DTI = new DataTable();
            DTI = DC.ReturnItem(itemnumber);
            foreach (DataRow R in DTI.Rows)
            {
                string UID = R["UID"].ToString();
                string ItemNumber = R["ItemNumber"].ToString();
                string Deleted = R["Deleted"].ToString();
                string Status = R["Status"].ToString();
                string ItemDescription = R["ItemDescription"].ToString();
                string ItemCostCtr = R["ItemCostCtr"].ToString();
                string ProductGroup = R["ProductGroup"].ToString();
                string ProductSubGroup = R["ProductSubGroup"].ToString();
                string StockUOM = R["StockingUOM"].ToString();
                string QuantityUOM = R["QuantityUOM"].ToString();
                string LastCost = R["LastCost"].ToString();
                string RetailPrice = R["RetailPrice"].ToString();
                string Vendor = R["PrimaryVendor"].ToString();
                string Supersede = R["SupersededItem"].ToString();
                string MinStockLvl = R["MinStockLevel"].ToString();
                string ActiveDate = R["ActiveDate"].ToString();
                string ExpireDate = R["ExpirationDate"].ToString();
                string Weight = R["ItemWeight"].ToString();
                string Length = R["ItemLength"].ToString();
                string Width = R["ItemWidth"].ToString();
                string Height = R["ItemHeight"].ToString();
                string OneBoxId = R["OneBoxID"].ToString();
                string Starter = R["StarterItem"].ToString();
                string StarterQuantity = R["StarterQuantity"].ToString();
                string StarterExpire = R["StarterExpireDate"].ToString();
                string MaxOrdQuantity = R["MaximumOrderQuantity"].ToString();
                string SubmittedBy = R["ID"].ToString();
                string SubmittedDate = R["SubmittedDate"].ToString();
                string Manufactured = R["Manufactured"].ToString();
                string ArtworkReleaseDate = R["ArtworkReleaseDate"].ToString();
                string ExpectedArrival = R["ExpectedArrival"].ToString();
                string ExpectedQuantity = R["ExpectedQuantity"].ToString();
                string PrintJTS = R["PrintJTS"].ToString();

                //Fix nulls and get ItemOwner  ---------------------
                int ItemOwner;
                NullTest = Convert.ToString(R["ItemOwner"]);
                if (NullTest == null || NullTest == string.Empty)
                { ItemOwner = 0; }
                else
                { ItemOwner = Convert.ToInt32(R["ItemOwner"]); }

                //Fix nulls and get Vendor Name  ---------------------
                int PrimaryVendor;
                NullTest = Convert.ToString(R["PrimaryVendor"]);
                if (NullTest == null || NullTest == string.Empty)
                { PrimaryVendor = 0; }
                else
                { PrimaryVendor = Convert.ToInt32(R["PrimaryVendor"]); }

                string VendorName = string.Empty;
                if (PrimaryVendor != 0)
                {
                    DataTable DTV = new DataTable();
                    DTV = DC.GetVendors();
                    foreach (DataRow RV in DTV.Rows)
                    {
                        if (PrimaryVendor == Convert.ToInt32(RV["OPVendor"]))
                            VendorName = RV["VendorName"].ToString();
                    }
                }

                //  Get submitted by info
                string SbmFirstName = string.Empty;
                string SbmLastName = string.Empty;
                string SbmEMail = string.Empty;
                DataTable RU = new DataTable();
                RU = DC.GetUserDetails(Convert.ToInt32(SubmittedBy));
                foreach (DataRow U in RU.Rows)
                {
                    SbmFirstName = U["FirstName"].ToString();
                    SbmLastName = U["LastName"].ToString();
                    SbmEMail = U["EMail"].ToString();
                }

                //  Get Owner Info
                string OwnName = string.Empty;
                //  string OwnLastName = string.Empty;
                string OwnEMail = string.Empty;
                string OPCustomer = "This Item Owner does not exist";
                int owner = Convert.ToInt32(ItemOwner);
                DataTable itemOwnersInfo = new DataTable();
                itemOwnersInfo = DC.GetItemOwners();
                foreach (DataRow itemOwnersRow in itemOwnersInfo.Rows)
                {

                    if (ItemOwner == Convert.ToInt32(itemOwnersRow["OPCustomer"]))
                    {
                        OPCustomer = itemOwnersRow["OPCustomer"].ToString();
                        OwnName = itemOwnersRow["OwnerName"].ToString();
                        //OwnLastName = itemOwnersRow["OwnerName"].ToString();
                        OwnEMail = itemOwnersRow["EMail"].ToString();
                    }
                }

                //  Get Product Group Description
                string ProductGroupDesc = string.Empty;
                DataTable DTG = new DataTable();
                DTG = DC.GetClientCodes(clientid, ProductGroup);
                foreach (DataRow GR in DTG.Rows)
                {
                    ProductGroupDesc = GR["DisplayText"].ToString();
                }

                //  Get Product SubGroup Description
                string ProductSubGroupDesc = string.Empty;
                DataTable DSG = new DataTable();
                DSG = DC.GetClientCodes(clientid, ProductSubGroup);
                foreach (DataRow SG in DSG.Rows)
                {
                    ProductSubGroupDesc = SG["DisplayText"].ToString();
                }

                // Set Status Message from Item Status
                string SbsStatus;
                string StatusMessage = string.Empty;
                SbsStatus = Status.Substring(0, 1);
                switch (SbsStatus)
                {
                    //case "1":
                    //    {
                    //        StatusMessage = "Incomplete";
                    //        break;
                    //    }
                    //case "2":
                    //    {
                    //        StatusMessage = "Receivable";
                    //        break;
                    //    }
                    //case "3":
                    //    {
                    //        StatusMessage = "Ready for One Box";
                    //        break;
                    //    }
                    case "4":
                        {
                            StatusMessage = "Complete";
                            break;
                        }
                    default:
                        {
                            StatusMessage = "Incomplete";
                            break;
                        }
                }

                // Set Status Message from Item Status
                string ItemPurpose = string.Empty;
                switch (Manufactured)
                {
                    case "A":
                        {
                            ItemPurpose = "Ariba";
                            break;
                        }
                    case "O":
                        {
                            ItemPurpose = "One Box";
                            break;
                        }
                    case "S":
                        {
                            // ItemPurpose = "Special Distribution";
                            ItemPurpose = "Inventory Only";
                            break;
                        }
                    case "P":
                        {
                            ItemPurpose = "One Box / Ariba";
                            break;
                        }
                    case "T":
                        {
                            ItemPurpose = "Special Distribution / Ariba";
                            break;
                        }
                    default:
                        {
                            ItemPurpose = "Unknown";
                            break;
                        }
                }

                //  Special Distribution Delivery Date (uses ArtworokReleseDate)
                string CheckDateStr;
                string SpecDistDelivery;
                int FirstSlash;
                int SecondSlash;
                CheckDateStr = ArtworkReleaseDate;
                FirstSlash = CheckDateStr.IndexOf("/");
                SecondSlash = CheckDateStr.Substring(FirstSlash + 1).IndexOf("/");
                string SPDMonth = CheckDateStr.Substring(0, FirstSlash);
                string SPDDay = CheckDateStr.Substring(FirstSlash + 1, SecondSlash);
                string SPDYear = CheckDateStr.Substring((4 + (FirstSlash - 1) + (SecondSlash - 1)), 4);
                if (SPDMonth.Length < 2)
                {
                    SPDMonth = "0" + SPDMonth.Trim();
                }
                if (SPDDay.Length < 2)
                {
                    SPDDay = "0" + SPDDay.Trim();
                }
                CheckDateStr = SPDMonth + "/" + SPDDay + "/" + SPDYear;
                if (CheckDateStr != "01/01/1900")
                {
                    SpecDistDelivery = CheckDateStr;
                }
                else
                {
                    SpecDistDelivery = string.Empty;
                }

                //  Make dates look nice
                if (StarterExpire != "1900-01-01")
                {
                    StarterExpire = StarterExpire.Substring(5, 2) + "/" + StarterExpire.Substring(8, 2) + "/" + StarterExpire.Substring(0, 4);
                }
                else
                {
                    StarterExpire = string.Empty;
                }

                if (ActiveDate != "1900-01-01")
                {
                    ActiveDate = ActiveDate.Substring(5, 2) + "/" + ActiveDate.Substring(8, 2) + "/" + ActiveDate.Substring(0, 4);
                }
                else
                {
                    ActiveDate = string.Empty;
                }

                if (ExpireDate != "1900-01-01")
                {
                    ExpireDate = ExpireDate.Substring(5, 2) + "/" + ExpireDate.Substring(8, 2) + "/" + ExpireDate.Substring(0, 4);
                }
                else
                {
                    ExpireDate = string.Empty;
                }

                if (ExpectedArrival != "1900-01-01")
                {
                    ExpectedArrival = ExpectedArrival.Substring(5, 2) + "/" + ExpectedArrival.Substring(8, 2) + "/" + ExpectedArrival.Substring(0, 4);
                }
                else
                {
                    ExpectedArrival = string.Empty;
                }

                //  Edit decimal quantities
                int DecimalPos;
                DecimalPos = MinStockLvl.IndexOf(".");
                MinStockLvl = MinStockLvl.Substring(0, DecimalPos);

                DecimalPos = MaxOrdQuantity.IndexOf(".");
                MaxOrdQuantity = MaxOrdQuantity.Substring(0, DecimalPos);

                DecimalPos = StarterQuantity.IndexOf(".");
                StarterQuantity = StarterQuantity.Substring(0, DecimalPos);

                DecimalPos = RetailPrice.IndexOf(".");
                RetailPrice = "$ " + RetailPrice.Substring(0, DecimalPos + 3);

                if (PrintJTS == "")
                {
                    PrintJTS = "None";
                }


                string emailbodystring =
"<!doctype html public \"-//w3c//dtd html 4.0 transitional//en\"> " +
"<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\"> " +
"<meta name = \"Author\" content=\"EPI Marketing Services\"><title>Chase IRF Form</title> " +
"</head><body><p><center><h1>Chase IRF</h1></center></p> " +
"<p><center><h2>One Box/Ariba/Inventory Request</h2></center></p> " +
"<br><P><h4>" + StatusMessage + "</h4></p> " +
"<br>&nbsp;<table WIDTH = \"100%\" > " +
"<tr> " +
"<td>Submitted by: " + SbmFirstName.Trim() + " " + SbmLastName.Trim() + "</td> " +
"<td>EMail: " + SbmEMail.Trim() + "</td> " +
"<td>Date: " + SubmittedDate + "</td> " +
"</tr> " +
"<tr> " +
"<td>Item Owner: " + OwnName.Trim() + "</td> " +
"<td>EMail: " + OwnEMail.Trim() + "</td> " +
"<td>&nbsp;</td> " +
"</tr> " +
"<tr><td colspan=\"3\">&nbsp;</td></tr> " +
"<tr><td colspan=\"3\"><h2>Item Details</h2><td></tr> " +
"<tr><td>Item number: " + ItemNumber + "</td> " +
"<td colspan=\"2\">Description: " + ItemDescription + "</td></tr> " +
"<tr><td>Item purpose: " + ItemPurpose + "</td> " +
"<td colspan=\"2\">Expected Arrival Date: " + ExpectedArrival + "</td></tr> " +
//"<tr><td colspan=\"3\">Item category: " + ProductGroup + "</tr> " +
"<tr><td colspan=\"3\">&nbsp;</td></tr> " +
"<tr><td>Cost Center: " + ItemCostCtr + "</td> " +
"<td>Production Cost : " + StockUOM + " $" + LastCost + "</td> " +
"<td>Unit of Measure: " + StockUOM + "</td></tr> " +
"<tr><td colspan=\"2\">Low water point: " + MinStockLvl + "</td> " +
"<td>Quantity per Unit of Measure: " + QuantityUOM + "</td></tr> " +
"<tr><td colspan=\"3\">&nbsp;</td></tr> " +
"<tr><td colspan=\"3\"><b>Item Dimensions</b><td></tr> " +
"<tr><td colspan=\"3\">Length: " + Length + "&nbsp;&nbsp;&nbsp; " +
"Width: " + Width + "&nbsp;&nbsp;&nbsp;Height: " + Height + "&nbsp;&nbsp;&nbsp; " +
"Weight: " + Weight + "</td></tr> " +
"<tr><td colspan=\"3\">Vendor: " + VendorName + "</tr> " +
"<tr><td colspan=\"3\">Expected Quantity: " + ExpectedQuantity + "</tr> " +
"<tr><td colspan=\"3\">&nbsp;</td></tr> " +
"<tr><td colspan=\"3\"><b>Distribution Rules</b><td></tr> ";

                //  Get Business Rules 
                DataTable BR = new DataTable();
                BR = DC.GetBusinessRules(ItemNumber);
                foreach (DataRow B in BR.Rows)
                {
                    emailbodystring += "<tr><td colspan=\"3\">Business Rule: " + B[3].ToString() + "</td></tr> ";
                    if (B[4].ToString() != "" && B[4].ToString() != null)
                    {
                        emailbodystring += "<tr><td colspan=\"3\">Custom List: " + B[4].ToString() + "</td></tr> ";
                    }
                }

                emailbodystring +=
                "<tr><td colspan=\"3\">&nbsp;</td></tr> " +
                "<tr><td colspan=\"3\"><h2>One Box Information</h2><td></tr> " +
                "<tr><td colspan=\"3\">One Box month/year: " + OneBoxId + "</td></tr> " +
                "<tr><td>Include in starter: " + Starter + "</td> " +
                "<td>Quantity to include: " + StarterQuantity + "</td> " +
                "<td>Expiration Date: " + StarterExpire + "</td></tr> " +
                "<tr><td colspan=\"3\">&nbsp;</td></tr> " +
                "<tr><td colspan=\"3\"><h2>Ariba Information</h2><td></tr> " +
                "<tr><td>Date to be added: " + ActiveDate + "</td> " +
                "<td>Unit Selling Price: " + RetailPrice + "</td> " +
                "<td>Existing item being replaced: " + Supersede + "</td></tr> " +
                "<tr><td>Date to be removed: " + ExpireDate + "</td> " +
                "<td>Maximum order quantity: " + MaxOrdQuantity + "</td> " +
                "<td>Maximum Order Quantity Interval: " + PrintJTS + "</td></tr>' " +
                "<tr><td colspan=\"3\">Ariba sub-category: " + ProductSubGroup + "</td></tr> " +
                "</table></body></html> ";


                //  Over Ride submitter's email to Darcy and send Bcc to AM Team
                //emailresult = DC.NewItemNotification(clientid, ItemNumber, ItemOwner,
                //               SbmFirstName + " " + SbmLastName, "Reports@epiinc.com", OwnEMail,
                //               "darcy.c.hall@chase.com", "ChaseAMTeam@epiinc.com",
                //               "Item " + ItemNumber + " Submitted", emailbodystring);
                emailresult = DC.NewItemNotification(clientid, ItemNumber, ItemOwner,
                               SbmFirstName + " " + SbmLastName, "Reports@epiinc.com", OwnEMail,
                               "darcy.c.hall@chase.com,rebecca.m.neal@jpmorgan.com", "ChaseAMTeam@epiinc.com",
                               "Item " + ItemNumber + " Submitted", emailbodystring);
                return emailresult;
            }
            return emailresult;
        }
        public Boolean SendEmailNotification(int emailuid)
        {
            EmailClass EC = new EmailClass();
            DataClass returnresult = new DataClass();

            bool HTML = true;
            int result = 0;

            DataTable DTN = new DataTable();
            DTN = returnresult.GetItemNotification(emailuid);
            foreach (DataRow R in DTN.Rows)
            {
                int UID = Convert.ToInt32(R["UID"]);
                string Status = R["Status"].ToString();
                string Body = R["EmBody"].ToString();
                string ToAddress = R["EmTo"].ToString();
                string ccAddress = R["EmCC"].ToString();
                string bccAddress = R["EmBCC"].ToString();
                string Subject = R["EmSubject"].ToString();

                result = EC.SendEmail(Body, ToAddress, ccAddress, bccAddress, Subject, HTML);
            }

            if (result == 1)
            {
                returnresult.UpdNotificationStatus(emailuid, "S");
                return true;
            }
            else
            {
                return false;
            }
        }





    }

    //public Boolean SendEmailNotification(int emailuid)
    //{
    //    EmailClass EC = new EmailClass();
    //    DataClass DC = new DataClass();

    //    bool HTML = true;
    //    int result = 0;

    //    DataTable DTN = new DataTable();
    //    DTN = DC.GetItemNotification(emailuid);
    //    foreach (DataRow R in DTN.Rows)
    //    {
    //        int UID = Convert.ToInt32(R["UID"]);
    //        string Status = R["Status"].ToString();
    //        string Body = R["EmBody"].ToString();
    //        string ToAddress = R["EmTo"].ToString();
    //        string Subject = R["EmSubject"].ToString();

    //        result = EC.SendEmail(Body, ToAddress, Subject, HTML);
    //    }

    //    if (result == 1)
    //    {
    //        DC.UpdNotificationStatus(emailuid, "S");
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}



}
//public class EmailClass
//{
//    public int SendEmail(string body, string ToAddress, string Subject, bool HTML)
//    {
//        try
//        {
//            //Initilize Email
//            MailMessage newmail = new MailMessage();
//            SmtpClient client = new SmtpClient();
//            client.Port = 25;
//          //  client.Host = "10.0.21.10";
//            client.Host = "172.21.3.182"; 
//            client.Timeout = 10000;
//            client.DeliveryMethod = SmtpDeliveryMethod.Network;
//            client.UseDefaultCredentials = true;


//            //Add Fields and Send
//            newmail.From = (new MailAddress("d_teja@epiinc.com"));
//            newmail.To.Add(new MailAddress(ToAddress));
//            //newmail.To.Add(new MailAddress("e_ours@epiinc.com"));
//            newmail.Subject = Subject;
//            newmail.Body = body;
//            newmail.IsBodyHtml = HTML;
//            client.Send(newmail);

//            return 1;
//        }
//        catch (Exception ex)
//        {
//            return 2;
//        }

//    }
//}
