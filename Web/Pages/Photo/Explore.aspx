<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="Explore.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.Explore" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="PhotoExplorerForm" method="get" runat="server">
            <div></div><asp:HyperLink ID="lnkBack" runat="server"
                NavigateUrl="~/Pages/MainPage.aspx"
                meta:resourcekey="lnkBack" /><div />
                    <asp:Localize ID="lclSearch" runat="server" meta:resourcekey="lclSearch" />
                <span class="entry">
                    <asp:TextBox ID="txtSearch" runat="server" Width="20%" Columns="16" meta:resourcekey="txtSearch"  style="text-align:center"></asp:TextBox>
                    <asp:DropDownList ID="CategoryDropDownList" AutoPostBack="False" runat="server" Width="20%"></asp:DropDownList>
                </span>
            <br />
                <span class="entry">
                    <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchClick" meta:resourcekey="btnSearch" />
                </span>
            <br />
           
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
                            DataNavigateUrlFormatString="~/Pages/Photo/PhotoDetails.aspx?photo={0}" />
                        <asp:BoundField DataField="photoDate" HeaderText="<%$ Resources:Common, photoDate %>"
                            DataFormatString="{0:d/M/yyyy}" />
                        <asp:ImageField HeaderText = "Link" DataImageUrlField="photoId" DataImageUrlFormatString="~/Images/{0}.jpg"  ControlStyle-Height="100px" ControlStyle-Width="100px"/>
           
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

