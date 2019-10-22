<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule.master" AutoEventWireup="true" CodeFile="KPI_New.aspx.cs" Inherits="KPI_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .mydatagrid a /** FOR THE PAGING ICONS  **/
{
	background-color: Transparent;
	
	color: #fff;
	text-decoration: none;
	font-weight: bold;
}
.mydatagrid th /** FOR THE PAGING ICONS  **/
{
	border:1px solid #e5e5e4 !important;
    border-bottom:2px solid #e5e5e4 !important;
	    text-align: right;
    font-weight: 700!important;
    color: #000;
    font-size: 14px !important;
    /*text-align:left;*/
    text-transform:uppercase;
    /*background-color:#CC3333 !important*/
	
}
.mydatagrid td{
    text-align:left;
}

.mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/
{
	/*background-color: #000;
	color: #fff;*/
}
.mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
{
	background-color: #c9c9c9;
	color: #000;

}
.pager
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	height: 30px;
	text-align: left;
}

.mydatagrid td
{
	text-align: right;
    font-size: 13px!important;
    font-weight: 400!important;
    font-family: "Lato", sans-serif;
    border-bottom:1px solid #e5e5e4 !important;
}
.mydatagrid tr:nth-child(even)
    {
        background-color:#ffffff !important;
    }
.mydatagrid tr:nth-child(odd)
    {
        background-color:#f9f9f9 !important;
    }
span{
    text-transform:unset !important;
}
 .mydatagrid a, .mydatagrid span
        {
            display: block;
            /*height: 15px;*/
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }
        .mydatagrid a
        {
            background-color: #f5f5f5;
            color: #969696;
            /*border: 1px solid #969696;*/
        }
        .mydatagrid span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="grid-div">
      <asp:Label ID="lblLastUpdated" runat="server" ></asp:Label><br /><br />
    <asp:GridView ID="GridView1" CssClass="mydatagrid" runat="server" OnRowDataBound="GridView1_RowDataBound">
     
    </asp:GridView>
        </div>
   
</asp:Content>

