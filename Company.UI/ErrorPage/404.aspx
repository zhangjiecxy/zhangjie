﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Mytest.ErrorPage._404" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>您所访问的页面不存在</title>
    <style type="text/css">
        body {
            background-color: #F7F7F7;
        }

        .not-support-browser {
            background-color: #FFFFFF;
            border: 1px solid #CED0D3;
            border-radius: 5px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            width: 650px;
            height: 350px;
            margin: 100px auto;
            padding: 0px;
        }

        .not-support-browser-contant {
            color: #666666;
            font-size: 18px;
            margin: 35px 25px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 内容区 -->
        <div class="not-support-browser">
            <div class="not-support-browser-contant">
                <h1 style="font-size: 44px">您所访问的页面不存在</h1>
                <img src="../Images/404_r1_c2.png" />
            </div>
        </div>
    </form>
</body>
</html>
