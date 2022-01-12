<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ModifyTags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.ModifyTags"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="ModifyTagForm" method="post" runat="server">

                <br />
                <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclTags"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:DropDownList ID="TagDropDownList" AutoPostBack="True" runat="server" Width="200"></asp:DropDownList>
                        <asp:Button ID="btnDelete" runat="server" meta:resourcekey="btnDelete" OnClick="BtnDelete_Click" />
                </span>

            </div>
                <asp:HyperLink ID="hlReturnToDetails" meta:resourcekey="hlReturnToDetails" runat="server" Visible="false"></asp:HyperLink>

            <br />
            <br />
            <asp:TextBox ID="tagBody" runat="server" meta:resourcekey="tagBodyTextBox"
                 ></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" meta:resourcekey="btnAdd" OnClick="BtnAdd_Click" />
        </form>
    </div>
</asp:Content>