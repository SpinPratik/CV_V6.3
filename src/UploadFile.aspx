<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true"
    CodeFile="UploadFile.aspx.cs" Inherits="UploadFile" Title="Upload File" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            height: 33px;
        }

        input[type=file]:focus, input[type=checkbox]:focus, input[type=radio]:focus {
            outline: 0;
        }

        .fileUpload {
            position: relative;
            overflow: hidden;
            margin: 10px;
        }

            .fileUpload input.upload {
                position: absolute;
                top: 0;
                right: 0;
                margin: 0;
                padding: 0;
                font-size: 20px;
                cursor: pointer;
                opacity: 0;
                filter: alpha(opacity=0);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row" style="background: white; margin: 1px;">
            <div class="col-md-12">
                <h3 style="text-align: left">Lost Customer Details File Upload</h3>
                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" CssClass="clsValidator"
                    Width="100%" />
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Left" style="width:100%">

                    <asp:TableRow>
                        <asp:TableCell>
                    
                        </asp:TableCell>

                        <asp:TableCell Style="border: 1px solid #d7d7d7;">
                            <h5 style="color:black !important"><b>&nbsp;&nbsp;Step 1:&nbsp;&nbsp;</b>Create Job Code file in excel and Records should not exceed 5000 [File format should be same as mentioned in the sample file format below]</h5>
				<h5 style="color:black !important"><b>&nbsp;&nbsp;Step 2:&nbsp;&nbsp;</b>Save the excel as .csv(comma delimited)</h5>
                        	<h5 style="color:black !important"><b>&nbsp;&nbsp;Step 3:&nbsp;&nbsp;</b>Upload the .csv file</h5>
                         <%--   <label>select &nbsp;&nbsp;</label>
                            <asp:FileUpload ID="FileUploadJobCode" CssClass="btn btn-primary" runat="server" />--%>
                            <div class="fileUpload btn btn-success" style="width:158px;">
                             <span style="color:white;font-weight:unset !important">Browse document</span>
                            
                        <asp:FileUpload ID="FileUploadJobCode"  CssClass="upload" runat="server" />
                      </div>
                        </asp:TableCell>

                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>

                        <asp:TableCell>
                            <asp:Button ID="btnJobcodeValidate" runat="server" Text="VALIDATE & PREVIEW DATA" OnClick="btnJobcodeValidate_Click"
                                CssClass="btn btn-primary" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" Width="24px" ImageUrl="imgButton/sample.png"
                                ToolTip="Sample File" AlternateText="Sample File" OnClick="ImageButton2_Click" />
                        </asp:TableCell>
                        <asp:TableCell>&nbsp;&nbsp;&nbsp;</asp:TableCell>

                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnUploadJobcode" runat="server" Text="UPLOAD DATA" Width="124px" OnClick="btnUploadJobcode_Click"
                                CssClass="btn btn-success" Enabled="False" />
                        </asp:TableCell>
                        <asp:TableCell>&nbsp;&nbsp;&nbsp;</asp:TableCell>


                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br />
                <asp:Panel runat="server" ID="panel1" Visible="TRUE">
                    <table class="mydatagrid row-padding" style="width: 100%;">
                        <tr>

                            <th>Month</th>
                            <th>Area</th>
                            <th>Dealer</th>
                            <th>Division</th>
                            <th>CSM Name</th>
                            <th>Chassis No</th>
                            <th>Reg No</th>
                            <th>LOB</th>
                            <th>PPL</th>
                            <th>PL</th>
                            <th>Priority</th>
                            <th>Customer List Type</th>
                            <th>Sale Date</th>
                            <th>Account Name</th>
                            <th>Name</th>
                            <th>Main Phone</th>
                            <th>PhoneCell</th>
                            <th>PhoneOff</th>
                            <th>PhoneRes</th>
                            <th>Address Line1</th>
                            <th>Address Line2</th>
                            <th>Last Service KM</th>
                            <th>Last Service Date</th>
                            <th>Month Difference</th>
                            <th>CustContactList</th>
                            <th>Month Difference1</th>
                            

                        </tr>
                        <tr>
                            <td>31-03-2017</td>
                            <td>Cargo Motors</td>
                            <td>1000-CV</td>
                            <td>ABC</td>
                            <td>ABC</td>
                            <td>445010MUZR17569</td>
                            <td>KA25AA5073</td>
                            <td>MCV Cargo</td>
                            <td>LPT1613</td>
                            <td>High</td>
                            <td>Due for Service</td>
                            <td>27-02-2016</td>
                            <td>CARGO MOTORS PVT LTD</td>
                            <td>Ms. CARGO MOTORS  PVT LTD</td>
                            <td>9879602222</td>
                            <td>9879602222</td>
                            <td>9879602224</td>
                            <td>ABC</td>
                            <td>DEFFF</td>
                            <td>1877</td>
                            <td>30-06-2017</td>
                            <td>14</td>
                            <td>1</td>
                            <td>14</td>

                        </tr>
                        <tr>
                            <td>31-03-2017</td>
                            <td>Cargo Motors</td>
                            <td>1000-CV</td>
                            <td>ABC</td>
                            <td>ABC</td>
                            <td>445010MUZR17569</td>
                            <td>KA25AA5073</td>
                            <td>MCV Cargo</td>
                            <td>LPT1613</td>
                            <td>High</td>
                            <td>Due for Service</td>
                            <td>27-02-2016</td>
                            <td>CARGO MOTORS PVT LTD</td>
                            <td>Ms. CARGO MOTORS  PVT LTD</td>
                            <td>9879602222</td>
                            <td>9879602222</td>
                            <td>9879602224</td>
                            <td>ABC</td>
                            <td>DEFFF</td>
                            <td>1877</td>
                            <td>30-06-2017</td>
                            <td>14</td>
                            <td>1</td>
                            <td>14</td>

                        </tr>
                    </table>

                </asp:Panel>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <br />
                <br />
                <asp:Table ID="TableGrid" runat="server" HorizontalAlign="left">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Panel ID="gridPnl1" runat="server" ScrollBars="Auto" CssClass="fullStyle">
                                <asp:GridView ID="gvPMK" runat="server" CellPadding="4" ForeColor="#333333" Width="100%" CssClass="mydatagrid">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkUpload" runat="server" AutoPostBack="True" OnCheckedChanged="chkUpload_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle />
                                    <FooterStyle />
                                    <HeaderStyle />
                                    <PagerStyle />
                                    <RowStyle />
                                    <SelectedRowStyle />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </div>

</asp:Content>
