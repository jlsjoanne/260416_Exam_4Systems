<%@ Page Title="相關連結" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links_Client.aspx.cs" Inherits="_260416_Exam_4Systems.System01_Links.Links_Client" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3><b>相關連結</b></h3>
        <br />
        <asp:LinkButton ID="Mgmt" runat="server" Visible ="False" OnClick="Mgmt_Click">後台管理</asp:LinkButton>
        <br />
        <div>
            <asp:DropDownList ID="CategortDrop" runat="server"></asp:DropDownList>
            <br />
            <asp:Repeater ID="RPLinks" runat="server">
                <ItemTemplate>
                    <p>
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                       </b>
                    </p>
                    <asp:ListView ID="LVLinks" runat="server"></asp:ListView>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>

