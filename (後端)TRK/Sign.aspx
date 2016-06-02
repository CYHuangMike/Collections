<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sign.aspx.cs" Inherits="Sign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!--header內容開始-->
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            background-size: cover;
            background-image: url(../mPic/TRKLO1.jpg);
            background-repeat:no-repeat;
            background-attachment: fixed;
        }
        .jumbotron {
            background: rgba(129,216,207,0.8);
        }
        h1 {
            font-family:'Microsoft JhengHei';
        }
    </style>
</head>
<body>
    <div class="jumbotron">
        <div class="container text-center">
            <h1 style="color:white">TRK帳號管理系統</h1>
        </div>
    </div>
    <form id="form1" runat="server" class="form-horizontal" role="form">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-4 col-sm-offset-4">
                        <div class="login-panel panel panel-default">
                            <div class="panel-heading" style="background-color:#57BBB0">
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-4 control-label" style="color:white;font-size:24px">電子郵件</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtaccount" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-4 control-label" style="color:white;font-size:24px">密碼</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-10">
                                    <div class="checkbox">
                                        <asp:Label ID="message" runat="server" Text="Label"></asp:Label>
                                        <label style="color:#35998E;font-size:24px">
                                            <input type="checkbox" />
                                            記住我
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="submit" runat="server" Text="登入" BackColor="#57BBB0" CssClass="btn btn-default btn-lg btn-block" OnClick="submit_Click" style="color:white;font-size:24px"/>
                        </div>
                    </div>
                </div>
            </div>
    </form>
    <!-- jQuery -->
    <script src="../js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../js/bootstrap.min.js"></script>
</body>
</html>
