<%@ Page Title="修改密碼" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPwd.aspx.cs" Inherits="_260416_Exam_4Systems.Users.ResetPwd" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>修改密碼</h3>
        <div>
            <b>原密碼: </b> &emsp; &emsp;
            <asp:TextBox ID="OldPwd" runat="server"></asp:TextBox>
            <br />
            <b>新密碼: </b> &emsp; &emsp;
            <asp:TextBox ID="NewPwd" runat="server"></asp:TextBox>
            <br />
            <b>確認新密碼: </b>
            <asp:TextBox ID="ConfirmNewPwd" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>