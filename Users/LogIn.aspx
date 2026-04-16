<%@ Page Title="登入" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="_260416_Exam_4Systems.Users.LogIn" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>登入</h3>
        <br />
        <div>
            <b>帳號: </b> &emsp;
            <asp:TextBox ID="Username" runat="server"></asp:TextBox>
            <br />
            <br />
            <b>密碼:</b> &emsp;
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
        <br />
        <div>
            <asp:LinkButton ID="ToSignUp" runat="server" OnClick="ToSignUp_Click">尚未註冊? 前往註冊</asp:LinkButton>
        </div>
    </main>
</asp:Content>