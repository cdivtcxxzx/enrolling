﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx-confirm.aspx.cs" Inherits="view_xsxx_confirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="format-detection" content="telephone=no" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
    <%--<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />--%>
    <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
    <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
    <%--<link rel="stylesheet" href="../nradmingl/plugins/table.css" />--%>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <style>
        .admin-main {
            margin: 5px 15px 10px 15px;
        }

        .jbxx, .lqxx {
            width: 100%;
            margin: 5px auto;
            position: relative;
        }
        .jbxx:after,.lqxx:after{
            content:".";
            width:0;
            height:0;
            display:block;
            clear:both;
        }
        .jbxx .img-content{
            float:left;
            width:30%;
            text-align:center;
            margin-bottom:10px;
        }
        .jbxx img {
            max-width: 180px;
        }
        .jbxx .xx-content,.lqxx .lqxx-wrapper{
            float:right;
            width:57%;
            margin-right:3%;
        }
        .btn-confirm{
            width:100%;
            text-align:center;
        }
        @media (min-width:930px){
            .jbxx .img-content{
                padding-left:25px;   
            }
            .jbxx img {
                max-width: 210px;
            }
            .jbxx .xx-content{
                padding-top:2px;
                padding-right:20px;
            }
            .lqxx .lqxx-wrapper{
                padding-right:20px;
            }
        }
        @media (max-width:650px){
            .jbxx .img-content{
                float:none;
                width:100%;      
            }
            .jbxx img {
                max-width: 150px;
            }
            .jbxx .xx-content{
                float:none;
                width:100%;
                padding-top:0px;
                padding-right:5px;
            }
            .lqxx .lqxx-wrapper{
                float:none;
                width:100%;
                padding-left:5px;
                padding-right:5px;
            }
        }
    </style>

</head>
<body>
    <div class="admin-main">
        <blockquote class="layui-elem-quote">
            <i class="layui-icon">&#xe602;</i>迎新管理<i class="layui-icon">&#xe602;</i>信息确认
            <span style="float: right">
                <a href="javascript:window.location.go(-1);" class="layui-btn layui-btn-small">
                    <i class="layui-icon">&#xe603;</i>
                </a>
            </span>

        </blockquote>
        <form id="form1" class="layui-form layui-form-pane" runat="server">
            <asp:HiddenField ID="hidden_pk_sno" runat="server" />
            <%--基本信息--%>
            <div class="jbxx">
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 5px;">
                    <legend>基本信息</legend>
                </fieldset>
                <div class="jbxx-wrapper">                    
                    <div class="img-content">
                        <asp:Image ID="xszpxx" ImageUrl="../images/xstp/test.jpg" runat="server" />
                    </div>
                    <div class="xx-content">
                        <div class="layui-form-item">
                            <label class="layui-form-label">姓名：</label>
                            <div class="layui-input-block">
                                <asp:Label ID="xsxx_xm" CssClass="layui-input" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="layui-form-item">
                            <label class="layui-form-label">性别：</label>
                            <div class="layui-input-block">
                                <asp:Label ID="xsxx_xb" CssClass="layui-input" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="layui-form-item">
                            <label class="layui-form-label">身份证号：</label>
                            <div class="layui-input-block">
                                <asp:Label ID="xsxx_sfzh" CssClass="layui-input" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="layui-form-item">

                            <label class="layui-form-label">联系电话：</label>
                            <div class="layui-input-block">
                                <asp:Label ID="xsxx_lxdh" CssClass="layui-input" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="layui-form-item">
                             <label class="layui-form-label">报名号：</label>
                            <div class="layui-input-block">
                                <asp:Label ID="xsxx_test" CssClass="layui-input" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--录取信息--%>
            <div class="lqxx">
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 5px;">
                    <legend>录取信息</legend>
                </fieldset>
                <div class="lqxx-wrapper">                    
                    <div class="layui-form-item">
                        <label class="layui-form-label">学号：</label>
                        <div class="layui-input-block">
                            <asp:Label ID="lqxx_xh" CssClass="layui-input" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <label class="layui-form-label">专业：</label>
                        <div class="layui-input-block">
                            <asp:Label ID="lqxx_zy" CssClass="layui-input" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <label class="layui-form-label">班级：</label>
                        <div class="layui-input-block">
                            <asp:Label ID="lqxx_bj" CssClass="layui-input" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <label class="layui-form-label">班主任：</label>
                        <div class="layui-input-block">
                            <asp:Label ID="lqxx_bzr" CssClass="layui-input" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <span style="color:#ffd800;margin-bottom:10px;display:inline-block;">请核对个人信息，确认信息无误；信息有误请及时联系班主任！</span>
                        <div class="layui-input-block">
                            <input type="checkbox" name="xsxxcomfirm" title="信息无误" />        
                        </div>
                    </div>
                </div>
            </div>
            <%--确认按钮--%>
            <div class="btn-confirm">
                <a href="#" class="layui-btn layui-btn-big">提  交</a>
            </div>
        </form>
    </div>
    <script src="../nradmingl/plugins/layui/layui.js"></script>
    <script src="../b_js/app/xsxx-confirm.js"></script>

</body>
</html>