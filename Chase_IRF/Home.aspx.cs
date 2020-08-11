using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SpritzDotNet;


namespace Chase_IRF
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                String PageDirection = "F";
                string contains = string.Empty;
                int LastRow = 0; 
                int MaxRows = 0;
                DataClass rep = new DataClass();
                DataTable dt = rep.ReturnItemListPage(Convert.ToInt32(Session["userid"]), LastRow, PageDirection, contains);

                GenerateTable(dt);
            }

        }
     

        private void GenerateTable(DataTable dt)
        {
            int LastRow = 0;
            int MaxRows = 0;
            string tablestring = "";
            //dt is datatable object which holds DB results.
            tablestring = tablestring +
                 "<table class='table table-striped table-hover' id='ordersTable'><thead><tr><th>Item</th><th>Description</th><th>Item Purpose</th><th>Status</th><th>Item Owner</th><th>Cost Center</th><th>OneBox Month/Yr</th><th>Item Replaced</th><th>Purchase Order</th><th>Action</th></tr></thead><tfoot><tr><th>Item</th><th>Description</th><th>Item Purpose</th><th>Status</th><th>Item Owner</th><th>Cost Center</th><th>OneBox Month/Yr</th><th>Item Replaced</th><th>Purchase Order</th><th>Action</th></tr></tfoot><tbody>";
                 //"< table class='table table-striped table-hover' id='ordersTable'><thead><tr><th>Item</th><th>Description</th><th>Item Category</th><th>Status</th><th>Item Owner</th><th>Cost Center</th><th>OneBox Month/Yr</th><th>Item Replaced</th><th>Action</th></tr></thead><tfoot><tr><th>Item</th><th>Description</th><th>Item Category</th><th>Status</th><th>Item Owner</th><th>Cost Center</th><th>OneBox Month/Yr</th><th>Item Replaced</th><th visible = false'></th></tr></tfoot><tbody>";
            foreach (DataRow dr in dt.Rows)
            {
                DB2DataClass DB2 = new DB2DataClass();
                DataTable ItemPOTable = DB2.RtvItemPONumber(dr[2].ToString().Trim());
                string PONumber = ItemPOTable.Rows[0]["PONumber"].ToString();
                tablestring = tablestring.Trim() + "<tr><td class='itemno'>" + dr[2].ToString().Trim() + "</td><td>"
                                          + dr[5].ToString().Trim() + "</td><td>"
                                          + dr[19].ToString().Trim() + "</td><td>"
                                          + dr[4].ToString().Trim() + "</td><td>" //status
                                          + dr[26].ToString().Trim() + "</td><td>" //itemOwenerId
                                          + dr[6].ToString().Trim() + "</td><td>"
                                          + dr[35].ToString().Trim() + "</td><td>"
                                          + dr[21].ToString().Trim() + "</td><td>"
                                          + PONumber.Trim() + "</td>" + "<td><button class='editrow'> Edit </button ></td ></tr> ";

                MaxRows = Convert.ToInt32(dr[61].ToString());
                LastRow = Convert.ToInt32(dr[62].ToString());
            }
            Session["lastrow"] = LastRow;

            tablestring = tablestring + "</tbody></table>";

            ItemsList.InnerHtml = tablestring;
        }



        protected void ForwardButton_Click(object sender, EventArgs e)
        {
            String PageDirection = "F";
            int LastRow = Convert.ToInt32(Session["lastrow"]);
            int MaxRows = 0;
            string contains = containsText.Value;
            DataClass rep = new DataClass();
            DataTable dt = rep.ReturnItemListPage(Convert.ToInt32(Session["userid"]), LastRow, PageDirection, contains);
            GenerateTable(dt);
        }



        protected void BackwardButton_Click(object sender, EventArgs e)
        {
            String PageDirection = "B";
            int LastRow = Convert.ToInt32(Session["lastrow"]);
            int MaxRows = 0;
            string contains = containsText.Value;
            DataClass rep = new DataClass();
            DataTable dt = rep.ReturnItemListPage(Convert.ToInt32(Session["userid"]), LastRow, PageDirection, contains);
            GenerateTable(dt);
        }
    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       String PageDirection = "F";
            //  int LastRow = string contains;
            //int LastRow = 1;
            int LastRow = 0;
            int MaxRows = 0;
            string contains = containsText.Value;
            DataClass rep = new DataClass();
            DataTable dt = rep.ReturnItemListPage(Convert.ToInt32(Session["userid"]), LastRow, PageDirection, contains);
            GenerateTable(dt);
        }
       

      
    }

}