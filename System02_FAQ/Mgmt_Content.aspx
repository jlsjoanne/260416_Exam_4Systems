<%@ Page Title="常見問題管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mgmt_Content.aspx.cs" Inherits="_260416_Exam_4Systems.System02_FAQ.Mgmt_Content" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回分類管理" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增FAQ" OnClick="AddNew_Click" />
        </div>
        <br />
        <h3>
            <asp:Label ID="CName" runat="server"></asp:Label>
            -FAQ管理
        </h3>
        <div>
            <asp:GridView ID="FAQGrid" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="FAQId"
                OnRowEditing="FAQGrid_RowEditing"
                OnRowUpdating="FAQGrid_RowUpdating"
                OnRowCancelingEdit="FAQGrid_RowCancelingEdit"
                OnRowDeleting="FAQGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="Question" HeaderText="問題" />
                    <asp:BoundField DataField="Answer" HeaderText="解答" />
                    <asp:BoundField DataField="PostDate" HeaderText="發表時間" ReadOnly="True" />
                    <asp:CheckBoxField DataField="IsTop" HeaderText="是否置頂" />
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="是否開放" />
                    <asp:BoundField DataField="FAQOrder" HeaderText="FAQ排序" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>
