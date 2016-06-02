<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="ManliCard.aspx.cs" Inherits="windowAdmin_ManliCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <h1 class="page-header">曼麗卡管理</h1>
    </div>
    <!-- 帳號管理 -->

    <div class="col-lg-4">
        <table class="nav-justified">
            <tr>
                <td>
                    <asp:Label ID="卡號" runat="server" Text="卡號"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtMcard" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="查詢" CssClass="btn btn-default" OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="清除" OnClick="btnCancel_Click" Height="38px" Width="64px" CssClass="btn btn-default" />                    
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                </td>
            </tr>

            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:prjTRKConn %>" DeleteCommand="DELETE FROM [ManliCard] WHERE [cardNumber] = @cardNumber" InsertCommand="INSERT INTO [ManliCard] ([cardNumber], [cardPoint]) VALUES (@cardNumber, @cardPoint)" SelectCommand="SELECT * FROM [ManliCard]" UpdateCommand="UPDATE [ManliCard] SET [cardPoint] = @cardPoint WHERE [cardNumber] = @cardNumber">
                        <DeleteParameters>
                            <asp:Parameter Name="cardNumber" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="cardNumber" Type="Int32" />
                            <asp:Parameter Name="cardPoint" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="cardPoint" Type="String" />
                            <asp:Parameter Name="cardNumber" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-striped" DataKeyNames="cardNumber" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField DataField="cardNumber" HeaderText="卡號" ReadOnly="True" SortExpression="cardNumber" />
                            <asp:BoundField DataField="cardPoint" HeaderText="點數" SortExpression="cardPoint" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎?')"></asp:LinkButton>
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



    <!--帳號管理end-->

</asp:Content>

