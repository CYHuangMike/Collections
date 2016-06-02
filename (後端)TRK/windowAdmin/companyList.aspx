<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="companyList.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            color: #FF3300;
            font-size: x-large;
        }

        .auto-style2 {
            font-size: x-large;
            color: #0000FF;
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">廠商帳號列表</h1>
        </div>
        <br />
        
        <hr />
     
        <table class="nav-justified">
            <tr>
                <td class="auto-style2"><strong>已配櫃廠商</strong>
                    <strong class="auto-style1">廠商名稱關鍵字:</strong>
                    <asp:TextBox ID="txtCname" runat="server" Width="201px"></asp:TextBox>
                    <asp:Button ID="BtnSearch" runat="server" Text="查詢" OnClick="BtnSearch_Click" />
                    <asp:Label ID="txtMsg" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:prjTRKConn %>" SelectCommand="SELECT * FROM [View_Company_Counter]"></asp:SqlDataSource>

        

                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-hover" DataKeyNames="廠商編號">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="廠商編號,櫃位號碼" DataNavigateUrlFormatString="CompanyDetailforAdmin.aspx?id={0}&amp;counter={1}" Text="編輯內容" />
                            <asp:BoundField DataField="廠商編號" HeaderText="廠商編號" SortExpression="廠商編號" ReadOnly="True" />
                            <asp:BoundField DataField="廠商名稱" HeaderText="廠商名稱" SortExpression="廠商名稱" />
                            <asp:BoundField DataField="負責人" HeaderText="負責人" SortExpression="負責人" />
                            <asp:BoundField DataField="櫃位號碼" HeaderText="櫃位號碼" ReadOnly="True" SortExpression="櫃位號碼" />
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
        <hr />
         <table class="nav-justified">
            <tr>
                <td class="auto-style1"><strong>尚未配櫃廠商</strong></td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:prjTRKConn %>" SelectCommand="SELECT * FROM [View_Company_NoCounter]"></asp:SqlDataSource>

                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="Black" GridLines="Vertical" CssClass="table table-hover" PageSize="5">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="廠商編號" DataNavigateUrlFormatString="CompanyDetailforAdmin.aspx?id={0}" Text="編輯內容" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="center" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        
                <br />
        <br />
    </div>
</asp:Content>

