﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Explore.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.Explore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="PhotoExplorerForm" method="get" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSearch" runat="server" meta:resourcekey="lclSearch" />
                </span>

                <span class="entry">
                    <asp:TextBox ID="txtSearch" runat="server" Width="30%" Columns="16" meta:resourcekey="txtSearch"></asp:TextBox>
                    <asp:DropDownList ID="CategoryDropDownList" AutoPostBack="False" runat="server" Width="30%"></asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchClick" meta:resourcekey="btnSearch" Width="30%" />
                </span>
            </div>
            <br />
            <br />
            <asp:HyperLink ID="lnkBack" runat="server"
                NavigateUrl="~/Pages/MainPage.aspx"
                meta:resourcekey="lnkBack" />
            <br />
            <br />
            <div>
                <p>
                    <asp:Label ID="lblNoPhotos" meta:resourcekey="lblNoPhotos" runat="server"></asp:Label>
                </p>
                <asp:GridView ID="gvPhotos" runat="server" GridLines="None" AutoGenerateColumns="False" Width="100%"
                    OnRowDataBound="gvPhotos_RowDataBound" OnRowCreated="gvPhotos_RowCreated" ViewStateMode="Disabled">
                    <Columns>
                        <asp:BoundField DataField="photoId" ItemStyle-CssClass="hiddencol" />
                        <asp:HyperLinkField DataTextField="title"
                            HeaderText="<%$ Resources:Common, title %>"
                            DataNavigateUrlFields="photoId"
                            DataNavigateUrlFormatString="/Pages/Photo/PhotoDetails.aspx?photo={0}" />
                        <asp:BoundField DataField="Category.categoryType"
                            HeaderText="<%$ Resources:Common, categoryType %>" />
                        <asp:BoundField DataField="photoDate" HeaderText="<%$ Resources:Common, photoDate %>"
                            DataFormatString="{0:d/M/yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- "Previous" and "Next" links. -->
            <div class="previousNextLinks">
                <span class="previousLink">
                    <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>"
                        runat="server" Visible="False"></asp:HyperLink>
                </span>
                <span class="nextLink">
                    <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server"
                        Visible="False"></asp:HyperLink>
                </span>
            </div>
        </form>
    </div>

</asp:Content>
