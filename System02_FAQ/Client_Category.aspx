<%@ Page Title="常見問題分類" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client_Category.aspx.cs" Inherits="_260416_Exam_4Systems.System02_FAQ.Client_Category" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <asp:Button ID="ToMgmt" runat="server" Visible="False"
                Text="FAQ後台管理" OnClick="ToMgmt_Click" />
        </div>
        <br />
        <h2>常見問題分類</h2>
        <div>
            <asp:Repeater ID="CategoryRP" runat="server" OnItemCommand="CategoryRP_ItemCommand">
                <ItemTemplate>
                    <p>
                        <asp:LinkButton ID="CategoryLink" runat="server"
                            CommandName="ToContent"
                            CommandArgument='<%# Eval("CategoryId") %>' >
                            <asp:Label ID="CName" runat="server" 
                                Text='<%# Eval("CategoryName") %>' ></asp:Label>
                            (<asp:Label ID="CCount" runat="server" 
                                Text='<%# Eval("Cnt") %>' ></asp:Label>)
                        </asp:LinkButton>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>