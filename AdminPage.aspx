<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModule1.master" AutoEventWireup="true"
    CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 8 || evt.keyCode == 46)) { return false; }
        }

        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td valign="middle" align="center">
                <asp:Label ID="Label1" runat="server" Text="Welcome To Admin" Font-Size="22px"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>