﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xszz_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //访问本网页用户的授权验证

        //string pk_sno = Request.QueryString["pk_sno"];//获取学号

        #region 控制业务代码
        //Session["pk_sno"] = pk_sno;
        this.pk_sno.Value = Session["pk_sno"].ToString().Trim();//其值应由会话中来，在学生登陆成功后被赋予初值
        #endregion
    }
}