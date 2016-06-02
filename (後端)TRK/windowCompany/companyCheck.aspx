<%@ Page Title="" Language="C#" MasterPageFile="~/windowCompany/MasterPageCC.master" AutoEventWireup="true" CodeFile="companyCheck.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="nav-justified">
            <tr>
                <h1 class="page-header">全部活動</h1>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="新增活動" OnClick="btnAdd_Click"/>
                    <asp:Button ID="btnSearch" runat="server" Text="上架活動" OnClick="btnSearch_Click" />
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="下架活動" />
                    <asp:Button ID="btnAll" runat="server" OnClick="btnAll_Click" Text="全部活動" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-hover" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" OnRowCommand="GridView1_RowCommand" DataKeyNames="活動代碼">


                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                         <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>                    
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                        OnClientClick="return confirm('您確定嗎?')"  
                                        CausesValidation="False" CommandName="Delete" Text="刪除" 
                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>

                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table class="nav-justified">
            <tr>
                <h1 class="page-header">待審核</h1>
            </tr>            
            <tr>
                <td>
                    <asp:GridView ID="GridView3" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-hover" AllowPaging="True" AllowSorting="True" DataKeyNames="活動代碼" OnRowCommand="GridView3_RowCommand" OnRowDeleting="GridView3_RowDeleting" OnRowDataBound="GridView3_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                         <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>                            
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                        OnClientClick="return confirm('您確定要刪除嗎?')"  
                                        CausesValidation="False" CommandName="Delete" Text="刪除" 
                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                        NavigateUrl='<%# Eval("活動代碼", "companyUpdate.aspx") %>' Text="" ></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                    
                </td>
            </tr>
        </table>
        <table class="nav-justified">
            <tr>
                <h1 class="page-header">退審</h1>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView4" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table table-hover" DataKeyNames="活動代碼" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound" OnRowDeleting="GridView4_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                         <EmptyDataTemplate>
                        No record availble here
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" 
                                        OnClientClick="return confirm('您確定要刪除嗎?')"  
                                        CausesValidation="False" CommandName="Delete" Text="刪除" 
                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server" 
                                        NavigateUrl='<%# Eval("活動代碼", "companyUpdate.aspx") %>' Text="" ></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                    
                </td>
            </tr>
        </table>        
    </div>
</asp:Content>

