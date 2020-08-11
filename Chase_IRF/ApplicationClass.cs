using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chase_IRF
{
    public class ApplicationClass
    {

        int ClientID = 1;

        //  Sync Product Groups  -------------------------------------------------------------------
        public void SyncProductGroups()
        {
            DB2DataClass DB2 = new DB2DataClass();
            DataClass DC = new DataClass();

            //  Check for global code and insert if not found
            Boolean codeFound = false;
            DataTable dg = new DataTable();
            dg = DC.GetGlobalCodes(1, "ProductGroup");
            foreach (DataRow dgr in dg.Rows)
            {
                codeFound = true;
            }
            if (codeFound == false)
            {
                DC.NewGlobalCode("A", 1, "ProductGroup", "Item Category", 9999);
            }

            //  Update client codes with product groups
            DataTable dt = new DataTable();
            dt = DB2.RtvProductGroups();
            foreach (DataRow dr in dt.Rows)
            {
                string pgprgr = dr["pgprgr"].ToString();
                string pgdesc = dr["pgdesc"].ToString();

                DC.NewClientCode("A", 1, "ProductGroup", 0, pgprgr, pgdesc);

            }
        }

        //  Sync Product Sub-Groups  ---------------------------------------------------------------
        public void SyncProductSubGroups()
        {
            DB2DataClass DB2 = new DB2DataClass();
            DataClass DC = new DataClass();

            //  Check for global code and insert if not found
            Boolean codeFound = false;
            DataTable dg = new DataTable();
            dg = DC.GetGlobalCodes(1, "ProductSubGroup");
            foreach (DataRow dgr in dg.Rows)
            {
                codeFound = true;
            }
            if (codeFound == false)
            {
                DC.NewGlobalCode("A", 1, "ProductSubGroup", "ePurchase Sub-Category", 9999);
            }

            //  Update client codes with product subgroups
            DataTable dt = new DataTable();
            dt = DB2.RtvProductSubGroups();

            foreach (DataRow dr in dt.Rows)
            {
                string sgprsg = dr["sgprsg"].ToString();
                string sgdesc = dr["sgdesc"].ToString();

                DC.NewClientCode("A", 1, "ProductSubGroup", 0, sgprsg, sgdesc);

            }
        }

        //  Sync Item Owners  ---------------------------------------------------------------
        public void SyncItemOwners()
        {
            DB2DataClass DB2 = new DB2DataClass();
            DataClass DC = new DataClass();

            DC.DltItemOwners(ClientID);

            DataTable dt = new DataTable();
            dt = DB2.RtvItemOwners();

            foreach (DataRow dr in dt.Rows)
            {
                string mlname = dr["mlname"].ToString();
                string mlemaila = dr["mlemaila"].ToString();
                string mlpost = dr["mlpost"].ToString();
                string mlcust = dr["mlcst#"].ToString();
                int mlcst = Convert.ToInt32(mlcust);

                DC.NewItemOwner(mlname, mlemaila, 1, mlcst);

            }
        }

        //  Sync Vendors  ---------------------------------------------------------------
        public void SyncVendors()
        {
            DB2DataClass DB2 = new DB2DataClass();
            DataClass DC = new DataClass();
            DataTable dt = new DataTable();
            dt = DB2.RtvVendors();

            foreach (DataRow dr in dt.Rows)
            {
                string opvendor = dr["AVVND#"].ToString();
                string vendorname = dr["AVNAME"].ToString();
                string contactname = dr["AVCONT"].ToString();
                string phone = dr["AVTEL1"].ToString();
                int avcst = Convert.ToInt32(opvendor);

                DC.NewVendor(vendorname, contactname, phone, avcst);

            }
        }

        //  Item Completion Notifications  ---------------------------------------------------------------
        public void ItemCompletionNotify()
        {
            DataClass DC = new DataClass();
            Items ip = new Items();

            DataTable dt = new DataTable();
            dt = DC.ReturnItemList(ClientID);
            foreach (DataRow dr in dt.Rows)
            {
                //  Parse Item Data from table row
                ip.LoadItem(dr); 

                //  Check to make sure item is not complete
                if (ip.ItemStatus != "C")
                {
                    //  If item has been updated since it was last uploaded then check it
                    if (ip.UpdatedDate >= ip.UploadedDate)
                    {
                        //  Determine the status that the IRF deserves

                        //  Are we OK for ePurchase?
                        if ((ip.ItemDescription != null && ip.ItemDescription != String.Empty) &&
                            (ip.ItemCostCtr != null && ip.ItemCostCtr != String.Empty) &&
                            (ip.ProductGroup != null && ip.ProductGroup != String.Empty) &&
                            (ip.ProductSubGroup != null && ip.ProductSubGroup != String.Empty) &&
                            (ip.StockingUOM != null && ip.StockingUOM != String.Empty) &&
                            (ip.QuantityUOM > 0) &&
                            (ip.LastCost > 0) &&
                            (ip.RetailPrice > 0) &&
                            (ip.PrimaryVendor > 0) &&
                            (ip.MinStockLevel > 0) &&
                            (ip.SupersededItem != null && ip.SupersededItem != String.Empty) &&
                            (ip.ActiveDate != null && ip.ActiveDate > DateTime.MinValue) &&
                            (ip.ExpirationDate != null && ip.ExpirationDate > DateTime.MinValue) &&
                            (ip.ItemOwner > 0) &&
                            (ip.ItemWeight > 0) &&
                            (ip.ItemLength > 0) &&
                            (ip.ItemWidth > 0) &&
                            (ip.ItemHeight > 0) &&
                            (ip.OneBoxID != null && ip.OneBoxID != String.Empty) &&
                            (ip.ItemHeight > 0))
                        {
                            if (ip.ItemStatus != "C")
                            { ip.ItemStatus = "C"; }
                        }

                        //  Are we OK for One Box?
                        else if ((ip.ItemDescription != null && ip.ItemDescription != String.Empty) &&
                            (ip.ItemCostCtr != null && ip.ItemCostCtr != String.Empty) &&
                            (ip.ProductGroup != null && ip.ProductGroup != String.Empty) &&
                            (ip.ProductSubGroup != null && ip.ProductSubGroup != String.Empty) &&
                            (ip.StockingUOM != null && ip.StockingUOM != String.Empty) &&
                            (ip.QuantityUOM > 0) &&
                            (ip.LastCost > 0) &&
                            (ip.RetailPrice > 0) &&
                            (ip.PrimaryVendor > 0) &&
                            (ip.MinStockLevel > 0) &&
                            (ip.SupersededItem != null && ip.SupersededItem != String.Empty) &&
                            (ip.ActiveDate != null && ip.ActiveDate > DateTime.MinValue) &&
                            (ip.ExpirationDate != null && ip.ExpirationDate > DateTime.MinValue) &&
                            (ip.ItemOwner > 0) &&
                            (ip.ItemWeight > 0) &&
                            (ip.ItemLength > 0) &&
                            (ip.ItemWidth > 0) &&
                            (ip.ItemHeight > 0) &&
                            (ip.OneBoxID != null && ip.OneBoxID != String.Empty) &&
                            (ip.ItemHeight > 0))
                        {
                            if (ip.ItemStatus != "O")
                            { ip.ItemStatus = "O"; }
                        }

                        //  Are we OK for Po Receipt?
                        else if ((ip.ItemDescription != null && ip.ItemDescription != String.Empty) &&
                            (ip.ItemCostCtr != null && ip.ItemCostCtr != String.Empty) &&
                            (ip.ProductGroup != null && ip.ProductGroup != String.Empty) &&
                            (ip.ProductSubGroup != null && ip.ProductSubGroup != String.Empty) &&
                            (ip.StockingUOM != null && ip.StockingUOM != String.Empty) &&
                            (ip.QuantityUOM > 0) &&
                            (ip.LastCost > 0) &&
                            (ip.RetailPrice > 0) &&
                            (ip.PrimaryVendor > 0) &&
                            (ip.MinStockLevel > 0) &&
                            (ip.SupersededItem != null && ip.SupersededItem != String.Empty) &&
                            (ip.ActiveDate != null && ip.ActiveDate > DateTime.MinValue) &&
                            (ip.ExpirationDate != null && ip.ExpirationDate > DateTime.MinValue) &&
                            (ip.ItemOwner > 0) &&
                            (ip.ItemWeight > 0) &&
                            (ip.ItemLength > 0) &&
                            (ip.ItemWidth > 0) &&
                            (ip.ItemHeight > 0) &&
                            (ip.OneBoxID != null && ip.OneBoxID != String.Empty) &&
                            (ip.ItemHeight > 0))
                        {
                            if (ip.ItemStatus != "R")
                            { ip.ItemStatus = "R"; }
                        }

                        //  We are not OK for anything
                        else
                        {
                            { ip.ItemStatus = "N"; }
                        }

                    }

                    ip.UpdateItem(ip.ItemNumber, ip.ItemDeleted, ip.ItemStatus, ip.ItemDescription,
                           ip.ItemCostCtr, ip.ProductGroup, ip.ProductGroup,
                           ip.StockingUOM, ip.QuantityUOM, ip.LastCost, ip.RetailPrice,
                           ip.PrimaryVendor, ip.SupersededItem, ip.MinStockLevel,
                           ip.ActiveDate, ip.ExpirationDate, ip.ItemOwner, ip.ItemWeight,
                           ip.ItemLength, ip.ItemWidth, ip.ItemHeight, ip.OneBoxID,
                           ip.StarterItem, ip.StarterQuantity, ip.StarterExpireDate,
                           ip.MaxOrderQuantity, ip.SubmittingUser, ip.UpdatingUser, "System");
                }

            }
        }







    }
}
