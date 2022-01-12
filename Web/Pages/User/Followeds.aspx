<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="Followeds.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Followeds"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
         <div class="field1">
        <asp:HyperLink ID="HyperLink1" runat="server"
                NavigateUrl="~/Pages/MainPage.aspx"
                meta:resourcekey="lnkBack" />

         </div>
        <form id="FollowersForm" method="get" runat="server">
             <asp:HyperLink ID="lnkBack" runat="server" meta:resourcekey="lnkBack"></asp:HyperLink>
            <br />
            <p>
                    <asp:Label ID="lblNoFollowers" meta:resourcekey="lblNoFollowers" runat="server"></asp:Label>
            </p>
            <asp:GridView ID="gvFollowers" runat="server" GridLines="None" AutoGenerateColumns="False"
                Width="100%" OnRowCreated="gvFollowers_RowCreated" >
                <Columns>
                    <asp:BoundField DataField="loginName" HeaderText="<%$ Resources:Common, loginName %>" />
                     <asp:HyperLinkField DataTextField="loginName"
                            HeaderText="<%$ Resources:Common, followers %>"
                            DataNavigateUrlFields="loginName"
                            DataNavigateUrlFormatString="~/Pages/User/Profile.aspx?loginName={0}" />         
                 </Columns>
            </asp:GridView>
          </form>
    </div>

    <!-- "Previous" and "Next" links. -->
</asp:Content>
