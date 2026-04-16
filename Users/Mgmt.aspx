<%@ Page Title="用戶帳號管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mgmt.aspx.cs" Inherits="_260416_Exam_4Systems.Users.Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>帳號管理</h3>
        <asp:GridView ID="UserGrid" runat="server" AutoGenerateColumns="False"></asp:GridView>
    </main>
</asp:Content>