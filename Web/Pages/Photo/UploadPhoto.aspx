<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="UploadPhoto.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Photo.UploadPhoto"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="UploadPhotoForm" method="post" runat="server">
            <p>
                
                <asp:Label ID="lblNoPhoto" meta:resourcekey="lblNoPhoto" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblUnlogedUser" meta:resourcekey="lblUnlogedUser" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:HyperLink ID="hlReturnToDetails" meta:resourcekey="hlReturnToDetails" runat="server" Visible="false"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="lnkBack" runat="server" meta:resourcekey="lnkBack"></asp:HyperLink>
            </p>
            <br />
            <br />

            <div class="entry">
                <span class="entry">
                <asp:FileUpload ID="ImageLoader" runat="server"></asp:FileUpload>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ImageLoader" Display="Static"
                         ErrorMessage="<%$ resources:Common, RequiredParam%>" ForeColor="Red" />
                    </span>
            </div>
            
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:DropDownList ID="CategoryDropDownList" AutoPostBack="True" runat="server" Width="200"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="CategoryDropDownList" InitialValue="0"
                         ErrorMessage="<%$ resources:Common, RequiredParam%>" ForeColor="Red" />
                </span>
            </div>
             <div class="field">
                <span class="label">
                    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclTags"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:DropDownList ID="DropDownList1" AutoPostBack="True" runat="server" Width="200"></asp:DropDownList>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:TextBox ID="txtTitle" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:TextBox ID="txtDesc" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclDiaphragm" runat="server" meta:resourcekey="lclDiaphragm"></asp:Localize>
                        </span>
                    <span class="entry">
                        <asp:TextBox ID="txtDia" runat="server" Width="200"></asp:TextBox>
                        <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtDia" Type="Integer" Operator="DataTypeCheck"
                         ErrorMessage="<%$ resources:Common, InputError%>" ForeColor="Red" /> 
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclExhibition" runat="server" meta:resourcekey="lclExhibition"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:TextBox ID="txtExhi" runat="server" Width="200"></asp:TextBox>
                    <asp:CompareValidator ID="cv2" runat="server" ControlToValidate="txtExhi" Type="Integer" Operator="DataTypeCheck"
                         ErrorMessage="<%$ resources:Common, InputError%>" ForeColor="Red"/> 
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclIso" runat="server" meta:resourcekey="lclIso"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:TextBox ID="txtIso" runat="server" Width="200"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclBalance" runat="server" meta:resourcekey="lclBalance"></asp:Localize>
                        </span>
                    <span class="entry">
                    <asp:TextBox ID="txtBalance" runat="server" Width="200"></asp:TextBox>
                    <asp:CompareValidator ID="cv3" runat="server" ControlToValidate="txtBalance" Type="Integer" Operator="DataTypeCheck"
                         ErrorMessage="<%$ resources:Common, InputError%>" ForeColor="Red" />
                </span>
            </div>
            
            <div class="button">
                <span class="button">
                <asp:Button ID="btnUploadPhoto" runat="server" OnClick="BtnUploadPhotoClick" meta:resourcekey="btnUploadPhoto" />
                </span>
            </div>
        </form>
    </div>
</asp:Content>
