<%@ Page Title="" Language="C#" MasterPageFile="~/windowCompany/MasterPagePWD.master" AutoEventWireup="true" CodeFile="updatepassword.aspx.cs" Inherits="windowCompany_updatepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">輸入新密碼</h1>
        </div>
    </div>
    <!-- 帳號管理 -->
    <div class="col-lg-6">
        <div class="form-group">
            <label>新密碼</label>
            <asp:TextBox ID="txtpassword1" runat="server" CssClass="form-control" TextMode="Password" ></asp:TextBox>
        </div>
        <div class="form-group">
            <label>確認密碼</label>
            <asp:TextBox ID="txtpassword2" runat="server" CssClass="form-control" TextMode="Password" ></asp:TextBox>
        </div>
         <div class="form-group"> 
           <asp:Label ID="message" runat="server" Text="Label"></asp:Label>
        </div>
        
        <div class="form-group">
            <asp:Button ID="btnsubmit" runat="server" Text="送出" CssClass="btn btn-default" OnClick="btnsubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="btn btn-default" OnClick="btnCancel_Click" />
        </div>
        <div class="form-group">
            <label>***更改密碼成功後請使用新密碼再重新登入，謝謝!***</label>
        </div>
    </div>
    <!--帳號管理end-->
</asp:Content>

