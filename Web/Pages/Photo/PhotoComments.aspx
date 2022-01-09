<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="PhotoComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.PhotoComments"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
            <asp:GridView ID="gvPhotos" runat="server" GridLines="None" AutoGenerateColumns="False"
                Width="100%" OnRowCreated="gvPhotos_RowCreated" OnRowDataBound="gvPhotos_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="commentId" ItemStyle-CssClass="hiddencol"/>
                    <asp:BoundField DataField="UserProfile.loginName" HeaderText="<%$ Resources:Common, loginName %>" />
                    <asp:BoundField DataField="comment1" HeaderText="<%$ Resources:Common, commentBody %>" />
                    <asp:BoundField DataField="commentDate" HeaderText="<%$ Resources:Common, commentDate %>"
                        DataFormatString="{0:d/M/yyyy}" />
                    <asp:TemplateField HeaderText="<%$ Resources:Common, commentTags %>">
                            <ItemTemplate>
                                <asp:TextBox ID="TextTags" runat="server" style="resize:none" BackColor="White" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                            </ItemTemplate>
                    </asp:TemplateField> 
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
