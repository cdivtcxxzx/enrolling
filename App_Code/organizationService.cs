﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model;

/// <summary>
///组织机构服务
/// bool 操作员身份是否有效
/// 操作员 获取某操作员数据
/// 学生 获取某学生数据
/// 专业 获取某专业数据
/// 班级 获取某班级数据
/// bool 学生身份是否有效
/// </summary>
public static class organizationService
{
    public static organizationModelDataContext oDC = new organizationModelDataContext();
    /// <summary>
    /// 功能描述：根据“员工编号”查询获取“密码明文”，如果“密文”==校验函数(“密码明文”,“验证码”)返回true。否则返回false。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="staffNo">员工编号</param>
    /// <param name="password">密码</param>
    /// <param name="verifyCode">验证码</param>
    /// <param name="pwdEncode">密文</param>
    /// <returns></returns>
    public static bool staffVerify(string staffNo,string password,string verifyCode,string pwdEncode)
    {
        Base_Staff staff = oDC.Base_Staffs.Where(s => s.PK_Staff_NO == staffNo && s.Password == pwdEncode).SingleOrDefault();
        if (staff == null) return false;
        if (pwdEncode == md5.MD5Encrypt(password, verifyCode))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 根据“员工编号”返回员工基本数据。否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="staffNo">员工编号</param>
    /// <returns>返回员工类</returns>
    public static Base_Staff getOperator(string staffNo)
    {
        return oDC.Base_Staffs.Where(s => s.PK_Staff_NO == staffNo).SingleOrDefault();
    }

    /// <summary>
    /// 根据“学号”返回学生基本数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sno">学号</param>
    /// <returns>返回学生JSON数据</returns>
    public static Base_STU getStu(string sno)
    {
        return oDC.Base_STUs.Where(s => s.PK_SNO == sno).SingleOrDefault();
    }

    /// <summary>
    /// 根据“学年”查找“专业编号”并返回对应专业数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="year">学年</param>
    /// <param name="sepCode">专业编号</param>
    /// <returns>返回专业类</returns>
    public static Fresh_SPE getSpe(string year,string sepCode)
    {
        return oDC.Fresh_SPEs.Where(s => s.Year == year && s.SPE_Code == sepCode).SingleOrDefault();
    }

    /// <summary>
    /// 根据“班级编号”查找并返回对应班级数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="classNo">班级编号</param>
    /// <returns>返回班级类</returns>
    public static Fresh_Class getClass(string classNo)
    {
        return oDC.Fresh_Classes.Where(b => b.PK_Class_NO == classNo).SingleOrDefault();
    }

    /// <summary>
    /// 功能描述：根据“学号”查询所在批次中“禁止操作标志”为false，并且被授权“迎新事务”的“事务性质”为“交互性”，“事务列席”为“学生自治”或“两者”的数据。否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sno">学号</param>
    /// <param name="password">密码</param>
    /// <param name="verifyCode">验证码</param>
    /// <param name="pwdEncode">密文</param>
    /// <returns></returns>
    public static bool stuVerify(string sno, string password, string verifyCode, string pwdEncode)
    {
        Base_STU stu = oDC.Base_STUs.Where(s => s.PK_SNO == sno && s.Password == pwdEncode).SingleOrDefault();
        if (stu == null) return false;
        if (pwdEncode == md5.MD5Encrypt(password, verifyCode))
            return true;
        else
            return false;
    }
}