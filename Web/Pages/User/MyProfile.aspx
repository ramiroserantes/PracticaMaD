<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.MyProfile" 
    meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    - <asp:HyperLink ID="HyperLink1" runat="server"
                        NavigateUrl="~/Pages/User/UpdateUserProfile.aspx"
                        meta:resourcekey="lnkUpdate" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">    
    <form id="form1" runat="server">
    <div class="field1">
        <asp:HyperLink ID="lnkBack" runat="server"
                NavigateUrl="~/Pages/MainPage.aspx"
                meta:resourcekey="lnkBack" />

    </div>
    <div class="field1">
        <p><asp:Label ID="LblUserName" runat="server" Font-Bold="true"></asp:Label></p>               
    </div>
    <div class="field2">
        <asp:Table ID="Table1" runat="server" Width="153px" HorizontalAlign="Center" >
            <asp:TableRow runat="server">
                <asp:TableHeaderCell ID="HeaderCell1" runat="server">
                    <asp:HyperLink ID="Followers" runat="server"
                    NavigateUrl="~/Pages/User/Followers.aspx"
                    meta:resourcekey="lnkFollowers" />
                </asp:TableHeaderCell>

                <asp:TableHeaderCell ID="HeaderCell2" runat="server">
                     <asp:HyperLink ID="HyperLink2" runat="server"
                      NavigateUrl="~/Pages/User/Followeds.aspx"
                      meta:resourcekey="lnkFolloweds" />
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

     



    </form>
</asp:Content>
