<%@ Page Title="相關連結" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Links_Client.aspx.cs" Inherits="_260416_Exam_4Systems.System01_Links.Links_Client" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3><b>相關連結</b></h3>
        <br />
        <asp:LinkButton ID="Mgmt" runat="server" Visible ="False" OnClick="Mgmt_Click">後台管理</asp:LinkButton>
        <br />
        <div>
            <asp:DropDownList ID="CategoryDrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CategoryDrop_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <asp:Repeater ID="RPLinks" runat="server" OnItemDataBound="RPLinks_ItemDataBound">
                <ItemTemplate>
                    <p>
                        <b>
                            <asp:Label ID="LCategory" runat="server"
                                Text='<%# Eval("CategoryName") %>' ></asp:Label>
                       </b>
                        (<asp:Label ID="CategoryCnt" runat="server" 
                            Text='<%# Eval("Cnt") %>' ></asp:Label>)
                    </p>
                    <asp:ListView ID="LVLinks" runat="server" OnItemCommand="LVLinks_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImgBtn" runat="server"
                                        Height="200px" Width="100%"
                                        ImageUrl='<%# _260416_Exam_4Systems.SystemMethod.CombinePath("~/Images/",Eval("ImgName"))%>'
                                        CommandName="ToUrl"
                                        CommandArgument='<%# Eval("LinkUrl") %>'
                                        AlternateText='<%# Eval("LinkName") %>'
                                        OnClientClick="document.forms[0].target='_blank';"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>

