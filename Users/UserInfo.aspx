<%@ Page Title="帳號資訊" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="_260416_Exam_4Systems.Users.UserInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>帳號資訊</h2>
        <div>
            <asp:Button ID="GoHome" runat="server" Text="返回首頁" OnClick="GoHome_Click" />
        </div>
        <br />
        <div>
            <asp:Label ID="Username" runat="server" Text="帳號: " Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Role" runat="server" Text="權限: "></asp:Label>
        </div>
        <br />
        <div>
            <asp:LinkButton ID="PwdReset" runat="server" Visible="False" OnClick="PwdReset_Click">修改密碼</asp:LinkButton>
            <br />
            <asp:LinkButton ID="Management" runat="server" Visible="False" OnClick="Management_Click">用戶帳號管理</asp:LinkButton>
        </div>
        <br />
        <div>
            <asp:LinkButton ID="LogOut" runat="server" Visible="False" OnClick="LogOut_Click">登出</asp:LinkButton>
        </div>
    </main>
</asp:Content>