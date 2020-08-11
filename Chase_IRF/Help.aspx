<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Chase_IRF.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel='stylesheet prefetch' href='https://fonts.googleapis.com/css?family=Open+Sans:600' />
    <link href="css/style.css" rel="stylesheet" />
    <script src="scripts/vendor/jquery-1.9.1.min.js"></script>
    <script src="scripts/main.js"></script>
    <style>
        .help-wrap {
            width: 100%;
            margin: auto;
            max-width: 1200px;
            min-height: 1750px;
            position: relative;
            background: url(images/Chase.png) no-repeat center;
            background-attachment: fixed;
            box-shadow: 0 12px 15px 0 rgba(0,0,0,.24),0 17px 50px 0 rgba(0,0,0,.19);
        }

        .help-html{
	        width:100%;
	        height:100%;
	        position:absolute;
	        padding:90px 70px 50px 70px;
	        background:rgba(40,57,101,.9);
        }
        .inlineBlockEle {
        text-transform: uppercase;
        font-size: 22px;
        margin-right: 15px;
        padding-bottom: 5px;
        margin: 0 15px 10px 0;
        display: inline-block;
        border-bottom: 2px solid transparent;
        color: #fff;
        border-color: #8d8d8d;
}
        .col-md-12{
            max-width: 1100px;
            color: antiquewhite;
        }
    </style>

    <title>Help</title>

    <body>
        <form id="helpform" runat="server">
            <div class="help-wrap">
                <div class="help-html">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                               
                                    <h4 class="inlineBlockEle">Purpose</h4>

                                    <h6>The Chase IRF application is designed to assist in organizing items for the One Box and Ariba.  Submissions are tracked and appropriate team members notified to promote new item awareness.  All revisions to items are tracked for security and reporting purposes. 
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">What to expect</h4>
                                    <h6>When an item owner logs into the IRF application they will be presented with a summary list of all active items that they have submitted.  Submissions will be color coded based on the item’s status.
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Item Status</h4>
                                    <h6>Item status’ have four states.  This is because it may be possible to process limited actions for an item before it is complete enough to display on Ariba.  It is possible to process an item in a One Box before we have the data required for Ariba.  It is also possible to receive an item into inventory with less information than is required for a One Box distribution.  Consequently there are four status’ for an item: New or unable to process,  Receivable  for items that can be received into inventory,  One Box for items that have enough data for use in a One Box distribution, and Complete when the item is complete and ready for Ariba.  Items that have sufficient data defined when created may never appear in any state other than complete.
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Creating and Editing Items</h4>
                                    <h6>Item owners will be allowed to maintain their own items.  Users who have been defined as administrators will see items for all item owners.  From the summary page you may choose to update an existing item or create a new one.  To edit or create a new item the user is taken to the item maintenance page.  When creating a new item the user may choose another item that is being replaced.  When the item to be replaced is chosen the details for that item are used to populate the input fields on the new item page and the old item number is placed in the superseded item input field automatically.  
                                        Once an item has been submitted the user will receive a confirmation for the item that shows all of the item data.  This acknowledgement is sent to appropriate members of the Chase Team.
                                    </h6>
                                    <br />
                                    <h5>Editing Existing or Creating New IRF: </h5><h6>From the HOME page, click on the EDIT button to make changes to an existing item or click on NEW IRF at the top to create a new item.</h6>
                                    <br />
                                    <h5>Copy Item feature: </h5><h6>When creating a new item, a user may choose to copy an existing item with similar item details.  When an existing item number is typed into the Item Number field and COPY ITEM is selected, the fields on that screen will populate with the information used for the similar item.  The user can then update those fields as necessary for their new item.</h6>
                                    <br />

                                    <h4 class="inlineBlockEle">Business Rules</h4>
                                    <h6>Business Rules have been expanded somewhat to allow for multiple rules and an added attachment.  Business rules can be separated in a single text box with commas (,).  If you have uploaded a custom list and wish to change it you can simply upload again and the application will replace the previously uploaded file with the new one.  There is currently no method to remove an uploaded list but it can be replaced by an empty one or you may email the Chase Team at ChaseAMTeam@epiinc.com to have them ignore the uploaded list for the item.
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Revisions</h4>
                                    <h6>All revisions to item information will be captured for reference.  Updated items capture both the before and after image of the item.  New items capture only the after image but even the creation of a new item produces a revision.
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Items being replaced / superseded items</h4>
                                    <h6>To replace an existing item, sometimes referred to as superseding an item, the item number to be replaced is entered into the appropriate field.  At that point the activation date of the new item becomes the date on which the replacement occurs.  The replaced item remains active until that day.
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Notifications</h4>
                                    <h6>Whenever an item is created or changed an email showing the updated values for the item is sent to the item owner and appropriate team members.  
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Contact Information</h4>
                                    <h6>The EPI account management team can be contacted at ChaseAMTeam@epiinc.com. 
                                    <h6><a href="\inetpub\wwwroot\Chase_IRF\uploads\2018OneBoxSchedule.pdf" id="oneboxschedule" style="color:lightgreen;">For a complete list of the current One Box schedule please follow this link.</a></h6>
                                    </h6>
                                    <br />
                                    <h4 class="inlineBlockEle">Stock Receiving</h4>
                                    <h6>Once the IRF is complete a PO# will be provided to the item owner to be placed on the Bill of Lading (BOL) in order for EPI to receive the materials into the warehouse.  EPI will receive materials and locate in inventory within one (1) business day after the physical receipt from the shipper.
                                    </h6>
                                    <h6>The address of the EPI receiving area is: EPI Fulfillment, Attn: JPMC #9934, 65 Clark Road, Battle Creek MI, 49037.
                                    </h6>
                                    <h6>Receiving and appointment scheduling phone: (269) 964-4600 x5818.
                                    </h6>                                   
                                    <h6>Receiving Fax: (269) 968-1187.
                                    </h6>
                                    <h6>Receiving Email: 65ClarkReceiving@epiinc.com</h6> 
                                    <br />
                                    <h4 class="inlineBlockEle">Adding a New Vendor</h4>
                                    <h6>If you do not see the vendor for your item in the Item Vendor dropdown list, please send an email with the following information to ChaseAMTeam@epiinc.com and we will add them to the list within 48 hours so you can complete your IRF.
                                    </h6>
                                    <h6>•	Vendor Company Name</h6>
                                    <h6>•	Address</h6>    
                                    <h6>•	City, State, Zip</h6>
                                    <h6>•	Country</h6>
                                    <h6>•	Contact Name</h6>
                                    <h6>•	Position   </h6> 
                                    <h6>•	Email</h6>
                                    <h6>•	Phone</h6>    
                              
                              
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </form>
    </body>
</asp:Content>
