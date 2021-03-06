﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bzr_classbeds.aspx.cs" Inherits="view_bzr_classbeds" validateRequest="false" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />    
        <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <style>
        .layui-form-label {
            float: left;
            display: block;
            padding: 9px 15px;
            width: 50px;
            font-weight: 400;
            text-align: left;
        }

                .layui-form-select dl dd.layui-this {
            background-color: #196BAB;
            color: #fff;
        }
.layui-form select {
    display: inherit;
     height: 37px;
}

    </style>
   
</head>
<body>
    <form id="form1" runat="server" class="layui-form">
            <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />
            <asp:HiddenField ID="hid_class_no" Value="" runat="server" />
            <asp:HiddenField ID="tsxx" runat="server" Value="" />
<%--              <asp:HiddenField ID="pk_batch_no" Value="" runat="server" />--%>
        <div class="admin-main">
            <blockquote class="layui-elem-quote">
                <i class="layui-icon">&#xe602;</i>辅导员<i class="layui-icon">&#xe602;</i>查看宿舍分配           
            </blockquote>
            <div>                
                <div class="layui-form-item">
                    <!--迎新批次下拉列表-->
                    <div class="layui-inline">
<%--                        <label class="layui-form-label">批次：</label>--%>
                        <div class="layui-input-inline">
                            <select name="batchlist" id="batchlist">

                            </select>
                        </div>
                    </div>
                    <!--班级下拉列表-->
                    <div class="layui-inline">
<%--                        <label class="layui-form-label">班级：</label>--%>
                        <div class="layui-input-inline">
                            <select name="classlist" id="classlist">
                                <option value="">请选择班级</option>
                                <option value="1">layer</option>
                                <option value="2">form</option>
                                <option value="3">layim</option>
                                <option value="4">element</option>
                            </select>
                        </div>
                    </div>
                     <div class="layui-inline" style="margin-bottom:0px;">
                         <a href="#" onclick="getclassbeds();" class="layui-btn layui-btn-small hidden-xs">
					    <i class="layui-icon">&#x1002;</i> 刷新
				        </a>&nbsp;&nbsp;
                         <asp:Button Text="导出数据" ID="btn_down" CssClass="layui-btn" runat="server" OnClick="btn_down_Click" />
                    </div> 

                <div class="layui-form-item" style="margin-bottom:0px;">
                    <table class="site-table table-hover" cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                        <tr>
                            <td id="count">学生总计：0人<</td>
                            <td id="boy_count">男生：0人</td>
                            <td id="girl_count">女生：0人</td>
                        </tr>
                        <tr>
                            <td id="bed_count">床位总数：0张</td>
                            <td id="bed_boy_count">男床位：0张</td>
                            <td id="bed_girl_count">女床位：0张</td>
                        </tr>
                        <tr>
                            <td id="stu_bed_count">已分床位总数：0张</td>
                            <td id="stu_bed_boy_count">已分男床位：0张</td>
                            <td id="stu_bed_girl_count">已分女床位：0张</td>
                        </tr>
                    </table>

                   
                    <table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="studentlist" style="border-collapse: collapse;">
                        <thead>
                            <tr>        
                                <th scope="col">序号</th>                        
                                <th scope="col" class="hidden-xs">校区</th>
                                <th scope="col">宿舍</th>
                                <th scope="col" class="hidden-xs">楼层</th>
                                <th scope="col" class="hidden-xs">房间类型</th>
                                <th scope="col">房间号</th>
                                <th scope="col" class="hidden-xs">类别</th>
                                <th scope="col" class="hidden-xs">床号</th>
                                <th scope="col" class="hidden-xs">位置</th>
                                <th scope="col">姓名</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>

                </div>

            </div>
        </div>
    </form>
        <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="plugins/layui/layui.js"></script>
        <script type="text/javascript" src="../b_js/app/bzr_classbeds.js"></script>

<%--        <script type="text/javascript" src="plugins/layui/layui.js"></script>--%>
    <script>
        load();
        //layui.use('form', function () {
        //    var form = layui.form(); //只有执行了这一步，部分表单元素才会修饰成功
        //});
        layui.use(['layer'], function () {
            var layer = layui.layer;
            if ($("#tsxx").val() != "") {
                parent.layer.open({ content: $("#tsxx").val(), title: '提示信息(30秒后自动关闭)', btn: ['关闭'], time: 30000 });
                $("#tsxx").value = "";
            }
            $('#btn_down').on('click', function () {
                $('#hid_class_no').val($('#classlist').children('option:selected').val());
            });
        });
        layui.config({
            base: 'plugins/layui/modules/'
        });
    </script>

</body>
</html>
