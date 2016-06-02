<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="companyCheckforAdmin.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">審核狀態
            </h1>
        </div>
        <table class="nav-justified">
            <tr>
                <td>
                    <asp:Button ID="btnAll" runat="server" Text="全部活動" OnClick="btnAll_Click" />
                    <asp:Button ID="btnOnshelf" runat="server" Text="上架活動" OnClick="btnOnshelf_Click" />
                    <asp:Button ID="btnoffshelf" runat="server" Text="下架活動" OnClick="btnoffshelf_Click" />
                    <asp:Button ID="btnAdd" runat="server" Text="審查活動" OnClick="btnCheck_Click" />
                </td>
            </tr>

            <tr>
                <td>

                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="table table-hover" ForeColor="#333333" GridLines="None" DataKeyNames="活動代碼"  >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("活動代碼", "WfrmNewsDetail.aspx?fId={0}") %>' Text="詳細內容"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
                        <tr>
                <td>
                    <h1 class="page-header">上架
            </h1>
                </td>
                </tr>
            <tr>
                <td>

                    <asp:GridView ID="GridView2" runat="server" CellPadding="4" CssClass="table table-hover" ForeColor="#333333" GridLines="None" DataKeyNames="活動代碼"  >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                         <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("活動代碼", "WfrmNewsDetail.aspx?fId={0}") %>' Text="詳細內容"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
                        <tr>
                <td>
                    <h1 class="page-header">下架
            </h1>
                </td>
                </tr>
            <tr>
                <td>

                    <asp:GridView ID="GridView3" runat="server" CellPadding="4" CssClass="table table-hover" ForeColor="#333333" GridLines="None" DataKeyNames="活動代碼"  >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                         <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("活動代碼", "WfrmNewsDetail.aspx?fId={0}") %>' Text="詳細內容"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
                        <tr>
                <td>
                    <h1 class="page-header">退審
            </h1>
                </td>
                </tr>
            <tr>
                <td>

                    <asp:GridView ID="GridView4" runat="server" CellPadding="4" CssClass="table table-hover" ForeColor="#333333" GridLines="None" DataKeyNames="活動代碼"  >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                         <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("活動代碼", "WfrmNewsDetail.aspx?fId={0}") %>' Text="詳細內容"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
        </table>
    </div>


</asp:Content>

