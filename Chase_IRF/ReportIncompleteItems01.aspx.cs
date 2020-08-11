using System;
using System.Data;
using System.Configuration;
using System.Web.UI;

namespace Chase_IRF
{
    public partial class ReportIncompleteItems01 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Boolean isAdmin = false;
            int ownerNumber = 0;

            string reportServerURI = ConfigurationManager.AppSettings["ReportServerURI"];

            DataClass rep = new DataClass();
            DataTable dt = rep.GetUserDetails(Convert.ToInt32(Session["userid"]));
            foreach (DataRow dr in dt.Rows)
            {
                isAdmin = Convert.ToBoolean(dr[9].ToString());
                ownerNumber = Convert.ToInt32(dr[13].ToString());
            }

            if (isAdmin == true)
            { ownerNumber = 0; }

            //ReportParameter reportParam1 = new ReportParameter("OwnerParameter", ownerNumber.ToString());

            ReportViewer1.ServerReport.ReportServerCredentials = new Report.MyReportServerCredentials();
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportServerURI);
            ReportViewer1.ServerReport.ReportPath = "/CHB/Chase_IncompleteItems01";
            // ReportViewer1.ServerReport.SetParameters(reportParam1);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
        }
    }
}