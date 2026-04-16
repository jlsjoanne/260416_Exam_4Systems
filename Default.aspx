<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_260416_Exam_4Systems._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h2>四大系統考試</h2>
        <p>Welcome 
            <asp:Label ID="Username" runat="server" Visible="False"></asp:Label>!
        </p>
        <div>
            <asp:LinkButton ID="LogIn" runat="server">登入</asp:LinkButton>
            <br />
            <asp:LinkButton ID="SignUp" runat="server">註冊</asp:LinkButton>
        </div>
        <div>
            <asp:LinkButton ID="LogOut" runat="server" Visible="False">登出</asp:LinkButton>
        </div>
    </main>

</asp:Content>
