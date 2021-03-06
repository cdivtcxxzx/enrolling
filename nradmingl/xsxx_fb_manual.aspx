﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx_fb_manual.aspx.cs" Inherits="nradmingl_xsxx_fb_manual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学生管理</title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->

    <link rel="stylesheet" href="plugins/layui/css/layui-qiu.css" media="all" />
    <link rel="stylesheet" href="plugins/global.css" media="all" />
    <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
    <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css" />
    <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
    <link rel="stylesheet" href="plugins/table.css" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <style>
        .container {
            margin:10px auto;
            padding:10px;
        }
        .tishi{
            color:red;
            margin-bottom:5px;
            display:inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" class="layui-form layui-form-pane" runat="server">        
        <div class="container">
            <asp:label runat="server" ID="ts" CssClass="tishi" Text="" ></asp:label>
            <div class="layui-form-item">
                <label class="layui-form-label">学院：</label>
                <div class="layui-input-block">
                    <asp:Label ID="Colleage_txt" CssClass="layui-input" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="layui-form-item">
                <label class="layui-form-label">专业：</label>
                <div class="layui-input-block">
                    <asp:Label ID="SPE" CssClass="layui-input" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="layui-form-item">
                <label class="layui-form-label">班级：</label>
                <div class="layui-input-block">
                    <asp:DropDownList ID="ddlClass" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="-1">选择班级</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="layui-form-item">
                <div class="layui-input-block">
                    <asp:button ID="btn_confirm" runat="server" cssClass="layui-btn layui-btn-normal" Text="设置" OnClick="btn_confirm_Click"/>
                </div>
            </div>
        </div>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="model.organizationModelDataContext" EntityTypeName="" Select="new (Name, PK_College)" TableName="Base_Colleges">
        </asp:LinqDataSource>
        <asp:LinqDataSource ID="LinqDataSource2" runat="server">
        </asp:LinqDataSource>
    </form>
</body>
</html>
