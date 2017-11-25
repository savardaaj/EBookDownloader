<%@ Page Async="true" Title="EBook Downloader" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="tbDownloadFolder" runat="server" Text="C:\Users\Alex\Downloads\EBooks"/><asp:FileUpload ID="fileUpload" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Book Name: </label>
                </td>
                <td>
                    <asp:TextBox ID="tbBookName" runat="server" Text="Book name..."></asp:TextBox><asp:ImageButton ID="ibtnDownload" runat="server" ImageUrl="~/Images/download30.png" UseSubmitBehavior="false" OnClick="BtnDownload_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Author: </label>
                </td>
                <td>
                    <asp:TextBox ID="tbAuthor" runat="server" Text="Author..."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Kindle Email: </label>
                </td>
                <td>
                    <asp:TextBox ID="tbKindleEmail" runat="server" Text="savarda91_01@kindle.com"></asp:TextBox><asp:ImageButton ID="ibtnEmail" runat="server" ImageUrl="~/Images/email30.png" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Custom File Name: </label>
                </td>
                <td>
                    <asp:TextBox ID="tbCustomName" runat="server" Text="Custom File Name..."></asp:TextBox><asp:ImageButton ID="ibtnDownloadCover" runat="server" ImageUrl="~/Images/downloadCover30.png"/>
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblDownloadPercent" runat="server" Text="Label">0%</asp:Label>

    <asp:TextBox ID="tbLog" runat="server" Height="150" Width="500" TextMode="MultiLine" Rows="10" Columns="10"></asp:TextBox>


    <script src="Scripts/default.js?v=1.0b" type="text/javascript" ></script>
    <link rel="stylesheet" href="Styles/Style.css?v1.0b" type="text/css">
</asp:Content>
