<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="QRCode.aspx.cs" Inherits="windowAdmin_QRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4><a href="CouponList.aspx">返回</a></h4>
    <div class="container-fluid">
        <div class="form-group">
            <div class="col-sm-6">
                <h4>選取設施：</h4>
                <asp:DropDownList ID="facilityList" runat="server" CssClass="form-control" OnSelectedIndexChanged="facilityList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <h4>優惠券標題：</h4>
                <asp:TextBox ID="txtTitle" Width="100%" runat="server" CssClass="form-control"></asp:TextBox>
                <h4>優惠券內容：</h4>
                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Width="100%" TextMode="MultiLine">
                </asp:TextBox>
                <br />
            </div>
            <div class="col-sm-4">
                <br />
                <br />
                
                <div class="col-sm-8">
                    &nbsp;&nbsp;
                <asp:Image ID="imgQRCode" runat="server" alt="QR Code圖片" CssClass="img-responsive img-thumbnail" Height="200" Width="200" />
                </div>
                <div class="col-sm-8">
                    <br />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnQRCode" runat="server" Text="產生 QRCode" OnClick="btnQRCode_Click" CssClass="btn btn-info active" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="清除" CssClass="btn btn-warning active" OnClick="btnClear_Click" />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMsg" runat="server" Text="Label" Style="font-family: 微軟正黑體; font-size: large; color: #FF3300;"></asp:Label>
                </div>
            </div>
        </div>
        <br />

    </div>



</asp:Content>

