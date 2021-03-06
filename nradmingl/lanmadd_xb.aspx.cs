﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class admin_lanmadd_xb : System.Web.UI.Page
{
    protected struct structLanM
    {
        public string lmid;
        public string lmmc;
        public string mcsm;
        public string fid;
        public string px;
        public string gjz;
        public string sfcdxs;
        public string sfdhxs;
        public string dhcdh;
        public string sfjlrz;
        public string sftop;
        public string sfqxyz;
        public string lmyyqx;
        public string lmtp;
        public string url;
        public string dkfs;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request["action"] == "add")
            {
                this.tijiao.Text = "确认添加";
                this.EddBind();

            }
            else if (Request["action"] == "edit" && Request["id"] != "")
            {
                this.tijiao.Text = "确认修改";
                editBind();
            }
            else
            {
                this.tijiao.Text = "提交";
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tijiao.Text == "确认添加")
        {//添加栏目数据
            //定义添加栏目结构体
            structLanM addStructLanm = new structLanM();
            //string checks = Request.Form["powerList"];


            addStructLanm.lmmc = TB_Name0.Value.ToString().Trim();
            addStructLanm.mcsm = TB_Content.Value.ToString().Trim();
            //若选择根目录，则fid字段设为－1

            //if (DropDownList3.SelectedIndex >0)
            //{
            //    addStructLanm.fid = DropDownList3.SelectedValue.ToString().Trim();
            //}
            //else 
            if (DropDownList2.SelectedIndex > 0)
            {
                addStructLanm.fid = DropDownList2.SelectedValue.ToString().Trim();
            }
            else
            {
                addStructLanm.fid = DropDownList1.SelectedValue.ToString().Trim();
            }

            addStructLanm.px = TB_OrderNum.Value.ToString().Trim();
            addStructLanm.gjz = TB_Name.Value.ToString().Trim();
            if (isKeyWordRepeat(addStructLanm.gjz))
            {
                basic.MsgBox(this.Page, "栏目关键字重复！", "-1"); return;
            }
            addStructLanm.sfcdxs = sfcdxs.Checked ? "1" : "0";
            addStructLanm.sfdhxs = sfdhxs.Checked ? "1" : "0";
            addStructLanm.sftop = sftop.Checked ? "1" : "0";
            addStructLanm.sfjlrz = sfjlrz.Checked ? "1" : "0";
            addStructLanm.dhcdh = "";
            //addStructLanm.dhcdh = TB_Tree.Value.ToString().Trim();
            addStructLanm.lmtp = TextBox1.Text.ToString().Trim();
            addStructLanm.url = TextBoxURL.Text.ToString().Trim();

            addStructLanm.dkfs = ddlDaKaiFangShi.SelectedValue.ToString();
            //addStructLanm.dkfs = TextBox3.Text.ToString().Trim();
            addStructLanm.lmyyqx = "";
            addStructLanm.sfqxyz = "";
            //bool sfqxyzIs0Tab = false;
            //获取栏目所有权限
            //for (int i = 0; i < rptAllEnablePower.Items.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)rptAllEnablePower.Items[i].FindControl("powerList") as CheckBox;
            //    if ("chk" != null && chk.Checked == true)
            //    {
            //        Label CKID = (Label)rptAllEnablePower.Items[i].FindControl("CheckBoxID");
            //        addStructLanm.lmyyqx = addStructLanm.lmyyqx + CKID.Text + ",";
            //    }
            //}

            //获取需要的权限验证
            //string powers = hiddenID.Value;
            //string[] groupPowers = null;
            //if (powers != "")
            //{
            //    groupPowers = new Power().getFromQx(powers);
            //    foreach (string power in groupPowers)
            //    {
            //        if (power != "")
            //        {
            //            try
            //            {
            //                string sqlStringGetQxidByQxmc = "SELECT Qxid FROM quanxdm WHERE qxmc=@qxmc";
            //                DataTable dt = Sqlhelper.Serach(sqlStringGetQxidByQxmc, new SqlParameter("qxmc", power));
            //                if (dt.Rows.Count > 0)
            //                {
            //                    //防止出现重置后checkbox不消失的bug
            //                    if (addStructLanm.lmyyqx.Contains(dt.Rows[0][0].ToString()))
            //                        addStructLanm.sfqxyz = addStructLanm.sfqxyz + dt.Rows[0][0].ToString() + ',';
            //                    sfqxyzIs0Tab = true;

            //                }

            //            }
            //            catch
            //            {
            //                basic.MsgBox(this.Page, "权限列表获取失败", "-1");
            //            }
            //        }
            //    }
            //}
            //if (!sfqxyzIs0Tab) addStructLanm.sfqxyz = "0";


            //上传照片及数据写入数据库
            bool fileOK = false;
            string path = Server.MapPath("~/admin/images");
            if (FileUpload1.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                string[] allowedExtensions = { ".gif", "png", ".bmp", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                try
                {
                    FileUpload1.SaveAs(path + addStructLanm.lmtp);

                }
                catch
                {
                    // Response.Write("<script type='text/javascript'>alert('图片上传失败！')");
                }
            }
            //ADD 展示类型 by chenzhiqiu 20150114

            string zsClass = "列表";
            if (RBIcon.Checked == true)
                zsClass = "图标";
            else if (RBIntroduce.Checked == true)
                zsClass = "介绍";
            else if (RBBigEvent.Checked == true)
                zsClass = "大事记";
            string xbdm = Request["yxdm"].ToString();

            //END ADD 展示类型 by chenzhiqiu 20150114
            string sqlInsertLanm = "INSERT INTO xibu_lanm(lmmc,mcsm,lx,fid,px,gjz,sfcdxs,sfdhxs,dhcdh,sfjlrz,sftop,sfqxyz,lmyyqx,lmtp,url,dkfs,xbdm) VALUES(@lmmc,@mcsm,@lx,@fid,@px,@gjz,@sfcdxs,@sfdhxs,@dhcdh,@sfjlrz,@sftop,@sfqxyz,@lmyyqx,@lmtp,@url,@dkfs,@xbdm)";
            if (Sqlhelper.ExcuteNonQuery(sqlInsertLanm,
                new SqlParameter("lmmc", addStructLanm.lmmc),
                new SqlParameter("mcsm", addStructLanm.mcsm),
                new SqlParameter("lx", zsClass),
                new SqlParameter("fid", addStructLanm.fid),
                new SqlParameter("px", addStructLanm.px),
                new SqlParameter("gjz", addStructLanm.gjz),
                new SqlParameter("sfcdxs", addStructLanm.sfcdxs),
                new SqlParameter("sfdhxs", addStructLanm.sfdhxs),
                new SqlParameter("dhcdh", addStructLanm.dhcdh),
                new SqlParameter("sfjlrz", addStructLanm.sfjlrz),
                new SqlParameter("sftop", addStructLanm.sftop),
                new SqlParameter("sfqxyz", addStructLanm.sfqxyz),
                new SqlParameter("lmyyqx", addStructLanm.lmyyqx),
                new SqlParameter("lmtp", addStructLanm.lmtp),
                new SqlParameter("url", addStructLanm.url),
                new SqlParameter("dkfs", addStructLanm.dkfs),
                new SqlParameter("xbdm", xbdm)) > 0)
            {
                basic.MsgBox(this.Page, "添加栏目成功！", "");
            }
            else
            {
                basic.MsgBox(this.Page, "添加栏目失败！", "");
            }
        }

        else if (this.tijiao.Text == "确认修改")
        {//更改lanm表数据

            //定义添加栏目结构体
            structLanM editStructLanm = new structLanM();

            //ADD 展示类型 by chenzhiqiu 20150114

            string zsClass = "列表";
            if (RBIcon.Checked == true)
                zsClass = "图标";
            else if (RBIntroduce.Checked == true)
                zsClass = "介绍";
            else if (RBBigEvent.Checked == true)
                zsClass = "大事记";

            //END ADD 展示类型 by chenzhiqiu 20150114

            //string checks = Request.Form["powerList"];


            editStructLanm.lmmc = TB_Name0.Value.ToString().Trim();
            editStructLanm.mcsm = TB_Content.Value.ToString().Trim();
            //若选择根目录，则fid字段设为－1
            //editStructLanm.fid = DropDownList1.SelectedValue.ToString();
            //int fid = (editStructLanm.fid == ("－1")) ? -1 : Convert.ToInt32(editStructLanm.fid);
            //if (DropDownList3.SelectedIndex > 0)
            //{
            //    editStructLanm.fid = DropDownList3.SelectedValue.ToString().Trim();
            //}
            //else if (DropDownList2.SelectedIndex > 0)
            if (DropDownList2.SelectedIndex > 0)
            {
                editStructLanm.fid = DropDownList2.SelectedValue.ToString().Trim();
            }
            else
            {
                editStructLanm.fid = DropDownList1.SelectedValue.ToString().Trim();
            }
            editStructLanm.px = TB_OrderNum.Value.ToString().Trim();
            editStructLanm.gjz = TB_Name.Value.ToString().Trim();
            editStructLanm.sfcdxs = sfcdxs.Checked ? "1" : "0";
            editStructLanm.sfdhxs = sfdhxs.Checked ? "1" : "0";
            editStructLanm.sftop = sftop.Checked ? "1" : "0";
            editStructLanm.sfjlrz = sfjlrz.Checked ? "1" : "0";
            editStructLanm.dhcdh = "";
            //editStructLanm.dhcdh = TB_Tree.Value.ToString().Trim();
            editStructLanm.lmtp = TextBox1.Text.ToString().Trim();
            editStructLanm.url = TextBoxURL.Text.ToString().Trim();

            editStructLanm.dkfs = ddlDaKaiFangShi.SelectedValue.ToString();
            //editStructLanm.dkfs = TextBox3.Text.ToString().Trim();
            editStructLanm.sfqxyz = this.GetLanm(Int32.Parse(Request["id"])).Rows[0]["sfqxyz"].ToString();
            //获取栏目所有权限
            //for (int i = 0; i < rptAllEnablePower.Items.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)rptAllEnablePower.Items[i].FindControl("powerList") as CheckBox;
            //    if ("chk" != null && chk.Checked == true)
            //    {
            //        Label CKID = (Label)rptAllEnablePower.Items[i].FindControl("CheckBoxID");
            //        editStructLanm.lmyyqx = editStructLanm.lmyyqx + CKID.Text + ",";
            //    }
            //}

            //获取需要的权限验证
            //string powers = hiddenID.Value;
            //string[] groupPowers = null;
            //if (powers != "")
            //{
            //    groupPowers = new Power().getFromQx(powers);
            //    editStructLanm.sfqxyz = "";//清空再填充修改后的数据
            //    foreach (string power in groupPowers)
            //    {
            //        if (power != "")
            //        {
            //            try
            //            {
            //                string sqlStringGetQxidByQxmc = "SELECT Qxid FROM quanxdm WHERE qxmc=@qxmc";
            //                DataTable dt = Sqlhelper.Serach(sqlStringGetQxidByQxmc, new SqlParameter("qxmc", power));                            
            //                if (dt.Rows.Count > 0)
            //                {
            //                    //防止出现重置后checkbox不消失的bug
            //                    if (editStructLanm.lmyyqx.Contains(dt.Rows[0][0].ToString()))
            //                        editStructLanm.sfqxyz = editStructLanm.sfqxyz + dt.Rows[0][0].ToString() + ',';
            //                }

            //            }
            //            catch
            //            {
            //                basic.MsgBox(this.Page, "权限列表获取失败", "-1");
            //            }
            //        }
            //    }
            //}

            //上传照片及数据写入数据库
            bool fileOK = false;
            string path = Server.MapPath("~/admin/images");
            if (FileUpload1.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                string[] allowedExtensions = { ".gif", "png", ".bmp", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                try
                {
                    FileUpload1.SaveAs(path + editStructLanm.lmtp);

                }
                catch
                {
                    // Response.Write("<script type='text/javascript'>alert('图片上传失败！')");
                }
            }
            if (editStructLanm.lmyyqx == null) editStructLanm.lmyyqx = "";
            string sqlUpdateLanm = "UPDATE xibu_lanm SET lmmc=@lmmc,mcsm=@mcsm,lx=@lx,fid=@fid,px=@px,gjz=@gjz,sfcdxs=@sfcdxs,sfdhxs=@sfdhxs,dhcdh=@dhcdh,sfjlrz=@sfjlrz,sftop=@sftop,sfqxyz=@sfqxyz,lmyyqx=@lmyyqx,lmtp=@lmtp,url=@url,dkfs=@dkfs WHERE lmid=@lmid";
            if (Sqlhelper.ExcuteNonQuery(sqlUpdateLanm,
                new SqlParameter("lmmc", editStructLanm.lmmc),
                new SqlParameter("mcsm", editStructLanm.mcsm),
                new SqlParameter("lx", zsClass),
                new SqlParameter("fid", editStructLanm.fid),
                new SqlParameter("px", editStructLanm.px),
                new SqlParameter("gjz", editStructLanm.gjz),
                new SqlParameter("sfcdxs", editStructLanm.sfcdxs),
                new SqlParameter("sfdhxs", editStructLanm.sfdhxs),
                new SqlParameter("dhcdh", editStructLanm.dhcdh),
                new SqlParameter("sfjlrz", editStructLanm.sfjlrz),
                new SqlParameter("sftop", editStructLanm.sftop),
                new SqlParameter("sfqxyz", editStructLanm.sfqxyz),
                new SqlParameter("lmyyqx", editStructLanm.lmyyqx),
                new SqlParameter("lmtp", editStructLanm.lmtp),
                new SqlParameter("url", editStructLanm.url),
                new SqlParameter("dkfs", editStructLanm.dkfs),
                new SqlParameter("lmid", Request["id"].ToString())) > 0)
            {
                basic.MsgBox(this.Page, "修改栏目成功！", "");
            }
            else
            {
                basic.MsgBox(this.Page, "修改栏目失败！", "");
            }
        }
        else
        {
            //参数不正确下点击了“提交”
        }
    }
    protected void DDL1XiBu_OnSelectedIndesChanged(object sender, EventArgs e)
    {
        DropDownList2.Visible = true;
        string lmid = DropDownList1.SelectedValue.ToString().Trim();
        DataTable lanmuXX = this.GetLanm(Int32.Parse(lmid));
        if (lanmuXX != null && lanmuXX.Rows.Count > 0)
        {
            //this.dhcdhTiSshi.Text = "该栏目导航编号（菜单号）：" + lanmuXX.Rows[0]["dhcdh"].ToString();
            //this.TB_Tree.Value = lanmuXX.Rows[0]["dhcdh"].ToString();

        }
        else
        {
            this.dhcdhTiSshi.Text = "";
        }

        DataTable lanmuxxByFid = this.GetLanm(lmid,Request["yxdm"].ToString());
        if (lanmuxxByFid != null && lanmuxxByFid.Rows.Count > 0)
        {
            DropDownList2.DataSource = lanmuxxByFid;
            DropDownList2.DataTextField = "lmmc";
            DropDownList2.DataValueField = "lmid";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "选择根目录下子栏目");
        }
        else
        {
            DropDownList2.Items.Clear();
            DropDownList2.Visible = false;
        }

    }
    //protected void DDL2XiBu_OnSelectedIndesChanged(object sender, EventArgs e)
    //{
    //    if (this.tijiao.Text == "确认添加") return;
    //    DropDownList3.Visible = true;
    //    string lmid = DropDownList2.SelectedValue.ToString().Trim();
    //    DataTable lanmuxxByFid = this.GetLanm(lmid);
    //    if (lanmuxxByFid != null && lanmuxxByFid.Rows.Count > 0)
    //    {
    //        DropDownList3.DataSource = lanmuxxByFid;
    //        DropDownList3.DataTextField = "lmmc";
    //        DropDownList3.DataValueField = "lmid";
    //        DropDownList3.DataBind();
    //        DropDownList3.Items.Insert(0, "选择二级子栏目");
    //    }
    //    else
    //    {
    //        DropDownList3.Items.Clear();
    //        DropDownList3.Visible = false;
    //    }
    //}

    /// <summary>
    /// 获取所有栏目所有信息
    /// </summary>
    /// <returns></returns>
    protected DataTable GetLanm()
    {
        try
        {
            string sqlStrGetFuLanm = "SELECT lmid,lmmc FROM xw_lanm WHERE fid ='-1' or fid='0'";
            string sqlStruGetSecLanm = "Select a.lmid,a.lmmc from xw_lanm a,(select lmid,lmmc,fid from lanm where fid='-1' or fid='0') b where a.fid = b.lmid";
            DataTable dtFuLanm = Sqlhelper.Serach(sqlStrGetFuLanm);
            DataTable dtSecLanm = Sqlhelper.Serach(sqlStruGetSecLanm);
            dtFuLanm.Merge(dtSecLanm);
            return dtFuLanm;
        }
        catch
        {
            return new DataTable();
        }
    }
    /// <summary>
    /// (重载)按lanmid查询栏目信息
    /// </summary>
    /// <returns></returns>
    protected DataTable GetLanm(int lmid)
    {
        try
        {
            string sqlStrGetLanm = "SELECT * FROM xibu_lanm WHERE lmid=@lmid";
            return Sqlhelper.Serach(sqlStrGetLanm, new SqlParameter("lmid", lmid));
        }
        catch
        {
            return new DataTable();
        }
    }
    /// <summary>
    /// (重载)按fid查询栏目信息
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    protected DataTable GetLanm(string fid,string yxdm)
    {
        try
        {
            string sqlStrGetLanm = "SELECT * FROM xibu_lanm WHERE fid=@fid and xbdm='"+yxdm+"' ";
            return Sqlhelper.Serach(sqlStrGetLanm, new SqlParameter("fid", fid));
        }
        catch
        {
            return new DataTable();
        }
    }
    /// <summary>
    /// 查询所有权限名称
    /// </summary>
    /// <returns></returns>
    protected DataTable getQuanxian()
    {
        try
        {
            string sqlStrQuanxian = "SELECT * FROM quanxdm";
            return Sqlhelper.Serach(sqlStrQuanxian);
        }
        catch
        {
            return new DataTable();
        }
    }
    /// <summary>
    /// (重载)对过权限ID来查询权限名称
    /// </summary>
    /// <param name="qxid"></param>
    /// <returns></returns>
    protected DataTable getQuanxian(int qxid)
    {
        try
        {
            string sqlStrQuanxian = "SELECT * FROM quanxdm WHERE qxid=@qxid";
            return Sqlhelper.Serach(sqlStrQuanxian, new SqlParameter("qxid", qxid));
        }
        catch
        {
            return new DataTable();
        }
    }
    /// <summary>
    /// 增加栏目页面初始化绑定数据
    /// </summary>
    protected void EddBind()
    {
        DataTable genglanmu1 = this.GetLanm("-1",Request["yxdm"].ToString());
        //genglanmu1.Merge(this.GetLanm("0"));
        DropDownList1.DataSource = genglanmu1;
        DropDownList1.DataTextField = "lmmc";
        DropDownList1.DataValueField = "lmid";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("根目录", "-1"));

        //rptAllEnablePower.DataSource = this.getQuanxian();
        //rptAllEnablePower.DataBind();

    }
    /// <summary>
    /// 修改栏目页面初始化绑定数据
    /// </summary>
    protected void editBind()
    {
        structLanM editBindStructLanm = new structLanM();
        //ADD 展示类型 BY chenzhiqiu 20150114
        string zsClass = RBList.Text.Trim();
        //END ADD 展示类型
        string strYijiLanmName = "根目录";  //存父栏目
        string strErJiLanmName = "";

        DataTable dt = this.GetLanm(Convert.ToInt32(Request["id"].ToString().Trim()));
        if (dt.Rows.Count > 0)
        {
            //获取相关栏目信息
            editBindStructLanm.lmmc = dt.Rows[0]["lmmc"].ToString();
            editBindStructLanm.mcsm = dt.Rows[0]["mcsm"].ToString();
            //add
            zsClass = dt.Rows[0]["lx"].ToString();
            //end add
            editBindStructLanm.fid = dt.Rows[0]["fid"].ToString();
            editBindStructLanm.px = dt.Rows[0]["px"].ToString();
            editBindStructLanm.gjz = dt.Rows[0]["gjz"].ToString();
            editBindStructLanm.sfcdxs = dt.Rows[0]["sfcdxs"].ToString();
            editBindStructLanm.sfdhxs = dt.Rows[0]["sfdhxs"].ToString();
            editBindStructLanm.dhcdh = dt.Rows[0]["dhcdh"].ToString();
            editBindStructLanm.sfjlrz = dt.Rows[0]["sfjlrz"].ToString();
            editBindStructLanm.sftop = dt.Rows[0]["sftop"].ToString();
            editBindStructLanm.sfqxyz = dt.Rows[0]["sfqxyz"].ToString();
            editBindStructLanm.lmyyqx = dt.Rows[0]["lmyyqx"].ToString();
            editBindStructLanm.lmtp = dt.Rows[0]["lmtp"].ToString();
            editBindStructLanm.url = dt.Rows[0]["url"].ToString();
            editBindStructLanm.dkfs = dt.Rows[0]["dkfs"].ToString();
        }
        else
        { basic.MsgBox(this.Page, "没有查询到相关数据！", "-1"); }

        //查询所属栏目名称,默认为"根目录"
        if (editBindStructLanm.fid != "-1" && editBindStructLanm.fid != "0")//为真，说明有子栏目
        {
            DataTable dtSerchDangQianlanm = this.GetLanm(Convert.ToInt32(editBindStructLanm.fid));//查找当前栏目
            string strDangQianlanmFid = "";//存当前栏目的fid
            string strDangQianLanmName = "";//存当前栏目的名称
            if (dtSerchDangQianlanm.Rows.Count > 0)
            {
                strDangQianLanmName = dtSerchDangQianlanm.Rows[0]["lmmc"].ToString();
                strDangQianlanmFid = dtSerchDangQianlanm.Rows[0]["fid"].ToString();

                if (strDangQianlanmFid != "-1" && strDangQianlanmFid != "0")//为真，说明有三级栏目
                {
                    //DataTable dtSanJiFulanm = this.GetLanm(editBindStructLanm.fid);
                    //DropDownList3.Visible = true;
                    //DropDownList3.DataSource = dtSanJiFulanm;
                    //DropDownList3.DataTextField = "lmmc";
                    //DropDownList3.DataValueField = "lmid";
                    //DropDownList3.DataBind();
                    //DropDownList3.Items.Insert(0, "选择二级目录下的子栏目");
                    //DropDownList3.Items.FindByText(editBindStructLanm.lmmc).Selected = true;


                    DataTable dtErjiLanm = this.GetLanm(strDangQianlanmFid,Request["yxdm"].ToString());
                    DropDownList2.Visible = true;
                    DropDownList2.DataSource = dtErjiLanm;
                    DropDownList2.DataTextField = "lmmc";
                    DropDownList2.DataValueField = "lmid";
                    DropDownList2.DataBind();
                    DropDownList2.Items.Insert(0, "选择根目录下的子栏目");
                    DropDownList2.Items.FindByText(strDangQianLanmName);

                    DataTable dtErjilanmFulan = this.GetLanm(Int32.Parse(strDangQianlanmFid));
                    strYijiLanmName = dtErjilanmFulan.Rows[0]["lmmc"].ToString();

                    DataTable dtFuLanmu = this.GetLanm("-1",Request["yxdm"].ToString());
                    //dtFuLanmu.Merge(this.GetLanm("0"));
                    DropDownList1.DataSource = dtFuLanmu;
                    DropDownList1.DataTextField = "lmmc";
                    DropDownList1.DataValueField = "lmid";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("根目录", "-1"));
                    DropDownList1.Items.FindByText(strYijiLanmName).Selected = true;


                }
                else
                { //只有二级栏目
                    DataTable dtErJiFulanm = this.GetLanm(editBindStructLanm.fid,Request["yxdm"].ToString());//得到二级栏目
                    strErJiLanmName = editBindStructLanm.lmmc;
                    DropDownList2.Visible = true;
                    DropDownList2.DataSource = dtErJiFulanm;
                    DropDownList2.DataTextField = "lmmc";
                    DropDownList2.DataValueField = "lmid";
                    DropDownList2.DataBind();
                    DropDownList2.Items.Insert(0, "选择根目录下的子栏目");
                    DropDownList2.Items.Remove(DropDownList2.Items.FindByText(editBindStructLanm.lmmc));
                    //DropDownList2.Items.FindByText(editBindStructLanm.lmmc).Selected = true;


                    DataTable dtFuLanmu = this.GetLanm("-1",Request["yxdm"].ToString());
                    //dtFuLanmu.Merge(this.GetLanm("0"));
                    DropDownList1.DataSource = dtFuLanmu;
                    DropDownList1.DataTextField = "lmmc";
                    DropDownList1.DataValueField = "lmid";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("根目录", "-1"));
                    DropDownList1.Items.FindByText(strDangQianLanmName).Selected = true;

                }

            }
            else
            {
                basic.MsgBox(this.Page, "栏目名称有误!", "-1");
            }

        }
        else
        { //当前为根目录
            DataTable dtFuLanmu = this.GetLanm("-1",Request["yxdm"].ToString());
            //dtFuLanmu.Merge(this.GetLanm("0"));
            DropDownList1.DataSource = dtFuLanmu;
            DropDownList1.DataTextField = "lmmc";
            DropDownList1.DataValueField = "lmid";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("根目录", "-1"));
            //DropDownList1.Items.FindByText(editBindStructLanm.lmmc).Selected = true;
        }


        if (editBindStructLanm.sfcdxs == "1") sfcdxs.Checked = true; else sfcdxs.Checked = false;
        if (editBindStructLanm.sfdhxs == "1") sfdhxs.Checked = true; else sfdhxs.Checked = false;
        if (editBindStructLanm.sfjlrz == "1") sfjlrz.Checked = true; else sfjlrz.Checked = false;
        if (editBindStructLanm.sftop == "1") sftop.Checked = true; else sftop.Checked = false;

        TB_Name0.Value = editBindStructLanm.lmmc;
        TB_Name.Value = editBindStructLanm.gjz;
        TB_Content.Value = editBindStructLanm.mcsm;
        //TB_Tree.Value = editBindStructLanm.dhcdh;
        TextBox1.Text = editBindStructLanm.lmtp;
        TextBoxURL.Text = editBindStructLanm.url;
        ddlDaKaiFangShi.DataBind();
        ddlDaKaiFangShi.Items.FindByValue(editBindStructLanm.dkfs).Selected = true;
        //TextBox3.Text = editBindStructLanm.dkfs;
        TB_OrderNum.Value = editBindStructLanm.px;
        //填充栏目模块项
        //rptAllEnablePower.DataSource = this.getQuanxian();
        //rptAllEnablePower.DataBind();        
        //string[] IDs = new Power().getFromQx(editBindStructLanm.lmyyqx);
        //string name = "";
        //if (IDs != null)
        //{
        //    foreach (string id in IDs)
        //    {
        //        DataTable dtGetLanmQuanxianName = this.getQuanxian(Convert.ToInt32(id));
        //        if (dt.Rows.Count > 0)
        //        {
        //            name = name + dtGetLanmQuanxianName.Rows[0]["qxmc"].ToString() + ",";
        //        }
        //    }
        //}
        //if (name != "")
        //{
        //    string[] names = new Power().getFromQx(name);
        //    for (int i = 0; i < rptAllEnablePower.Controls.Count; i++)
        //    {
        //        CheckBox powerChk = rptAllEnablePower.Items[i].FindControl("powerList") as CheckBox;
        //        if (names != null)
        //        {
        //            foreach (string x in names)
        //            {
        //                if (x == powerChk.Text) powerChk.Checked = true;
        //            }
        //        }
        //    }
        //}
        //editStructLanm.lmyyqx = name;//将栏目“栏目拥有权限”id序列变为名称序列，方便查询判断

        //填充权限列表
        //enablePower.Text = "";
        //string[] PowerListIDs = new Power().getFromQx(editBindStructLanm.sfqxyz);
        //if (editBindStructLanm.sfqxyz != "0" && PowerListIDs != null)
        //{
        //    foreach (string PowerListID in PowerListIDs)
        //    {
        //        DataTable dtGetLanmPowerListName = this.getQuanxian(Convert.ToInt32(PowerListID));
        //        if (dt.Rows.Count > 0)
        //        {
        //            enablePower.Text = enablePower.Text +
        //                "<input type='checkbox' checked=true class='enableCheckbox' onclick='getEnablePowerCheckbox()' value='" + dtGetLanmPowerListName.Rows[0]["qxmc"].ToString() + "' /><lable>" + dtGetLanmPowerListName.Rows[0]["qxmc"].ToString() + "</lable>";
        //        }
        //    }
        //}
        //ADD 展示类型
        //string zsClass = "列表";
        if (zsClass == "图标")
            RBIcon.Checked = true;
        else if (zsClass == "介绍")
            RBIntroduce.Checked = true;
        else if (zsClass == "大事记")
            RBBigEvent.Checked = true;
        //END ADD

    }
    protected bool isKeyWordRepeat(string KeyWork)
    {
        string sqlstring = "SELECT gjz FROM xw_lanm WHERE gjz=@gjz";
        try
        {
            return (Sqlhelper.Serach(sqlstring, new SqlParameter("gjz", KeyWork)).Rows.Count > 0) ? true : false;
        }
        catch
        {
            return true;
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Image1.ImageUrl = this.DropDownList3.SelectedValue;
        this.TextBox1.Text = this.DropDownList3.SelectedValue;
    }
}