<%@ Page Title="新增連結" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links_Add.aspx.cs" Inherits="_260416_Exam_4Systems.System01_Links.Links_Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3><b>新增連結</b></h3>
        <div>
            <b>連結分類: </b>
            <asp:DropDownList ID="CategoryDrop" runat="server" 
                DataSourceID="CategoryDS"
                DataTextField="CategoryName"
                DataValueField="CategoryId"></asp:DropDownList>
            <asp:SqlDataSource ID="CategoryDS" runat="server" ConnectionString="<%$ ConnectionStrings:SystemsDB %>" SelectCommand="SELECT * FROM [Link_Category]"></asp:SqlDataSource>
            <br />
            <b>連結名稱: </b>
            <asp:TextBox ID="LName" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>連結位址: </b>
            <asp:TextBox ID="LUrl" runat="server" Width="100%"></asp:TextBox>
            <br />
            <b>上傳圖片: </b>
            <asp:FileUpload ID="ImgUpload" runat="server" />
        </div>
        <br />
        <div>
            <asp:Button ID="Submit" runat="server" Text="送出" OnClick="Submit_Click" />
        </div>
    </main>
</asp:Content>