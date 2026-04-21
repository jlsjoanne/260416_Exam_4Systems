<%@ Page Title="新增常見問題" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add_Content.aspx.cs" Inherits="_260416_Exam_4Systems.System02_FAQ.Add_Content" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3><b>新增常見問題</b></h3>
        <div>
            <b>分類: </b>
            <asp:DropDownList ID="CategoryDrop" runat="server"></asp:DropDownList>
            <br />
            <b>問題: </b>
            <asp:TextBox ID="QInput" runat="server" 
                TextMode="MultiLine" Rows="5"></asp:TextBox>
            <br />
            <b>答案: </b>
            <asp:TextBox ID="AInput" runat="server"
                TextMode="MultiLine" Rows="10"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>