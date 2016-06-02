<%@ Page Title="" Language="C#" MasterPageFile="~/windowCompany/MasterPageCC.master" AutoEventWireup="true" CodeFile="companyDetail.aspx.cs" Inherits="companyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }

        .auto-style2 {
            color: #FF0000;
        }
    </style>
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
            $('#btnNewsUpdate').popover('show')
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">廠商帳號</h1>
             <asp:Button ID="btnpasswordchange" runat="server" Text="更改密碼" CssClass="btn btn-default" OnClick="btnpasswordchange_Click" />
        </div>
    </div>
    <!-- 帳號管理 -->
    <div class="col-lg-5">
        <div class="form-group">
            <label>櫃位 <span class="auto-style2">* 不可更改</span></label>
            <asp:TextBox ID="txtCounter" runat="server" CssClass="auto-style1" Width="500px" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>廠商名稱</label>
            <br />
            <asp:TextBox ID="txtName" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>E-mail <span class="auto-style2">* 不可更改</span></label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="auto-style1" Width="500px" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>備用信箱</label>
            <asp:TextBox ID="txtSpareEmail" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>負責人</label>
            <asp:TextBox ID="txtManager" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>連絡電話</label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>地址</label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
        </div>
    </div>
    <div class="col-lg-5">
        <%--  <form role="form">--%>

        <div class="form-group">
            <div>
                <label>上傳圖片</label>
            </div>
            <asp:Image ID="picUP" runat="server" Height="300" Width="300" />
        </div>
        <div class="form-group">
            <asp:FileUpload ID="FileUpload1" runat="server" BackColor="Gray" onchange="previewFileProduct(this)" ClientIDMode="Static" />
        </div>
        <div class="form-group">
            <label>簡介</label>
            <br />
            <asp:TextBox ID="txtContent" runat="server" CssClass="form-group" Width="500px" Height="85px" Rows="5" TextMode="MultiLine"></asp:TextBox>
        </div>
        <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="btn btn-default" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn btn-default" OnClick="btnCancel_Click" />
        <%--    </form>--%>
    </div>
    <!--帳號管理end-->
</asp:Content>
