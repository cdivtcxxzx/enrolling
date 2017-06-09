﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssgl_qsxq.aspx.cs" Inherits="nradmingl_qsxq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>寝室详情</title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->

        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all">
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    

</head>
<body>
    <style>
        .layui-tab-brief > .layui-tab-more li.layui-this::after, .layui-tab-brief > .layui-tab-title .layui-this::after {
            border-radius: 0;
            border-bottom: 3px solid #196BAB;
            border-top: 3px;
        }
        .layui-tab-brief > .layui-tab-title .layui-this {
            color: #023d6b;
        }

        .layui-tab-title .layui-this {
            background: #e2e2e2;
            border-top: 3px;
            border-left: 1px;
            border-right: 1px;
        }

        .layui-tab-title .layui-this {
            background: #e2e2e2;
            border-top: 3px;
            border-left: 1px;
            border-right: 1px;
            border-bottom: 3px;
        }

        .layui-tab-title li:hover {
            background: #e2e2e2;
            border-bottom: 3px solid #196BAB;
            border-top: 3px;
        }

        element.style {
            height: 100%;
        }

        .layui-elem-field {
            margin-bottom: 5px;
            padding: 0;
            border: 1px solid #e2e2e2;
        }

        .qszp img {
            height: 250px;
        }

    </style>    
    <form id="form1" class="layui-form layui-form-pane" runat="server">
        <div class="admin-main">
            <fieldset class="layui-elem-field layui-field-title" style="margin-top: 5px;">
                <legend style="font-size: 14px;">寝室基础信息及预分配信息</legend>

                <asp:GridView  CssClass="site-table table-hover"  ID="GridView2" runat="server" AutoGenerateColumns="true" 
                    DataSourceID="SqlDataSource1">
                   
                </asp:GridView>





                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="select row_number() over (order by  已分配院系)  AS 序号,* from (SELECT DISTINCT                       TOP (10) Base_Campus.Campus_Name AS 校区, Fresh_Dorm.Name AS 公寓楼名称, Fresh_Room.Floor AS 楼层, Fresh_Room.Room_NO AS 房间编号,                       Fresh_Room_Type.Type_Name AS 房间类型, Fresh_Room.Gender AS 性别, Base_College.Name AS 已分配院系, Fresh_Class.Name AS 已分配班级 FROM         Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO RIGHT OUTER JOIN                      Base_College RIGHT OUTER JOIN                      Fresh_Bed ON Base_College.College_NO = Fresh_Bed.College_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      Base_Campus RIGHT OUTER JOIN                      Fresh_Dorm RIGHT OUTER JOIN                      Fresh_Room_Type RIGHT OUTER JOIN                      Fresh_Room ON Fresh_Room_Type.PK_Room_Type = Fresh_Room.FK_Room_Type ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO ON                       Base_Campus.Campus_NO = Fresh_Dorm.Campus_NO ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE     (Fresh_Room.Room_NO = @Room_NO)) t order by  已分配院系">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Room_NO" QueryStringField="roomno" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>





              <%--  <table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
                    <tbody>
                        <tr align="center">
                            <th scope="col">校区名称</th>
                            <th class="hidden-xs" scope="col">楼栋名称</th>
                            <th scope="col">楼层</th>
                            <th class="hidden-xs" scope="col">房间编号</th>
                            <th scope="col">房间类型</th>
                            <th class="hidden-xs">房间人数</th>
                            <th class="hidden-xs" scope="col">性别</th>
                            <th>空余床位数</th>
                        </tr>
                        <tr>
                            <td>天府校区</td>
                            <td class="hidden-xs">1栋</td>
                            <td>1楼</td>
                            <td>1101</td>
                            <td class="hidden-xs">普通六人间</td>
                            <td class="hidden-xs">6</td>
                            <td class="hidden-xs">女</td>
                            <td>2</td>
                        </tr>
                    </tbody>
                </table>--%>
            </fieldset>
            <div class="layui-tab layui-tab-brief" lay-filter="docDemoTabBrief">
                <ul class="layui-tab-title">
                    <li class="layui-this">床位及入住学生信息</li>
                    <li>寝室缩影</li>
                </ul>
                <div class="layui-tab-content" style="height: 100%;">
                    <div class="layui-tab-item layui-show">

                     <asp:GridView  CssClass="site-table table-hover"  ID="GridView3" runat="server" AutoGenerateColumns="true" 
                    DataSourceID="SqlDataSource2">
                   
                </asp:GridView>





                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系, 
                      Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话
FROM         Fresh_Bed LEFT OUTER JOIN
                      Fresh_Bed_Log LEFT OUTER JOIN
                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN
                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN
                      Fresh_Class RIGHT OUTER JOIN
                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON 
                      Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN
                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO
WHERE     (Fresh_Room.Room_NO = @Room_NO)
ORDER BY 床位编号">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Room_NO" QueryStringField="roomno" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                       <%-- <table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
                            <tbody>
                                <tr align="center">
                                    <th scope="col">序号</th>
                                    <th class="hidden-xs" scope="col">学生姓名</th>
                                    <th scope="col">学生编号</th>
                                    <th scope="col">院系</th>
                                    <th class="hidden-xs" scope="col">专业</th>
                                    <th class="hidden-xs" scope="col">班级</th>
                                    <th class="hidden-xs" scope="col">床位号</th>
                                    <th class="hidden-xs" scope="col">床位描述</th>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td class="hidden-xs">李莉</td>
                                    <td>2017020105</td>
                                    <td>建筑工程学院</td>
                                    <td class="hidden-xs">建筑装饰工程技术</td>
                                    <td class="hidden-xs">1班</td>
                                    <td class="hidden-xs">1</td>
                                    <td class="hidden-xs">上铺靠窗</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td class="hidden-xs">刘筱幽</td>
                                    <td>2017030507</td>
                                    <td>财经管理学院</td>
                                    <td class="hidden-xs">会计</td>
                                    <td class="hidden-xs">2班</td>
                                    <td class="hidden-xs">3</td>
                                    <td class="hidden-xs">下铺靠窗</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td class="hidden-xs">张婷</td>
                                    <td>2017030523</td>
                                    <td>财经管理学院</td>
                                    <td class="hidden-xs">会计</td>
                                    <td class="hidden-xs">2班</td>
                                    <td class="hidden-xs">4</td>
                                    <td class="hidden-xs">下铺靠门</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td class="hidden-xs">陈艳</td>
                                    <td>2017060955</td>
                                    <td>物流工程学院</td>
                                    <td class="hidden-xs">物流管理</td>
                                    <td class="hidden-xs">2班</td>
                                    <td class="hidden-xs">6</td>
                                    <td class="hidden-xs">上铺靠门</td>
                                </tr>
                            </tbody>
                        </table>--%>
                    </div>
                    <div class="layui-tab-item">

                        <div class="layui-input-block" style="margin-left: 10px!important">
                            <div class="layui-form-mid layui-word-aux-ts qszp" style="margin-left: 10px; text-align: center; float: none!important">
                                <img src="../images/xstp/qs.jpg" runat="server" />
                            </div>
                        </div>


                    </div>

                </div>
            </div>
            <div>
                <div style="text-align: center">
                    <span style="padding: 8px;">
                       <%-- <a href="" class="layui-btn layui-btn-small" id="">
                            <i class="layui-icon">&#x1006;</i> 关闭
                        </a>--%>
                    </span>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="plugins/layui/layui.js"></script>
    <script>
layui.use('element', function(){
  var $ = layui.jquery
  ,element = layui.element(); //Tab的切换功能，切换事件监听等，需要依赖element模块
  
  //触发事件
  var active = {
    tabAdd: function(){
      //新增一个Tab项
      element.tabAdd('demo', {
        title: '新选项'+ (Math.random()*1000|0) //用于演示
        ,content: '内容'+ (Math.random()*1000|0)
        ,id: new Date().getTime() //实际使用一般是规定好的id，这里以时间戳模拟下
      })
    }
    ,tabDelete: function(othis){
      //删除指定Tab项
      element.tabDelete('demo', '44'); //删除：“商品管理”
      
      
      othis.addClass('layui-btn-disabled');
    }
    ,tabChange: function(){
      //切换到指定Tab项
      element.tabChange('demo', '22'); //切换到：用户管理
    }
  };
  
  $('.site-demo-active').on('click', function(){
    var othis = $(this), type = othis.data('type');
    active[type] ? active[type].call(this, othis) : '';
  });
  
  //Hash地址的定位
  var layid = location.hash.replace(/^#test=/, '');
  element.tabChange('test', layid);
  
  element.on('tab(test)', function(elem){
    location.hash = 'test='+ $(this).attr('lay-id');
  });
  
});
</script>
</body>

</html>
