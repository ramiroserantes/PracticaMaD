﻿<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ModifyComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.ModifyComment"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="ModifyCommentForm" method="post" runat="server">
            <p>
                <asp:Label ID="lblNoComment" meta:resourcekey="lblNoComment" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblUnlogedUser" meta:resourcekey="lblUnlogedUser" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <br />
                <asp:HyperLink ID="hlReturnToDetails" meta:resourcekey="hlReturnToDetails" runat="server" Visible="false"></asp:HyperLink>
            </p>
            <br />
            <br />
            <asp:TextBox ID="commentBody" runat="server" meta:resourcekey="commentBodyTextBox"
                Width="50%" TextMode="MultiLine"></asp:TextBox>
            <div class="button">
                <asp:Button ID="btnEditComment" runat="server" OnClick="BtnEditCommentClick" meta:resourcekey="btnEditComment" />
            </div>
        </form>
    </div>
</asp:Content>