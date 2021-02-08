<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ReportRevisions02.aspx.cs" Inherits="Chase_IRF.ReportRevisions02" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="NewIRF" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" />
    <link href="scripts/jquery-ui.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script type="text/javascript" src="/scripts/NewIRFScript.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"   rel="Stylesheet" type="text/css" />


     <style>
        .required {
            color: red;
        }

        input[type='file'] {
            color: transparent;
        }

         .user-form .field input {
                display: inline-block;
                min-height: 36px !important;
                border: 3px #ededed !important;
                border-radius: 0;
                
            }
    </style>
    <link href="css/datepicker.css" rel="stylesheet" />
    <link href="css/NewIRFcss.css" rel="stylesheet" />


      <%--<form data-toggle="validator" id="reportform" role="form" enctype="multipart/form-data" runat="server" style="max-width:600px">--%>
      
            <div class="container"id="reportListDiv" style="height: 968px">
               <div class="newIrf-wrap" style="width: 100%; max-width: 1600px; overflow : scroll; max-height:860px;">
                <form id="Form1" role="form" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" Height="100%" Width="1225px" SizeToReportContent="true">--%>
             <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" Width="100%" Height="100%" FixedData="true" SizeToReportContent="true" >
                <ServerReport  />
            </rsweb:ReportViewer>
            </form>
       
            </div>
        </div>

  <%--  </form>--%>
</asp:Content>
