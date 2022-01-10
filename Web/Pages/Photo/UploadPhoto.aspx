<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="UploadPhoto.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.UploadPhoto"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="UploadPhotoForm" method="post" runat="server">
            <p>
                
                <asp:Label ID="lblNoPhoto" meta:resourcekey="lblNoPhoto" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblUnlogedUser" meta:resourcekey="lblUnlogedUser" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:HyperLink ID="hlReturnToDetails" meta:resourcekey="hlReturnToDetails" runat="server" Visible="false"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="lnkBack" runat="server" meta:resourcekey="lnkBack"></asp:HyperLink>
            </p>
            <br />
            <br />
            <div class="field">
                <span class="label">
                    <asp:DropDownList ID="CategoryDropDownList" AutoPostBack="False" runat="server" Width="200"></asp:DropDownList>
                </span>
            </div>
            <asp:FileUpload ID="ImageLoader" runat="server"></asp:FileUpload>
            <asp:Image ID="Image1" runat="server" Height = "100" Width = "100" />
            <div class="field">
                <span class="label">
                    <asp:TextBox ID="txtTitle" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:TextBox ID="txtDesc" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:TextBox Text="hola" ID="txtDia" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:TextBox ID="txtLink" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:TextBox ID="txtExhi" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:TextBox ID="txtIso" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:TextBox ID="txtBalance" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            
            <div class="button">
                <span class="button">
                <asp:Button ID="btnUploadPhoto" runat="server" OnClick="BtnUploadPhotoClick" meta:resourcekey="btnUploadPhoto" />
                </span>
            </div>
        </form>
    </div>
</asp:Content>
