using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpritzDotNet;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;

namespace Chase_IRF
{
    public partial class NewIRF : System.Web.UI.Page
    {
        int UIDData = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {

                //Set Cache History
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Session["FileUpload"] = null;
                //Check Validation - Decrypt and Decode Parameters
                string encryptionkey = "ChaseIRFKey";
                string Validatedencrypted = Request.QueryString["Validated"];
                var Validated = Spritz.EPIDecrypt(Validatedencrypted, encryptionkey);
                DateTime validatedtime = Convert.ToDateTime(Validated);
                string idencrypted = Request.QueryString["userid"];
                var id = Spritz.EPIDecrypt(idencrypted, encryptionkey);

                //SetupLimitDiv.Visible = false;
                btnSubmit.EnableViewState = false;
                SubmitterName.Value = Session["fullname"] != null ? Convert.ToString(Session["fullname"]) : "";
                SubmitterEmailID.Value = Session["EmailId"] != null ? Convert.ToString(Session["EmailId"]) : "";
                fillitemowners();
                ItemOwnerEmailID.Value = string.Empty;
                //SubmitterPhoneNumber.Value = string.Empty;
                //ItemOwnerPhoneNumber.Value = string.Empty;
                SelectItem.Value = string.Empty;
                ItemNumber.Value = string.Empty;
                ItemDescription.Value = string.Empty;
                fillcategory();
                DistributionRuleText.Value = string.Empty;
                //distributionrule.Value = string.Empty;

                CostCenter.Value = string.Empty;
                Quantity.Value = string.Empty;
                UofM.Value = string.Empty;
                ProdCost.Value = string.Empty;
                UnitLength.Value = null;
                UnitWidth.Value = null;
                UnitHeight.Value = null;
                ItemWeight.Value = string.Empty;
                //PrimaryVendor.Value = string.Empty;
                LowWaterPoint.Value = string.Empty;
                OneBoxID.Value = string.Empty;
                SelectBool.SelectedValue = string.Empty;

                StarterBoxQuantity.Value = string.Empty;
                ExpireDate.Text = string.Empty;
                DateToBeAdded.Text = string.Empty;
                DateToBeRemoved.Text = string.Empty;
                SupersededItem.Value = string.Empty;
                RetailPrice.Value = string.Empty;
                MaxOrderQuantity.Value = string.Empty;
                ExpectedArrival.Text = string.Empty;
                ExpectedQuantity.Value = string.Empty;
                IPAriba.Checked = false;
                viewableOnlyCheckbox.Checked = false;
                viewableOnlyCheckbox.Visible = true;
                OrderLimitIntervalDropDownList.SelectedValue = string.Empty;
                distributionDiv.Visible = false;
                UploadDiv.Visible = false;
                if (IPAriba.Checked == true)
                {
                    viewableOnlyCheckbox.Visible = true;
                }
                IPOneBox.Checked = false;
                if (IPOneBox.Checked == true)
                {
                    viewableOnlyCheckbox.Visible = false;
                }
                IPOneBoxAriba.Checked = false;
                if (IPOneBoxAriba.Checked == true)
                {
                    viewableOnlyCheckbox.Visible = true;
                }
                IPSpecial.Checked = false;
                if (IPSpecial.Checked == true)
                {
                    viewableOnlyCheckbox.Visible = false;
                }
                IPSpecialAriba.Checked = false;
                if (IPSpecialAriba.Checked == true)
                {
                    viewableOnlyCheckbox.Visible = false;
                }
                fillsubcategory();
                fillVendors();
                fillOneBoxID();
                fillUnitOfMeasure();

                string ItemValueId = Convert.ToString(Request.QueryString["ItemValue"]);
                string ItemType = Convert.ToString(Request.QueryString["ItemType"]);
                string itemnumber = Request.QueryString["itemNumber"];
                //prepopulate Item Owner's email ID dynamically upon owner selection from ddl
                int itemOwenerId = Convert.ToInt32(Request.QueryString["value"]) != 0 ? Convert.ToInt32(Request.QueryString["value"]) : 0;

                if (string.IsNullOrEmpty(ItemOwnerEmailID.Value) && Session["ItemOwnerEmailID"] != null)
                {
                    Session["ItemOwnerEmailID"] = null;
                }
                if (Session["ItemOwnerEmailID"] != null)
                {
                    ItemOwnerEmailID.Value = Convert.ToString(Session["ItemOwnerEmailID"]);
                }
                else if (itemOwenerId != 0)
                {
                    DataClass newevent = new DataClass();
                    DataTable dt = newevent.ReturnItemOwner(itemOwenerId);
                    if (dt.Rows.Count > 0)
                    {
                        SelectItemOwnerName.Items.FindByText(dt.Rows[0].ItemArray[1].ToString()).Selected = true;
                        ItemOwnerEmailID.Value = dt.Rows[0].ItemArray[0].ToString();
                        Session["ItemOwnerEmailID"] = ItemOwnerEmailID.Value;
                    }
                }
                //When replaced item is selected in ItemType dropdown,check if the item exists in DB 
                if (itemnumber != null || (!string.IsNullOrEmpty(ItemValueId) && (ItemType == "replaceditem" || ItemType == "copieditem")))
                {

                    DataClass newevent = new DataClass();
                    if (!string.IsNullOrEmpty(ItemValueId) && (ItemType == "replaceditem" || ItemType == "copieditem"))
                    {

                        int isExists = newevent.CheckExistingItem(1, ItemValueId);
                        if (isExists == 0)
                        {
                            SelectItem.Items.FindByValue("newitem").Selected = true;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Number", "Item " + ItemValueId + " does not exist.  Please pick another."), true);
                            Session["itemnumber"] = ItemValueId;
                            ItemNumber.Value = string.Empty;    // ItemValueId;
                        }
                    }

                    //Prepopulate all fields based on existing item in DB
                    itemnumber = itemnumber != null ? itemnumber : ItemValueId;
                    DataTable dt = newevent.GetItems(itemnumber);
                    if (dt.Rows.Count == 0)
                    {
                        dt = newevent.GetItems(Convert.ToString(Session["itemnumber"]));
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (ItemType != null)
                        {
                            SelectItem.Items.FindByValue(ItemType).Selected = true;
                        }

                        //PREPOPLATION UPON CLICKING EDIT AND BEING REDIRECTED TO NEWIRF PAGE
                        Session["UIDData"] = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                        if (Convert.ToInt32(Session["isAdmin"]) != 1)
                        {
                            SubmitterName.Value = dt.Rows[0].ItemArray[55].ToString();
                        }

                        SubmitterName.Disabled = true;

                        SelectItemOwnerName.Value = dt.Rows[0].ItemArray[26].ToString();//Dropdown
                        if (Convert.ToInt32(Session["isAdmin"]) != 1)
                        {
                            SelectItemOwnerName.Disabled = true;
                        }
                        else
                        {
                            SelectItemOwnerName.Disabled = false;
                        }
                        ItemOwnerEmailID.Value = dt.Rows[0].ItemArray[61].ToString();

                        SelectItem.Value = dt.Rows[0].ItemArray[62].ToString();
                        if (Convert.ToInt32(Session["isAdmin"]) != 1)
                        {
                            ItemOwnerEmailID.Disabled = true;
                        }

                        SelectItem.Disabled = true;
                        //btnCopy.Visible = false;
                        btnReplace.Visible = false;
                        //  ItemNumber.Disabled = true;
                        // SelectItem.Value = dt.Rows[0].ItemArray[60].ToString();  correct array element is 62  spp-s 10/30
                        ItemNumber.Value = itemnumber != null ? itemnumber : dt.Rows[0].ItemArray[2].ToString();

                        //-----------------------------------------------------------------
                        //  Check for existence of OP inventory

                        ItemDescription.Disabled = false;
                        UofM.Disabled = false;
                        Quantity.Disabled = false;

                        string thisItem = dt.Rows[0].ItemArray[2].ToString();
                        string thisQOH = string.Empty;

                        DB2DataClass DB2QOH = new DB2DataClass();
                        try
                        {
                            DataTable QOHdt = DB2QOH.RtvItemQOH(thisItem);
                            foreach (DataRow QOHdr in QOHdt.Rows)
                            {
                                thisQOH = QOHdr[0].ToString();
                            }
                        }
                        catch { }

                        decimal QOH = 0;
                        try
                        { QOH = Convert.ToDecimal(thisQOH); }
                        catch
                        { QOH = 0; }

                        if (QOH <= 0)
                        {
                            ItemDescription.Value = dt.Rows[0].ItemArray[5].ToString();
                            UofM.Value = dt.Rows[0].ItemArray[11].ToString().Trim();
                            Quantity.Value = dt.Rows[0].ItemArray[12].ToString();
                        }
                        if (QOH > 0)
                        {
                            DB2DataClass DB2Val = new DB2DataClass();
                            DataTable Valdt = DB2Val.RtvItemInventoryValues(thisItem);
                            foreach (DataRow Valdr in Valdt.Rows)
                            {
                                ItemDescription.Value = Valdr[0].ToString();
                                UofM.Value = Valdr[1].ToString().Trim();
                                Quantity.Value = Valdr[2].ToString();
                            }
                            ItemDescription.Disabled = true;
                            UofM.Disabled = true;
                            Quantity.Disabled = true;
                            if (ItemType == "replaceditem" || ItemType == "copieditem")
                            {
                                //ItemNumber.Value = "x_" + ItemNumber.Value.ToString();
                                ItemNumber.Value = ItemNumber.Value.ToString();
                                ItemDescription.Disabled = false;
                                UofM.Disabled = false;
                                Quantity.Disabled = false;
                            }
                        }
                        //-----------------------------------------------------------------

                        CostCenter.Value = dt.Rows[0].ItemArray[6].ToString();
                        SelectGroup.Value = dt.Rows[0].ItemArray[9].ToString().Trim();
                        SelectSubCategory.Value = dt.Rows[0].ItemArray[10].ToString().Trim();
                        UnitLength.Value = dt.Rows[0].ItemArray[31].ToString();
                        UnitWidth.Value = dt.Rows[0].ItemArray[32].ToString();
                        UnitHeight.Value = dt.Rows[0].ItemArray[33].ToString();
                        ItemWeight.Value = dt.Rows[0].ItemArray[30].ToString();
                        PrimaryVendor.Value = dt.Rows[0].ItemArray[20].ToString();
                        ProdCost.Value = dt.Rows[0].ItemArray[13].ToString();
                        RetailPrice.Value = dt.Rows[0].ItemArray[14].ToString();
                        OrderLimitIntervalDropDownList.SelectedValue = dt.Rows[0].ItemArray[41].ToString();
                        //  Truncate decimals from returned LowWaterPoint
                        string LowWtrTxt = dt.Rows[0].ItemArray[22].ToString();
                        int LowWtrPos = LowWtrTxt.IndexOf(".");
                        LowWaterPoint.Value = LowWtrTxt.Substring(0, LowWtrPos);
                        //LowWaterPoint.Value = dt.Rows[0].ItemArray[22].ToString();

                        //  Truncate decimals from returned ExpectedQuantity
                        string ExpQtyTxt = dt.Rows[0].ItemArray[66].ToString();
                        int ExpQtyPos = ExpQtyTxt.IndexOf(".");
                        ExpectedQuantity.Value = ExpQtyTxt.Substring(0, ExpQtyPos);
                        //ExpectedQuantity.Value = dt.Rows[0].ItemArray[66].ToString();

                        //  Set Item Deleted CheckBox
                        if (dt.Rows[0].ItemArray[3].ToString() == "Y")
                        { CheckDelete.Checked = true; }

                        //  Set Radio Button Group
                        if (dt.Rows[0].ItemArray[19].ToString() == "A")
                        {
                            IPAriba.Checked = true;
                            viewableOnlyCheckbox.Disabled = false;
                        }
                        if (dt.Rows[0].ItemArray[19].ToString() == "O")
                        {
                            IPOneBox.Checked = true;
                            viewableOnlyCheckbox.Disabled = true;
                            viewableOnlyCheckbox.Checked = false;
                        }
                        if (dt.Rows[0].ItemArray[19].ToString() == "S")
                        {
                            IPSpecial.Checked = true;
                            viewableOnlyCheckbox.Disabled = true;
                            viewableOnlyCheckbox.Checked = false;
                        }
                        if (dt.Rows[0].ItemArray[19].ToString() == "P")
                        {
                            IPOneBoxAriba.Checked = true;
                            viewableOnlyCheckbox.Disabled = false;
                        }
                        if (dt.Rows[0].ItemArray[19].ToString() == "T")
                        {
                            IPSpecialAriba.Checked = true;
                            viewableOnlyCheckbox.Disabled = true;
                            viewableOnlyCheckbox.Checked = false;
                        }

                        //  Set Viewable only CheckBox
                        if (dt.Rows[0].ItemArray[18].ToString() == "Y")
                        {
                            viewableOnlyCheckbox.Checked = true;
                        }

                        //if (dt.Rows[0].ItemArray[19].ToString() == "A" || dt.Rows[0].ItemArray[19].ToString() == "P" || dt.Rows[0].ItemArray[19].ToString() == "T")
                        //{
                        //    SetupLimitDiv.Visible = true;
                        //    if (dt.Rows[0].ItemArray[40].ToString() == "Y")
                        //    {
                        //        SetupLimit.Checked = true;
                        //    }
                        //    else if (dt.Rows[0].ItemArray[40].ToString() == "N")
                        //    {
                        //        SetupLimit.Checked = false;
                        //    }
                        //}
                        //else
                        //{
                        //    SetupLimitDiv.Visible = false;
                        //}

                        if (ItemType != "replaceditem")
                        {
                            SupersededItem.Value = dt.Rows[0].ItemArray[21].ToString();
                            OneBoxID.Value = dt.Rows[0].ItemArray[35].ToString();
                            SelectBool.SelectedValue = dt.Rows[0].ItemArray[36].ToString();

                            //  Truncate decimals from returned StarterBoxQuantity
                            string StrBoxTxt = dt.Rows[0].ItemArray[37].ToString();
                            int StrBoxPos = StrBoxTxt.IndexOf(".");
                            StarterBoxQuantity.Value = StrBoxTxt.Substring(0, StrBoxPos);
                            //StarterBoxQuantity.Value = dt.Rows[0].ItemArray[37].ToString();
                        }
                        else
                        {
                            SupersededItem.Value = string.Empty;
                            //ItemNumber.Value = string.Empty;
                            OneBoxID.Value = string.Empty;
                            SelectBool.SelectedValue = string.Empty;
                            StarterBoxQuantity.Value = string.Empty;
                        }

                        if (SelectBool.SelectedItem.Value.ToString().Equals("Y"))
                        {
                            specifyQuantityandDate.Attributes["style"] = "display:block";
                        }
                        else
                        {
                            specifyQuantityandDate.Attributes["style"] = "display:none";
                            StarterBoxQuantity.Value = string.Empty;
                            ExpireDate.Text = string.Empty;

                        }

                        //  Set up date fields as MM/dd/yyyy and clear default date so datepicker works right
                        String CheckDateStr = string.Empty;

                        //  Item Active date
                        CheckDateStr = dt.Rows[0].ItemArray[24].ToString();
                        if ((CheckDateStr != "1900-01-01") && (ItemType != "replaceditem"))
                        {
                            DateToBeAdded.Text = CheckDateStr.Substring(5, 2) + "/" + CheckDateStr.Substring(8, 2) + "/" + CheckDateStr.Substring(0, 4);
                        }
                        else
                        {
                            DateToBeAdded.Text = string.Empty;
                        }

                        //  Item Expiration date
                        CheckDateStr = dt.Rows[0].ItemArray[25].ToString();
                        if ((CheckDateStr != "1900-01-01") && (ItemType != "replaceditem"))
                        {
                            DateToBeRemoved.Text = CheckDateStr.Substring(5, 2) + "/" + CheckDateStr.Substring(8, 2) + "/" + CheckDateStr.Substring(0, 4);
                        }
                        else
                        {
                            DateToBeRemoved.Text = string.Empty;
                        }

                        //  Starter Branch Item Expires On date
                        CheckDateStr = dt.Rows[0].ItemArray[38].ToString();
                        if ((CheckDateStr != "1900-01-01") && (ItemType != "replaceditem"))
                        {
                            ExpireDate.Text = CheckDateStr.Substring(5, 2) + "/" + CheckDateStr.Substring(8, 2) + "/" + CheckDateStr.Substring(0, 4);
                        }
                        else
                        {
                            ExpireDate.Text = string.Empty;
                        }

                        //  Expected Arrival Date
                        CheckDateStr = dt.Rows[0].ItemArray[65].ToString();
                        if ((CheckDateStr != "1900-01-01") && (ItemType != "replaceditem"))
                        {
                            ExpectedArrival.Text = CheckDateStr.Substring(5, 2) + "/" + CheckDateStr.Substring(8, 2) + "/" + CheckDateStr.Substring(0, 4);
                        }
                        else
                        {
                            ExpectedArrival.Text = string.Empty;
                        }

                        //  Special Distribution Delivery Date (uses ArtworokReleseDate)
                        // int FirstSlash;
                        // int SecondSlash;
                        //CheckDateStr = dt.Rows[0].ItemArray[44].ToString();
                        //FirstSlash = CheckDateStr.IndexOf("/");
                        //SecondSlash = CheckDateStr.Substring(FirstSlash + 1).IndexOf("/");
                        //string SPDMonth = CheckDateStr.Substring(0, FirstSlash);
                        //string SPDDay = CheckDateStr.Substring(FirstSlash + 1, SecondSlash);
                        //string SPDYear = CheckDateStr.Substring((4 + (FirstSlash - 1) + (SecondSlash - 1)), 4);
                        //if (SPDMonth.Length < 2)
                        //{
                        //    SPDMonth = "0" + SPDMonth.Trim();
                        //}
                        //if (SPDDay.Length < 2)
                        //{
                        //    SPDDay = "0" + SPDDay.Trim();
                        //}
                        // CheckDateStr = SPDMonth + "/" + SPDDay + "/" + SPDYear;
                        //if ((CheckDateStr != "01/01/1900") && (ItemType != "replaceditem"))
                        //{
                        //    SpecDistDelivery.Text = CheckDateStr;
                        //}
                        //else
                        //{
                        //    SpecDistDelivery.Text = string.Empty;
                        //}

                        //  Truncate decimals from returned MaxOrderQuantity
                        string MaxOrdTxt = dt.Rows[0].ItemArray[39].ToString();
                        int MaxOrdPos = MaxOrdTxt.IndexOf(".");
                        MaxOrderQuantity.Value = MaxOrdTxt.Substring(0, MaxOrdPos);
                        //MaxOrderQuantity.Value = dt.Rows[0].ItemArray[39].ToString();

                        DistributionRuleText.Value = dt.Rows[0].ItemArray[63].ToString();
                        //Notes.Value = dt.Rows[0].ItemArray[68].ToString();
                        if (dt.Rows[0].ItemArray[68].ToString().Equals("PBusinessR"))
                        {
                            PerBusinessRule.Checked = true;
                            distributionDiv.Visible = true;
                            UploadDiv.Visible = false;
                        }
                        else if (dt.Rows[0].ItemArray[68].ToString().Equals("PCustomL"))
                        {
                            PerCustomList.Checked = true;
                            distributionDiv.Visible = false;
                            UploadDiv.Visible = true;
                        }

                        if ((!string.IsNullOrWhiteSpace(dt.Rows[0].ItemArray[64].ToString())) && (ItemType != "replaceditem"))
                        {
                            //RulesFile.Value = dt.Rows[0].ItemArray[64].ToString();
                            filedownload.InnerText = dt.Rows[0].ItemArray[64].ToString();
                            string filePath = Convert.ToString(ConfigurationManager.AppSettings["filepath"]);
                            // filedownload.HRef = filedownload.Value;
                        }

                    }

                }



                //if (Request.QueryString["ItemType"]!=null)
                //{ 
                //Request.QueryString.Remove("ItemType");
                //}


                //     else
                //     {
                //         Response.Redirect("Unauthorized.aspx");
                //     } // TODO: Validate time in query string....
            }

            if (IsPostBack)
            {
                if (Session["FileUpload"] == null && distributionrule.HasFile)
                {
                    Session["FileUpload"] = distributionrule;
                    distributionruleLabel.InnerText = distributionrule.FileName; // get the name 
                }
                // This condition will occur on next postbacks        
                else if (Session["FileUpload"] != null && (!distributionrule.HasFile))
                {
                    distributionrule = (FileUpload)Session["FileUpload"];
                    distributionruleLabel.InnerText = distributionrule.FileName;
                }
                //  when Session will have File but user want to change the file 
                // i.e. wants to upload a new file using same FileUpload control
                // so update the session to have the newly uploaded file
                else if (distributionrule.HasFile)
                {
                    Session["FileUpload"] = distributionrule;
                    distributionruleLabel.InnerText = distributionrule.FileName;
                }
                int itemownerReload = Convert.ToInt32(SelectItemOwnerName.Value.ToString());
                if (itemownerReload != 0)
                {
                    DataClass newevent = new DataClass();
                    DataTable itemownerdata = newevent.ReturnItemOwner(itemownerReload);
                    foreach (DataRow R in itemownerdata.Rows)
                    {
                        ItemOwnerEmailID.Value = R["Email"].ToString();
                    }
                }
                //if (IPSpecial.Checked == true)
                //{
                //    SpecDistDelivery.Visible = true;
                //}
                //else
                //{
                //    SpecDistDelivery.Visible = false;
                //}
                if (SelectBool.SelectedItem.Value.ToString().Equals("Y"))
                {
                    specifyQuantityandDate.Attributes["style"] = "display:block";
                }
                else
                {
                    specifyQuantityandDate.Attributes["style"] = "display:none";
                    StarterBoxQuantity.Value = string.Empty;
                    ExpireDate.Text = string.Empty;
                }
                if (IPAriba.Checked == true)
                {
                    viewableOnlyCheckbox.Disabled = false;

                    //SetupLimitDiv.Visible = true;
                }
                if (IPOneBox.Checked == true)
                {
                    viewableOnlyCheckbox.Disabled = true;
                    viewableOnlyCheckbox.Checked = false;

                    //SetupLimitDiv.Visible = false;
                    //SetupLimit.Checked = false;
                }
                if (IPOneBoxAriba.Checked == true)
                {
                    viewableOnlyCheckbox.Disabled = false;

                    //SetupLimitDiv.Visible = true;
                }
                if (IPSpecial.Checked == true)
                {
                    viewableOnlyCheckbox.Disabled = true;
                    viewableOnlyCheckbox.Checked = false;

                    //SetupLimitDiv.Visible = false;
                    //SetupLimit.Checked = false;
                }
                if (IPSpecialAriba.Checked == true)
                {
                    viewableOnlyCheckbox.Disabled = true;
                    viewableOnlyCheckbox.Checked = false;

                    //SetupLimitDiv.Visible = true;
                }

                if (PerBusinessRule.Checked == true)
                {
                    distributionDiv.Visible = true;
                    UploadDiv.Visible = false;
                }
                else if (PerCustomList.Checked == true)
                {
                    distributionDiv.Visible = false;
                    UploadDiv.Visible = true;
                }
                else
                {
                    distributionDiv.Visible = false;
                    UploadDiv.Visible = false;
                }
            }
        }

        //protected void IPSpecial_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (IPSpecial.Checked == true)
        //    {
        //        SpecDistDelivery.Enabled = true;
        //    }
        //    else
        //    {
        //        SpecDistDelivery.Enabled = false;
        //    }
        //}

        protected void Download_Click(object sender, EventArgs e)
        {
            try
            {
                string strURL = filedownload.InnerText;
                int FileSep = strURL.IndexOf(".");
                string newFile = strURL.Substring(0, (FileSep)) + '_' + ItemNumber.Value + strURL.Substring(FileSep);
                string filePath = Convert.ToString(ConfigurationManager.AppSettings["filepath"]);
                string path = filePath + "/" + newFile;
                WebClient req = new WebClient();
                // req.DownloadFile(path, strURL);
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + path + "\"");
                byte[] data = req.DownloadData(path);
                response.BinaryWrite(data);
                response.End();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean IsError = false;
            //  Start input validation

            //When Item Number is left blank
            string itemnumberValidate = ItemNumber.Value;
            if (itemnumberValidate == null || itemnumberValidate.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Number", "Item number cannot be blank."), true);
                IsError = true;
            }

            //When Item Description is left blank
            string itemdescriptionValidate = ItemDescription.Value;
            if (itemdescriptionValidate == null || itemdescriptionValidate.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Description", "Item description cannot be blank."), true);
                IsError = true;
            }

            //When Owner is left blank
            int itemownerValidate = Convert.ToInt32(SelectItemOwnerName.Value.ToString());
            if (itemownerValidate == 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Owner", "Please select an item owner."), true);
                IsError = true;
            }

            //When Item purpose is left blank
            if (IPAriba.Checked == false && IPOneBox.Checked == false && IPOneBoxAriba.Checked == false && IPSpecial.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Purpose", "Please select an item purpose."), true);
                IsError = true;
            }

            //if (PerBusinessRule.Checked == true && DistributionRuleText.Value.ToString().Trim().Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Owner", "Please Specify Business Rule."), true);
            //    IsError = true;
            //}

            //if (PerCustomList.Checked == true && (distributionruleLabel.InnerText.Trim().Length == 0 && filedownload.InnerHtml.Trim().Length == 0 && !distributionrule.HasFile))
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", "Item Owner", "Please upload a custom list."), true);
            //    IsError = true;
            //}

            // Only proceed if no erros are found
            if (IsError == false)
            {

                //Response.Redirect("Home.aspx");

                string encryptionkey = "ChaseIRFKey";
                //string idencrypted = Request.QueryString["ID"];
                string idencrypted = Request.QueryString["OPCustomer"];
                var iddecrypted = Spritz.EPIDecrypt(idencrypted, encryptionkey);
                var id = Convert.ToInt32(iddecrypted);

                //User info div details
                string submittinguser = Session["UserName"] != null ? Convert.ToString(Session["UserName"]) : "";
                string email = SubmitterEmailID.Value;
                int itemowner = Convert.ToInt32(SelectItemOwnerName.Value.ToString());
                //missing itemowner emailID here


                //Item Details div
                string status = "1";
                string itemtype = SelectItem.Value;
                string itemnumber = ItemNumber.Value;
                string itemdescription = ItemDescription.Value;
                string productgroup = SelectGroup.Value;
                string businessrule = DistributionRuleText.Value;
                //Save distribution rule attachments in preferred path
                var filedata = Request.Files["distributionrule"];
                string newFile = string.Empty;
                if (Session["FileUpload"] != null || (distributionrule.HasFile))
                {
                    if (distributionrule.HasFile)
                    {
                        filedata = distributionrule.PostedFile;
                    }
                    else
                    {
                        filedata = (HttpPostedFile)Session["FileUpload"];
                    }
                    string fileNameApplication = System.IO.Path.GetFileName(filedata.FileName);
                    //  string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);

                    int FileSep = fileNameApplication.IndexOf(".");
                    newFile = fileNameApplication.Substring(0, (FileSep)) + '_' + itemnumber + fileNameApplication.Substring(FileSep);
                    string filePath = Convert.ToString(ConfigurationManager.AppSettings["filepath"]);
                    // string filePath = System.IO.Path.Combine(Server.MapPath("uploads"), newFile);

                    if (fileNameApplication != String.Empty)
                    {
                        filedata.SaveAs(Path.Combine(filePath, newFile));
                    }
                }
                string businessRuleFile = newFile;
                string costctr = CostCenter.Value;
                string stockinguom = UofM.Value;
                int quantityuom = 0;
                if (Quantity.Value != "")
                {
                    quantityuom = Convert.ToInt32(Quantity.Value);
                }

                decimal expectedquantity = 0;
                if (ExpectedQuantity.Value != "")
                {
                    expectedquantity = Convert.ToDecimal(ExpectedQuantity.Value);
                }

                decimal lastcost = 0;
                if (ProdCost.Value != "")
                {
                    lastcost = Convert.ToDecimal(ProdCost.Value);
                }
                decimal itemlength = UnitLength.Value != "" ? Convert.ToDecimal(UnitLength.Value) : 0;
                decimal itemwidth = UnitWidth.Value != "" ? Convert.ToDecimal(UnitWidth.Value) : 0;
                decimal itemheight = UnitHeight.Value != "" ? Convert.ToDecimal(UnitHeight.Value) : 0;
                decimal itemweight = ItemWeight.Value != "" ? Convert.ToDecimal(ItemWeight.Value) : 0;
                int primaryvendor = PrimaryVendor.Value != "" ? Convert.ToInt32(PrimaryVendor.Value) : 0;
                //OneBox div details
                string oneboxid = OneBoxID.Value;
                string starteritem = SelectBool.SelectedValue;
                decimal starterquantity = StarterBoxQuantity.Value != "" ? Convert.ToDecimal(StarterBoxQuantity.Value) : 0;
                string starterexpiredate = Request.Form[ExpireDate.UniqueID]; // ExpireDate.Text; 

                //ePurchase Div Details
                //string activedate = Request.Form[DateToBeAdded.UniqueID]; //DateToBeAdded.Text;
                string activedate = null;
                if (DateToBeAdded.Text != "")
                {
                    activedate = Request.Form[DateToBeAdded.UniqueID];
                }
                else
                {
                    activedate = "01/01/1900";
                }

                string expiredate = null;
                if (DateToBeRemoved.Text != "")
                {
                    expiredate = Request.Form[DateToBeRemoved.UniqueID];
                }
                else
                {
                    expiredate = "01/01/1900";
                }

                string expectedarrival = null;
                if (ExpectedArrival.Text != "")
                {
                    expectedarrival = Request.Form[ExpectedArrival.UniqueID];
                }
                else
                {
                    expectedarrival = "01/01/1900";
                }

                DateTime artreleasedate;
                //if (SpecDistDelivery.Text != "")
                //{
                //    artreleasedate = DateTime.Parse(Request.Form[SpecDistDelivery.UniqueID]);
                //}
                //else
                {
                    artreleasedate = DateTime.Parse("01/01/1900");
                }

                //This is the old item which is being replaced by new item
                string supersededitem = SupersededItem.Value;
                decimal retailprice = 0;
                if (RetailPrice.Value != "")
                {
                    retailprice = Convert.ToDecimal(RetailPrice.Value);
                }
                decimal maxorderquantity = MaxOrderQuantity.Value != "" ? Convert.ToDecimal(MaxOrderQuantity.Value) : 0;
                string productsubgroup = SelectSubCategory.Value;

                //Extra fields not needed by the Chase client
                string glclass = "";
                string taxclass = "";
                string dropship = "";
                string phaseout = "";
                string stockitem = "";
                string salable = "";
                string manufactured = "";
                decimal minstocklevel = LowWaterPoint.Value != "" ? Convert.ToDecimal(LowWaterPoint.Value) : 0;
                decimal maxstocklevel = 0;
                string printondemand = "";
                int printondemandvendor = 0;
                string webviewable = "";
                decimal prefpackquantity = 0;
                string printrequesttype = "N";
                string printjts = OrderLimitIntervalDropDownList.SelectedValue; ;
                string printcostcenter = "";
                string printbuscase = "";
                string creativeagency = "";
                decimal printquantity = 0;
                int printversions = 0;
                int printpages = 0;
                int printcolors = 0;
                string paperstock = "";
                int printsides = 0;
                decimal flatlength = 0;
                decimal flatwidth = 0;
                string uniquealgorythm = "";
                string userid = Session["UserName"] != null ? Convert.ToString(Session["UserName"]) : "";
                string comments = "";
                string deleted = "";

                //if(SetupLimit.Checked == true)
                //{
                //    printrequesttype = "Y";
                //}

                //  Set Item Deleted CheckBox
                if (CheckDelete.Checked == true)
                { deleted = "Y"; }

                //Set viewableOnlyCheckbox value to true or false
                string ViewOnly = "N";
                if (viewableOnlyCheckbox.Checked == true)
                {
                    ViewOnly = "Y";
                }
                else
                {
                    ViewOnly = "N";
                }

                //  Set Item Purpose from Radio Button Group
                if (IPAriba.Checked == true)
                {
                    manufactured = "A";
                    viewableOnlyCheckbox.Visible = true;
                }
                if (IPOneBox.Checked == true)
                {
                    manufactured = "O";
                    viewableOnlyCheckbox.Checked = false;
                    viewableOnlyCheckbox.Visible = false;
                }
                if (IPSpecial.Checked == true)
                {
                    manufactured = "S";
                    viewableOnlyCheckbox.Checked = false;
                    viewableOnlyCheckbox.Visible = false;
                    //SpecDistDelivery.Visible = true;
                }
                if (IPOneBoxAriba.Checked == true)
                {
                    manufactured = "P";
                    viewableOnlyCheckbox.Visible = true;
                }
                if (IPSpecialAriba.Checked == true)
                {
                    manufactured = "T";
                    viewableOnlyCheckbox.Checked = false;
                    viewableOnlyCheckbox.Visible = false;
                }

                //  Setting Status
                status = "1";

                //  Ariba and special Distribution to Ariba
                //  if ((manufactured == "A") || (manufactured == "T"))
                if (manufactured == "A")
                {
                    if ((itemdescription != null && itemdescription != String.Empty) &&
                        (costctr != null && costctr != String.Empty) &&
                        (productgroup != null && productgroup != String.Empty) &&
                        (productsubgroup != null && productsubgroup != String.Empty) &&
                        (stockinguom != null && stockinguom != String.Empty) &&
                        (quantityuom > 0) &&
                        (lastcost > 0) &&
                        (primaryvendor > 0) &&
                        (minstocklevel > 0) &&
                        (activedate != null) &&  //&& activedate > DateTime.MinValue
                        (expiredate != null && Convert.ToDateTime(expiredate) > DateTime.MinValue) &&
                        (itemowner > 0) &&
                        (itemweight > 0) &&
                        (itemlength > 0) &&
                        (itemwidth > 0) &&
                        (itemheight > 0) &&
                        (itemheight > 0))
                    {

                        status = "4";
                    }
                }

                //  Are we OK for One Box to Ariba?
                if (manufactured == "P")
                {
                    if ((itemdescription != null && itemdescription != String.Empty) &&
                        (costctr != null && costctr != String.Empty) &&
                        (productgroup != null && productgroup != String.Empty) &&
                        (productsubgroup != null && productsubgroup != String.Empty) &&
                        (stockinguom != null && stockinguom != String.Empty) &&
                        (quantityuom > 0) &&
                        (lastcost > 0) &&
                        (primaryvendor > 0) &&
                        (minstocklevel > 0) &&
                        (itemowner > 0) &&
                        (itemweight > 0) &&
                        (itemlength > 0) &&
                        (itemwidth > 0) &&
                        (itemheight > 0) &&
                        (businessrule != null && businessrule != string.Empty) || (businessRuleFile != null && businessRuleFile != string.Empty) &&
                        (oneboxid != null && oneboxid != String.Empty) &&
                        ((starteritem == "N") ||
                        (starteritem == "Y") &&
                        (starterquantity > 0) &&
                        (starterexpiredate != null && starterexpiredate != String.Empty))
                        )
                    {
                        status = "4";
                    }
                }

                //  Are we OK for One Box only?
                if (manufactured == "O")
                {
                    if ((itemdescription != null && itemdescription != String.Empty) &&
                        (costctr != null && costctr != String.Empty) &&
                        (productgroup != null && productgroup != String.Empty) &&
                        (stockinguom != null && stockinguom != String.Empty) &&
                        (quantityuom > 0) &&
                        (primaryvendor > 0) &&
                        (itemowner > 0) &&
                        (itemweight > 0) &&
                        (itemlength > 0) &&
                        (itemwidth > 0) &&
                        (itemheight > 0) &&
                        (businessrule != null && businessrule != string.Empty) || (businessRuleFile != null && businessRuleFile != string.Empty) &&
                        (oneboxid != null && oneboxid != String.Empty) &&
                        ((starteritem == "N") ||
                        (starteritem == "Y") &&
                        (starterquantity > 0) &&
                        (starterexpiredate != null && starterexpiredate != String.Empty))
                        )
                    {
                        status = "4";
                    }
                }

                //  Are we OK for Special Distribution?
                if (manufactured == "S")
                {
                    if ((itemdescription != null && itemdescription != String.Empty) &&
                        (costctr != null && costctr != String.Empty) &&
                        (productgroup != null && productgroup != String.Empty) &&
                        (stockinguom != null && stockinguom != String.Empty) &&
                        (quantityuom > 0) &&
                        (primaryvendor > 0) &&
                        (minstocklevel > 0) &&
                        (lastcost > 0) &&
                        (itemweight > 0) &&
                        (itemlength > 0) &&
                        (itemwidth > 0) &&
                        (itemheight > 0) &&
                        (itemowner > 0))
                    {
                        status = "4";
                    }
                }

                //  Get OP values before update if necessary
                string thisItem = itemnumber;
                string thisQOH = string.Empty;

                DB2DataClass DB2QOH = new DB2DataClass();
                DataTable QOHdt = DB2QOH.RtvItemQOH(thisItem);
                foreach (DataRow QOHdr in QOHdt.Rows)
                {
                    thisQOH = QOHdr[0].ToString();
                }

                decimal QOH = 0;
                try
                { QOH = Convert.ToDecimal(thisQOH); }
                catch
                { QOH = 0; }

                if (QOH > 0)
                {
                    DB2DataClass DB2Val = new DB2DataClass();
                    DataTable Valdt = DB2Val.RtvItemInventoryValues(thisItem);
                    foreach (DataRow Valdr in Valdt.Rows)
                    {
                        itemdescription = Valdr[0].ToString();
                        stockinguom = Valdr[1].ToString().Trim();
                        quantityuom = Convert.ToInt32(Valdr[2].ToString());
                    }
                }

                //  Maintain Item
                int trycatchresult = 2;
                try
                {
                    DataClass newevent = new DataClass();
                    int UID = 0;
                    //Convert.ToInt32(Session["UIDData"])==0  ||
                    //if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["itemNumber"])) && (itemtype == "newitem" || itemtype == "replaceditem" || itemtype == "copieditem"))
                    int ItemExistFg;
                    ItemExistFg = newevent.CheckExistingItem(1, itemnumber);
                    if (ItemExistFg == 0)
                    {
                        UID = newevent.NewItem(status,
                                                 itemnumber,
                                                 itemdescription,
                                                 costctr,
                                                 glclass,
                                                 taxclass,
                                                 productgroup,
                                                 productsubgroup,
                                                 stockinguom,
                                                 quantityuom,
                                                 lastcost,
                                                 retailprice,
                                                 dropship,
                                                 phaseout,
                                                 stockitem,
                                                 //salable,
                                                 manufactured,
                                                 primaryvendor,
                                                 supersededitem,
                                                 minstocklevel,
                                                 maxstocklevel,
                                                activedate,
                                                //Convert.ToDateTime(expiredate),
                                                expiredate,
                                                 itemowner,
                                                 printondemand,
                                                 printondemandvendor,
                                                 webviewable,
                                                 itemweight,
                                                 itemlength,
                                                 itemwidth,
                                                 itemheight,
                                                 prefpackquantity,
                                                 oneboxid,
                                                 starteritem,
                                                 starterquantity,
                                                starterexpiredate,
                                                 maxorderquantity,
                                                 printrequesttype,
                                                 printjts,
                                                 printcostcenter,
                                                 printbuscase,
                                                 artreleasedate,
                                                 creativeagency,
                                                 printquantity,
                                                 printversions,
                                                 printpages,
                                                 printcolors,
                                                 paperstock,
                                                 printsides,
                                                 flatlength,
                                                 flatwidth,
                                                 uniquealgorythm,
                                                 submittinguser,
                                                 userid,
                                                 comments,
                                                 itemtype,
                                                 expectedarrival,
                                                 expectedquantity,
                                                 ViewOnly);
                    }
                    else
                    {
                        UID = newevent.UpdItem(status,
                                                 itemnumber,
                                                 itemdescription,
                                                 costctr,
                                                 glclass,
                                                 taxclass,
                                                 productgroup,
                                                 productsubgroup,
                                                 stockinguom,
                                                 quantityuom,
                                                 Math.Truncate(lastcost),
                                                 retailprice,
                                                 dropship,
                                                 phaseout,
                                                 stockitem,
                                                 //salable,
                                                 manufactured,
                                                 primaryvendor,
                                                 supersededitem,
                                                 minstocklevel,
                                                 maxstocklevel,
                                                activedate,
                                                expiredate,
                                                 //Convert.ToDateTime(expiredate),
                                                 itemowner,
                                                 printondemand,
                                                 printondemandvendor,
                                                 webviewable,
                                                 itemweight,
                                                 itemlength,
                                                 itemwidth,
                                                 itemheight,
                                                 prefpackquantity,
                                                 oneboxid,
                                                 starteritem,
                                                 starterquantity,
                                                starterexpiredate,
                                                 maxorderquantity,
                                                 printrequesttype,
                                                 printjts,
                                                 printcostcenter,
                                                 printbuscase,
                                                 artreleasedate,
                                                 creativeagency,
                                                 printquantity,
                                                 printversions,
                                                 printpages,
                                                 printcolors,
                                                 paperstock,
                                                 printsides,
                                                 flatlength,
                                                 flatwidth,
                                                 uniquealgorythm,
                                                 submittinguser,
                                                 expectedarrival,
                                                 expectedquantity,
                                                 userid,
                                                 comments,
                                                 deleted,
                                                 ViewOnly);
                    }

                    DataClass returnresult = new DataClass();

                    // DataTable dt = returnresult.ReturnItem(UID);

                    EmailClass EC = new EmailClass();
                    bool HTML = true;
                    int result = 0;
                    //Try Catch block
                    trycatchresult = 1;

                    if (UID >= 1)
                    {
                        DataClass dc = new DataClass();
                        if (!string.IsNullOrEmpty(businessrule) || !string.IsNullOrEmpty(newFile) || (filedata != null && filedata.ContentLength > 0))
                        {
                            string fileNameApplication = "";
                            if (filedata != null && filedata.ContentLength > 0)
                            {
                                fileNameApplication = System.IO.Path.GetFileName(filedata.FileName);
                            }
                            string ruleType = PerBusinessRule.Checked == true ? "PBusinessR" : (PerCustomList.Checked == true ? "PCustomL" : "");
                            //string notesText = Notes.InnerText;
                            string notesText = string.Empty;
                            int BusinessRuleId = dc.NewBusinessRule(itemnumber, oneboxid, businessrule, fileNameApplication, newFile, submittinguser, notesText, ruleType);
                        }
                    }

                    if (UID != 0)
                    {
                        //Send Email
                        DataTable dtNotification = new DataTable();
                        // DataClass returnresult = new DataClass();
                        // EmailClass EC = new EmailClass();


                        // bool HTML = true;
                        //int result = 0;

                        //  Send notification only if new item
                        if (ItemExistFg == 0)
                        {
                            string fromEmailId = Convert.ToString(ConfigurationManager.AppSettings["fromEmailId"]);
                            string toEmailID = Convert.ToString(ConfigurationManager.AppSettings["toEmailID"]);
                            string CCId = Convert.ToString(ConfigurationManager.AppSettings["CCId"]);
                            string BccID = Convert.ToString(ConfigurationManager.AppSettings["BccID"]);
                            //string subject = "Chase IRF entry received";
                            Emails model = new Emails();
                            int NotificationId = model.CreateEmailNotification(itemnumber, toEmailID);
                            // int  NotificationId = returnresult.NewItemNotification(1, itemnumber, itemowner, submittinguser, fromEmailId, toEmailID, CCId, BccID, subject, htmlBody);
                            dtNotification = returnresult.GetItemNotification(NotificationId);
                            foreach (DataRow R in dtNotification.Rows)
                            {
                                //  int UID = Convert.ToInt32(R["UID"]);
                                string Status = R["Status"].ToString();
                                string Body = R["EmBody"].ToString();

                                string ToAddress = R["EmTo"].ToString();
                                string ccAddress = R["EmCC"].ToString();
                                string bccAddress = R["EmBCC"].ToString();

                                //  For testing and early go live  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                                //ToAddress = "c_rabey@epiinc.com";
                                //ccAddress = "g_loza@epiinc.com";
                                //bccAddress = "s_stevens@epiinc.com";
                                //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                                string Subject = R["EmSubject"].ToString();
                                // bool HTML = false;
                                result = EC.SendEmail(Body, ToAddress, ccAddress, bccAddress, Subject, true);
                            }
                        }

                        //  Maintain OP Item
                        DB2DataClass db2DataClassInstance = new DB2DataClass();
                        db2DataClassInstance.IRFCreate(itemnumber,
                                                        itemdescription,
                                                        itemowner,
                                                        productgroup,
                                                        productsubgroup,
                                                        costctr,
                                                        stockinguom,
                                                        quantityuom,
                                                        itemlength,
                                                        itemwidth,
                                                        itemheight,
                                                        itemweight,
                                                        oneboxid,
                                                        primaryvendor,
                                                        activedate,
                                                        expiredate,
                                                        maxorderquantity,
                                                        minstocklevel,
                                                        maxstocklevel,
                                                        supersededitem,
                                                        retailprice,
                                                        ViewOnly,
                                                        manufactured);
                    }

                }
                catch (Exception ex)
                {
                    trycatchresult = 2;
                }

                if (trycatchresult == 1)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Response.Redirect("Unauthorized.aspx");
                }
            }
        }


        //Category Dropdown List
        private void fillcategory()
        {
            DataClass newevent = new DataClass();
            DataTable dtdropdownlist = newevent.getddl();
            SelectGroup.Items.Clear();
            SelectGroup.DataSource = dtdropdownlist;
            SelectGroup.DataTextField = "DisplayText";
            SelectGroup.DataValueField = "CodeValue";
            SelectGroup.DataBind();
            SelectGroup.Items.Insert(0, new ListItem("- Select Category", ""));
        }

        private void fillsubcategory()
        {
            //Sub Category Dropdown List
            DataClass newevent = new DataClass();
            DataTable dtdropdownsubcategory = newevent.getddlsubcategory();
            SelectSubCategory.Items.Clear();
            SelectSubCategory.DataSource = dtdropdownsubcategory;
            SelectSubCategory.DataTextField = "DisplayText";
            SelectSubCategory.DataValueField = "CodeValue";
            SelectSubCategory.DataBind();
            SelectSubCategory.Items.Insert(0, new ListItem("- Select Sub Category", ""));
        }
        private void fillitemowners()
        {
            //Item Owners Dropdown List
            DataClass newevent = new DataClass();
            DataTable dtdropdownItemOwners = newevent.GetItemOwners();
            SelectItemOwnerName.Items.Clear();
            SelectItemOwnerName.DataSource = dtdropdownItemOwners;
            SelectItemOwnerName.DataTextField = "OwnerName";
            SelectItemOwnerName.DataValueField = "OPCustomer";
            SelectItemOwnerName.DataBind();
            SelectItemOwnerName.Items.Insert(0, new ListItem("- Select Item Owner", "0"));
        }

        //Vendor Dropdown List
        private void fillVendors()
        {
            DataClass newevent = new DataClass();
            DataTable vendorDropdownList = newevent.GetVendors();
            PrimaryVendor.Items.Clear();
            PrimaryVendor.DataSource = vendorDropdownList;
            PrimaryVendor.DataTextField = "VendorName";
            PrimaryVendor.DataValueField = "OPVendor";
            PrimaryVendor.DataBind();
            PrimaryVendor.Items.Insert(0, new ListItem("- Select Vendor", "0"));
        }

        //One Box Dropdown List
        private void fillOneBoxID()
        {
            DataClass newevent = new DataClass();
            DataTable OneBoxIDDropdownList = newevent.GetProject(1);
            OneBoxID.Items.Clear();
            OneBoxID.DataSource = OneBoxIDDropdownList;
            OneBoxID.DataTextField = "Description";
            OneBoxID.DataValueField = "OneBoxID";
            OneBoxID.DataBind();
            OneBoxID.Items.Insert(0, new ListItem("- Select One Box", ""));
        }

        //Unit of Measure Drop Down List
        private void fillUnitOfMeasure()
        {
            DataClass newevent = new DataClass();
            DataTable UOMDropdownList = newevent.GetClientCodes(1, "UnitOfMeasure");
            UofM.Items.Clear();
            UofM.DataSource = UOMDropdownList;
            UofM.DataTextField = "DisplayText";
            UofM.DataValueField = "CodeValue";
            UofM.DataBind();
            UofM.Items.Insert(0, new ListItem("- Select Unit of Measure", ""));
        }

        [WebMethod]
        public static string rtnSelectItemOwnerName(string Param1)
        {
            string param1 = Param1;
            int opcustomer = 0;
            opcustomer = Convert.ToInt32(param1);
            string RtnData = string.Empty;

            DataClass newevent = new DataClass();
            DataTable itemownerdata = newevent.ReturnItemOwner(opcustomer);
            foreach (DataRow R in itemownerdata.Rows)
            {
                RtnData = R["Email"].ToString();
            }
            return RtnData;
        }


    }
}



