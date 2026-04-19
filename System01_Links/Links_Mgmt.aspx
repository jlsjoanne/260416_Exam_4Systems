<%@ Page Title="連結管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links_Mgmt.aspx.cs" Inherits="_260416_Exam_4Systems.System01_Links.Links_Mgmt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="GoBack" runat="server" Text="返回前台" OnClick="GoBack_Click" />
            &emsp; &emsp;
            <asp:Button ID="AddNew" runat="server" Text="新增連結" OnClick="AddNew_Click" />
            &emsp; &emsp;
            <asp:Button ID="CategoryMgmt" runat="server" Text="分類管理" Visible="False" OnClick="CategoryMgmt_Click"/>
        </div>
        <br />
        <div>
            <p><b>連結管理</b></p>
            <asp:DropDownList ID="CategoryDrop" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="CategoryDrop_SelectedIndexChanged"></asp:DropDownList>
            <br /> <br />

            <asp:GridView ID="LinksGrid" runat="server" Width="100%"
                AutoGenerateColumns="False" DataKeyNames="LinkId"
                OnRowEditing="LinksGrid_RowEditing"
                OnRowUpdating="LinksGrid_RowUpdating"
                OnRowCancelingEdit="LinksGrid_RowCancelingEdit"
                OnRowDeleting="LinksGrid_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CategoryName" HeaderText="類別" ReadOnly="True" />
                    <asp:BoundField DataField="LinkName" HeaderText="名稱" />
                    <asp:BoundField DataField="LinkUrl" HeaderText="位址(Url)" />
                    <asp:TemplateField HeaderText="圖片">
                        <ItemTemplate>
                            <asp:Image ID="LinkImg" runat="server"
                                Height="200px" Width="100%"
                                ImageUrl='<%# _260416_Exam_4Systems.SystemMethod.CombinePath("~/Images/",Eval("ImgName")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="排序" DataField="LinkOrder" />
                    <asp:BoundField DataField="PostDate" HeaderText="上傳時間" ReadOnly="True" />
                    <asp:CheckBoxField DataField="IsPublished" HeaderText="是否開放" />
                </Columns>
            </asp:GridView>
        </div>
    </main>
</asp:Content>