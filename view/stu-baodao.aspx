﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stu-baodao.aspx.cs" Inherits="view_stu_baodao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报到需知</title>
    <meta charset="UTF-8" content="编码" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="format-detection" content="telephone=no" />
    <style>
        .admin-main {
            width: 90%;
            background-color: #1470d3;
            color: #ffffff;
            margin: 10px 5%;
            padding: 10px;
        }

        h2 {
            width: 150px;
            text-align: center;
            margin: 0 auto;
        }

        p {
            text-indent: 2em;
        }

        .btn_panel {
            width: 100%;
            margin: 0 auto;
            font-weight: bold;
            text-align: center;
        }
        
        .btnConfirm {
            background-color: #ff6a00;
            color:#ffffff;
            border: none;
            width: 120px;
            margin-left: 12px;
            height: 28px;
            font-size: 1em;
        }

        .btn_disable{
            background-color:#808080;
            color:#ff6a00;
            border:none;
            width: 120px;
            margin-left: 12px;
            height: 28px;
            font-size: 1em;
        }
    </style>
</head>
<body>
    <form id="form1" class="layui-form layui-form-pane" runat="server">
        <asp:HiddenField ID="server_msg" runat="server" />
        <div class="admin-main">
            <div class="container">
                <h2>新生报到需知</h2>
                <h3>1、新生资格审核</h3>
                <p>到达学校，你从校车下来后，就会看到各院系的迎新人员举着标明院系的彩旗在车门口迎接新生。只要找到自己的院系的迎新人员，他们就会领你到你们院系的新生接待处。到了自己院系的新生接待处，就会有院系学工组的老师为你做新生资格审核，你必须出示自己的身份证、录取通知书和准考证，自带档案的同学需上交档案。而后你会被告知自己的宿舍房间号和领到一张报到证，上面会注明你的院系和你的学号，你凭着这张报到证去办理后续的手续。</p>

                <h3>2、住宿手续办理</h3>
                <p>
                    迎新的老师会安排一位师兄或师姐陪你去办理各项手续，有什么不懂的可以马上请教。各位师兄师姐顶着烈日甚至有时是冒着大雨都面带微笑热情地为你服务，一定要记得对他们说声谢谢。学长会先带你去宿舍，办好住宿手续。住宿手续一般比较简单，通常只要到住宿管理中心或公寓一楼的宿管处，向工作人员出示你的报到证并填一些简单的个人资料，对方会告知你的宿舍号和床号并交给你一把宿舍钥匙。将行李放到分配好的宿舍，然后就可以轻轻松松地去办理入学手续。
                </p>
                <p>温馨小贴士：</p>
                <p>
                    记得要把行李摆放在自己的位置，防止别人拿错，可请留在宿舍的舍友帮忙照看一下行李，或者宿舍内有衣柜的话可将行李锁好。贵重物品如银行卡、手机等要随身带，切勿留在宿舍，因为新生宿舍里人进人出，一不小心就会被人顺手牵羊。
                </p>

                <h3>3、 缴纳学杂费
                </h3>
                <p>
                    地点是学校的财务处
缴纳学杂费有2种情况：
                </p>
                <p>第一是现金交付。缴纳完毕后收好缴费收据。</p>
                <p>
                    第二是已经通过银行代扣了学杂费。已通过银行代扣学杂费的同学可凭通知书直接领缴费收据。
                </p>
                <p>温馨小帖士：</p>
                <p>
                    用现金交付学费的学生应注意看好财务，最好在缴费之前再准备现金，以免因其他手续繁杂或特殊情况发生现金被盗或者丢失的情况。
                </p>

                <h3>4、 办理保险</h3>
                <p>
                    办理保险的地点：学工处
                </p>
                <p>温馨提示：</p>
                <p>
                    学生本着自愿原则购买保险，如购买则在入学时一次性交完大学四年的保险费和办理相关手续，所买的保险为意外伤害保险和住院医疗保险。学生保险保费不高，且理赔的范围涉及面广，所以我们建议如果经济条件许可的话，新生应该购买保险。
                </p>
                <h3>5、 办理校园一卡通</h3>
                <p>
                    新生办理一卡通的地点：学校后勤集团
                </p>
                <p>校园一卡通的功能：</p>
                <p>校园一卡通是学校为了方便学生而推出的，集吃饭、洗浴和其他所有学校内部消费（有的学校一卡通月同时充当了宿舍门禁卡）功能于一体的电子卡片，每张卡都存储有相应持卡人的信息，就跟刷信用卡消费一样,非常简便快捷，但仅限于校园内消费。</p>
                <p>校园一卡通的办理程序：</p>
                <p>学校在入学前已为你办好了一卡通并预存了一定的金额，你只需交预存的金额和办卡费就可领到自己的一卡通。</p>

                <h3>6、办理党团关系
                </h3>
                <p>党团关系办理地点：团委</p>
                <p>党团关系办理流程：</p>
                <p>交党团组织介绍信。党团组织介绍信是的新生入学手册里要求你办理并携带至学校的材料，一般是在你以前就读的高中办理的。</p>
            </div>

            <div class="btn_panel">
                <asp:CheckBox ID="checkCofirm" runat="server" Text="已阅读" />
                <asp:Button ID="btnConfirm" CssClass="btn_disable" runat="server" Text="10 秒" Enabled="false" OnClick="btnConfirm_Click" />
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
    <script src="../b_js/app/stu-baodao.js"></script>
    
</body>
</html>