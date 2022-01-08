<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="PhotoDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.PhotoDetails" meta:resourcekey="Page" 
    %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <asp:HyperLink ID="lnkBack" runat="server" 
            NavigateUrl="~/Pages/Photo/Explore.aspx"
            meta:resourcekey="lnkBack"/>
    <br />
    <br />
    <div class="errors">
        <asp:Label ID="lblPhotoError" runat="server" ForeColor="Red" Style="position: relative"
        Visible="False" meta:resourcekey="lblPhotoError"></asp:Label>
    </div>
    <br />
 
    <asp:Table ID="TablePhotoInfo" runat="server" Width="100%">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell ID="cellCaptionPhotoID" runat="server"
                Text="<%$ Resources:Common, photoId %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionPhotoTitle" runat="server"
                Text="<%$ Resources:Common, title %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionPhotoCategory" runat="server"
                Text="<%$ Resources:Common, categoryType %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionPhotoDate" runat="server"
                Text="<%$ Resources:Common, photoDate %>"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellPhotoID" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellPhotoTitle" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellCategoryType" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellPhotoDate" runat="server"></asp:TableCell>

        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <div class="field">
        <asp:HyperLink ID="hlAddComment" runat="server" meta:resourcekey="hlAddComment" />
        <asp:Label ID="lblDash1" runat="server" Text=" - "></asp:Label>
        <asp:HyperLink ID="hlComments" runat="server" meta:resourcekey="hlComments" />
        <asp:HyperLink ID="hlUser" runat="server" meta:resourcekey="hlUser" />
    </div>
    <br />

</asp:Content>
