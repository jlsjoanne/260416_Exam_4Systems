<%@ Page Title="常見問題" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client_Content.aspx.cs" Inherits="_260416_Exam_4Systems.System02_FAQ.Client_Content" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回FAQ分類" OnClick="GoBack_Click" />
        </div>
        <br />
        <h3>
            <asp:Label ID="CategoryName" runat="server"></asp:Label>
        </h3>
        <asp:Repeater ID="FaQRepeater" runat="server">
            <ItemTemplate>
                <b><asp:Label ID="Question" runat="server"
                    Text='<%# Eval("Question") %>' ></asp:Label></b>
                <br />
                <asp:Label ID="Answer" runat="server" 
                    Text='<%# Eval("Answer") %>' ></asp:Label>
                <br />
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </main>
</asp:Content>