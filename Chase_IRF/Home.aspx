<%@ Page Title="Home" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Chase_IRF.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .searchTxtBox {
            box-shadow: none !important;
            padding: 0px !important;
            border: 0px solid !important;
        }

        .containsText {
            box-shadow: none !important;
            padding: 0px !important;
            border: 0px solid !important;
        }

        .formField {
            min-height: 0px;
        }

        body {
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 14px;
            line-height: 1.428571429;
            color: #333333;
            background-color: #ffffff;
        }

        body {
            padding-top: 80px;
            font-size: 12px;
            color: #34495e;
            background: #f5f5f5;
        }

        .red {
            background-color: red;
        }

        .green {
            background-color: greenyellow;
        }

        .grey {
            background-color: grey;
        }

        .home-wrap {
            width: 100%;
            margin: auto;
            max-width: 1246px;
            min-height: 670px;
            position: relative;
            box-shadow: 0 12px 15px 0 rgba(0, 0, 0, 0.24), 0 17px 50px 0 rgba(0, 0, 0, 0.19);
        }

        .signup-html {
            width: 100%;
            position: absolute;
            padding: 90px 70px 50px 70px;
            background: rgba(40, 57, 101, 0.9);
        }

        #ordersTable_filter {
            visibility: hidden !important;
        }

        #ordersTable_wrapper {
            margin-top: -4% !important;
        }

        .user-form .field input {
            display: inline-block;
            min-height: 36px !important;
            border: 3px #ededed !important;
            border-radius: 25px;
        }

        .dropdownIRF {
            border: 5px;
            width: 25%;
            border-radius: 25px;
            height: 37px;
        }

        .btnIRF {
            height: 35px;
            vertical-align: top;
            width: 10%;
            border-radius: 25px;
        }

        .editrow {
            border-radius: 25px;
            width: 80%;
        }

        .row {
            margin-left: 0px !important;
        }

        /*.container{
            width: 1300px !important;
        }*/
    </style>
    <%-- <script src="scripts/jquery-1.12.4.js"></script>
    <script src="https://getbootstrap.com/dist/js/bootstrap.min.js"></script>--%>
    <script src="scripts/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".editrow").click(function () {
                var item = $(this).closest("tr")   // Finds the closest row <tr> 
                                   .find(".itemno")     // Gets a descendent with class="nr"
                                    .text();         // Retrieves the text within <td>
                var url = '../NewIRF.aspx?itemNumber=' + item;
                window.location.href = url;
                return false;
            });

            //Color coding rows based on statuses
            $('#ordersTable').DataTable({
                "bPaginate": false,
                "bLengthChange": true,
                "bFilter": true,
                "bInfo": false,
                "bAutoWidth": false,
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    if (aData[3] == "Complete") {
                        $('td', nRow).css('background-color', '#e6ffee');
                    }
                    else if (aData[3] == "Incomplete") {
                        $('td', nRow).css('background-color', '#ffd6cc');
                    }
                }
            });

            //Apply search for each column
            $('#ordersTable tfoot th').each(function () {
                var title = $(this).text();
                $(this).html('<input style="height: 30px;width: 110%;border-radius:25px;" type="text" placeholder="' + title + '" />');
            });


            // DataTable
            var otable = $('#ordersTable').DataTable();

            // Apply the search
            otable.columns().every(function () {
                var that = this;
                $('input', this.footer()).html('<input style="padding: 0px; height: 10px;width: 90%;" type="text" />');
                $('input', this.footer()).on('keyup change', function () {
                    if (that.search() !== this.value) {
                        that.search(this.value).draw();
                    }
                });
            });


        });


    </script>


    <form id="homeform" runat="server">
        <div class="home-wrap">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group user-form">
                            <div class="field" style="text-align: right; margin-top: 4%">
                                <%--<label for="containsText" class="control-label">Search:</label>--%>
                                <input type="text" class="form-control formField" id="containsText" runat="server" placeholder="Seach Items Here" style="height: 35px !important; width: 20%;" />
                                <button id="btnSearch" style="padding: 0.5%; vertical-align: top" class="btn-primary btnIRF" onserverclick="btnSearch_Click" type="submit" runat="server">Search Items</button>
                            </div>

                            <br />
                            <h3>Summary of Items</h3>
                            <div class="table-responsive" id="ItemsList" runat="server"></div>
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="paginate_button" style="text-align: right">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 73%"></td>
                                                    <td style="align-items: flex-end">
                                                        <button id="btnBackward" type="submit" class="btn-primary btnIRF" style="width: 150px" onserverclick="BackwardButton_Click" runat="server">Previous 50 Items</button>
                                                    </td>
                                                    <td>
                                                        <button id="btnForward" type="submit" class="btn-primary btnIRF" style="width: 150px" onserverclick="ForwardButton_Click" runat="server">Next 50 Items</button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <br />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>

</asp:Content>

