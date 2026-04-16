<%@ Page Title="註冊" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="_260416_Exam_4Systems.Users.SignUp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>註冊帳號</h3>
        <br />
        <div>
            <b>帳號:</b> &emsp; &emsp;
            <asp:TextBox ID="Username" runat="server"></asp:TextBox>
            <br /> <br />
            <b>密碼:</b> &emsp; &emsp;
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <br /> <br />
            <b>確認密碼:</b> &nbsp;
            <asp:TextBox ID="PwdConfirm" runat="server" TextMode="Password"></asp:TextBox>
            
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>