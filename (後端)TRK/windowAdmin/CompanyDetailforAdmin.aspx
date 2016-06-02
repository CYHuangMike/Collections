<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CompanyDetailforAdmin.aspx.cs" Inherits="CompanyDetailforAdmin" EnableEventValidation="false" %>

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
            <h1 class="page-header">廠商帳號
            </h1>
        </div>
    </div>
    <!-- 帳號管理 -->
    <div class="col-lg-5">
        <div class="form-group">
            <asp:DropDownList ID="comTypeList" runat="server" CssClass="form-control" OnSelectedIndexChanged="comTypeList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label>櫃位 <span class="auto-style2"></span></label>
            <br />
            <asp:DropDownList ID="buildlist" runat="server" OnSelectedIndexChanged="buildlist_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            &nbsp;側
             <asp:DropDownList ID="layerlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="layerlist_SelectedIndexChanged">
             </asp:DropDownList>
            &nbsp;樓
            <asp:DropDownList ID="counterlist" runat="server" AutoPostBack="True"></asp:DropDownList>
            &nbsp;櫃&nbsp;           
             <asp:Button ID="btnChangeCounter" runat="server" Text="撤櫃(換櫃)" OnClick="btnChangeCounter_Click" CssClass="btn btn-default" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="取消" />
            <br />
            <asp:Label ID="lblCounter" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblHidden" runat="server" style="color: #FF0000" Text="Label" Visible="False"></asp:Label>
        </div>
        <div class="form-group">
            <label>廠商名稱</label>
            <br />
            <asp:TextBox ID="txtName" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>主要E-mail <span class="auto-style2">*帳號不允許更改</span> <span class="auto-style2"></span></label>
            <asp:TextBox ID="txtEmail" placeholder="username@domain" runat="server" CssClass="auto-style1" Width="500px" ReadOnly="True"></asp:TextBox>
             <asp:RegularExpressionValidator ID="reg主郵件" runat="server" ErrorMessage="※Email格式錯誤"
                 ControlToValidate="txtEmail" Display="Dynamic" EnableClientScript="False"
                 ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="color: #FF0000"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label>備用信箱 </label>&nbsp;<asp:TextBox ID="txtSpareEmail" placeholder="username@domain" runat="server" CssClass="auto-style1" Width="500px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="reg備用信箱" runat="server" ErrorMessage="※Email格式錯誤"
                 ControlToValidate="txtSpareEmail" Display="Dynamic" EnableClientScript="False"
                 ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="color: #FF0000"></asp:RegularExpressionValidator>
            <asp:Label ID="lblMsg" runat="server" style="color: #FF0000" Text="lblMsg" Visible="False"></asp:Label>
        </div>
        <div class="form-group">
            <label>負責人 <span class="auto-style2">*必填</span></label>
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
        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="btn btn-default" OnClick="btnDelete_Click"/>
        <br />
    </div>
    <!--帳號管理end-->
</asp:Content>

