using System;
using System.Data;
using IBM.Data.DB2.iSeries;
using SpritzDotNet;

namespace Chase_IRF
{
    public class DB2DataClass
    {

        //  Return Product Groups from iSeries  -------------------------------------------------------
        public DataTable RtvProductGroups()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVPRDGRPS";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving product groups.", ex);
                        }
                    }

                }

            }
        }

        //  Return Product Sub-Groups from iSeries  ----------------------------------------------------
        public DataTable RtvProductSubGroups()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVSUBGRPS";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving product sub-groups.", ex);
                        }
                    }

                }

            }
        }

        //  Return Product Sub-Groups from iSeries  ----------------------------------------------------
        public DataTable RtvItemOwners()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVITMOWNR";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving Item Owners", ex);
                        }
                    }

                }

            }
        }

        //  Return Product Sub-Groups from iSeries  ----------------------------------------------------
        public DataTable RtvVendors()
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVVENDORS";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving Vendors", ex);
                        }
                    }

                }

            }
        }

        //  ---------------------------------------------------------------------------------------
        /// <summary>
        /// Create IRF Item
        /// </summary>
        /// 
        public int IRFCreate(string itemnum,
                             string itemdsc,
                             int itemown,
                             string itemprgr, 
                             string itemprsg,
                             string itemcctr,
                             string itemuom,
                             int itemuomqty,
                             iDB2Numeric itemlen,
                             iDB2Numeric itemwdt,
                             iDB2Numeric itemhei,
                             iDB2Numeric itemwei,
                             string oneboxid,
                             int itemvend,
                             string itemactd,
                             string itemexpd,
                             iDB2Numeric maxordqty,
                             iDB2Numeric minstock,
                             iDB2Numeric maxstock,
                             string itemsede,
                             iDB2Numeric itemprc,
                             string ViewOnly,
                             string ipurpose

            )
        {
            int result = 0;
            string pgmrtn;
            string actdate;
            string expdate;

            actdate = itemactd.Substring(6, 4) + "-" + itemactd.Substring(0, 2) + "-" + itemactd.Substring(3, 2);
            expdate = itemexpd.Substring(6, 4) + "-" + itemexpd.Substring(0, 2) + "-" + itemexpd.Substring(3, 2);

            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.MT217010S";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set up parameters
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMNUM", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMNUM"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMDSC", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMDSC"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMOWN", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMOWN"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMPRGR", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMPRGR"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMPRSG", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMPRSG"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMCCTR", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMCCTR"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMUOM", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMUOM"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMUOMQ", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMUOMQ"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMLENG", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMLENG"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMWDTH", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMWDTH"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMHEIG", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMHEIG"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMWGHT", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMWGHT"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMBOXI", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMBOXI"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMVEND", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMVEND"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMACTD", iDB2DbType.iDB2Date));
                    cmd.Parameters["@ITEMACTD"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMEXPD", iDB2DbType.iDB2Date));
                    cmd.Parameters["@ITEMEXPD"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMMAXQ", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMMAXQ"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMMINL", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMMINL"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMMAXL", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMMAXL"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMSUPE", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEMSUPE"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@ITEMPRIC", iDB2DbType.iDB2Numeric));
                    cmd.Parameters["@ITEMPRIC"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@VIEWONLY", iDB2DbType.iDB2Char));
                    cmd.Parameters["@VIEWONLY"].Direction = ParameterDirection.Input;
                    //Durga added IPURPOSE 11-27-2018---to include manufactured status in OP
                    cmd.Parameters.Add(new iDB2Parameter("@IPURPOSE", iDB2DbType.iDB2Char));
                    cmd.Parameters["@IPURPOSE"].Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(new iDB2Parameter("@RETURNVALUE", iDB2DbType.iDB2Char));
                    cmd.Parameters["@RETURNVALUE"].Direction = ParameterDirection.Output;

                    //  Set parameter values
                    cmd.Parameters["@ITEMNUM"].Value = itemnum.ToUpper();
                    cmd.Parameters["@ITEMDSC"].Value = itemdsc;
                    cmd.Parameters["@ITEMOWN"].Value = itemown;
                    cmd.Parameters["@ITEMPRGR"].Value = itemprgr;
                    cmd.Parameters["@ITEMPRSG"].Value = itemprsg;
                    cmd.Parameters["@ITEMCCTR"].Value = itemcctr;
                    cmd.Parameters["@ITEMUOM"].Value = itemuom;
                    cmd.Parameters["@ITEMUOMQ"].Value = itemuomqty;
                    cmd.Parameters["@ITEMLENG"].Value = itemlen;
                    cmd.Parameters["@ITEMWDTH"].Value = itemwdt;
                    cmd.Parameters["@ITEMHEIG"].Value = itemhei;
                    cmd.Parameters["@ITEMWGHT"].Value = itemwei;
                    cmd.Parameters["@ITEMBOXI"].Value = oneboxid;
                    cmd.Parameters["@ITEMVEND"].Value = itemvend;
                    cmd.Parameters["@ITEMACTD"].Value = actdate;
                    cmd.Parameters["@ITEMEXPD"].Value = expdate;
                    cmd.Parameters["@ITEMMAXQ"].Value = maxordqty;
                    cmd.Parameters["@ITEMMINL"].Value = minstock;
                    cmd.Parameters["@ITEMMAXL"].Value = maxstock;
                    cmd.Parameters["@ITEMSUPE"].Value = itemsede;
                    cmd.Parameters["@ITEMPRIC"].Value = itemprc;
                    cmd.Parameters["@VIEWONLY"].Value = ViewOnly;
                    cmd.Parameters["@IPURPOSE"].Value = ipurpose;

                    //  Call the stored procedure
                    try
                    {
                        cmd.ExecuteNonQuery();
                        pgmrtn = cmd.Parameters["@RETURNVALUE"].Value.ToString();
                        if (pgmrtn == "Y") { result = 1; }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw new System.Exception("Error creating new item.", ex);
                    }
                }
            }
            
        }

        //  Return Product Inventory On Hand Quantity  ----------------------------------------------------
        public DataTable RtvItemQOH(string itemnumber)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVITEMQOH";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set up parameters
                    cmd.Parameters.Add(new iDB2Parameter("@ITEM", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEM"].Direction = ParameterDirection.Input;

                    //  Set parameter values
                    cmd.Parameters["@ITEM"].Value = itemnumber.ToUpper();

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving Item On-Hand Quantity", ex);
                        }
                    }

                }

            }
        }

        //  Return Product Inventory Field Values  ----------------------------------------------------
        public DataTable RtvItemInventoryValues(string itemnumber)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                //test

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVITEMVAL";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set up parameters
                    cmd.Parameters.Add(new iDB2Parameter("@ITEM", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEM"].Direction = ParameterDirection.Input;

                    //  Set parameter values
                    cmd.Parameters["@ITEM"].Value = itemnumber.ToUpper();

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving Item Inventory Values", ex);
                        }
                    }

                }

            }
        }

        //Retrieving PO# for each item on HomePage
        public DataTable RtvItemPONumber(string itemnumber)
        {
            string encryptionkey = "ChaseIRFKey";
            string connStringencrypted = System.Configuration.ConfigurationManager.AppSettings["DB2ConnectionString"];
            var connString = Spritz.EPIDecrypt(connStringencrypted, encryptionkey);
            //var connString = connStringencrypted;

            //  if parameters are valid run the create ITR procedure 
            using (iDB2Connection con = new iDB2Connection(connString))
            {
                // Open our connection
                con.Open();

                //test

                // Retrieve User
                using (iDB2Command cmd = new iDB2Command())
                {
                    cmd.CommandText = "CS217O.RTVIRFPO";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set up parameters
                    cmd.Parameters.Add(new iDB2Parameter("@ITEM", iDB2DbType.iDB2Char));
                    cmd.Parameters["@ITEM"].Direction = ParameterDirection.Input;

                    //  Set parameter values
                    cmd.Parameters["@ITEM"].Value = itemnumber.ToUpper();

                    //  Call the stored procedure
                    using (iDB2DataAdapter sda = new iDB2DataAdapter(cmd))
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            throw new System.Exception("Error retrieving Item PO#", ex);
                        }
                    }

                }

            }
        }

    }
}