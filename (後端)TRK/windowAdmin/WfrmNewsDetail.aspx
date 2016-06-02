<%@ Page Title="" Language="C#" MasterPageFile="~/windowAdmin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="WfrmNewsDetail.aspx.cs" Inherits="WfrmNewsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">審核最新消息</h1>
        </div>
    </div>
    <!-- 最新消息內容 -->
    <div class="col-lg-5">
        <div class="form-group">
            <label>發布單位</label>
            <asp:TextBox ID="txtResponsible" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>標題</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>開始日期</label>
            <asp:TextBox ID="startDate" runat="server" Enabled="False"></asp:TextBox>
            <label>結束日期</label>
            <asp:TextBox ID="endDate" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>審核狀態</label>
            <asp:TextBox ID="txtcheck" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>活動代碼</label>
            <asp:TextBox ID="txtNewsID" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>公告分類</label>
            <asp:TextBox ID="txtNewsClass" runat="server" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>內容</label>
            <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Rows="10" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <!--原因-->
    <div class="col-lg-5">
        <div class="form-group">
            <div>
                <label>圖片</label>
            </div>
            <asp:Image ID="picUP" runat="server" Height="300" Width="300" />
        </div>
        <div class="form-group">
            <label>備註</label>
            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Rows="9" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="VerificationTrue" runat="server" Text="通過" Width="130px" OnClick="VerificationTrue_Click" />
            <asp:Button ID="VerificationFalse" runat="server" Text="不通過" Width="130px" OnClick="VerificationFalse_Click" />
        </div>
    </div>
</asp:Content>
