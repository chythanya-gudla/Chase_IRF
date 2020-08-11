using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chase_IRF
{
    public class Items
    {
        //  Default Global Values
        private int ChaseClientID = 1;

        //  Parsed Item Values
        private int I_ItemUID;
        private int I_ClientID;
        private string I_ItemNumber;
        private string I_ItemDeleted;
        private string I_ItemStatus;
        private string I_ItemDescription;
        private string I_ItemCostCtr;
        private string I_ProductGroup;
        private string I_ProductSubGroup;
        private string I_StockingUOM;
        private int I_QuantityUOM;
        private decimal I_LastCost;
        private decimal I_RetailPrice;
        private int I_PrimaryVendor;
        private string I_SupersededItem;
        private decimal I_MinStockLevel;
        private DateTime I_ActiveDate;
        private DateTime I_ExpirationDate;
        private int I_ItemOwner;
        private decimal I_ItemWeight;
        private decimal I_ItemLength;
        private decimal I_ItemWidth;
        private decimal I_ItemHeight;
        private string I_OneBoxID;
        private string I_StarterItem;
        private decimal I_StarterQuantity;
        private DateTime I_StarterExpireDate;
        private decimal I_MaxOrderQuantity;
        private string I_SubmittingUser;
        private string I_UpdatingUser;
        private DateTime I_UpdatedDate;
        private string I_Uploaded;
        private DateTime I_UploadedDate;
        private DateTime I_SubmittedDate;
        private string I_VendorName;
        private string I_OwnerName;
        private string I_OwnerEmail;
        private string I_ProductGroupDesc;
        private string I_ProductSubGroupDesc;
        
        private int R_RuleUID;
        private int R_ClientID;
        private string R_ItemNumber;
        private string R_RuleText;
        private string R_OrgFileName;
        private string R_EPIFileName;
        private int R_RuleSequence;
        private string R_UpdateUser;
        private DateTime R_UpdateDate;


        // * -----------------------------------------------------------------------------------------
        //   Constructors
        // * -----------------------------------------------------------------------------------------
        // Create with no load
        public Items()
        { }

        // Create and load datarow
        public Items(DataRow dr)
        { LoadItem(dr); }

        // * -----------------------------------------------------------------------------------------
        //   Parse datarow values for Item
        // * -----------------------------------------------------------------------------------------
        public void LoadItem(DataRow dr)
        {
            String NullTest = String.Empty;
            DateTime DftDate = Convert.ToDateTime("1900-01-01");

            try
            {
                I_ItemUID = Convert.ToInt32(dr["UID"]);
                I_ClientID = Convert.ToInt32(dr["ClientID"]);    // Client ID cannot be null

                I_ItemNumber = Convert.ToString(dr["ItemNumber"]);
                I_ItemDeleted = Convert.ToString(dr["Deleted"]);
                I_ItemStatus = Convert.ToString(dr["Status"]);
                I_ItemDescription = Convert.ToString(dr["ItemDescription"]);
                I_ItemCostCtr = Convert.ToString(dr["ItemCostCtr"]);
                I_ProductGroup = Convert.ToString(dr["ProductGroup"]);
                I_ProductSubGroup = Convert.ToString(dr["ProductSubGroup"]);
                I_StockingUOM = Convert.ToString(dr["StockingUOM"]);

                //Fix nulls for quantity / UOM  ---------------------
                NullTest = Convert.ToString(dr["QuantityUOM"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_QuantityUOM = 0; }
                else
                { I_QuantityUOM = Convert.ToInt32(dr["QuantityUOM"]); }

                //Fix nulls for Last Cost  --------------------------
                NullTest = Convert.ToString(dr["LastCost"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_LastCost = 0; }
                else
                { I_LastCost = Convert.ToDecimal(dr["LastCost"]); }

                //Fix nulls for Retail Price  -----------------------
                NullTest = Convert.ToString(dr["RetailPrice"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_RetailPrice = 0; }
                else
                { RetailPrice = Convert.ToDecimal(dr["RetailPrice"]); }

                //Fix nulls for Primary Vendor  -----------------------
                NullTest = Convert.ToString(dr["PrimaryVendor"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_PrimaryVendor = 0; }
                else
                { I_PrimaryVendor = Convert.ToInt32(dr["PrimaryVendor"]); }

                //------------------------------------------------------
                I_SupersededItem = Convert.ToString(dr["SupersededItem"]);

                //Fix nulls for min stock level  -----------------------
                NullTest = Convert.ToString(dr["MinStockLevel"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_MinStockLevel = 0; }
                else
                { I_MinStockLevel = Convert.ToDecimal(dr["MinStockLevel"]); }

                NullTest = Convert.ToString(dr["ActiveDate"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ActiveDate = DftDate; }
                else
                { I_ActiveDate = Convert.ToDateTime(dr["ActiveDate"]); }

                NullTest = Convert.ToString(dr["ExpirationDate"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ExpirationDate = DftDate; }
                else
                { I_ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]); }

                NullTest = Convert.ToString(dr["ItemOwner"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ItemOwner = 0; }
                else
                { I_ItemOwner = Convert.ToInt32(dr["ItemOwner"]); }

                NullTest = Convert.ToString(dr["ItemWeight"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ItemWeight = 0; }
                else
                { I_ItemWeight = Convert.ToDecimal(dr["ItemWeight"]); }

                NullTest = Convert.ToString(dr["ItemLength"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ItemLength = 0; }
                else
                { I_ItemLength = Convert.ToDecimal(dr["ItemLength"]); }

                NullTest = Convert.ToString(dr["ItemWidth"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ItemWidth = 0; }
                else
                { I_ItemWidth = Convert.ToDecimal(dr["ItemWidth"]); }

                NullTest = Convert.ToString(dr["ItemHeight"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_ItemHeight = 0; }
                else
                { I_ItemHeight = Convert.ToDecimal(dr["ItemHeight"]); }

                I_OneBoxID = Convert.ToString(dr["OneBoxID"]);
                I_StarterItem = Convert.ToString(dr["StarterItem"]);

                NullTest = Convert.ToString(dr["StarterQuantity"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_StarterQuantity = 0; }
                else
                { I_StarterQuantity = Convert.ToDecimal(dr["StarterQuantity"]); }

                NullTest = Convert.ToString(dr["StarterExpireDate"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_StarterExpireDate = DftDate; }
                else
                { I_StarterExpireDate = Convert.ToDateTime(dr["StarterExpireDate"]); }

                NullTest = Convert.ToString(dr["MaximumOrderQuantity"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_MaxOrderQuantity = 0; }
                else
                { I_MaxOrderQuantity = Convert.ToDecimal(dr["MaximumOrderQuantity"]); }

                I_SubmittingUser = Convert.ToString(dr["SubmittingUser"]);
                I_UpdatingUser = Convert.ToString(dr["UserUpdated"]);

                NullTest = Convert.ToString(dr["SubmittedDate"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_SubmittedDate = DftDate; }
                else
                { I_SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]); }

                NullTest = Convert.ToString(dr["LastUpdated"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_UpdatedDate = DftDate; }
                else
                { I_UpdatedDate = Convert.ToDateTime(dr["LastUpdated"]); }

                I_Uploaded = Convert.ToString(dr["Uploaded"]);

                NullTest = Convert.ToString(dr["LastUploaded"]);
                if (NullTest == null || NullTest == string.Empty)
                { I_UploadedDate = DftDate; }
                else
                { I_UploadedDate = Convert.ToDateTime(dr["LastUploaded"]); }

                if (dr.Table.Columns["VendorName"] == null)
                { I_VendorName = ""; }
                else
                { I_VendorName = Convert.ToString(dr["VendorName"]); }

                if (dr.Table.Columns["OwnerName"] == null)
                { I_OwnerName = ""; }
                else
                { I_OwnerName = Convert.ToString(dr["OwnerName"]); }

                if (dr.Table.Columns["Email"] == null)
                { I_OwnerEmail = ""; }
                else
                { I_OwnerEmail = Convert.ToString(dr["Email"]); }

                if (dr.Table.Columns["ProductGroupDescription"] == null)
                { I_ProductGroupDesc = ""; }
                else
                { I_ProductGroupDesc = Convert.ToString(dr["ProductGroupDescription"]); }

                if (dr.Table.Columns["ProductSubGroupDescription"] == null)
                { I_ProductSubGroupDesc = ""; }
                else
                { I_ProductSubGroupDesc = Convert.ToString(dr["ProductSubGroupDescription"]); }

            }
            catch (Exception ex)
            {
                throw new System.Exception("Error while parsing Item Information", ex);
            }

        }

        // * -----------------------------------------------------------------------------------------
        //   Item Getters / Setters
        // * -----------------------------------------------------------------------------------------
        public int ItemUID { get { return I_ItemUID; } }
        public int ClientID { get { return I_ClientID; } }
        public string ItemNumber { get { return I_ItemNumber; } set { I_ItemNumber = value; } }
        public string ItemDeleted { get { return I_ItemDeleted; } set { I_ItemDeleted = value; } }
        public string ItemStatus { get { return I_ItemStatus; } set { I_ItemStatus = value; } }
        public string ItemDescription { get { return I_ItemDescription; } set { I_ItemDescription = value; } }
        public string ItemCostCtr { get { return I_ItemCostCtr; } set { I_ItemCostCtr = value; } }
        public string ProductGroup { get { return I_ProductGroup; } set { I_ProductGroup = value; } }
        public string ProductSubGroup { get { return I_ProductSubGroup; } set { I_ProductSubGroup = value; } }
        public string StockingUOM { get { return I_StockingUOM; } set { I_StockingUOM = value; } }
        public int QuantityUOM { get { return I_QuantityUOM; } set { I_QuantityUOM = value; } }
        public decimal LastCost { get { return I_LastCost; } set { I_LastCost = value; } }
        public decimal RetailPrice { get { return I_RetailPrice; } set { I_RetailPrice = value; } }
        public int PrimaryVendor { get { return I_PrimaryVendor; } set { I_PrimaryVendor = value; } }
        public string SupersededItem { get { return I_SupersededItem; } set { I_SupersededItem = value; } }
        public decimal MinStockLevel { get { return I_MinStockLevel; } set { I_MinStockLevel = value; } }
        public DateTime ActiveDate { get { return I_ActiveDate; } set { I_ActiveDate = value; } }
        public DateTime ExpirationDate { get { return I_ExpirationDate; } set { I_ExpirationDate = value; } }
        public int ItemOwner { get { return I_ItemOwner; } set { I_ItemOwner = value; } }
        public decimal ItemWeight { get { return I_ItemWeight; } set { I_ItemWeight = value; } }
        public decimal ItemLength { get { return I_ItemLength; } set { I_ItemLength = value; } }
        public decimal ItemWidth { get { return I_ItemWidth; } set { I_ItemWidth = value; } }
        public decimal ItemHeight { get { return I_ItemHeight; } set { I_ItemHeight = value; } }
        public string OneBoxID { get { return I_OneBoxID; } set { I_OneBoxID = value; } }
        public string StarterItem { get { return I_StarterItem; } set { I_StarterItem = value; } }
        public decimal StarterQuantity { get { return I_StarterQuantity; } set { I_StarterQuantity = value; } }
        public DateTime StarterExpireDate { get { return I_StarterExpireDate; } set { I_StarterExpireDate = value; } }
        public decimal MaxOrderQuantity { get { return I_MaxOrderQuantity; } set { I_MaxOrderQuantity = value; } }
        public string SubmittingUser { get { return I_SubmittingUser; } set { I_SubmittingUser = value; } }
        public string UpdatingUser { get { return I_UpdatingUser; } set { I_UpdatingUser = value; } }
        public DateTime SubmittedDate { get { return I_SubmittedDate; } set { I_SubmittedDate = value; } }
        public DateTime UpdatedDate { get { return I_UpdatedDate; } set { I_UpdatedDate = value; } }
        public string Uploaded { get { return I_Uploaded; } set { I_Uploaded = value; } }
        public DateTime UploadedDate { get { return I_UploadedDate; } set { I_UploadedDate = value; } }
        public string VendorName { get { return I_VendorName; } set { I_VendorName = value; } }
        public string OwnerName { get { return I_OwnerName; } set { I_OwnerName = value; } }
        public string OwnerEmail { get { return I_OwnerEmail; } set { I_OwnerEmail = value; } }
        public string ProductGroupDesc { get { return I_ProductGroupDesc; } set { I_ProductGroupDesc = value; } }
        public string ProductSubGroupDesc { get { return I_ProductSubGroupDesc; } set { I_ProductSubGroupDesc = value; } }

        // * -----------------------------------------------------------------------------------------
        //   Insert New Item
        // * -----------------------------------------------------------------------------------------
        public Boolean InsertItem(string itemnumber, string deleted, string status, string itemdescription,
                           string costctr, string productgroup, string productsubgroup,
                           string stockinguom, int quantityuom, decimal lastcost, decimal retailprice,
                           int primaryvendor, string supersededitem, decimal minstocklevel,
                           DateTime activedate, DateTime expiredate, int itemowner, decimal itemweight,
                           decimal itemlength, decimal itemwidth, decimal itemheight, string oneboxid,
                           string starteritem, decimal starterquantity, DateTime starterexpiredate,
                           decimal maxorderquantity, string submittinguser, string userid, string comments)
        {
            DataClass DC = new DataClass();
            int rc = 0;
            Boolean result = false;

            if (DC.ExistsItem(ChaseClientID, itemnumber) == false)
            {
                try
                {
                    rc = DC.NewItem(ChaseClientID, itemnumber, deleted, status, itemdescription, costctr, null, null,
                                   productgroup, productsubgroup, stockinguom, quantityuom, lastcost, retailprice,
                                   null, null, null, null, null, primaryvendor, supersededitem,
                                   minstocklevel, (minstocklevel * 100), activedate, expiredate, itemowner,
                                   null, null, null, itemweight, itemlength, itemwidth,
                                   itemheight, null, oneboxid, starteritem, starterquantity,
                                   starterexpiredate, maxorderquantity, null, null, null,
                                   null, null, null, null, null, null, null, null, null, null, null, null,
                                   submittinguser, userid, comments);
                    if (rc > 0)
                    { result = true; }
                }
                catch (Exception ex)
                {
                    throw new System.Exception("Error inserting item " + itemnumber, ex);
                }
            }
            else
            {
                throw new System.Exception("Item " + itemnumber + " already exists in table.");
            }
            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Update Existing Item
        // * -----------------------------------------------------------------------------------------
        public Boolean UpdateItem(string itemnumber, string deleted, string status, string itemdescription,
                           string costctr, string productgroup, string productsubgroup,
                           string stockinguom, int quantityuom, decimal lastcost, decimal retailprice,
                           int primaryvendor, string supersededitem, decimal minstocklevel,
                           DateTime activedate, DateTime expiredate, int itemowner, decimal itemweight,
                           decimal itemlength, decimal itemwidth, decimal itemheight, string oneboxid,
                           string starteritem, decimal starterquantity, DateTime starterexpiredate,
                           decimal maxorderquantity, string submittinguser, string userid, string comments)
        {
            DataClass DC = new DataClass();
            int rc = 0;
            Boolean result = false;

            if (DC.ExistsItem(ChaseClientID, itemnumber) == true)
            {
                try
                {
                    rc = DC.UpdItem(ChaseClientID, itemnumber, deleted, status, itemdescription, costctr, null, null,
                           productgroup, productsubgroup, stockinguom, quantityuom, lastcost, retailprice,
                           null, null, null, null, null, primaryvendor, supersededitem,
                           minstocklevel, (minstocklevel * 100), activedate, expiredate, itemowner,
                           null, null, null, itemweight, itemlength, itemwidth,
                           itemheight, null, oneboxid, starteritem, starterquantity,
                           starterexpiredate, maxorderquantity, null, null, null,
                           null, null, null, null, null, null, null, null, null, null, null, null,
                           submittinguser, userid, comments);
                    if (rc > 0)
                    { result = true; }
                }
                catch (Exception ex)
                {
                    throw new System.Exception("Error updating item " + itemnumber, ex);
                }
            }
            else
            {
                throw new System.Exception("Item " + itemnumber + " cannot be found in table.");
            }
            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Return Single Item
        // * -----------------------------------------------------------------------------------------
        public Boolean ReturnItem(string itemnumber)
        {
            DataClass DC = new DataClass();
            DataTable dt = new DataTable();
            Boolean result = false;

            if (DC.ExistsItem(ChaseClientID, itemnumber) == true)
            {
                try
                {
                    dt = DC.ReturnItem(ChaseClientID, itemnumber);
                    foreach (DataRow R in dt.Rows)
                    {
                        LoadItem(R);
                    }

                    if (dt.Rows.Count > 0)
                    { result = true; }
                }
                catch (Exception ex)
                {
                    throw new System.Exception("Error retrieving item " + itemnumber, ex);
                }
            }
            else
            {
                throw new System.Exception("Item " + itemnumber + " cannot be found in table.");
            }
            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Return Single Item with Extended Descriptions
        // * -----------------------------------------------------------------------------------------
        public Boolean ReturnItemX(string itemnumber)
        {
            DataClass DC = new DataClass();
            DataTable dt = new DataTable();
            Boolean result = false;

            if (DC.ExistsItem(ChaseClientID, itemnumber) == true)
            {
                try
                {
                    dt = DC.ReturnItemExt(ChaseClientID, itemnumber);
                    foreach (DataRow R in dt.Rows)
                    {
                        LoadItem(R);
                    }

                    if (dt.Rows.Count > 0)
                    { result = true; }
                }
                catch (Exception ex)
                {
                    throw new System.Exception("Error retrieving item " + itemnumber, ex);
                }
            }
            else
            {
                throw new System.Exception("Item " + itemnumber + " cannot be found in table.");
            }
            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Return Multiple Items
        // * -----------------------------------------------------------------------------------------
        public DataTable ReturnItems()
        {
            DataClass DC = new DataClass();
            DataTable dt = new DataTable();

            //  Define output data table
            DataTable dto = new DataTable();
            dto.Columns.Add("Item Number", typeof(string));
            dto.Columns.Add("Item Deleted", typeof(string));
            dto.Columns.Add("Item Status", typeof(string));
            dto.Columns.Add("Item Description", typeof(string));
            dto.Columns.Add("Item Cost Ctr", typeof(string));
            dto.Columns.Add("Product Group", typeof(string));
            dto.Columns.Add("Product SubGroup", typeof(string));
            dto.Columns.Add("Stocking UOM", typeof(string));
            dto.Columns.Add("Quantity UOM", typeof(int));
            dto.Columns.Add("Last Cost", typeof(decimal));
            dto.Columns.Add("Retail Price", typeof(decimal));
            dto.Columns.Add("Primary Vendor", typeof(int));
            dto.Columns.Add("Superseded Item", typeof(string));
            dto.Columns.Add("Min Stock Level", typeof(decimal));
            dto.Columns.Add("Active Date", typeof(DateTime));
            dto.Columns.Add("Expiration Date", typeof(DateTime));
            dto.Columns.Add("Item Owner", typeof(int));
            dto.Columns.Add("Item Weight", typeof(decimal));
            dto.Columns.Add("Item Length", typeof(decimal));
            dto.Columns.Add("Item Width", typeof(decimal));
            dto.Columns.Add("Item Height", typeof(decimal));
            dto.Columns.Add("One Box ID", typeof(string));
            dto.Columns.Add("Starter Item", typeof(string));
            dto.Columns.Add("Starter Quantity", typeof(decimal));
            dto.Columns.Add("Starter ExpireDate", typeof(DateTime));
            dto.Columns.Add("MaxOrder Quantity", typeof(decimal));
            dto.Columns.Add("Submitting User", typeof(string));
            dto.Columns.Add("Updating User", typeof(string));
            dto.Columns.Add("Updated Date", typeof(DateTime));
            dto.Columns.Add("Uploaded", typeof(string));
            dto.Columns.Add("Uploaded Date", typeof(DateTime));
            dto.Columns.Add("Submitted Date", typeof(DateTime));

            try
            {
                dt = DC.ReturnItemList(ChaseClientID);
                foreach (DataRow dr in dt.Rows)
                {
                    LoadItem(dr);
                    dto.Rows.Add(I_ItemNumber,
                                I_ItemDeleted,
                                I_ItemStatus,
                                I_ItemDescription,
                                I_ItemCostCtr,
                                I_ProductGroup,
                                I_ProductSubGroup,
                                I_StockingUOM,
                                I_QuantityUOM,
                                I_LastCost,
                                I_RetailPrice,
                                I_PrimaryVendor,
                                I_SupersededItem,
                                I_MinStockLevel,
                                I_ActiveDate,
                                I_ExpirationDate,
                                I_ItemOwner,
                                I_ItemWeight,
                                I_ItemLength,
                                I_ItemWidth,
                                I_ItemHeight,
                                I_OneBoxID,
                                I_StarterItem,
                                I_StarterQuantity,
                                I_StarterExpireDate,
                                I_MaxOrderQuantity,
                                I_SubmittingUser,
                                I_UpdatingUser,
                                I_UpdatedDate,
                                I_Uploaded,
                                I_UploadedDate,
                                I_SubmittedDate);
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error retrieving items", ex);
            }
            return dto;
        }

        // * -----------------------------------------------------------------------------------------
        // Business Rules Section                                                ---------------------
        // * -----------------------------------------------------------------------------------------
        //   Parse datarow values for Item
        // * -----------------------------------------------------------------------------------------
        public void LoadRule(DataRow dr)
        {
            String NullTest = String.Empty;
            DateTime DftDate = Convert.ToDateTime("1900-01-01");

            try
            {
                R_RuleUID = Convert.ToInt32(dr["ID"]);
                R_ClientID = Convert.ToInt32(dr["ClientID"]);    // Client ID cannot be null

                R_ItemNumber = Convert.ToString(dr["ItemNumber"]);
                R_RuleText = Convert.ToString(dr["RuleText"]);
                R_OrgFileName = Convert.ToString(dr["OrgFileName"]);
                R_EPIFileName = Convert.ToString(dr["EPIFileName"]);

                //Fix nulls for Rule Sequence  ---------------------
                NullTest = Convert.ToString(dr["RuleSequence"]);
                if (NullTest == null || NullTest == string.Empty)
                { R_RuleSequence = 0; }
                else
                { R_RuleSequence = Convert.ToInt32(dr["RuleSequence"]); }

                R_UpdateUser = Convert.ToString(dr["UpdUser"]);

                NullTest = Convert.ToString(dr["UpdDate"]);
                if (NullTest == null || NullTest == string.Empty)
                { R_UpdateDate = DftDate; }
                else
                { R_UpdateDate = Convert.ToDateTime(dr["UpdDate"]); }

            }
            catch (Exception ex)
            {
                throw new System.Exception("Error while parsing Rule Information", ex);
            }
        }

        // * -----------------------------------------------------------------------------------------
        //   Rule Getters / Setters
        // * -----------------------------------------------------------------------------------------
        public int RuleUID { get { return R_RuleUID; } }
        public int RuleClientID { get { return R_ClientID; } }
        public string RuleItemNumber { get { return R_ItemNumber; } set { R_ItemNumber = value; } }
        public string RuleText { get { return R_RuleText; } set { R_RuleText = value; } }
        public string RuleOrgFile { get { return R_OrgFileName; } set { R_OrgFileName = value; } }
        public string RuleEPIFile { get { return R_EPIFileName; } set { R_EPIFileName = value; } }
        public int RuleSequence { get { return R_RuleSequence; } set { R_RuleSequence = value; } }
        public string RuleUpdUser { get { return R_UpdateUser; } set { R_UpdateUser = value; } }
        public DateTime RuleUpdDate { get { return R_UpdateDate; } set { R_UpdateDate = value; } }

        // * -----------------------------------------------------------------------------------------
        //   Insert New Business Rule
        // * -----------------------------------------------------------------------------------------
        public Boolean InsertRule(int clientid, string itemnumber, string ruletext, string orgfile,
                                   string epifile, string userid)
        {
            DataClass DC = new DataClass();
            int rc = 0;
            Boolean result = false;

            try
            {
                rc = DC.NewBusinessRule(ChaseClientID, itemnumber, ruletext, orgfile, epifile, userid);
                if (rc > 0)
                { result = true; }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error inserting rule for " + itemnumber, ex);
            }
            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Update Existing Business Rule
        // * -----------------------------------------------------------------------------------------
        public Boolean UpdateRule(int clientid, string itemnumber, int ruleseq, string ruletext, string orgfile,
                                   string epifile, string userid)
        {
            DataClass DC = new DataClass();
            int rc = 0;
            Boolean result = false;

            try
            {
                rc = DC.UpdBusinessRule(ChaseClientID, itemnumber, ruleseq, ruletext, orgfile, epifile, userid);
                if (rc > 0)
                { result = true; }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error updating rule for " + itemnumber, ex);
            }
            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Return Single Business Rule
        // * -----------------------------------------------------------------------------------------
        public Boolean ReturnRules(int client, string itemnumber, int ruleseq)
        {
            DataClass DC = new DataClass();
            DataTable dt = new DataTable();
            Boolean result = false;

            try
            {
                dt = DC.GetBusinessRules(ChaseClientID, itemnumber, ruleseq);
                DataRow dr = dt.NewRow();
                LoadRule(dr);

                if (dt.Rows.Count > 0)
                { result = true; }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error retrieving business rule for " + itemnumber, ex);
            }

            return result;
        }

        // * -----------------------------------------------------------------------------------------
        //   Return Multiple Business Rules
        // * -----------------------------------------------------------------------------------------
        public DataTable ReturnRules(int client, string itemnumber)
        {
            DataClass DC = new DataClass();
            DataTable dt = new DataTable();

            //  Define output data table
            DataTable dto = new DataTable();
            dto.Columns.Add("Item Number", typeof(string));
            dto.Columns.Add("Rule Text", typeof(string));
            dto.Columns.Add("Original Attachment Name", typeof(string));
            dto.Columns.Add("EPI Attachment Name", typeof(string));
            dto.Columns.Add("Rule Sequence", typeof(int));
            dto.Columns.Add("Updating User", typeof(string));
            dto.Columns.Add("Updated Date", typeof(DateTime));

            try
            {
                dt = DC.GetBusinessRules(ChaseClientID, itemnumber);
                foreach (DataRow dr in dt.Rows)
                {
                    LoadRule(dr);
                    dto.Rows.Add(R_ItemNumber,
                                 R_RuleText,
                                 R_OrgFileName,
                                 R_EPIFileName,
                                 R_RuleSequence,
                                 R_UpdateUser,
                                 R_UpdateDate );
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error retrieving Rules for " + itemnumber, ex);
            }
            return dto;
        }




    }
}