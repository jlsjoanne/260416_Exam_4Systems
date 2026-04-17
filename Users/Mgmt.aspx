<%@ Page Title="用戶帳號管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mgmt.aspx.cs" Inherits="_260416_Exam_4Systems.Users.Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>帳號管理</h3>
        <asp:GridView ID="UserGrid" runat="server" 
            AutoGenerateColumns="False" DataKeyNames="UserId"
            OnRowEditing="UserGrid_RowEditing"
            OnRowUpdating="UserGrid_RowUpdating"
            OnRowCancelingEdit="UserGrid_RowCancelingEdit"
            OnRowDeleting="UserGrid_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="UserName" HeaderText="帳號" />
                <asp:TemplateField HeaderText="密碼">
                    <ItemTemplate>
                        <asp:Label ID="LabelPwd" runat="server" Text="******"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TBPwd" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RoleId" HeaderText="權限代號" />
                <asp:BoundField DataField="RoleName" HeaderText="權限名稱" ReadOnly="True" />
                <asp:BoundField DataField="CreatedAt" HeaderText="創建日期" ReadOnly="True" />
            </Columns>
    </asp:GridView>
    </main>
</asp:Content>