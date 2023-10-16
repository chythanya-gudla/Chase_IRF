using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Web.UI.HtmlControls;
using SpritzDotNet;

namespace Chase_IRF
{
    public class DataClass
    {

        //  Login Procedure  ------------------------------------------------------------
        public DataTable Login(string UserID, string Password, string IPAddress)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqllogin = new SqlConnection(connString);

            SqlCommand sqlcmd = sqllogin.CreateCommand();

            sqlcmd.CommandText = "spLogin";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", UserID);
            sqlcmd.Parameters.AddWithValue("@Password", Password);
            sqlcmd.Parameters.AddWithValue("@IPAddress", IPAddress);

            //var returnParameter = sqlcmd.Parameters.Add("@ReturnVal", SqlDbType.VarChar);
            //returnParameter.Direction = ParameterDirection.ReturnValue;

            sqllogin.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqllogin.Close();

            return dt;
        }

        //  Administration Login  -------------------------------------------------------------
        public DataTable AdminLogin(string UserID, string Password, string IPAddress)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqllogin = new SqlConnection(connString);

            SqlCommand sqlcmd = sqllogin.CreateCommand();

            sqlcmd.CommandText = "spAdminLogin";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", UserID);
            sqlcmd.Parameters.AddWithValue("@Password", Password);
            sqlcmd.Parameters.AddWithValue("@IPAddress", IPAddress);

            sqllogin.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqllogin.Close();

            return dt;
        }
        public DataTable GetItems(string ItemNumber)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqllogin = new SqlConnection(connString);

            SqlCommand sqlcmd = sqllogin.CreateCommand();

            sqlcmd.CommandText = "spGetItems";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ItemNumber", ItemNumber);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            sqllogin.Open();
            DataTable dt = new DataTable();
            sqlcmd.ExecuteNonQuery();
            da.Fill(dt);
            //dt.Load(sqlcmd.ExecuteReader());

            sqllogin.Close();

            return dt;
        }

        public DataTable getddlsubcategory()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqllogin = new SqlConnection(connString);
            SqlCommand sqlcmd = sqllogin.CreateCommand();
            sqlcmd.CommandText = "spGetProductSubGroup";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            sqllogin.Open();
            DataTable dt = new DataTable();
            sqlcmd.ExecuteNonQuery();
            da.Fill(dt);
            sqllogin.Close();
            return dt;
        }
        public DataTable getddl()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqllogin = new SqlConnection(connString);
            SqlCommand sqlcmd = sqllogin.CreateCommand();
            sqlcmd.CommandText = "spGetProductGroup";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            sqllogin.Open();
            DataTable dt = new DataTable();
            sqlcmd.ExecuteNonQuery();
            da.Fill(dt);
            sqllogin.Close();
            return dt;
        }

        // Create new item  -------------------------------------------------------------------
        public int NewItem(string status, string itemnumber, string itemdescription, string costctr,
                           string glclass, string taxclass, string productgroup, string productsubgroup,
                           string stockinguom, int quantityuom, decimal lastcost, decimal retailprice,
                           string dropship, string phaseout, string stockitem,
                           string manufactured, int primaryvendor, string supersededitem, decimal minstocklevel,
                           decimal maxstocklevel, string activedate, string expiredate, int itemowner,
                           string printondemand, int printondemandvendor, string webviewable, decimal itemweight,
                           decimal itemlength, decimal itemwidth, decimal itemheight, decimal prefpackquantity,
                           string oneboxid, string starteritem, decimal starterquantity, string starterexpiredate,
                           decimal maxorderquantity, string printrequesttype, string printjts, string printcostcenter,
                           string printbuscase, DateTime artreleasedate, string creativeagency, decimal printquantity,
                           int printversions, int printpages, int printcolors, string paperstock, int printsides,
                           decimal flatlength, decimal flatwidth, string uniquealgorythm, string submittinguser,
                           string userid, string comments,string itemtype, string expectedarrival, decimal expectedquantity,string ViewOnly)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertItem";
            sqlcmd.CommandType = CommandType.StoredProcedure;



            sqlcmd.Parameters.AddWithValue("@ClientID", "1");
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);
            sqlcmd.Parameters.AddWithValue("@Deleted", "");
            sqlcmd.Parameters.AddWithValue("@Status", status);
            sqlcmd.Parameters.AddWithValue("@ItemDescription", itemdescription);
            sqlcmd.Parameters.AddWithValue("@ItemCostCtr", costctr);
            sqlcmd.Parameters.AddWithValue("@GL_Class", glclass);
            sqlcmd.Parameters.AddWithValue("@Tax_Class", taxclass);
            sqlcmd.Parameters.AddWithValue("@ProductGroup", productgroup);
            sqlcmd.Parameters.AddWithValue("@ProductSubGroup", productsubgroup);
            sqlcmd.Parameters.AddWithValue("@StockingUOM", stockinguom);
            sqlcmd.Parameters.AddWithValue("@QuantityUOM", quantityuom);
            sqlcmd.Parameters.AddWithValue("@LastCost", lastcost);
            sqlcmd.Parameters.AddWithValue("@RetailPrice", retailprice);
            sqlcmd.Parameters.AddWithValue("@DropShip", dropship);
            sqlcmd.Parameters.AddWithValue("@PhaseOut", paperstock);
            sqlcmd.Parameters.AddWithValue("@StockItem", stockitem);
            //sqlcmd.Parameters.AddWithValue("@Salable", salable);
            sqlcmd.Parameters.AddWithValue("@Manufactured", manufactured);
            sqlcmd.Parameters.AddWithValue("@PrimaryVendor", primaryvendor);
            sqlcmd.Parameters.AddWithValue("@SupersededItem", supersededitem);
            sqlcmd.Parameters.AddWithValue("@MinStockLevel", minstocklevel);
            sqlcmd.Parameters.AddWithValue("@MaxStockLevel", maxstocklevel);
            sqlcmd.Parameters.AddWithValue("@ActiveDate", activedate);
            sqlcmd.Parameters.AddWithValue("@ExpirationDate", expiredate);
            sqlcmd.Parameters.AddWithValue("@ItemOwner", itemowner);
            sqlcmd.Parameters.AddWithValue("@PrintOnDemandItem", printondemand);
            sqlcmd.Parameters.AddWithValue("@PrintOnDemandVendor", printondemandvendor);
            sqlcmd.Parameters.AddWithValue("@WebViewable", webviewable);
            sqlcmd.Parameters.AddWithValue("@ItemWeight", itemweight);
            sqlcmd.Parameters.AddWithValue("@ItemLength", itemlength);
            sqlcmd.Parameters.AddWithValue("@ItemWidth", itemwidth);
            sqlcmd.Parameters.AddWithValue("@ItemHeight", itemheight);
            sqlcmd.Parameters.AddWithValue("@PreferredPackQuantity", prefpackquantity);
            sqlcmd.Parameters.AddWithValue("@OneBoxID", oneboxid);
            sqlcmd.Parameters.AddWithValue("@StarterItem", starteritem);
            sqlcmd.Parameters.AddWithValue("@StarterQuantity", starterquantity);
            sqlcmd.Parameters.AddWithValue("@StarterExpireDate", starterexpiredate);
            sqlcmd.Parameters.AddWithValue("@MaximumOrderQuantity", maxorderquantity);
            sqlcmd.Parameters.AddWithValue("@PrintRequestType", printrequesttype);
            sqlcmd.Parameters.AddWithValue("@PrintJTS", printjts);
            sqlcmd.Parameters.AddWithValue("@PrintCostCenter", printcostcenter);
            sqlcmd.Parameters.AddWithValue("@PrintBusinessCase", printbuscase);
            sqlcmd.Parameters.AddWithValue("@ArtworkReleaseDate", artreleasedate);
            sqlcmd.Parameters.AddWithValue("@CreativeAgency", creativeagency);
            sqlcmd.Parameters.AddWithValue("@PrintQuantity", printquantity);
            sqlcmd.Parameters.AddWithValue("@PrintVersions", printversions);
            sqlcmd.Parameters.AddWithValue("@PrintPages", printpages);
            sqlcmd.Parameters.AddWithValue("@PrintColors", printcolors);
            sqlcmd.Parameters.AddWithValue("@PaperStock", paperstock);
            sqlcmd.Parameters.AddWithValue("@PrintSides", printsides);
            sqlcmd.Parameters.AddWithValue("@FlatSizeLength", flatlength);
            sqlcmd.Parameters.AddWithValue("@FlatSizeWidth", flatwidth);
            sqlcmd.Parameters.AddWithValue("@UniqueAlgorythmCodes", uniquealgorythm);
            sqlcmd.Parameters.AddWithValue("@SubmittingUser", submittinguser);
            sqlcmd.Parameters.AddWithValue("@UserId", userid);
            sqlcmd.Parameters.AddWithValue("@Salable", ViewOnly);

            sqlcmd.Parameters.AddWithValue("@Comments", comments);

            sqlcmd.Parameters.AddWithValue("@ItemType", itemtype);
            sqlcmd.Parameters.AddWithValue("@ExpectedArrival", expectedarrival);
            sqlcmd.Parameters.AddWithValue("@ExpectedQuantity", expectedquantity);
            var returnParameter = sqlcmd.Parameters.Add("@SeedMaster", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;


            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        internal int NewItem(object p1, HtmlInputGenericControl itemnumber, HtmlInputText itemdescription, string costctr, object p2, object p3, object p4, object p5, string stockinguom, int quantityuom, string lastcost, string retailprice, object p6, object p7, object p8, object p9, object p10, HtmlInputText primaryvendor, string supersedeitem, object p11, object p12, string activedate, HtmlInputGenericControl expiredate, string itemowner, object p13, object p14, object p15, int itemwght, int dimensions1, int dimensions2, int dimensions3, object p16, DateTime oneboxmnthyr, object p17, string starterboxquntity, string starterexpiredate, object p18, object p19, object p20, object p21, object p22, object p23, object p24, object p25, object p26, object p27, object p28, object p29, object p30, object p31, object p32, object p33, string submittinguser, IPrincipal user, object comments)
        {
            throw new NotImplementedException();
        }

        // Update Existing Item  ------------------------------------------------------------
        public int UpdItem(string status, string itemnumber, string itemdescription, string costctr,
                           string glclass, string taxclass, string productgroup, string productsubgroup,
                           string stockinguom, int quantityuom, decimal lastcost, decimal retailprice,
                           string dropship, string phaseout, string stockitem, 
                           string manufactured, int primaryvendor, string supersededitem, decimal minstocklevel,
                           decimal maxstocklevel, string activedate, string expiredate, int itemowner,
                           string printondemand, int printondemandvendor, string webviewable, decimal itemweight,
                           decimal itemlength, decimal itemwidth, decimal itemheight, decimal prefpackquantity,
                           string oneboxid, string starteritem, decimal starterquantity, string starterexpiredate,
                           decimal maxorderquantity, string printrequesttype, string printjts, string printcostcenter,
                           string printbuscase, DateTime artreleasedate, string creativeagency, decimal printquantity,
                           int printversions, int printpages, int printcolors, string paperstock, int printsides,
                           decimal flatlength, decimal flatwidth, string uniquealgorythm, string submittinguser,
                           string expectedarrival, decimal expectedquantity, string userid, string comments, string deleted,string ViewOnly )
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditItem";
            sqlcmd.CommandType = CommandType.StoredProcedure;

            sqlcmd.Parameters.AddWithValue("@ClientID", "1");
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);
            sqlcmd.Parameters.AddWithValue("@Deleted", deleted);
            sqlcmd.Parameters.AddWithValue("@Status", status);
            sqlcmd.Parameters.AddWithValue("@ItemDescription", itemdescription);
            sqlcmd.Parameters.AddWithValue("@ItemCostCtr", costctr);
            sqlcmd.Parameters.AddWithValue("@GL_Class", glclass);
            sqlcmd.Parameters.AddWithValue("@Tax_Class", taxclass);
            sqlcmd.Parameters.AddWithValue("@ProductGroup", productgroup);
            sqlcmd.Parameters.AddWithValue("@ProductSubGroup", productsubgroup);
            sqlcmd.Parameters.AddWithValue("@StockingUOM", stockinguom);
            sqlcmd.Parameters.AddWithValue("@QuantityUOM", quantityuom);
            sqlcmd.Parameters.AddWithValue("@LastCost", lastcost);
            sqlcmd.Parameters.AddWithValue("@RetailPrice", retailprice);
            sqlcmd.Parameters.AddWithValue("@DropShip", dropship);
            sqlcmd.Parameters.AddWithValue("@PhaseOut", paperstock);
            sqlcmd.Parameters.AddWithValue("@StockItem", stockitem);
            //sqlcmd.Parameters.AddWithValue("@Salable", salable);
            sqlcmd.Parameters.AddWithValue("@Manufactured", manufactured);
            sqlcmd.Parameters.AddWithValue("@PrimaryVendor", primaryvendor);
            sqlcmd.Parameters.AddWithValue("@SupersededItem", supersededitem);
            sqlcmd.Parameters.AddWithValue("@MinStockLevel", minstocklevel);
            sqlcmd.Parameters.AddWithValue("@MaxStockLevel", maxstocklevel);
            sqlcmd.Parameters.AddWithValue("@ActiveDate", activedate);
            sqlcmd.Parameters.AddWithValue("@ExpirationDate", expiredate);
            sqlcmd.Parameters.AddWithValue("@ItemOwner", itemowner);
            sqlcmd.Parameters.AddWithValue("@PrintOnDemandItem", printondemand);
            sqlcmd.Parameters.AddWithValue("@PrintOnDemandVendor", printondemandvendor);
            sqlcmd.Parameters.AddWithValue("@WebViewable", webviewable);
            sqlcmd.Parameters.AddWithValue("@ItemWeight", itemweight);
            sqlcmd.Parameters.AddWithValue("@ItemLength", itemlength);
            sqlcmd.Parameters.AddWithValue("@ItemWidth", itemwidth);
            sqlcmd.Parameters.AddWithValue("@ItemHeight", itemheight);
            sqlcmd.Parameters.AddWithValue("@PreferredPackQuantity", prefpackquantity);
            sqlcmd.Parameters.AddWithValue("@OneBoxID", oneboxid);
            sqlcmd.Parameters.AddWithValue("@StarterItem", starteritem);
            sqlcmd.Parameters.AddWithValue("@StarterQuantity", starterquantity);
            sqlcmd.Parameters.AddWithValue("@StarterExpireDate", starterexpiredate);
            sqlcmd.Parameters.AddWithValue("@MaximumOrderQuantity", maxorderquantity);
            sqlcmd.Parameters.AddWithValue("@PrintRequestType", printrequesttype);
            sqlcmd.Parameters.AddWithValue("@PrintJTS", printjts);
            sqlcmd.Parameters.AddWithValue("@PrintCostCenter", printcostcenter);
            sqlcmd.Parameters.AddWithValue("@PrintBusinessCase", printbuscase);
            sqlcmd.Parameters.AddWithValue("@ArtworkReleaseDate", artreleasedate);
            sqlcmd.Parameters.AddWithValue("@CreativeAgency", creativeagency);
            sqlcmd.Parameters.AddWithValue("@PrintQuantity", printquantity);
            sqlcmd.Parameters.AddWithValue("@PrintVersions", printversions);
            sqlcmd.Parameters.AddWithValue("@PrintPages", printpages);
            sqlcmd.Parameters.AddWithValue("@PrintColors", printcolors);
            sqlcmd.Parameters.AddWithValue("@PaperStock", paperstock);
            sqlcmd.Parameters.AddWithValue("@PrintSides", printsides);
            sqlcmd.Parameters.AddWithValue("@FlatSizeLength", flatlength);
            sqlcmd.Parameters.AddWithValue("@FlatSizeWidth", flatwidth);
            sqlcmd.Parameters.AddWithValue("@UniqueAlgorythmCodes", uniquealgorythm);
            sqlcmd.Parameters.AddWithValue("@SubmittingUser", submittinguser);
            sqlcmd.Parameters.AddWithValue("@ExpectedArrival", expectedarrival);
            sqlcmd.Parameters.AddWithValue("@ExpectedQuantity", expectedquantity);
            sqlcmd.Parameters.AddWithValue("@UserId", userid);
            sqlcmd.Parameters.AddWithValue("@Salable", ViewOnly);

            sqlcmd.Parameters.AddWithValue("@Comments", comments);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;


            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return Item  ---------------------------------------------------------------------------
       // public DataTable ReturnItem(string item)
             public DataTable ReturnItem(string item)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();

            sqlcmd.CommandText = "spReturnItem";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", 1);
            sqlcmd.Parameters.AddWithValue("@ItemNumber", item);
            

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();

            return dt;
        }

        //  Return Item List ----------------------------------------------------------------------
        //public DataTable ReturnItemList()
        //{
        //    string encryptionkey = "ChaseIRFKey";
        //    string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        //    var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
        //    SqlConnection con = new SqlConnection(connString);

        //    SqlCommand sqlcmd = con.CreateCommand();

        //    sqlcmd.CommandText = "spReturnItemList";
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    sqlcmd.Parameters.AddWithValue("@ItemOwner", 0);
        //    sqlcmd.Parameters.AddWithValue("@OneBoxID", "");

        //    con.Open();
        //    DataTable dt = new DataTable();
        //    dt.Load(sqlcmd.ExecuteReader());

        //    con.Close();

        //    return dt;
        //}

        //  Return Item List ----------------------------------------------------------------------
        //public DataTable ReturnItemList(int itemowner)
        //{
        //    string encryptionkey = "ChaseIRFKey";
        //    string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        //    var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
        //    SqlConnection con = new SqlConnection(connString);

        //    SqlCommand sqlcmd = con.CreateCommand();

        //    //sqlcmd.CommandText = "spReturnItemList";
        //    sqlcmd.CommandText = "spReturnItem";
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    //sqlcmd.Parameters.AddWithValue("@ItemOwner", itemowner);
        //    //sqlcmd.Parameters.AddWithValue("@OneBoxID", "");
        //    sqlcmd.Parameters.AddWithValue("@ClientID", itemowner);
        //    sqlcmd.Parameters.AddWithValue("@ItemNumber", "");

        //    con.Open();
        //    DataTable dt = new DataTable();
        //    dt.Load(sqlcmd.ExecuteReader());

        //    con.Close();


        //    return dt;
        //}
        public DataTable ReturnItemListPage(int itemowner, string startitem)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();

            sqlcmd.CommandText = "spReturnItemsPageDown";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", 1);
            sqlcmd.Parameters.AddWithValue("@UserID", itemowner);
            //sqlcmd.Parameters.AddWithValue("@LastRowNumber", lastrow);
            sqlcmd.Parameters.AddWithValue("@StartItem", startitem);
            //sqlcmd.Parameters.AddWithValue("@Contains", "");

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();


            return dt;
        }

        public DataTable ReturnItemListPage(int itemowner, int lastrow, string direction, string contains)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();

            sqlcmd.CommandText = "spReturnItemsPageDownXX";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", 1);
            sqlcmd.Parameters.AddWithValue("@UserID", itemowner);
            sqlcmd.Parameters.AddWithValue("@LastRowNumber", lastrow);
            sqlcmd.Parameters.AddWithValue("@Direction", direction);
            sqlcmd.Parameters.AddWithValue("@Contains", contains);

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();


            return dt;
        }

        public DataTable ReturnItemList(int itemowner)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();

            sqlcmd.CommandText = "spReturnItems";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", 1);
            sqlcmd.Parameters.AddWithValue("@UserID", itemowner);
            sqlcmd.Parameters.AddWithValue("@Contains","");

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();

            return dt;
        }

        public DataTable ReturnItemList(int itemowner, string contains)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();
;
            sqlcmd.CommandText = "spReturnItems";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", 1);
            sqlcmd.Parameters.AddWithValue("@UserID", itemowner);
            sqlcmd.Parameters.AddWithValue("@Contains", contains);

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();

            return dt;
        }

        public DataTable ReturnItemOwner(int itemOwenerId)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();

            sqlcmd.CommandText = "spReturnItemOwner";
            sqlcmd.CommandType = CommandType.StoredProcedure;
          //  sqlcmd.Parameters.AddWithValue("@ID", itemOwenerId);
            sqlcmd.Parameters.AddWithValue("@OPCustomer", itemOwenerId);

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();

            return dt;
        }


        //  Return Item List ----------------------------------------------------------------------
        public DataTable ReturnItemList(string oneboxid)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection con = new SqlConnection(connString);

            SqlCommand sqlcmd = con.CreateCommand();

            sqlcmd.CommandText = "spReturnItemList";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ItemOwner", 0);
            sqlcmd.Parameters.AddWithValue("@OneBoxID", oneboxid);

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            con.Close();

            return dt;
        }

        //  Return Item List ----------------------------------------------------------------------
        //public DataTable ReturnItemList(int itemowner, string oneboxid)
        //{
        //    string encryptionkey = "ChaseIRFKey";
        //    string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        //    var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
        //    SqlConnection con = new SqlConnection(connString);

        //    SqlCommand sqlcmd = con.CreateCommand();

        //    sqlcmd.CommandText = "spReturnItemList";
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    sqlcmd.Parameters.AddWithValue("@ItemOwner", itemowner);
        //    sqlcmd.Parameters.AddWithValue("@OneBoxID", oneboxid);

        //    con.Open();
        //    DataTable dt = new DataTable();
        //    dt.Load(sqlcmd.ExecuteReader());

        //    con.Close();

        //    return dt;
        //}

        // Delete Existing Item  -------------------------------------------------------------
        public int DeleteItem(string itemnumber, string userid, string comments)

        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spDeleteItem";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);
            sqlcmd.Parameters.AddWithValue("@UserId", userid);
            sqlcmd.Parameters.AddWithValue("@Comments", comments);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Get user password  ----------------------------------------------------------------
        public DataTable GetUserPassword(string UserID)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spReturnUserPswd";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", UserID);

            sqlconn.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlconn.Close();

            return dt;
        }

        //  Get Client Record  -------------------------------------------------------------------
        public string GetClient(int clientidint)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spGetClients";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", clientidint);

            sqlconn.Open();
            
            string clientname = sqlcmd.ExecuteScalar().ToString();

            sqlconn.Close();

            return clientname;

        }

        //  Insert a new user  -------------------------------------------------------------------
        public int NewUser(int clientID, string userid, string password, string firstname, string lastname, 
                           string email, string Phone, string manager, string title, string AltUserId, bool IsActive, 
                           bool IsAdmin, int ItemOwnerNumber, string upduserid, string comments)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertUser";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", userid);
            sqlcmd.Parameters.AddWithValue("@UserPassword", password);
            sqlcmd.Parameters.AddWithValue("@FirstName", firstname);
            sqlcmd.Parameters.AddWithValue("@LastName", lastname);
            sqlcmd.Parameters.AddWithValue("@Email", email); 
            sqlcmd.Parameters.AddWithValue("@Phone", Phone);
            sqlcmd.Parameters.AddWithValue("@ClientID", clientID);
            sqlcmd.Parameters.AddWithValue("@IsActive", IsActive);
            sqlcmd.Parameters.AddWithValue("@IsAdmin", IsAdmin);
            sqlcmd.Parameters.AddWithValue("@ItemOwnerNumber", ItemOwnerNumber);
            sqlcmd.Parameters.AddWithValue("@Manager", manager);
            sqlcmd.Parameters.AddWithValue("@Title", title);
            sqlcmd.Parameters.AddWithValue("@AltContactUserID", AltUserId);
            sqlcmd.Parameters.AddWithValue("@UpdUserId", upduserid);
            sqlcmd.Parameters.AddWithValue("@Comments", comments);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Existing User  -------------------------------------------------------------------
        public int UpdUser(int ID, string userID, string password, string firstname, string lastname, 
                           string email, int clientID, bool IsActive, bool IsAdmin,
                           string manager, string title, string AltUserId, string upduserid, string comments)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditUser";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ID", ID);
            sqlcmd.Parameters.AddWithValue("@UserID", userID);
            sqlcmd.Parameters.AddWithValue("@UserPassword", password);
            sqlcmd.Parameters.AddWithValue("@FirstName", firstname);
            sqlcmd.Parameters.AddWithValue("@LastName", lastname);
            sqlcmd.Parameters.AddWithValue("@Email", email);
            sqlcmd.Parameters.AddWithValue("@ClientID", clientID);
            sqlcmd.Parameters.AddWithValue("@IsActive", IsActive);
            sqlcmd.Parameters.AddWithValue("@IsAdmin", IsAdmin);
            sqlcmd.Parameters.AddWithValue("@Manager", manager);
            sqlcmd.Parameters.AddWithValue("@Title", title);
            sqlcmd.Parameters.AddWithValue("@AltContactUserID", AltUserId);
            sqlcmd.Parameters.AddWithValue("@UpdUserId", upduserid);
            sqlcmd.Parameters.AddWithValue("@Comments", comments);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Existing User  -------------------------------------------------------------------
        public int UpdUserPassword(string userID, string oldpassword, string newpassword)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditUserPassword";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", userID);
            sqlcmd.Parameters.AddWithValue("@OldPassword", oldpassword);
            sqlcmd.Parameters.AddWithValue("@NewPassword", newpassword);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        public int ResetUserPassword(string userID, string newpassword)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spResetUserPassword";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", userID);
           // sqlcmd.Parameters.AddWithValue("@OldPassword", oldpassword);
            sqlcmd.Parameters.AddWithValue("@NewPassword", newpassword);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Get user details with EMail  ---------------------------------------------------------------
        public DataTable GetUserDetailsbyEMail(int id, string email)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnUserEMail";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", id);
            sqlcmd.Parameters.AddWithValue("@UserEMail", email);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Get user details with UserID  ---------------------------------------------------------------
        public DataTable GetUserDetailsbyUserID(int id, string userid)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnUserID";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", id);
            sqlcmd.Parameters.AddWithValue("@UserId", userid);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Get user details  ----------------------------------------------------------------------
        public DataTable GetUserDetails(int id)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnUser";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ID", id);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Get users details  ----------------------------------------------------------------------
        public DataTable GetUsers(int id, string status)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnUsers";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", id);
            sqlcmd.Parameters.AddWithValue("@Status", status);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Delete a user  --------------------------------------------------------------------------
        public int DeleteUser(int id, string userid, string comments)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spDeleteUser";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ID", id);
            sqlcmd.Parameters.AddWithValue("@UserId", userid);
            sqlcmd.Parameters.AddWithValue("@Comments", comments);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Insert a Global Code  -------------------------------------------------------------------
        public int NewGlobalCode(string status, string code, string codedescription, int maxsequence)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertGlobalCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Status", status);
            sqlcmd.Parameters.AddWithValue("@Code", code);
            sqlcmd.Parameters.AddWithValue("@CodeDescription", codedescription);
            sqlcmd.Parameters.AddWithValue("@MaxSequence", maxsequence);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Global Code  -------------------------------------------------------------------
        public int UpdGlobalCode(string status, string code, string codedescription, int maxsequence)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditGlobalCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Status", status);
            sqlcmd.Parameters.AddWithValue("@Code", code);
            sqlcmd.Parameters.AddWithValue("@CodeDescription", codedescription);
            sqlcmd.Parameters.AddWithValue("@MaxSequence", maxsequence);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return Global Code  --------------------------------------------------------------
        public DataTable GetGlobalCode(string code)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnGlobalCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Code", code);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Delete Global Code  -------------------------------------------------------------------
        public int DeleteGlobalCode(string code)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spDeleteGlobalCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Code", code);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Insert a Client Code  -------------------------------------------------------------------
        public int NewClientCode(string status, int client, string code, int sequence, 
                                 string codevalue, string displaytext)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertClientCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Status", status);
            sqlcmd.Parameters.AddWithValue("@ClientID", client);
            sqlcmd.Parameters.AddWithValue("@Code", code);
            sqlcmd.Parameters.AddWithValue("@Sequence", sequence);
            sqlcmd.Parameters.AddWithValue("@CodeValue", codevalue);
            sqlcmd.Parameters.AddWithValue("@DisplayText", displaytext);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Global Code  -------------------------------------------------------------------
        public int UpdClientCode(string status, int client, string code, int sequence, 
                                 string codevalue, string displaytext)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditClientCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@Status", status);
            sqlcmd.Parameters.AddWithValue("@ClientID", client);
            sqlcmd.Parameters.AddWithValue("@Code", code);
            sqlcmd.Parameters.AddWithValue("@Sequence", sequence);
            sqlcmd.Parameters.AddWithValue("@CodeValue", codevalue);
            sqlcmd.Parameters.AddWithValue("@DisplayText", displaytext);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return Client Code  --------------------------------------------------------------
        public DataTable GetClientCode(int client, string code, int sequence)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnClientCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", client);
            sqlcmd.Parameters.AddWithValue("@Code", code);
            sqlcmd.Parameters.AddWithValue("@Sequence", sequence);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Return All Client Code Sequences  ------------------------------------------------
        public DataTable GetClientCodes(int client, string code)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnClientCodes";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", client);
            sqlcmd.Parameters.AddWithValue("@Code", code);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Delete Global Code  -------------------------------------------------------------------
        public int DeleteClientCode(int client, string code, int sequence)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spDeleteClientCode";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", client);
            sqlcmd.Parameters.AddWithValue("@Code", code);
            sqlcmd.Parameters.AddWithValue("@Sequence", sequence);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Insert an Item Owner  -------------------------------------------------------------------
        public int NewItemOwner(string ownername, string email, int clientid,  string manager,
                                 string title, int opcustomer)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertItemOwner";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@OwnerName", ownername);
            sqlcmd.Parameters.AddWithValue("@Email", email);
            sqlcmd.Parameters.AddWithValue("@ClientID", clientid);
            sqlcmd.Parameters.AddWithValue("@Manager", manager);
            sqlcmd.Parameters.AddWithValue("@Title", title);
            sqlcmd.Parameters.AddWithValue("@OPCustomer", opcustomer);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return All Item Owners  ------------------------------------------------
        public DataTable GetItemOwners()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand sqlcmd = conn.CreateCommand();

            sqlcmd.CommandText = "spReturnItemOwners";
            sqlcmd.Parameters.AddWithValue("@clientID", 1);
            sqlcmd.CommandType = CommandType.StoredProcedure;
        

            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            conn.Close();

            return dt;
        }

        //  Insert a New Vendor  -------------------------------------------------------------------
        public int NewVendor(string vendorname, string contact, string phone, int opvendor)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertVendor";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@VendorName", vendorname);
            sqlcmd.Parameters.AddWithValue("@ContactName", contact);
            sqlcmd.Parameters.AddWithValue("@Phone", phone);
            sqlcmd.Parameters.AddWithValue("@OPVendor", opvendor);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return All Vendors  ------------------------------------------------
        public DataTable GetVendors()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand sqlcmd = conn.CreateCommand();

            sqlcmd.CommandText = "spReturnVendors";
            sqlcmd.CommandType = CommandType.StoredProcedure;


            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            conn.Close();

            return dt;
        }


        //  Insert a New Project  -------------------------------------------------------------------
        public int NewProject(int clientID, string oneboxid, string description, DateTime startdate)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertProject";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", clientID);
            sqlcmd.Parameters.AddWithValue("@OneBoxId", oneboxid);
            sqlcmd.Parameters.AddWithValue("@Description", description);
            sqlcmd.Parameters.AddWithValue("@StartDate", startdate);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Existing Project  -------------------------------------------------------------------
        public int UpdProject(int clientID, string oneboxid, string description, DateTime startdate)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditProject";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", clientID);
            sqlcmd.Parameters.AddWithValue("@OneBoxId", oneboxid);
            sqlcmd.Parameters.AddWithValue("@Description", description);
            sqlcmd.Parameters.AddWithValue("@StartDate", startdate);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Check for Existing Item  -------------------------------------------------------------------
        public int CheckExistingItem(int ClientID, string ItemNumber)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();
           
            sqlcmd.CommandText = "spExistsItem";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", ClientID);
            sqlcmd.Parameters.AddWithValue("@ItemNumber", ItemNumber);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return Project Information  --------------------------------------------------------------
        public DataTable GetProject(int clientID, string oneboxid)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnProjects";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", clientID);
            sqlcmd.Parameters.AddWithValue("@OneBoxId", oneboxid);


            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }


        //  Return Project Information  --------------------------------------------------------------
        public DataTable GetProject(int clientID)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnProjects";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", clientID);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Insert a New Business Rule  -------------------------------------------------------------------
        public int NewBusinessRule(string itemnumber, string oneboxid, string ruletext, string orgfile,
                                   string epifile, string userid, string NoteText, string RuleType)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertRule";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID","1");
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);
            
            sqlcmd.Parameters.AddWithValue("@RuleText", ruletext);
            sqlcmd.Parameters.AddWithValue("@OrgFileName", orgfile);
            sqlcmd.Parameters.AddWithValue("@EPIFileName", epifile);
            sqlcmd.Parameters.AddWithValue("@UpdUser", userid);
            sqlcmd.Parameters.AddWithValue("@NoteText", NoteText);
            sqlcmd.Parameters.AddWithValue("@RuleType", RuleType);
            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.VarChar);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Existing Business Rule  -------------------------------------------------------------------
        public int UpdBusinessRule(int id, string itemnumber, string oneboxid, string ruletext, string orgfile,
                                   string epifile, string userid)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spEditRule";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ID", id);
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);
            sqlcmd.Parameters.AddWithValue("@OneBoxId", oneboxid);
            sqlcmd.Parameters.AddWithValue("@RuleText", ruletext);
            sqlcmd.Parameters.AddWithValue("@OrgFileName", orgfile);
            sqlcmd.Parameters.AddWithValue("@EPIFileName", epifile);
            sqlcmd.Parameters.AddWithValue("@UpdUser", userid);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return Business Rule  --------------------------------------------------------------
        public DataTable GetBusinessRule(int id)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnRule";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ID", id);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }


        //  Return Business rules for Item  --------------------------------------------------------------
        public DataTable GetBusinessRules(string itemnumber)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnRules";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", 1);
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }

        //  Delete Business Rule  -------------------------------------------------------------------
        public int DltBusinessRule(int id)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spDeleteRule";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ID", id);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //Item Notification ----------------------------------------------------------------

        //public int NewItemNotification(int clientid, int ItemNumber,int ItemOwner,
        //                      string SbmFirstName, string SbmLastName, string  OwnEMail,
        //                     string  emailbodystring)
        //{
        //    string encryptionkey = "ChaseIRFKey";
        //    string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        //    var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
        //    SqlConnection sqlconn = new SqlConnection(connString);

        //    SqlCommand sqlcmd = sqlconn.CreateCommand();

        //    sqlcmd.CommandText = "spUpdateNotificationStatus";
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    sqlcmd.Parameters.AddWithValue("@UID", ItemNumber);
        //    sqlcmd.Parameters.AddWithValue("@Status", 0);

        //    var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
        //    returnParameter.Direction = ParameterDirection.ReturnValue;

        //    sqlconn.Open();

        //    sqlcmd.ExecuteNonQuery();

        //    sqlconn.Close();

        //    int result = Convert.ToInt32(returnParameter.Value);
        //    return result;
        //}

        public int NewItemNotification(int clientid, string itemnumber, int itemowner, string submitter,
                                   string from, string to, string cc, string bcc, string subject,
                                   string bodyhtml)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spInsertItemNotification";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ClientID", clientid);
            sqlcmd.Parameters.AddWithValue("@ItemNumber", itemnumber);
            sqlcmd.Parameters.AddWithValue("@ItemOwner", itemowner);
            sqlcmd.Parameters.AddWithValue("@SubmittingUser", submitter);
            sqlcmd.Parameters.AddWithValue("@EmFrom", from);
            sqlcmd.Parameters.AddWithValue("@EmTo", to);
            sqlcmd.Parameters.AddWithValue("@EmCC", cc);
            sqlcmd.Parameters.AddWithValue("@EmBCC", bcc);
            sqlcmd.Parameters.AddWithValue("@EmSubject", subject);
            sqlcmd.Parameters.AddWithValue("@EmBody", bodyhtml);

            var returnParameter = sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Update Item Notification Status  ------------------------------------------------------------
        public int UpdNotificationStatus(int uid, string status)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlconn = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlconn.CreateCommand();

            sqlcmd.CommandText = "spUpdateNotificationStatus";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UID", uid);
            sqlcmd.Parameters.AddWithValue("@Status", status);

            var returnParameter = sqlcmd.Parameters.Add("@RC", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            sqlconn.Open();

            sqlcmd.ExecuteNonQuery();

            sqlconn.Close();

            int result = Convert.ToInt32(returnParameter.Value);
            return result;
        }

        //  Return Item Notification  --------------------------------------------------------------
        public DataTable GetItemNotification(int uid)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            SqlConnection sqlgetuser = new SqlConnection(connString);

            SqlCommand sqlcmd = sqlgetuser.CreateCommand();

            sqlcmd.CommandText = "spReturnItemNotification";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UID", uid);

            sqlgetuser.Open();
            DataTable dt = new DataTable();
            dt.Load(sqlcmd.ExecuteReader());

            sqlgetuser.Close();

            return dt;
        }


        //public DataTable ReturnEvent(int EventID)
        //{
        //    string encryptionkey = "ChaseIRFKey";
        //    string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        //    var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
        //    SqlConnection sqlconn = new SqlConnection(connString);

        //    SqlCommand sqlcmd = sqlconn.CreateCommand();

        //    sqlcmd.CommandText = "spReturnEvent";
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    sqlcmd.Parameters.AddWithValue("@ID", EventID);


        //    sqlconn.Open();
        //    DataTable dt = new DataTable();
        //    dt.Load(sqlcmd.ExecuteReader());

        //    sqlconn.Close();

        //    return dt;
        //}


        //  Encrypt a string  --------------------------------------------------------------
        //       public string encryptString(string Str)
        //       {
        //           string EncStr = string.Empty;
        //           string encryptionkey = "ChaseIRFKey";

        //           EncStr = Spritz.EPIEncrypt(Str, encryptionkey);

        //           return EncStr;

        // test code to encrypt a string if needed
        //DataClass edc = new DataClass();
        //string edcstr = string.Empty;
        //edcstr = edc.encryptString("");
        //       }

        //  Decrypt a string  --------------------------------------------------------------
        //       public string decryptString(string Str)
        //       {
        //           string DecStr = string.Empty;
        //           string decryptionkey = "ChaseIRFKey";

        //           DecStr = Spritz.EPIDecrypt(Str, decryptionkey);

        //           return DecStr;

        // test code to decrypt a string if needed
        //DataClass edc = new DataClass();
        //string edcstr = string.Empty;
        //edcstr = edc.decryptString("");
        //       }


    }
}

