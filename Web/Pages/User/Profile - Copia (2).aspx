<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Profile"
    meta:resourcekey="Page" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">    
    <form id="form1" runat="server">
    <div class="field1">
        <p><asp:Label ID="LblUserName" runat="server" Font-Bold="true"></asp:Label></p>               
    </div>
    
    <div class="field2">
        <asp:Table ID="Table1" runat="server" Width="153px" HorizontalAlign="Center" >
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="HeaderCell1" runat="server">
                    <asp:Label ID="LblFollowed" runat="server" meta:resourcekey="LblFollowedTrans"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell ID="HeaderCell2" runat="server">
                    <asp:Label ID="LblFollowers" runat="server" meta:resourcekey="LblFollowersTrans"></asp:Label>
                </asp:TableHeaderCell> 
            </asp:TableRow>

            <asp:TableRow runat="server">
                <asp:TableCell ID="Cell1" runat="server">
                     <asp:Label ID="LblFollowedText" runat="server" Font-Italic="true"></asp:Label> 
                 </asp:TableCell>                
                <asp:TableCell ID="Cell2" runat="server">
                    <asp:Label ID="LblFollowersText" runat="server" Font-Italic="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
               <asp:Table ID="lclTableImages" runat="server"  HorizontalAlign="Center" meta:resourcekey="lclTableImages" Height="34px" Width="243px">
               <asp:TableHeaderRow ID="Header1" runat="server"> 
                   <asp:TableHeaderCell ID="lclHeaderImage" meta:resourceKey="lclHeaderImage" ></asp:TableHeaderCell>
                   <asp:TableHeaderCell ID="lclHeaderImageLink" meta:resourceKey="lclHeaderImageLink" ></asp:TableHeaderCell>
               </asp:TableHeaderRow>
       </asp:Table>
    </div>


    <div class="fieldFollow">
        <asp:Button ID="BtnUnfollow" runat="server" OnClick="BtnUnfollow_Click" meta:resourcekey="BtnUnfollow" Visible="False"/>
        <br />
        <asp:Button ID="BtnFollow" runat="server" OnClick="BtnFollow_Click" meta:resourcekey="BtnFollow" Visible="False"/>
    </div>
    </form>
</asp:Content>