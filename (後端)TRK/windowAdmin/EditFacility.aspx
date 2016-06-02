<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="EditFacility.aspx.cs" Inherits="windowAdmin_EditFacility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function previewFileProduct() {
            var preview = document.querySelector('#<%=picUP.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <div class="col-lg-12">
            <h1 class="page-header">設施管理
            </h1>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <asp:Label ID="Label3" runat="server" Text="設施ID" Style="font-family: 微軟正黑體; font-size: large"></asp:Label>
                <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="Label1" runat="server" Text="設施名稱" Style="font-family: 微軟正黑體; font-size: large"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                <br />               
                <asp:Image ID="picUP" runat="server" CssClass="preview" Height="300" Width="300" ClientIDMode="Static" /><br />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="upl" ClientIDMode="Static" />
                <br/>
            <asp:Label ID="Label2" runat="server" Text="特色內容" Style="font-family: 微軟正黑體; font-size: large"></asp:Label>
                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Width="100%" Height="300px" TextMode="MultiLine"></asp:TextBox>
                <div class="size"></div>
                <br />
                <asp:Button ID="btnInsert" runat="server" Text="新增" CssClass="btn btn-default active" OnClick="btnInsert_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="btn btn-default active" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnClear" runat="server" Text="清除" CssClass="btn btn-default active" OnClick="btnClear_Click" />
            </div>
            <br />
            <div class="col-sm-6">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:prjTRKConn %>" DeleteCommand="DELETE FROM [Facility] WHERE [facilityID] = @facilityID" InsertCommand="INSERT INTO [Facility] ([facilityID], [name], [content], [pic]) VALUES (@facilityID, @name, @content, @pic)" SelectCommand="SELECT * FROM [Facility] ORDER BY [facilityID]" UpdateCommand="UPDATE [Facility] SET [name] = @name, [content] = @content, [pic] = @pic WHERE [facilityID] = @facilityID">
                    <DeleteParameters>
                        <asp:Parameter Name="facilityID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="facilityID" Type="Int32" />
                        <asp:Parameter Name="name" Type="String" />
                        <asp:Parameter Name="content" Type="String" />
                        <asp:Parameter Name="pic" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="name" Type="String" />
                        <asp:Parameter Name="content" Type="String" />
                        <asp:Parameter Name="pic" Type="String" />
                        <asp:Parameter Name="facilityID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="facilityID" DataSourceID="SqlDataSource1" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="facilityID" HeaderText="編號" ReadOnly="True" SortExpression="facilityID" />
                        <asp:BoundField DataField="name" HeaderText="設施名稱" SortExpression="name" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('你確定要刪除嗎?')"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

</asp:Content>

