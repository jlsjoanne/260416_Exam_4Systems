<%@ Page Title="新增分類" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links_Add_Category.aspx.cs" Inherits="_260416_Exam_4Systems.System01_Links.Links_Add_Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>新增連結分類</h3>
        <div>
            <b>分類名稱: </b>
            <asp:TextBox ID="CName" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>