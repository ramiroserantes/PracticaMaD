<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="PhotoComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.PhotoComments"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="PhotoCommentForm" method="get" runat="server">
             <asp:HyperLink ID="lnkBack" runat="server" meta:resourcekey="lnkBack"></asp:HyperLink>
            <br />
            <asp:Table ID="ownComment" runat="server" Visible="false" Width="100%">
                <asp:TableHeaderRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOwnCommentProfileName" runat="server"
                        Text="<%$ Resources:Common, loginName %>"></asp:TableHeaderCell>

                    <asp:TableHeaderCell ID="cellCaptionOwnCommentBody" runat="server"
                        Text="<%$ Resources:Common, commentDescription %>"></asp:TableHeaderCell>

                    <asp:TableHeaderCell ID="cellCaptionOwnCommentDate" runat="server"
                        Text="<%$ Resources:Common, commentDate %>"></asp:TableHeaderCell>

                </asp:TableHeaderRow>
                <asp:TableRow runat="server">
                    <asp:TableCell ID="cellOwnCommentId" runat="server" Visible="false"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentProfileName" runat="server"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentBody" runat="server"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentDate" runat="server"></asp:TableCell>

                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Button ID="btnDelete" runat="server" meta:resourcekey="btnDelete" OnClick="BtnDelete_Click" />
            <asp:Button ID="btnModify" runat="server" meta:resourcekey="btnModify" OnClick="BtnModify_Click" />
            <br />
            <br />
            <p>
                <asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server"></asp:Label>
            </p>
            <br />
            <asp:GridView ID="gvPhotos" runat="server" GridLines="None" AutoGenerateColumns="False"
                Width="100%" OnRowCreated="gvPhotos_RowCreated" OnRowDataBound="gvPhotos_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="commentId" ItemStyle-CssClass="hiddencol"/>
                    <asp:BoundField DataField="UserProfile.loginName" HeaderText="<%$ Resources:Common, loginName %>" />
                     <asp:HyperLinkField DataTextField="userName"
                            HeaderText="<%$ Resources:Common, user %>"
                            DataNavigateUrlFields="userName"
                            DataNavigateUrlFormatString="~/Pages/User/Profile.aspx?loginName={0}" />
                    <asp:BoundField DataField="commentDescription" HeaderText="<%$ Resources:Common, commentDescription %>" />
                    <asp:BoundField DataField="commentDate" HeaderText="<%$ Resources:Common, commentDate %>"
                        DataFormatString="{0:d/M/yyyy}" />
                   
                </Columns>
            </asp:GridView>
        </form>
    </div>

    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>"
                runat="server" Visible="False"></asp:HyperLink>
        </span>
        <span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>"
                runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
</asp:Content>

