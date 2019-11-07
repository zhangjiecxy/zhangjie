<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Company.UI.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统登录</title>
    <style type="text/css">
        html
        {
            overflow-y: scroll;
            vertical-align: baseline;
        }
        body
        {
            font-family: Microsoft YaHei,Segoe UI,Tahoma,Arial,Verdana,sans-serif;
            font-size: 12px;
            color: #fff;
            height: 100%;
            line-height: 1;
            background: #999;
        }
        *
        {
            margin: 0;
            padding: 0;
        }
        ul, li
        {
            list-style: none;
        }
        h1
        {
            font-size: 30px;
            font-weight: 700;
            text-shadow: 0 1px 4px rgba(0,0,0,.2);
        }
        .login-box
        {
            width: 410px;
            margin: 120px auto 0 auto;
            text-align: center;
            padding: 30px;
            background: url(Images/mask.png);
            border-radius: 10px;
        }
        .login-box .name, .login-box .password, .login-box .code, .login-box .remember
        {
            font-size: 16px;
            text-shadow: 0 1px 2px rgba(0,0,0,.1);
        }
        .login-box .remember input
        {
            box-shadow: none;
            width: 15px;
            height: 15px;
            margin-top: 25px;
        }
        .login-box .remember
        {
            padding-left: 40px;
        }
        .login-box .remember label
        {
            display: inline-block;
            height: 42px;
            width: 70px;
            line-height: 34px;
            text-align: left;
        }
        .login-box label
        {
            display: inline-block;
            width: 100px;
            text-align: right;
            vertical-align: middle;
        }
        .login-box #code
        {
            width: 120px;
        }
        .login-box .codeImg
        {
            float: right;
            margin-top: 26px;
        }
        .login-box img
        {
            width: 148px;
            height: 42px;
            border: none;
        }
        input[type=text], input[type=password]
        {
            width: 270px;
            height: 42px;
            margin-top: 25px;
            padding: 0px 15px;
            border: 1px solid rgba(255,255,255,.15);
            border-radius: 6px;
            color: #fff;
            letter-spacing: 2px;
            font-size: 16px;
            background: transparent;
        }
        .button
        {
            cursor: pointer;
            width: 100%;
            height: 44px;
            padding: 0;
            background: #ef4300;
            border: 1px solid #ff730e;
            border-radius: 6px;
            font-weight: 700;
            color: #fff;
            font-size: 24px;
            letter-spacing: 15px;
            text-shadow: 0 1px 2px rgba(0,0,0,.1);
        }
        input:focus
        {
            outline: none;
            box-shadow: 0 2px 3px 0 rgba(0,0,0,.1) inset,0 2px 7px 0 rgba(0,0,0,.2);
        }
        button:hover
        {
            box-shadow: 0 15px 30px 0 rgba(255,255,255,.15) inset,0 2px 7px 0 rgba(0,0,0,.2);
        }
        .screenbg
        {
            position: fixed;
            bottom: 0;
            left: 0;
            z-index: -999;
            overflow: hidden;
            width: 100%;
            height: 100%;
            min-height: 100%;
        }
        .screenbg ul li
        {
            display: block;
            list-style: none;
            position: fixed;
            overflow: hidden;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: -1000;
            float: right;
        }
        .screenbg ul a
        {
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            display: inline-block;
            margin: 0;
            padding: 0;
            cursor: default;
        }
        .screenbg a img
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            vertical-align: middle;
            display: inline;
            display: block;
            list-style: none;
            position: fixed;
            overflow: hidden;
            width: 100%;
            height: 100%;
            z-index: -1000;
            float: right;
            top: 0px;
            left: 0px;
            bottom: -202px;
        }
        .bottom
        {
            margin: 8px auto 0 auto;
            width: 100%;
            position: fixed;
            text-align: center;
            bottom: 0;
            left: 0;
            overflow: hidden;
            padding-bottom: 8px;
            color: #ccc;
            word-spacing: 3px;
            zoom: 1;
        }
        .bottom a
        {
            color: #FFF;
            text-decoration: none;
        }
        .bottom a:hover
        {
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="Scripts/FunctionJS.js"></script>
    <script type="text/javascript" src="Layer/layer.js"></script>
     <script type="text/javascript">
        $(function () {
            $(".screenbg ul li").each(function () {
                $(this).css("opacity", "0");
            });
            $(".screenbg ul li:first").css("opacity", "1");
            var index = 0;
            var t;
            var li = $(".screenbg ul li");
            var number = li.size();            
            function change(index) {
                li.css("visibility", "visible");
                li.eq(index).siblings().animate({ opacity: 0 }, 3000);
                li.eq(index).animate({ opacity: 1 }, 3000);
            }
            function show() {
                index = index + 1;
                if (index <= number - 1) {
                    change(index);
                } else {
                    index = 0;
                    change(index);
                }
            }
            t = setInterval(show, 8000);
            
        });
        /*
        检查浏览器是否支持
        */
        var isIE = !!window.ActiveXObject;
        var isIE6 = isIE && !window.XMLHttpRequest;
        if (isIE6) {
            window.location.href = "/ErrorPage/Browser_Not_Support.aspx";
        }
        //回车键
        document.onkeydown = function (e) {
            if (!e) e = window.event; //火狐中是 window.event
            if ((e.keyCode || e.which) == 13) {
                var obtnSearch = document.getElementById("Log_Submit")
                obtnSearch.focus(); //让另一个控件获得焦点就等于让文本输入框失去焦点
                obtnSearch.click();
            }
        }
        function LoginBtn() {
            var Account = $("#Account").val();
            var Pwd = $("#Pwd").val();
            var code = $("#Code").val();
            if (Account == "") {
                $("#Account").focus();
                layer.tips('请输入登陆账号', '#Account', {
                    tips: [1, '#FF0000'],
                    time: 2000
                });
                return false;
            } else if (Pwd == "") {
                $("#Pwd").focus();
                layer.tips('请输入登陆密码', '#Pwd', {
                    tips: [1, '#FF0000'],
                    time: 2000
                });
                return false;
            } else if (code == "") {
                $("#Code").focus();
                layer.tips('请输入验证码', '#Code', {
                    tips: [1, '#FF0000'],
                    time: 2000
                });
                return false;
            } else if (code.length != 4) {
                $("#Code").focus();
                layer.tips('验证码必须为4位', '#Code', {
                    tips: [1, '#FF0000'],
                    time: 2000
                });
                return false;
            } else {
                return true;
            }
        }
        /**
        数据验证完整性
        **/
        function CheckUserDataValid() {
            if (!LoginBtn()) {
                return false;
            }
            else {
                var Account = $("#Account").val();
                var Pwd = $("#Pwd").val();
                var code = $("#Code").val();
                var parm = 'action=login&Account=' + escape(Account) + '&Pwd=' + escape(Pwd) + '&code=' + escape(code);
                getAjax('Frame.ashx', parm, function (rs) {                    
                    if (parseInt(rs) == 1) {
                        debugger
                        $("#Code").focus();
                        layer.tips('验证码输入不正确', '#Code', {
                            tips: [1, '#FF0000'],
                            time: 2000
                        });
                        $("#Code").val("");
                        ToggleCode("Verify_codeImag", 'VerifyCode.ashx');
                         
                    } else if (parseInt(rs) == 2) {
                        $("#Account").focus();
                        showTopMsg("登录账户被停用", 4000, 'error');
                        
                    } else if (parseInt(rs) == 4) {
                        $("#Account").focus();
                        layer.tips('账户或密码有错误', '#Account', {
                            tips: [1, '#FF0000'],
                            time: 2000
                        });
                        resetInput();
                        
                    } else if (parseInt(rs) == 6) {
                        $("#Account").focus();
                        showTopMsg("该用户已经登录", 4000, 'error');
                        
                    } else if (parseInt(rs) == 3) {
                         self.location = "Home/About"; //登陆成功进入后台界面
                        
                    } else if (parseInt(rs) == 7) {
                        $("#Account").focus();
                        layer.tips('内部错误,登录失败', '#Account', {
                            tips: [1, '#FF0000'],
                            time: 2000
                        });
                        
                    } else {
                        
                        alert(rs);
                        // window.location.href = window.location.href.replace('#', '');
                    }
                });
            }
        }
        //清空
        function resetInput() {
            $("#Account").focus(); //默认焦点
            $("#Account").val("");
            $("#Pwd").val("");
            $("#Code").val("");
        }

    </script>
</head>
<body>
    <div class="login-box">
        <h1>
            后台管理系统</h1>
        <form method="post" action="">
        <div class="name">
            <label id="lblAccount">
                管理员账号：</label>
            <input type="text" name="" id="Account" maxlength="20" tabindex="1" autocomplete="off" />
        </div>
        <div class="password">
            <label id="lblPwd">
                密 码：</label>
            <input type="password" name="" maxlength="16" id="Pwd" tabindex="2" />
        </div>
        <div class="code">
            <label id="lblCode" style="margin-left: -150px;">
                验证码：</label>
            <input type="text" name="" maxlength="4" id="Code" tabindex="3" style="width: 120px;" />
            <%--<div class="codeImg">--%>
                <img src="VerifyCode.ashx" id="Verify_codeImag" width="80" height="28" alt="点击切换验证码"
                    title="点击切换验证码" style="margin:-44px;margin-left: 215px;" onclick="ToggleCode(this.id, 'VerifyCode.ashx');return false;" />
            <%--</div>--%>
        </div>
<%--        <div class="remember">
            <input type="checkbox" id="remember" tabindex="4" />
            <label>
                记住密码</label>
        </div>--%>
        <br />
        <div class="login">
            <input  type="button" id="Log_Submit"  class="button" onclick="CheckUserDataValid()" value="登录"/>
        </div>
        </form>
    </div>
<%--    <div class="bottom">
        ©2014 Leting <a href="javascript:;" target="_blank">关于</a> <span>京ICP证030173号</span><img
            width="13" height="16" src="Images/copy_rignt_24.png" /></div>--%>
    <div class="screenbg">
        <ul>
            <li><a href="javascript:;">
                <img src="Images/0.jpg"></a></li>
            <li><a href="javascript:;">
                <img src="Images/1.jpg"></a></li>
            <li><a href="javascript:;">
                <img src="Images/2.jpg"></a></li>
        </ul>
    </div>
</body>
</html>

