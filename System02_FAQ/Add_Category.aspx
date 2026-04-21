<%@ Page Title="新增分類" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add_Category.aspx.cs" Inherits="_260416_Exam_4Systems.System02_FAQ.Add_Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3><b>新增分類</b></h3>
        <div>
            <b>分類名稱: </b>
            <asp:TextBox ID="CName" runat="server" Width="100%"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>
