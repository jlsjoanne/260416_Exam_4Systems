<%@ Page Title="連結分類管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links_Mgmt_Category.aspx.cs" Inherits="_260416_Exam_4Systems.System01_Links.Links_Mgmt_Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="AddNew" runat="server" Text="新增分類" OnClick="AddNew_Click" />
        </div>
        <br />
        <div>
            <p><b>連結分類管理</b></p>
            <asp:GridView ID="CategoryGrid" runat="server" Width="100%"
                AutoGenerateColumns="False" DataKeyNames="CategoryId"
                OnRowEditing="CategoryGrid_RowEditing"
                OnRowUpdating="CategoryGrid_RowUpdating"
                OnRowCancelingEdit="CategoryGrid_RowCancelingEdit"
                OnRowDeleting="CategoryGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CategoryName" HeaderText="分類名稱" />
                    <asp:BoundField DataField="CategoryOrder" HeaderText="分類排序" />
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="是否開放" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>