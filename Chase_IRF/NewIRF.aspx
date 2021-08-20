<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewIRF.aspx.cs" Inherits="Chase_IRF.NewIRF" %>

<asp:Content ID="NewIRF" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" />
    <link href="scripts/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/NewIRFScript.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <script>
        $(function () {
            $("[id$=ExpireDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            });
            $("[id$=DateToBeAdded]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            });
            $("[id$=DateToBeRemoved]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            });
            $("[id$=ExpectedArrival]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            });
            //            $("[id$=SpecDistDelivery]").datepicker({
            //                showOn: 'button',
            //                buttonImageOnly: true,
            //                buttonImage: 'images/calendar.png'
            //            });
        });

        $(function specialDistributionDate() {
            $('input[name="IPSpecial"]').on('change', function () {
                if ($(this).val() == 'Yes') {
                    $("#SpecDistDelivery").prop('disabled', true);
                } else {
                    $("#SpecDistDelivery").prop('disabled', true);
                }
            });
        });


        $(function () {
            $("select").on("change", function () {
                //debugger;
                if (this.id == 'head_SelectItemOwnerName') {
                    var Param1 = $(this).find(":selected").val();
                    var result;

                    $.ajax({
                        type: "POST",
                        url: "NewIRF.aspx/rtnSelectItemOwnerName",
                        data: "{ Param1: '" + Param1 + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: OnSuccess,
                        error: OnErrorCall
                    });

                    function OnSuccess(response) {
                        $("#head_ItemOwnerEmailID").val(response.d);
                    }

                    function OnErrorCall(response) {
                        alert("Error occured. Please contact Admin");
                    }
                }

            });
        });

        var validate = true;
        var inputs = ['ItemNumber', 'OneBoxID'];
        var focusedElement = null;
        $.each(inputs, function (index, value) {
            var element = $('#' + value);
            if (element.val() == "") {
                if (focusedElement == null) {
                    focusedElement = element;
                }
                element.css('border', "1px solid red");
                validate = false;
            } else {
                element.css('border', '');
            }
            return false;
        });
        //var re=
        //if (!re.test($('#OneBoxID').val())) {
        //    validate = false;
        //    alert('Please Select One Box ID');
        //    return false;
        //}
        //if (validate) {
        //    return confirm('Are you sure you want to submit the item?');
        //}
        //else {
        //    focusedElement.focus();
        //    return false;
        //}

        //};

        //$(document).ready(function () {
        //    $('input[type="file"]').change(function (e) {
        //        var fileName = "New Selection: " + e.target.files[0].name;
        //        $("#RulesFile").val(fileName);
        //        //alert('The file "' + fileName + '" has been selected.');
        //    });
        //});
    </script>
    <style>
        body {
            background: #f5f5f5 !important;
        }

        .required {
            color: red;
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
            vertical-align: top;
            width: 10%;
            border-radius: 25px;
        }

        .datepickerIRF {
            width: 25%;
        }

        .dollar {
            display: inline-block;
            position: relative;
            width: 50%;
        }

            .dollar input {
                padding-left: 15px;
            }

            .dollar:before {
                position: absolute;
                content: "$";
                left: 9px;
                top: 13px !important;
            }

        .contactInfoLabel {
            Width: 10% !important;
            vertical-align: middle !important;
            /*padding: 5px 0px 0px 0px !important;*/
        }

        .labelItemDetails {
        }

        select {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            background: url(https://cloud.githubusercontent.com/assets/7913347/20338079/3e020c9a-abee-11e6-8e5a-ca49c55b22ee.png) right / 20px no-repeat #fff;
            padding-right: 20px;
        }
    </style>
    <link href="css/datepicker.css" rel="stylesheet" />
    <link href="css/NewIRFcss.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <%-- Form Start --%>
    <form data-toggle="validator" id="contactinfoform" role="form" enctype="multipart/form-data" runat="server">
        <div class="newIrf-wrap">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <h3 class="inlineBlockEle">CONTACT INFORMATION</h3>
                        <br />
                        <h4 class="inlineBlockEle"></h4>
                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <label class="contactInfoLabel" for="submittername">Submitted By:</label>
                                <input type="text" style="padding: 5px 0 0 0; width: 33%" runat="server" id="SubmitterName" required="required" readonly="readonly" />
                                <label class="contactInfoLabel" for="itemownername">Item Owner:<span class="required"> *</span></label>
                                <select id="SelectItemOwnerName" style="padding: 2px 0 0 0;" class="dropdownIRF" multiple="false" runat="server">
                                </select>
                            </div>
                            <div class="field">
                                <label class="contactInfoLabel" for="inputName">Email:</label>
                                <input type="email" style="padding: 5px 0 0 0; width: 33%" runat="server" id="SubmitterEmailID" readonly="readonly" disabled />
                                <label class="contactInfoLabel" for="inputName">Email:</label>
                                <input type="email" runat="server" style="padding: 2px 0 0 0;" id="ItemOwnerEmailID" readonly="readonly" disabled />
                            </div>
                            <br />
                        </div>
                        <h3 class="inlineBlockEle">ITEM DETAILS</h3>
                        <br />
                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <label for="inputName" class="control-label">Enter new item details or an item to copy.</label>
                                <select class="dropdownIRF" id="SelectItem" runat="server" style="visibility: hidden; padding: 5px 0 0 0;">
                                    <option value="selectitem">-Select Item Type-</option>
                                    <option value="newitem">New Item</option>
                                    <option value="replaceditem">Replace Existing Item</option>
                                    <option value="copieditem">Copy Existing Item</option>
                                </select>
                            </div>
                            <div class="field">
                                <label style="height: 32px;" for="inputName">Item Number:<span class="required"> *</span></label>
                                <input type="text" runat="server" class="form-control formField" id="ItemNumber" required="required" maxlength="21" />
                                <button id="btnReplace" type="button" class="btn-primary btnIRF" onclick="ReplaceItemExists()" runat="server">Copy Item</button>
                                <label for="inputName" runat="server" class="text-right">Delete Item</label>
                                <input type="checkbox" id="CheckDelete" runat="server" class="chechbox" />
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">
                                    Item Description:<span class="required"> *</span><br />
                                    <span style="font-size: 80%">Limit 40 characters</span></label>
                                <input type="text" runat="server" class="form-control formField" id="ItemDescription" required="required" maxlength="40" />
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">
                                    Item Purpose:<span class="required"> *</span><br />
                                    <span style="font-size: 80%">Please Select One</span></label>

                                <asp:RadioButton GroupName="ItemPurpose" style="background-color: transparent" runat="server" class="inlineBlockEle" AutoPostBack="true" ID="IPAriba">
                                </asp:RadioButton>
                                <label for="IPAriba" style="width: 75px">Ariba</label>

                                <asp:RadioButton GroupName="ItemPurpose" style="background-color: transparent" runat="server" class="inlineBlockEle" AutoPostBack="true" ID="IPOneBox"></asp:RadioButton>
                                <label for="IPOneBox" style="width: 90px">One Box</label>

                                <asp:RadioButton GroupName="ItemPurpose" style="background-color: transparent" runat="server" class="inlineBlockEle" AutoPostBack="true" ID="IPOneBoxAriba"></asp:RadioButton>
                                <label for="IPOneBoxAriba" style="width: 250px">One Box and Ariba</label>

                                <asp:RadioButton GroupName="ItemPurpose" style="background-color: transparent" runat="server" class="inlineBlockEle" AutoPostBack="true" ID="IPSpecial"></asp:RadioButton>
                                <label for="IPSpecial" style="width: 150px">Inventory Only</label>

                                <asp:RadioButton GroupName="ItemPurpose" style="background-color: transparent" runat="server" class="inlineBlockEle" AutoPostBack="true" visible="false" ID="IPSpecialAriba">
                                </asp:RadioButton><label for="IPSpecialAriba" style="width: 210px; visibility: hidden">Special Distribution and Ariba</label>

                            </div>
                            <div class="field" runat="server" id="SetupLimitDiv">
                                <label for="inputName">Setup Order Limit:</label>
                                <input type="checkbox" id="SetupLimit" class="chechbox" runat="server" />
                            </div>

                            <%--                            <div class="field" >
                                <label for="inputName" class="control-label" style="visibility: hidden">
                                    Special Distribution Delivery Date:<br />
                                    <span style="font-size: 80%;visibility: hidden">If Special Distribution/Special Distribution and Ariba are selected, please provide the Delivery Date</span></label>
                                <asp:TextBox class="datepickerIRF" ID="SpecDistDelivery" style="visibility: hidden" visible="false" runat="server" placeholder="mm/dd/yyyy"></asp:TextBox>
                            </div>--%>

                            <div class="field">
                                <label for="inputName" class="control-label">Item Category:</label>
                                <select id="SelectGroup" class="dropdownIRF" runat="server"></select>
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">
                                    Distribution Rule:<br />
                                    <span style="font-size: 80%">Specify geography, branch type,<br />
                                        quantity per location,<br />
                                        multicultural preference,<br />
                                        or upload a custom list below.</span></label>
                                <input type="text" runat="server" class="form-control formField" id="DistributionRuleText" style="width: 450px;" />

                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Notes:</label>
                                <textarea id="Notes" class="form-control formField" runat="server" maxlength="255" style="display: initial;"></textarea>
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label" style="width: 25%;">Upload a Custom List:</label>
                                <input type="file" id="distributionrule" name="distributionrule" style="height: 40px; border-radius: 0px;"/>
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Uploaded Custom List (if any):</label>
                                <a id="filedownload" runat="server" style="text-align: left;" onserverclick="Download_Click" readonly="readonly" disabled="disabled" />
                                <input type="text" id="RulesFile" style="text-align: left;" readonly="readonly" disabled="disabled" />
                                <%-- <a id="filedownload" runat="server" style="text-align: left;" onserverclick="Download_Click" disabled="disabled"/>
                                <input type="text" id="RulesFile" runat="server" style="text-align: left;" readonly="readonly" disabled="disabled"/>--%>
                            </div>
                        <div class="field">
                            <label for="inputName" class="control-label" style="margin-bottom: 16px;">Cost Center:</label>
                            <input type="text" runat="server" class="form-control formField" id="CostCenter" />
                        </div>
                        <div class="field">
                            <label for="inputName" class="control-label">Unit Of Measure:</label>
                            <select id="UofM" class="dropdownIRF" runat="server"></select>
                        </div>
                        <div class="field">
                            <label for="inputName" style="height: 32px;" class="control-label">Quantity per Unit of Measure:</label>
                            <input type="text" runat="server" placeholder="0-9 only" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" class="form-control formField" id="Quantity" />
                        </div>
                        <div class="field">
                            <label for="inputName" style="height: 32px;" class="control-label">Production Cost per Unit of Measure in US Dollars:</label>
                            <div class="dollar">
                                <input type="text" style="width: 50%" runat="server" placeholder="0.0000" onkeypress="return isNumberKey(event)" id="ProdCost" />
                            </div>
                        </div>
                        <div class="field">
                            <label for="inputName" style="vertical-align: bottom;" class="control-label">Unit Dimensions(L*W*H of finished package) In Inches:</label>

                            <input id="UnitLength" title="UnitLength" onkeypress="return isNumberKey(event)" placeholder="0.000" min="0" maxlength="10" class="icon" runat="server" />
                            <input id="UnitWidth" title="UnitWidth" onkeypress="return isNumberKey(event)" placeholder="0.000" min="0" maxlength="10" class="icon" runat="server" />
                            <input id="UnitHeight" title="UnitHeight" onkeypress="return isNumberKey(event)" placeholder="0.000" min="0" maxlength="10" class="icon" runat="server" />
                        </div>
                        <div class="field">
                            <label for="inputName" style="height: 32px;" class="control-label">Weight Of Item In Pounds:</label>
                            <input title="ItemWeight" placeholder="0.000" onkeypress="return isNumberKey(event)" min="0" maxlength="10" runat="server" class="form-control formField" id="ItemWeight" />
                        </div>
                        <div class="field">
                            <label for="inputName" style="height: 32px;" class="control-label">Total Quantity Delivering to EPI:</label>
                            <input type="text" placeholder="0-9 Only" onkeypress="return isNumberKey(event)" min="0" maxlength="10" runat="server" class="form-control formField" id="ExpectedQuantity" />
                        </div>
                        <div class="field">
                            <label for="inputName" class="control-label">Expected Arrival Date:</label>
                            <asp:TextBox ID="ExpectedArrival" class="datepickerIRF" runat="server" placeholder="mm/dd/yyyy"></asp:TextBox>
                        </div>
                        <div class="field">
                            <label for="inputName" class="control-label">Item Vendor:</label>
                            <select class="dropdownIRF" id="PrimaryVendor" runat="server"></select>
                        </div>
                        <div class="field">
                            <label for="inputName" style="height: 32px;" class="control-label">Low Water Point:</label>
                            <input title="LowWaterPoint" placeholder="0-9 Only" onkeypress="return isNumberKey(event)" min="0" maxlength="10" runat="server" class="form-control formField" id="LowWaterPoint" />
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="inlineBlockEle">ONE BOX</h3>
                        <br />
                        <h5>If item will be distributed in a One Box, all questions must be answered before the item can be included.</h5>
                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <label for="inputName" class="control-label">One Box</label>
                                <select class="dropdownIRF" id="OneBoxID" runat="server"></select>
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Include in new build/relo starter One Box?:</label>
                                <asp:DropDownList id="SelectBool" runat="server" AutoPostBack="true" class="dropdownIRF">
                                    <asp:ListItem value="">-Please Select-</asp:ListItem>
                                    <asp:ListItem value="Y">Yes</asp:ListItem>
                                    <asp:ListItem value="N">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="specifyQuantityandDate" runat="server" style="display: none">
                                <div class="field">
                                    <label for="starterboxquantity" style="height: 32px;" class="control-label">If yes, specify quantity:</label>
                                    <input type="text" runat="server" placeholder="0-9 only" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" class="form-control formField" id="StarterBoxQuantity" />
                                </div>
                                <div class="field">
                                    <label for="expiredate" style="height: 32px;" class="control-label">If yes, specify Expiration Date:</label>
                                    <asp:TextBox ID="ExpireDate" class="datepickerIRF" runat="server" placeholder="mm/dd/yyyy"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="inlineBlockEle">ARIBA</h3>
                        <br />
                        <h5 class="inlineBlockEle">If item will be added to Ariba, all questions must be answered before item can be activated.</h5>
                        <br />
                        <br />
                        <div class="form-group user-form">
                            <div class="field">
                                <label for="inputName" class="control-label">Date to be added:</label>
                                <asp:TextBox ID="DateToBeAdded" runat="server" class="datepickerIRF" placeholder="mm/dd/yyyy"></asp:TextBox>
                                <label for="inputName" class="text-right">Viewable Only</label>
                                <input type="checkbox" id="viewableOnlyCheckbox" class="chechbox" runat="server" />
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Date to be removed:</label>
                                <asp:TextBox ID="DateToBeRemoved" runat="server" class="datepickerIRF" placeholder="mm/dd/yyyy"></asp:TextBox>
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Existing Item Number being Replaced (If applicable):</label>
                                <input type="text" runat="server" class="form-control formField" id="SupersededItem" />
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Unit Sell Price ($0 if no cost to branch):</label>
                                <div class="dollar">
                                    <input type="text" runat="server" placeholder="0.000" onkeypress="return isNumberKey(event)" min="0" maxlength="10" style="width: 50%" id="RetailPrice" />
                                </div>
                            </div>
                            <div class="field">
                                <label for="inputName" style="height: 32px;" class="control-label">Maximum Order Quantity:</label>
                                <input type="text" runat="server" placeholder="0-9 Only" onkeypress="return isNumberKey(event)" min="0" maxlength="10" class="form-control formField" id="MaxOrderQuantity" />
                            </div>
                            <div class="field">
                                <label for="inputName" class="control-label">Ariba Sub-Category(Select one):</label>
                                <select style="margin-bottom: 1%" class="dropdownIRF" id="SelectSubCategory" runat="server"></select>
                                <br />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="paginate_button" style="text-align: center">
                            <table style="width: 100%">
                                <tr>

                                    <td style="width: 95%">
                                        <button id="btnSubmit" type="submit" class="btn-primary btnIRF" style="width: 300px" onserverclick="btnSubmit_Click" runat="server">Submit</button>
                                    </td>
                                    <td style="width: 13%; align-items: flex-end">
                                        <button id="btnCancel" class="btn-primary" style="height: 35px; vertical-align: top; border-radius: 25px; width: 125%" onclick="cancelNewIRF()" type="button" runat="server">Cancel</button>
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
    </form>

</asp:Content>


