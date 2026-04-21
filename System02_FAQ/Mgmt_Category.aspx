<%@ Page Title="FAQ分類管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mgmt_Category.aspx.cs" Inherits="_260416_Exam_4Systems.System02_FAQ.Mgmt_Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回前台" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddCategory" runat="server" Text="新增分類" OnClick="AddCategory_Click" Visible="False" />
            &emsp; &emsp;
            <asp:Button ID="AddFAQ" runat="server" Text="新增FAQ" OnClick="AddFAQ_Click" />
        </div>
        <br />
        <div>
            <h3>分類管理</h3>
            <asp:GridView ID="CategoryGrid" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="CategoryId"
                OnRowEditing="CategoryGrid_RowEditing"
                OnRowUpdating="CategoryGrid_RowUpdating"
                OnRowCancelingEdit="CategoryGrid_RowCancelingEdit"
                OnRowDeleting="CategoryGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" />
                    <asp:BoundField DataField="CategoryName" HeaderText="分類名稱" />
                    <asp:BoundField DataField="CategoryOrder" HeaderText="分類排序" />
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="是否開放" />
                    <asp:HyperLinkField DataNavigateUrlFields="CategoryId" DataNavigateUrlFormatString="Mgmt_Content.aspx?CategoryId={0}" HeaderText="分類FAQ" Text="Manage" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
