<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Chase_IRF.Reports" %>


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


    <form data-toggle="validator" id="reportform" role="form" enctype="multipart/form-data" runat="server">
        <div class="newIrf-wrap">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <h3 class="inlineBlockEle">Reports</h3>

                        <br />
                        <h4 class="inlineBlockEle">Please click Report Link for desired report.</h4>

                        <br /> <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <a role="presentation" id="ItemsReport" href="ReportItem01.aspx">Item Detail Report</a>
                            </div>

                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <a role="presentation" id="RevisionsReport" href="ReportRevisions02.aspx">IRF Revisions Report</a>
                            </div>

                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <a role="presentation" id="OneBoxReport" href="ReportOneBox01.aspx">One Box Items Report</a>
                            </div>

                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <a role="presentation" id="IncompleteItemsReport" href="ReportIncompleteItems01.aspx">Incomplete Items Report</a>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>