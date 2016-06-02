<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="CouponList.aspx.cs" Inherits="windowAdmin_CouponList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function previewFileProduct() {
            var preview = document.querySelector('#<%=imgQRCode.ClientID %>');

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
        <h1 class="page-header">優惠券管理</h1>
    </div>
    <a class="btn btn-success" href="QRCode.aspx">新增優惠券QR Code</a>
    <div class="table-responsive">
        <table class="nav-justified table">
            <tr>
                <td class="col-sm-4">
                    <asp:TextBox ID="txtQRCode" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <asp:Button ID="btnSearchQRCode" runat="server" Text="搜尋QRCode" CssClass="btn btn-default active" OnClick="btnSearchQRCode_Click" />
                </td>
                <td class="col-sm-1">
                    <asp:Image ID="imgQRCode" runat="server" alt="QR Code圖片" CssClass="img-responsive" Width="200px" />
                </td>
                <td class="col-sm-4">

                    <asp:Label ID="lblBname" runat="server" Text=" "></asp:Label>
                    <br />
                    <asp:Label ID="lblTitle" runat="server" Text=" "></asp:Label>

                </td>
            </tr>
        </table>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblUrl" runat="server" Text="圖片網址："></asp:Label>
    </div>
    <br />
    <hr />
    <h4>選取設施看清單：</h4>
    <div class="col-md-5">
        <asp:DropDownList ID="facilityList" runat="server" CssClass="form-control" OnSelectedIndexChanged="facilityList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    </div>
    <br />
    <br />
    <br />
    <div class="col-md-12">
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"></asp:GridView>
    </div>
</asp:Content>

