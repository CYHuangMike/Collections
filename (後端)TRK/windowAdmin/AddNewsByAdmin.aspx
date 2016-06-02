<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="AddNewsByAdmin.aspx.cs" Inherits="windowCompany_AddNewsByAdmin" %>

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

        $(function () {
            //日期選擇器
            $('#startDate').datepicker({
                dateFormat: 'yy/mm/dd', showOn: "both", changeYear: true, yearRange: "c-0:c+50", minDate: "Today"
            });
            $('#endDate').datepicker({
                dateFormat: 'yy/mm/dd', showOn: "both", changeYear: true, yearRange: "c-0:c+50", minDate: "Today"
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">最新消息</h1>
            </div>
            <!-- 帳號管理 -->
            <div class="col-md-6">
                <div class="form-group">
                    <label>標題</label>
                    <asp:TextBox ID="titleInput" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>類型</label>
                    <asp:DropDownList ID="drdtype" runat="server" CssClass="form-control" OnSelectedIndexChanged="drdtype_SelectedIndexChanged"></asp:DropDownList>

                </div>

                <!--datepicker-->
                <div class="form-group">
                    <label>開始日期</label>
                    <asp:TextBox ID="startDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label>結束日期</label>
                    <asp:TextBox ID="endDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                </div>
                <!--datepicker-->
                <div class="form-group">
                    <div>
                        <label>上傳圖片</label>
                    </div>
                    <asp:Image ID="picUP" runat="server" Height="300" Width="300" />
                </div>
                <div class="form-group">
                    <asp:FileUpload ID="FileUpload1" runat="server" BackColor="Gray" onchange="previewFileProduct(this)" ClientIDMode="Static" />
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>內容</label>
                    <div>
                        <asp:TextBox ID="txtNewsContent" runat="server" CssClass="form-control" Rows="10" TextMode="MultiLine" Height="500px"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnNewsdate" runat="server" Text="送出" CssClass="btn btn-default" OnClick="btnNewsdate_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn btn-default" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

    <!--帳號管理end-->

</asp:Content>

