﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///名称：班级预分配床位
///作者：张明
/// </summary>
public class Fresh_Bed_Class_Log
{
    //班级预分配床位
    public string PK_Bed_Class_Log;//主键
    public string FK_Bed_No;//床位主键
    public string FK_Class_NO;//班级编号

}
/// <summary>
///名称：班级预分配床位类
///作者：张明
/// </summary>
public class Fresh_Bed_Log
{
    
    //班级预分配床位类
    public string PK_Bed_Log{ get; set; }//主键
    public string FK_Bed_No{ get; set; }//床位主键
    public string FK_SNO{ get; set; }//学号
    public string Updater{ get; set; }//操作人
    public DateTime Update_DT{ get; set; }//操作时间
}
/// <summary>
///名称：床位类
///作者：张明
/// </summary>
public class Fresh_Bed{
    //床位类
    public string PK_Bed_No{ get; set; }//床位主键
    public string Bed_NO{ get; set; }//床位编号
    public string FK_Bed_Type{ get; set; }//床位位置主键
    public string FK_Room_NO{ get; set; }//房间主键
}
/// <summary>
///名称：宿舍类
///作者：张明
/// </summary>
public class Fresh_Dorm{
    //宿舍类
     public string PK_Dorm_NO{ get; set; }//主键
    public string Dorm_NO{ get; set; }//宿舍号
    public string Year{ get; set; }//学年
    public string Name{ get; set; }//宿舍名称
    public string Campus_NO{ get; set; }//校区编号
}
/// <summary>
///名称：房间类
///作者：张明
/// </summary>
public class Fresh_Room{
    //房间类
     public string PK_Room_NO{ get; set; }//主键
    public string Room_NO{ get; set; }//房间编号
    public string FK_Dorm_NO{ get; set; }//宿舍主键
    public string Floor{ get; set; }//楼层
    public string Gender{ get; set; }//入住性别编码
    public string FK_Room_Type{ get; set; }//房间类型主键
}
/// <summary>
///名称：房间类型
///作者：张明
/// </summary>
public class Fresh_Room_Type{
    //房间类型
     public string PK_Room_Type{ get; set; }//主键
    public string Type_NO{ get; set; }//房间类型编码
    public string FK_Fee_Item{ get; set; }//收费项目代码
    public string Year{ get; set; }//学年
    public string Room_Layout{ get; set; }//房间布局简图
    public string Bed_Layout{ get; set; }//床位布局简图
    public string Type_Name { get; set; }//房间类型名称
}
/// <summary>
///名称：床位位置（类型）
///作者：张明
/// </summary>
public class Fresh_Bed_Type{
    //床位位置（类型）
     public string PK_Bed_Type{ get; set; }//主键
    public string Type_Name{ get; set; }//床位类型
    public string FK_Room_Type{ get; set; }//房间类型主键
    public string Bed_Index{ get; set; }//床位序号
    public string Bed_NO{ get; set; }//床位编号
    
}

//将datatable转换为实体list的类
//用法： List<类名> result = ModelConvertHelper<类名>.ConvertToModel(datatable);
public class ModelConvertHelper<T> where T : new()
{
    public static IList<T> ConvertToModel(DataTable dt)
    {
        // 定义集合    
        IList<T> ts = new List<T>();

        // 获得此模型的类型   
        Type type = typeof(T);
        string tempName = "";

        foreach (DataRow dr in dt.Rows)
        {
            T t = new T();
            // 获得此模型的公共属性      
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;  // 检查DataTable是否包含此列    

                if (dt.Columns.Contains(tempName))
                {
                    // 判断此属性是否有Setter      
                    if (!pi.CanWrite) continue;

                    object value = dr[tempName];
                    if (value != DBNull.Value)
                        pi.SetValue(t, value, null);
                }
            }
            ts.Add(t);
        }
        return ts;
    }     
    public static List<T> FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            string tempName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                //string x = dr["FK_SNO"].ToString();
                //T model = (T)Activator.CreateInstance(typeof(T));  
                T model = new T();
                PropertyInfo[] propertys = model.GetType().GetProperties();
                foreach(PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if(dt.Columns.Contains(tempName))
                    {
                        object value = dr[tempName];
                        if(value!=DBNull.Value)
                        {
                            pi.SetValue(model, value, null);
                        }
                    }
                }
                //foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                //{
                //    model.GetType().GetProperty(propertyInfo.Name).SetValue(model, dr[propertyInfo.Name], null);
                //}
                modelList.Add(model);
            }
            return modelList;
        }
}
/// <summary>
///dormitory 宿舍服务类
/// </summary>
public class dormitory
{
	public dormitory()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static bool logzt = true;//设置本类中方法是否记录日志,真为要记
    /// <summary>
    /// 功能描述：20学生是否已分配宿舍(学号):根据“学号”判断学生是否已经分配了宿舍，已分配返回true，否则返回false。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="xh">学号</param>
    /// <returns>真假</returns>
    public static bool isbillet(string xh)
    {
        try
        {
            //到宿舍分配表中使用学号查询，如果记录大于0，即已分配；
            DataTable billet = Sqlhelper.Serach("SELECT TOP 1 [FK_Bed_NO]  FROM [Fresh_Bed_Log] where [FK_SNO]=@xh", new SqlParameter("xh", xh));
            if (billet.Rows.Count > 0) return true;
        }
        catch (Exception err)
        {
            
                try
                {
                    if (logzt)new c_log().logAdd("dormitory.cs", "isbillet", err.Message, "2", "zhangming1");//记录错误日志
                }
                catch { }
                throw;
                //return false;
            
        }
        return false;
    }
    /// <summary>
    /// 功能描述：21床位分配 学生已分配床位(学号):根据“学号”返回学生已分配的“床位分配”datatable数据，否则返回null。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="xh">学号</param>
    /// <returns>datatable【主键、床位主键、学号、操作人、操作时间】</returns>
    public static DataTable billetdata(string xh)
    {
        DataTable billet = new DataTable();
        try
        {
            //到宿舍分配表中使用学号查询,并联查出相关床位分配信息；
            billet = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_Log],[FK_Bed_NO],[FK_SNO],[Updater],[Update_DT]  FROM [Fresh_Bed_Log] where [FK_SNO]=@xh", new SqlParameter("xh", xh));
        if(billet.Rows.Count>0)
        {
           

        }
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "billetdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }
           
        }
        return billet;
    }
    /// <summary>
    /// 功能描述：21床位分配 学生已分配床位(学号):根据“学号”返回学生已分配的“床位分配”list数据，否则返回null。
    /// 编写人：张明
    /// 创建时间：2017.3.22
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="xh">学号</param>
    /// <returns>list【主键、床位主键、学号、操作人、操作时间】</returns>
    public static List<Fresh_Bed_Log> listbilletdata(string xh)
    {
        //List<Fresh_Bed_Log> result = null;
        DataTable billet = new DataTable();
        try
        {
            //到宿舍分配表中使用学号查询,并联查出相关床位分配信息；
            billet = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_Log],[FK_Bed_NO],[FK_SNO],[Updater],[Update_DT]  FROM [Fresh_Bed_Log] where [FK_SNO]=@xh", new SqlParameter("xh", xh));
            
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "billetdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Bed_Log> result = ModelConvertHelper<Fresh_Bed_Log>.FillModel(billet);
        return result;
    }


    /// <summary>
    /// 功能描述：22床位 获取某床位数据(床位主键):根据“床位主键”返回“床位”数据table，否则返回空table。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">主键</param>
    /// <returns>datatable【主键、床位编号、床位位置主键、房间主键】</returns>
    public static DataTable getbed(string mkey)
    {
        DataTable bed = new DataTable();
        try
        {
            //到宿舍分配表中使用学号查询,并联查出相关床位分配信息；
            bed = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_NO],[Bed_NO],[FK_Bed_Type],[FK_Room_NO]  FROM [Fresh_Bed] where [PK_Bed_NO]=@mkey", new SqlParameter("mkey", mkey));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "beddata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return bed;
    }
    /// <summary>
    /// 功能描述：22床位 获取某床位数据(床位主键):根据“床位主键”返回“床位”数据table，否则返回空table。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">主键</param>
    /// <returns>datatable【主键、床位编号、床位位置主键、房间主键】</returns>
    public static List<Fresh_Bed> listgetbed(string mkey)
    {
        DataTable bed = new DataTable();
        try
        {
            //到宿舍分配表中使用学号查询,并联查出相关床位分配信息；
            bed = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_NO],[Bed_NO],[FK_Bed_Type],[FK_Room_NO]  FROM [Fresh_Bed] where [PK_Bed_NO]=@mkey", new SqlParameter("mkey", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if(logzt)new c_log().logAdd("dormitory.cs", "listbeddata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Bed> result = ModelConvertHelper<Fresh_Bed>.FillModel(bed);
        return result;
    }
    /// <summary>
    /// 功能描述：23、房间 获取某房间数据(房间主键):根据“房间主键”返回“房间”datatable数据，否则返回空TABLE。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">房间主键</param>
    /// <returns>datatable【主键、宿舍主键、房间编号、楼层、入住性别编码、房间类型编码】</returns>
    public static DataTable getroom(string mkey)
    {
        DataTable room = new DataTable();
        try
        {
            //到房间表中使用房间主键查询房间相关信息；
            room = Sqlhelper.Serach("SELECT top 1 [PK_Room_NO],[Room_NO],[FK_Dorm_NO],[Floor],[Gender],[FK_Room_Type]  FROM [Fresh_Room] where PK_Room_NO=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "roomdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return room;
    }

    /// <summary>
    /// 功能描述：23、房间 获取某房间数据(房间主键):根据“房间主键”返回“房间”list数据，否则返回空TABLE。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">房间主键</param>
    /// <returns>list【主键、宿舍主键、房间编号、楼层、入住性别编码、房间类型编码】</returns>
    public static List<Fresh_Room> listgetroom(string mkey)
    {
        DataTable room = new DataTable();
        try
        {
            //到房间表中使用房间主键查询房间相关信息；
            room = Sqlhelper.Serach("SELECT top 1 [PK_Room_NO],[Room_NO],[FK_Dorm_NO],[Floor],[Gender],[FK_Room_Type]  FROM [Fresh_Room] where PK_Room_NO=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "listroomdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Room> result = ModelConvertHelper<Fresh_Room>.FillModel(room);
        return result;
    }

    /// <summary>
    /// 功能描述：24、宿舍 获取某宿舍数据(宿舍主键):根据“宿舍主键”返回“宿舍”datatable数据，否则返回空datatable数据。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">主键</param>
    /// <returns>datatable【主键、宿舍号、学年、宿舍名称、校区编号】</returns>
    public static DataTable getdorm(string mkey)
    {
        DataTable dorm = new DataTable();
        try
        {
            //到宿舍表中使用主键查询宿舍相关信息；
            dorm = Sqlhelper.Serach("SELECT TOP 1 [PK_Dorm_NO],[Dorm_NO],[Year],[Name],[Campus_NO]  FROM [Fresh_Dorm] where PK_Dorm_NO=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "dormdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return dorm;
    }

    /// <summary>
    /// 功能描述：24、宿舍 获取某宿舍数据(宿舍主键):根据“宿舍主键”返回“宿舍”list数据，否则返回空datatable数据。
    /// 编写人：张明
    /// 创建时间：2017.3.22
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">主键</param>
    /// <returns>list【主键、宿舍号、学年、宿舍名称、校区编号】</returns>
    public static List<Fresh_Dorm> listgetdorm(string mkey)
    {
        DataTable dorm = new DataTable();
        try
        {
            //到宿舍表中使用主键查询宿舍相关信息；
            dorm = Sqlhelper.Serach("SELECT TOP 1 [PK_Dorm_NO],[Dorm_NO],[Year],[Name],[Campus_NO]  FROM [Fresh_Dorm] where PK_Dorm_NO=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "listdormdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Dorm> result = ModelConvertHelper<Fresh_Dorm>.FillModel(dorm);
        return result;
    }



    /// <summary>
    /// 功能描述：25、床位位置 获取某床位位置数据(床位位置主键):根据“床位位置主键”返回“床位位置”datatable数据，否则返回nulldatatable。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">床位位置主键</param>
    /// <returns>datatable【主键、床位类型、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static DataTable getbedtype(string mkey)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //到床位位置表中使用主键查询床位位置相关信息；
            bedtype = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_Type],[Type_Name],[FK_Room_Type],[Bed_Index],[Bed_NO] FROM [Fresh_Bed_Type] where PK_Bed_Type=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "bedtypedata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return bedtype;
    }

    /// <summary>
    /// 功能描述：25、床位位置 获取某床位位置数据(床位位置主键):根据“床位位置主键”返回“床位位置”list数据，否则返回null。
    /// 编写人：张明
    /// 创建时间：2017.3.22
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">床位位置主键</param>
    /// <returns>datatable【主键、床位类型、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static List<Fresh_Bed_Type> listgetbedtype(string mkey)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //到床位位置表中使用主键查询床位位置相关信息；
            bedtype = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_Type],[Type_Name],[FK_Room_Type],[Bed_Index],[Bed_NO] FROM [Fresh_Bed_Type] where PK_Bed_Type=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "listbedtypedata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Bed_Type> result = ModelConvertHelper<Fresh_Bed_Type>.FillModel(bedtype);
        return result;
    }

    /// <summary>
    /// 功能描述：26、房间类型 获取某房间类型数据(房间类型主键):根据“房间类型主键”返回“房间类型”datatable数据，否则返回nulldatatable。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">房间类型主键</param>
    /// <returns>datatable【主键、房间类型编码、收费项目代码、学年、房间布局简图、床位布局简图、房间类型名称】</returns>
    public static DataTable getroomtype(string mkey)
    {
        DataTable roomtype = new DataTable();
        try
        {
            //到房间类型表中使用类型主键查询房间类型相关信息；
            roomtype = Sqlhelper.Serach("SELECT TOP 1 [PK_Room_Type],[Type_NO],[FK_Fee_Item],[Year],[Room_Layout],[Bed_Layout],[Type_Name]  FROM [Fresh_Room_Type] where PK_Room_Type=@mkey", new SqlParameter("mkey", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "roomtypedata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return roomtype;
    }
    /// <summary>
    /// 功能描述：26、房间类型 获取某房间类型数据(房间类型主键):根据“房间类型主键”返回“房间类型”list数据，否则返回null。
    /// 编写人：张明
    /// 创建时间：2017.3.22
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">房间类型主键</param>
    /// <returns>list【主键、房间类型编码、收费项目代码、学年、房间布局简图、床位布局简图、房间类型名称】</returns>
    public static List<Fresh_Room_Type> listgetroomtype(string mkey)
    {
        DataTable roomtype = new DataTable();
        try
        {
            //到房间类型表中使用类型主键查询房间类型相关信息；
            roomtype = Sqlhelper.Serach("SELECT TOP 1 [PK_Room_Type],[Type_NO],[FK_Fee_Item],[Year],[Room_Layout],[Bed_Layout],[Type_Name]  FROM [Fresh_Room_Type] where PK_Room_Type=@mkey", new SqlParameter("mkey", mkey));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "listroomtypedata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Room_Type> result = ModelConvertHelper<Fresh_Room_Type>.FillModel(roomtype);
        return result;
    }

    ///// <summary>
    ///// 功能描述：27、收费款项条目 获取某收费款项条目数据(学年，收费项目代码) :根据“学年”和“收费项目代码”返回“收费款项条目”datatable数据，否则返回datatable。
    ///// 编写人：张明
    ///// 创建时间：2017.2.20
    ///// 更新记录：无
    ///// 版本记录：v0.0.1
    ///// </summary>
    ///// <param name="year">学年</param>
    ///// <param name="chargedm">收费项目代码</param>
    ///// <returns>datatable【主键、收费项目代码、条目名称、收费标准、收费款项主键】</returns>
    //public static DataTable getfee_item(string year,string feecode)
    //{
    //    DataTable fee_item = new DataTable();
    //    try
    //    {
    //        //到房间类型表中使用类型主键查询房间类型相关信息；
    //        fee_item = Sqlhelper.Serach("SELECT     TOP (1) Fresh_Fee_Item.PK_Fee_Item, Fresh_Fee_Item.FK_Fee, Fresh_Fee_Item.Fee_Code, Fresh_Fee_Item.Fee_Name, Fresh_Fee_Item.Fee_Amount FROM  Fresh_Fee_Item INNER JOIN Fresh_Fee ON Fresh_Fee_Item.FK_Fee = Fresh_Fee.PK_Fee_NO where fresh_fee.Year=@year and Fresh_Fee_Item.Fee_Code=@code", new SqlParameter("year", year), new SqlParameter("code", feecode));
    //    }
    //    catch (Exception err)
    //    {
    //        try
    //        {
    //            new c_log().logAdd("dormitory.cs", "fee_item", err.Message, "2", "zhangming1");//记录错误日志
    //            throw;
    //        }
    //        catch { }

    //    }
    //    return fee_item;
    //}


    /// <summary>
    /// 功能描述：28、房间类型[] 获取某班级当前可用的预分房间类型列表(班级编号,性别) :根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”预分配房间中当前可用床位的“房间类型”列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.2.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>
   
    /// <returns>datatable【校区代码、校区名称、性别、房间类型主键、房间类型编码、收费项目代码、学年、房间布局简图、床位布局简图、房间类型名称】</returns>
    public static DataTable classgetroomtype(string class_no, string gender)
    {
        DataTable roomtype = new DataTable();
        try
        {
            //
            roomtype = Sqlhelper.Serach("SELECT     Base_Campus.Campus_NO, Base_Campus.Campus_Name, Fresh_Room.Gender, Fresh_Room_Type.PK_Room_Type, Fresh_Room_Type.Type_NO,Fresh_Room_Type.Year, Fresh_Room_Type.Room_Layout, Fresh_Room_Type.Bed_Layout, Fresh_Room_Type.FK_Fee_Item, Fresh_Room_Type.Type_Name FROM         Fresh_Bed INNER JOIN  Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO INNER JOIN     Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type LEFT OUTER JOIN    Fresh_Bed_Class_Log INNER JOIN    Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO INNER JOIN   Base_Campus ON Fresh_Class.FK_Campus_NO = Base_Campus.Campus_NO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO where  Fresh_Bed_Class_Log.FK_class_NO=@classno and Fresh_Room.Gender=@gender", new SqlParameter("classno", class_no), new SqlParameter("gender", gender));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "listclassgetroomtype", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return roomtype;
    }
    /// <summary>
    /// 功能描述：28、房间类型[] 获取某班级当前可用的预分房间类型列表(班级编号,性别) :根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”预分配房间中当前可用床位的“房间类型”列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.2.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>
    /// <param name="class_no">性别（男或女）</param>
    /// <returns>datatable【校区代码、校区名称、性别、房间类型主键、房间类型编码、收费项目代码、学年、房间布局简图、床位布局简图、房间类型名称】</returns>
    public static List<Fresh_Room_Type> listclassgetroomtype(string class_no, string gender)
    {
        DataTable roomtype = new DataTable();
        try
        {
            //
            roomtype = Sqlhelper.Serach("SELECT     Fresh_Room_Type.PK_Room_Type, Fresh_Room_Type.Type_NO,Fresh_Room_Type.Year, Fresh_Room_Type.Room_Layout, Fresh_Room_Type.Bed_Layout, Fresh_Room_Type.FK_Fee_Item, Fresh_Room_Type.Type_Name FROM         Fresh_Bed INNER JOIN  Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO INNER JOIN     Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type LEFT OUTER JOIN    Fresh_Bed_Class_Log INNER JOIN    Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO INNER JOIN   Base_Campus ON Fresh_Class.FK_Campus_NO = Base_Campus.Campus_NO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO where  Fresh_Bed_Class_Log.FK_class_NO=@classno and Fresh_Room.Gender=@gender", new SqlParameter("classno", class_no), new SqlParameter("gender", gender));
        }
        catch (Exception err)
        {
            try
            {
                if(logzt)new c_log().logAdd("dormitory.cs", "classgetroomtype", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Room_Type> result = ModelConvertHelper<Fresh_Room_Type>.FillModel(roomtype);
        return result;
    }

    /// <summary>
    /// 功能描述：29、床位位置[] 获取某班级某房间类型可用床位位置列表(班级编号,性别,房间类型编号):
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”和“房间类型编号”的
    /// 预分配房间中当前可用床位的“床位位置”列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.2.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>

    /// <returns>datatable【校区编号、校区名称、性别、床位位置主键、床位类型、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static DataTable classgetbedtype(string class_no)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //
            bedtype = Sqlhelper.Serach("SELECT     Base_Campus.Campus_NO, Base_Campus.Campus_Name, Fresh_Room.Gender, Fresh_Bed_Type.PK_Bed_Type, Fresh_Bed_Type.Type_Name, Fresh_Bed_Type.FK_Room_Type, Fresh_Bed_Type.Bed_Index, Fresh_Bed_Type.Bed_NO FROM         Fresh_Class INNER JOIN       Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO INNER JOIN      Base_Campus ON Fresh_Class.FK_Campus_NO = Base_Campus.Campus_NO RIGHT OUTER JOIN    Fresh_Bed_Type INNER JOIN       Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN    Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO where  Fresh_Bed_Class_Log.FK_class_NO=@classno ", new SqlParameter("classno", class_no));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "classgetbedtype", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return bedtype;
    }


    /// <summary>
    /// 功能描述：29、床位位置[] 获取某班级某房间类型可用床位位置列表(班级编号,性别,房间类型编号):
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”和“房间类型编号”的
    /// 预分配房间中当前可用床位的“床位位置”列表的list数据，否则返回空。
    /// 编写人：张明
    /// 创建时间：2017.3.23
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>
     /// <param name="gender">性别</param>
     /// <param name="roomtypebh">房间类型编号</param>
    /// <returns>list【床位位置主键、床位类型名称、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static List<Fresh_Bed_Type> listclassgetbedtype(string class_no,string gender,string roomtypebh)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //
            bedtype = Sqlhelper.Serach("SELECT      Fresh_Bed_Type.PK_Bed_Type, Fresh_Bed_Type.Type_Name, Fresh_Bed_Type.FK_Room_Type, Fresh_Bed_Type.Bed_Index, Fresh_Bed_Type.Bed_NO FROM         Fresh_Class INNER JOIN       Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO INNER JOIN      Base_Campus ON Fresh_Class.FK_Campus_NO = Base_Campus.Campus_NO RIGHT OUTER JOIN    Fresh_Bed_Type INNER JOIN       Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN    Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO where  Fresh_Bed_Class_Log.FK_class_NO=@classno and Fresh_Room.Gender=@gender and Fresh_Room_Type.Type_NO=@roomtypebh ", new SqlParameter("classno", class_no), new SqlParameter("gender", gender), new SqlParameter("roomtypebh", roomtypebh));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "classgetbedtype", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Bed_Type> result = ModelConvertHelper<Fresh_Bed_Type>.FillModel(bedtype);
        return result;
    }

    /// <summary>
    /// 功能描述：30、床位[] 获取某班级某房间类型某床位位置的可用床位列表(班级编号,性别,房间类型编号,床位位置序号):
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”、“房间类型编号”和“床位位置序号”的
    /// 预分配房间中当前可用床位列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.2.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// 已经停止使用
    /// </summary>
    /// <param name="class_no">班级编号</param>
   
    /// <returns>datatable【校区编号、校区名称、性别、床位位置主键、床位类型、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static DataTable classgetbed(string class_no)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //
            bedtype = Sqlhelper.Serach("SELECT     Base_Campus.Campus_NO, Base_Campus.Campus_Name, Fresh_Room.Gender, Fresh_Bed_Type.PK_Bed_Type, Fresh_Bed_Type.Type_Name, Fresh_Bed_Type.FK_Room_Type, Fresh_Bed_Type.Bed_Index, Fresh_Bed_Type.Bed_NO FROM         Fresh_Class INNER JOIN     Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO INNER JOIN   Base_Campus ON Fresh_Class.FK_Campus_NO = Base_Campus.Campus_NO RIGHT OUTER JOIN   Fresh_Bed_Type INNER JOIN    Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN  Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO where  Fresh_Bed_Class_Log.FK_class_NO=@classno", new SqlParameter("classno", class_no));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "classgetbed", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return bedtype;
    }


    /// <summary>
    /// 功能描述：30、床位[] 获取某班级某房间类型某床位位置的可用床位列表(班级编号,性别,房间类型编号,床位位置序号):
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”、“房间类型编号”和“床位位置序号”的
    /// 预分配房间中当前可用床位列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.3.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    
    /// </summary>
    /// <param name="class_no">班级编号</param>
    /// <param name="gender">性别</param>
    /// <param name="roomtypebh">房间类型编号</param>
    /// <param name="roomtypebh">房间类型编号</param>

    /// <returns>datatable【校区编号、校区名称、性别、床位位置主键、床位类型、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static List<Fresh_Bed> listclassgetbed(string class_no, string gender, string roomtypebh,string bed_index)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //
            bedtype = Sqlhelper.Serach("SELECT     Fresh_Bed.PK_Bed_NO, Fresh_Bed.Bed_NO, Fresh_Bed.FK_Bed_Type, Fresh_Bed.FK_Room_NO FROM         Fresh_Class INNER JOIN     Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO RIGHT OUTER JOIN           Fresh_Bed_Type INNER JOIN       Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN       Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO where Fresh_Bed_Class_Log.FK_class_NO=@classno and Fresh_Room.Gender=@gender and Fresh_Room_Type.Type_NO=@roomtypebh and Fresh_Bed_Type.Bed_index = @Bed_index ", new SqlParameter("classno", class_no), new SqlParameter("gender", gender), new SqlParameter("roomtypebh", roomtypebh), new SqlParameter("Bed_index", bed_index));
        }
        catch (Exception err)
        {
            try
            {
                if(logzt)new c_log().logAdd("dormitory.cs", "listclassgetbed", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Bed> result = ModelConvertHelper<Fresh_Bed>.FillModel(bedtype);
        return result;
    }


    /// <summary>
    /// 功能描述：31、宿舍[] 获取某班级某房间类型某床位位置的可用宿舍列表(班级编号,性别,房间类型编号,床位位置序号):
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”、“房间类型编号”和“床位位置序号”
    /// 的预分配房间中当前可用床位所在宿舍列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.2.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>

    /// <returns>datatable【校区编号、校区名称、宿舍主键、宿舍编号、学年、宿舍名、性别、房间类型编号、床位位置序号】</returns>
    public static DataTable classgetdorm(string class_no)
    {
        DataTable dorm = new DataTable();
        try
        {
            //
            dorm = Sqlhelper.Serach("SELECT     Base_Campus.Campus_NO, Base_Campus.Campus_Name, Fresh_Dorm.PK_Dorm_NO, Fresh_Dorm.Dorm_NO, Fresh_Dorm.Year, Fresh_Dorm.Name,Fresh_Room.Gender, Fresh_Bed_Type.FK_Room_Type, Fresh_Bed_Type.Bed_Index FROM         Fresh_Class INNER JOIN    Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO RIGHT OUTER JOIN    Base_Campus INNER JOIN    Fresh_Dorm INNER JOIN   Fresh_Bed_Type INNER JOIN   Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO ON      Base_Campus.Campus_NO = Fresh_Dorm.Campus_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO where  Fresh_Bed_Class_Log.FK_class_NO=@classno ", new SqlParameter("classno", class_no));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "classgetdorm", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return dorm;
    }

    /// <summary>
    /// 功能描述：31、宿舍[] 获取某班级某房间类型某床位位置的可用宿舍列表(班级编号,性别,房间类型编号,床位位置序号):
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”、“房间类型编号”和“床位位置序号”
    /// 的预分配房间中当前可用床位所在宿舍列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.2.20
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>
    /// <param name="gender">性别</param>
    /// <param name="Room_NO">房间类型编号</param>
    /// <param name="Bed_Index">床位位置序号</param>
    /// <returns>list【宿舍主键、宿舍编号、学年、宿舍名、校区编号】</returns>
    public static List<Fresh_Dorm> listclassgetdorm(string class_no, string gender, string Room_NO, string Bed_Index)
    {
        DataTable dorm = new DataTable();
        try
        {
            //
            dorm = Sqlhelper.Serach("SELECT     Fresh_Dorm.PK_Dorm_NO, Fresh_Dorm.Dorm_NO, Fresh_Dorm.Year, Fresh_Dorm.Name,Fresh_Dorm.Campus_NO FROM         Fresh_Room_Type INNER JOIN    Fresh_Dorm INNER JOIN        Fresh_Bed_Type INNER JOIN          Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN              Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO ON                       Fresh_Room_Type.PK_Room_Type = Fresh_Room.FK_Room_Type LEFT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO WHERE     (Fresh_Bed_Class_Log.FK_Class_NO =@class_no) AND (Fresh_Room.Gender = @gender) and Fresh_Room_Type.Type_NO=@Room_NO AND (Fresh_Bed_Type.Bed_Index = @Bed_Index)", new SqlParameter("class_no", class_no), new SqlParameter("gender", gender), new SqlParameter("Room_NO", Room_NO), new SqlParameter("Bed_Index", Bed_Index));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "classgetdorm", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        List<Fresh_Dorm> result = ModelConvertHelper<Fresh_Dorm>.FillModel(dorm);
        return result;
    }


    /// <summary>
    /// 功能描述：32、string[] 获取某班级某房间类型某床位位置某宿舍可用楼层列表(班级编号,性别,房间类型编号,床位位置序号,宿舍号)：
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”、“房间类型编号”、“床位位置序号”和“宿舍号”的
    /// 预分配房间中当前可用床位所在楼层列表的datatable数据，否则返回空datatable。
    /// 编写人：张明
    /// 创建时间：2017.3.23
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>
    /// <param name="gender">性别</param>
    /// <param name="Room_NO">房间类型编号</param>
    /// <param name="Bed_Index">床位位置序号</param>
    /// <param name="Dorm_NO">宿舍号</param>
    /// <returns>string[]【可用楼层列表】</returns>
    public static List<String> listFloor(string class_no, string gender, string Room_NO, string Bed_Index, string Dorm_NO)
    {

        DataTable dorm = new DataTable();
        List<String> str = new List<String>();
        try
        {
            //根据条件查询楼层信息
            dorm = Sqlhelper.Serach("SELECT     distinct Fresh_Room.Floor FROM         Fresh_Room_Type INNER JOIN    Fresh_Dorm INNER JOIN        Fresh_Bed_Type INNER JOIN          Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN              Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO ON                       Fresh_Room_Type.PK_Room_Type = Fresh_Room.FK_Room_Type LEFT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO WHERE     (Fresh_Bed_Class_Log.FK_Class_NO =@class_no) AND (Fresh_Room.Gender = @gender) and Fresh_Room_Type.Type_NO=@Room_NO AND (Fresh_Bed_Type.Bed_Index = @Bed_Index) and Fresh_Dorm.Dorm_NO=@Dorm_NO order by Fresh_Room.Floor", new SqlParameter("class_no", class_no), new SqlParameter("gender", gender), new SqlParameter("Room_NO", Room_NO), new SqlParameter("Bed_Index", Bed_Index), new SqlParameter("Dorm_NO", Dorm_NO));
        }
        catch (Exception err)
        {
            try
            {
                if(logzt)new c_log().logAdd("dormitory.cs", "listfloor", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        if(dorm.Rows.Count>0)
        {
            for(int i=0;i<dorm.Rows.Count;i++)
            {
                str.Add(dorm.Rows[i][0].ToString());
                //将楼层数据添加到list<string>中
            }
        }
                
        return str;
    }

    /// <summary>
    /// 功能描述：33房间[] 获取某班级某房间类型某床位位置某宿舍某楼层可用房间列表(班级编号,性别,房间类型编号,床位位置序号,宿舍号,楼层)：
    /// 根据“班级编号”，分类统计并返回该班级所在迎新学年、所在校区对应“性别”、“房间类型编号”、“床位位置序号”、“宿舍号”
    /// 和“楼层”的预分配房间中当前可用房间列表，否则返回null
    /// 编写人：张明
    /// 创建时间：2017.3.23
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="class_no">班级编号</param>
    /// <param name="gender">性别</param>
    /// <param name="Room_NO">房间类型编号</param>
    /// <param name="Bed_Index">床位位置序号</param>
    /// <param name="Dorm_NO">宿舍号</param>
    /// <param name="floor">楼层</param>
    /// <returns>string[]【可用房间列表】</returns>
    public static List<String> listroom(string class_no, string gender, string Room_NO, string Bed_Index, string Dorm_NO,string floor)
    {

        DataTable room = new DataTable();
        List<String> str = new List<String>();
        try
        {
            //根据条件查询可用房间信息
            room = Sqlhelper.Serach("SELECT     distinct Fresh_Room.Room_NO FROM         Fresh_Room_Type INNER JOIN    Fresh_Dorm INNER JOIN        Fresh_Bed_Type INNER JOIN          Fresh_Bed ON Fresh_Bed_Type.PK_Bed_Type = Fresh_Bed.FK_Bed_Type INNER JOIN              Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO ON                       Fresh_Room_Type.PK_Room_Type = Fresh_Room.FK_Room_Type LEFT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO WHERE     (Fresh_Bed_Class_Log.FK_Class_NO =@class_no) AND (Fresh_Room.Gender = @gender) and Fresh_Room_Type.Type_NO=@Room_NO AND (Fresh_Bed_Type.Bed_Index = @Bed_Index) and Fresh_Dorm.Dorm_NO=@Dorm_NO and Fresh_Room.Floor=@floor order by Fresh_Room.Room_NO", new SqlParameter("class_no", class_no), new SqlParameter("gender", gender), new SqlParameter("Room_NO", Room_NO), new SqlParameter("Bed_Index", Bed_Index), new SqlParameter("Dorm_NO", Dorm_NO), new SqlParameter("floor", floor));
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "listfloor", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        if (room.Rows.Count > 0)
        {
            for (int i = 0; i < room.Rows.Count; i++)
            {
                str.Add(room.Rows[i][0].ToString());
                //将楼层数据添加到list<string>中
            }
        }

        return str;
    }
    /// <summary>
    /// 功能描述：34、床位分配 学生分配宿舍(学号，宿舍号，房间编号，床位位置序号，操作人)
    /// 如果该“学号”未分配过宿舍，则根据“学号”所在迎新学年的“宿舍号”、“房间编号”、“床位位置序号”
    /// 和“操作人”新建“床位分配”记录。同时，根据所在迎新学年对应的“房间类型”收费标准，产生 “学生应交费”记录。
    /// 以上操作成功完成后，返回新创建的“床位分配”记录。

    /// </summary>
    /// <param name="xh">学号</param>
    /// <param name="Dorm_NO">宿舍号</param>
    /// <param name="Room_NO">房间编号</param>
    /// <param name="Bed_Index">床位位置序号</param>
    /// <param name="Updater">操作人</param>
    /// <returns>list【宿舍主键、宿舍编号、学年、宿舍名、校区编号】</returns>
    /// ?????该方法还需进一步研究，因收费产生变化，和需对接
    public static List<Fresh_Bed_Class_Log> updateFresh_Bed_Class_Log(string xh, string Dorm_NO, string Room_NO, string Bed_Index, string Updater)
    {
        DataTable update = new DataTable();
        List<Fresh_Bed_Class_Log> result = ModelConvertHelper<Fresh_Bed_Class_Log>.FillModel(update);
        return result;
    }
    /// <summary>
    /// 传入房间类型编码，收费项目代码，学年，房间布局简图，床位布局简图地址，创建或更新房间类型表,房间类型名称
    /// </summary>
    /// <param name="PK_Room_Type">主键</param>
    /// <param name="Type_NO">房间类型编码</param>
    /// <param name="FK_Fee_Item">收费项目代码</param>
    /// <param name="Year">学年</param>
    /// <param name="Room_Layout">房间布局简图</param>
    /// <param name="Bed_Layout">床位布局简图</param>
    /// <param name="Type_Name">房间类型名称</param>
    /// <returns>提示信息:1,成功；0失败，错误信息</returns>
    
    public static string update_Roomtype(string PK_Room_Type, string Type_NO, string FK_Fee_Item, string Year, string Room_Layout, string Bed_Layout,string Type_Name)
    {
        string sql = "";
        string usql = "";
        string csql = "";
        string guid = Guid.NewGuid().ToString();
        string cxnonull="";
        //判断数据
        string err = "";
        try
        {
            if (Type_NO.Length < 1)
            {
                err = "房间类型编码不正确！";
            }
            else
            {
                //查询是否已经有该房间类型
                DataTable cx = Sqlhelper.Serach("SELECT TOP 1 *  FROM [Fresh_Room_Type] where [Type_NO]='" + Type_NO + "'");
                if (cx.Rows.Count > 0)
                {
                    cxnonull="1";
                    usql += "Type_NO='" + Type_NO + "'";
                }
                else
                {
                    usql += "Type_NO='" + Type_NO + "'";
                    csql += ",'" + Type_NO + "'";
                }
            }
            if (FK_Fee_Item.Length < 1)
            {
                err = "收费项目代码不正确！";
            }
            else
            {
                usql += ",FK_Fee_Item='" + FK_Fee_Item + "'";
                csql += ",'" + FK_Fee_Item + "'";
            }
            if (Year.Length < 1)
            {
                err = "年度不能为空！";
            }
            else
            {
                usql += ",Year='" + Year + "'";
                csql += ",'" + Year + "'";
            }
            if (Room_Layout.Length > 0)
            {
                usql += ",Room_Layout='" + Room_Layout + "'";
                csql += ",'" + Room_Layout + "'";
            }
            else
            {
                csql += ",'" + Room_Layout + "'";
            }
            if (Bed_Layout.Length > 0)
            {
                usql += ",Bed_Layout='" + Bed_Layout + "'";
                csql += ",'" + Bed_Layout + "'";
            }
            else
            {
                csql += ",'" + Bed_Layout + "'";
            }

            if (Type_Name.Length < 1)
            {
                err = "房间类型名称不能为空！";
            }
            else
            {
                usql += ",Type_Name='" + Type_Name + "'";
                csql += ",'" + Type_Name + "'";
            }

            if (err.Length > 0) return "0," + err;

            //操作
            if (PK_Room_Type.Length > 0||cxnonull=="1")
            {
                //更新
                if (cxnonull == "1")
                {
                    if (Sqlhelper.ExcuteNonQuery("UPDATE [Fresh_Room_Type] set " + usql + " WHERE Type_NO='" + Type_NO + "'") > 0)
                    {
                        return "1,更新数据成功！";
                    }
                    else
                    {
                        return "0,更新数据失败！";
                    }
                }
                else
                {
                    if (Sqlhelper.ExcuteNonQuery("UPDATE [Fresh_Room_Type] set " + usql + " WHERE PK_Room_Type='" + PK_Room_Type + "'") > 0)
                    {
                        return "1,更新数据成功！";
                    }
                    else
                    {
                        return "0,更新数据失败！";
                    }
                }

            }
            else
            {
                //创建
                if (Sqlhelper.ExcuteNonQuery("INSERT INTO [Fresh_Room_Type]([PK_Room_Type],[Type_NO],[FK_Fee_Item],[Year],[Room_Layout],[Bed_Layout],[Type_Name])VALUES('" + guid + "'" + csql + ")") > 0)
                {
                    return "1,插入数据成功！";
                }
                else
                {
                    return "0,插入数据失败！";
                }
            }
        }
        catch (Exception e1){
            return "0,失败"+e1.Message;
        }
        
        
    }


    /// <summary>
    /// 传入主键，宿舍号，学年，宿舍名称，校区名称，创建或更新宿舍，根据校区名称获取校区编号
    /// </summary>
    /// <param name="PK_Dorm_NO">主键</param>
    /// <param name="Dorm_NO">宿舍号</param>
    /// <param name="Year">学年</param>
    /// <param name="Name">宿舍名称</param>
    /// <param name="Campus_Name">校区名称</param>
    /// <returns>提示信息:1,成功；0失败，错误信息</returns>

    public static string update_Fresh_Dorm(string PK_Dorm_NO, string Dorm_NO, string Year, string Name, string Campus_Name)
    {
        string sql = "";
        string usql = "";
        string csql = "";
        string guid = Guid.NewGuid().ToString();
        string cxnonull = "";
        //判断数据
        string err = "";
        try
        {
            if (Dorm_NO.Length < 1)
            {
                err = "宿舍号不正确！";
            }
            else
            {
                //查询是否已经有该房间类型
                DataTable cx = Sqlhelper.Serach("SELECT TOP 1 *  FROM [Fresh_Dorm] where Dorm_NO='" + Dorm_NO + "'");
                if (cx.Rows.Count > 0)
                {
                    cxnonull = "1";
                    usql += "Dorm_NO='" + Dorm_NO + "'";
                }
                else
                {
                    usql += "Dorm_NO='" + Dorm_NO + "'";
                    csql += ",'" + Dorm_NO + "'";
                }
            }
            
            if (Year.Length < 1)
            {
                err = "年度不能为空！";
            }
            else
            {
                usql += ",Year='" + Year + "'";
                csql += ",'" + Year + "'";
            }
            if (Name.Length > 0)
            {
                usql += ",Name='" + Name + "'";
                csql += ",'" + Name + "'";
            }
            else
            {
                err = "宿舍名称不能为空";
               
            }
            if (Campus_Name.Length > 0)
            {
                //查询校区编号
                string Campus_NO="";
                DataTable cx = Sqlhelper.Serach("SELECT TOP 1 *  FROM [Base_Campus] where Campus_Name='" + Dorm_NO + "'");
                if (cx.Rows.Count > 0)
                {
                    Campus_NO = cx.Rows[0]["Campus_NO"].ToString();
                }


                usql += ",Campus_NO='" + Campus_NO + "'";
                csql += ",'" + Campus_Name + "'";
            }
            else
            {
                csql += ",'" + Campus_Name + "'";
            }

            

            if (err.Length > 0) return "0," + err;

            //操作
            if (PK_Dorm_NO.Length > 0 || cxnonull == "1")
            {
                //更新
                if (cxnonull == "1")
                {
                    if (Sqlhelper.ExcuteNonQuery("UPDATE [Fresh_Room_Type] set " + usql + " WHERE Type_NO='" + Dorm_NO + "'") > 0)
                    {
                        return "1,更新数据成功！";
                    }
                    else
                    {
                        return "0,更新数据失败！";
                    }
                }
                else
                {
                    if (Sqlhelper.ExcuteNonQuery("UPDATE [Fresh_Room_Type] set " + usql + " WHERE PK_Room_Type='" + Dorm_NO + "'") > 0)
                    {
                        return "1,更新数据成功！";
                    }
                    else
                    {
                        return "0,更新数据失败！";
                    }
                }

            }
            else
            {
                //创建
                if (Sqlhelper.ExcuteNonQuery("INSERT INTO [Fresh_Room_Type]([PK_Room_Type],[Type_NO],[FK_Fee_Item],[Year],[Room_Layout],[Bed_Layout],[Type_Name])VALUES('" + guid + "'" + csql + ")") > 0)
                {
                    return "1,插入数据成功！";
                }
                else
                {
                    return "0,插入数据失败！";
                }
            }
        }
        catch (Exception e1)
        {
            return "0,失败" + e1.Message;
        }


    }


    /// <summary>
    /// 传入学生学号，获取班级编号
    /// </summary>
    /// <param name="PK_SNO">学号</param>
   
    /// <returns>班级编号id，班级名称name</returns>

    public static DataTable serch_bjbh(string PK_SNO)
    {
        DataTable bjbh = new DataTable();
        try
        {
           bjbh = Sqlhelper.Serach("SELECT     TOP (10) Fresh_Class.PK_Class_NO AS id, Fresh_Class.Name AS name FROM  Base_STU LEFT OUTER JOIN Fresh_Class ON Base_STU.FK_Class_NO = Fresh_Class.PK_Class_NO where PK_SNO='" + PK_SNO + "'");
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_bjbh", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }
    /// <summary>
    /// 传入学生学号，获取可分配的房间类型数据
    /// </summary>
    /// <param name="PK_SNO">学号</param>

    /// <returns>datatable 类型编号，类型名称</returns>

    public static DataTable serch_room_type(string PK_SNO)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT   distinct    TOP (50) Fresh_Room_Type.Type_NO AS id, Fresh_Room_Type.Type_Name AS name FROM         Fresh_Bed_Class_Log LEFT OUTER JOIN     Fresh_Room LEFT OUTER JOIN                      Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type RIGHT OUTER JOIN                      Fresh_Bed ON Fresh_Room.PK_Room_NO = Fresh_Bed.FK_Room_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO RIGHT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO RIGHT OUTER JOIN                      Base_STU ON Fresh_Class.PK_Class_NO = Base_STU.FK_Class_NO WHERE     Base_STU.PK_SNO ='" + PK_SNO + "'  order by name desc");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_room_type", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }
    //楼栋SELECT  distinct   TOP (10)  Fresh_Dorm.Dorm_NO AS id, Fresh_Dorm.Name AS name FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO LEFT OUTER JOIN                      Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type ON                       Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO RIGHT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO RIGHT OUTER JOIN                      Base_STU ON Fresh_Class.PK_Class_NO = Base_STU.FK_Class_NO WHERE     (Base_STU.PK_SNO = '2') order by name desc

    /// <summary>
    /// 传入学生学号，获取可分配的宿舍数据
    /// </summary>
    /// <param name="PK_SNO">学号</param>

    /// <returns>datatable 宿舍ID，宿舍名称</returns>

    public static DataTable serch_dorm(string PK_SNO)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT  distinct   TOP (50)  Fresh_Dorm.Dorm_NO AS id, Fresh_Dorm.Name AS name FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO LEFT OUTER JOIN                      Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type ON                       Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO RIGHT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO RIGHT OUTER JOIN                      Base_STU ON Fresh_Class.PK_Class_NO = Base_STU.FK_Class_NO WHERE     (Base_STU.PK_SNO = '" + PK_SNO + "') order by name desc");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_dorm", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }

    //SELECT  distinct   TOP (10)  Fresh_Dorm.Dorm_NO AS id, Fresh_Dorm.Name AS name FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO LEFT OUTER JOIN                      Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type ON                       Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO RIGHT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO RIGHT OUTER JOIN                      Base_STU ON Fresh_Class.PK_Class_NO = Base_STU.FK_Class_NO WHERE     (Base_STU.PK_SNO = '2') and Fresh_Room_Type.Type_NO='001' order by name desc
    /// <summary>
    /// 传入学生学号，获取可分配的宿舍数据
    /// </summary>
    /// <param name="PK_SNO">学号</param>
    /// <param name="dormid">楼栋ID</param>
    /// <returns>datatable 楼栋ID，楼栋名称name，楼层列表floor</returns>

    public static DataTable serch_dorm(string PK_SNO,string dormid)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT  distinct   TOP (50)  Fresh_Dorm.Dorm_NO AS id, Fresh_Dorm.Name AS name, Fresh_Room.Floor floor FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO LEFT OUTER JOIN                      Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type ON                       Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO RIGHT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO RIGHT OUTER JOIN                      Base_STU ON Fresh_Class.PK_Class_NO = Base_STU.FK_Class_NO WHERE     (Base_STU.PK_SNO = '" + PK_SNO + "') and Fresh_Dorm.Dorm_NO='"+dormid+"' order by name desc");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_dorm", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }
    /// <summary>
    /// 传入学生学号，获取可分配的房间列表
    /// </summary>
    /// <param name="PK_SNO">学号</param>
    /// <param name="dormid">楼栋ID</param>
    /// <param name="floor">楼层</param>
    /// <returns>datatable 房间ID，房间编号name</returns>

    public static DataTable serch_room(string PK_SNO, string dormid,string floor)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT DISTINCT TOP (100) Fresh_Room.PK_Room_NO id, Fresh_Room.Room_NO name FROM         Fresh_Bed LEFT OUTER JOIN                  Fresh_Room LEFT OUTER JOIN                     Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO LEFT OUTER JOIN                      Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type ON                       Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO RIGHT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO RIGHT OUTER JOIN                      Base_STU ON Fresh_Class.PK_Class_NO = Base_STU.FK_Class_NO WHERE     (Base_STU.PK_SNO = '" + PK_SNO + "') AND (Fresh_Dorm.Dorm_NO = '"+dormid+"') AND (Fresh_Room.Floor = '"+floor+"') ORDER BY name DESC ");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_floor", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }


    /// <summary>
    /// 传入房间主键，获取可分配的床位数据
    /// </summary>
    /// <param name="PK_SNO">房间主键</param>
    /// <returns>datatable 床位ID，床位编号name，床位说明bz</returns>

    public static DataTable serch_bed(string roomid)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT     TOP (50) PK_Bed_NO id, Bed_NO name, Bed_Name bz  FROM         Fresh_Bed WHERE     (FK_Room_NO = '"+roomid+"') order by Bed_NO");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_bed", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }

    /// <summary>
    /// 传入床位主键，获取可分配的床位数据
    /// </summary>
    /// <param name="PK_Bed_NO">床位主键</param>
    /// <returns>datatable 床位ID，床位编号name，床位说明bz</returns>

    public static DataTable serch_bedbz(string bedid)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT     TOP (50) PK_Bed_NO id, Bed_NO name, Bed_Name bz  FROM         Fresh_Bed WHERE     (PK_Bed_NO = '" + bedid + "') order by Bed_NO");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_bedbz", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }

    //SELECT     TOP (10) Fresh_Bed.Bed_Name AS 床位, Fresh_Dorm.Name AS 楼栋名称, Fresh_Room.Room_NO AS 房间名称, Fresh_Bed_Log.FK_SNO FROM         Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO RIGHT OUTER JOIN                     Fresh_Bed ON Fresh_Room.PK_Room_NO = Fresh_Bed.FK_Room_NO RIGHT OUTER JOIN                       Fresh_Bed_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO where Fresh_Bed_Log.FK_SNO='2'
    /// <summary>
    /// 传入学号，获取已分配的床位，房间楼栋数据
    /// </summary>
    /// <param name="PK_Bed_NO">床位主键</param>
    /// <returns>datatable 床位，楼栋名称，房间名称</returns>

    public static DataTable serch_yfpbed(string xh)
    {
        DataTable bjbh = new DataTable();
        try
        {
            bjbh = Sqlhelper.Serach("SELECT     TOP (10) Fresh_Bed.Bed_Name AS 床位, Fresh_Dorm.Name AS 楼栋名称, Fresh_Room.Room_NO AS 房间名称,Fresh_Bed.Bed_NO AS 床位, Fresh_Bed_Log.FK_SNO FROM         Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO RIGHT OUTER JOIN                     Fresh_Bed ON Fresh_Room.PK_Room_NO = Fresh_Bed.FK_Room_NO RIGHT OUTER JOIN                       Fresh_Bed_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO where Fresh_Bed_Log.FK_SNO='" + xh + "'");

        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_bedbz", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjbh;

    }

    /// <summary>
    /// 传入学号，获取已分配的床位，房间楼栋数据
    /// </summary>
    /// <param name="PK_Bed_NO">床位主键</param>
    /// <returns>datatable 床位，楼栋名称，房间名称</returns>

    public static string update_yfpbed(string xh,string bedid,string czy)
    {
        try
        {
        //guid 生成
        string guid = Guid.NewGuid().ToString();
        //检查是否已经选择寝室
        DataTable bjbh = new DataTable();
        
        bjbh = Sqlhelper.Serach("SELECT     TOP (10) Fresh_Bed.Bed_Name AS 床位, Fresh_Dorm.Name AS 楼栋名称, Fresh_Room.Room_NO AS 房间名称,Fresh_Bed.Bed_NO AS 床位, Fresh_Bed_Log.FK_SNO FROM         Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO RIGHT OUTER JOIN                     Fresh_Bed ON Fresh_Room.PK_Room_NO = Fresh_Bed.FK_Room_NO RIGHT OUTER JOIN                       Fresh_Bed_Log ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO where Fresh_Bed_Log.FK_SNO='" + xh + "'");

        if (bjbh.Rows.Count > 0) return "0,选寝失败，该生已经选择过寝室了，【"+bjbh.Rows[0][1].ToString()+bjbh.Rows[0][2].ToString()+"，"+bjbh.Rows[0][3].ToString()+"床位】";
        //更新寝室数据

        if (Sqlhelper.ExcuteNonQuery("INSERT INTO [Fresh_Bed_Log]([PK_Bed_Log],[FK_Bed_NO],[FK_SNO],[Updater],[Update_DT])VALUES('" + guid + "','" + bedid + "','" + xh + "','" + czy + "','" + DateTime.Now.ToString() + "')") > 0)
        {
            return "1,寝室已选择";
        }
        else
        {
            return "0,选择寝室失败，请重试！";
        }


       }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_bedbz", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return "0,选择寝室失败，请重试！";

    }

    //获取房间详细信息
    //SELECT     TOP (500) Fresh_Room.PK_Room_NO AS id, Base_Campus.Campus_Name AS 校区, Fresh_Dorm.Name AS 公寓楼名称, Fresh_Room.Floor AS 楼层, Fresh_Room.Room_NO AS 房间编号, Fresh_Room_Type.Type_Name AS 房间类型, Fresh_Room.Gender AS 性别 FROM         Base_Campus RIGHT OUTER JOIN      Fresh_Dorm ON Base_Campus.Campus_NO = Fresh_Dorm.Campus_NO RIGHT OUTER JOIN      Fresh_Room ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO LEFT OUTER JOIN    Fresh_Room_Type ON Fresh_Room.FK_Room_Type = Fresh_Room_Type.PK_Room_Type
    /// <summary>
    /// 传入学号，获取已分配的床位，房间楼栋数据
    /// </summary>
    /// <param name="xq">校区编号</param>
/// <param name="dorm">公寓ID</param>
   /// <param name="floor">楼层</param>
   /// <param name="bjbh">班级编号</param>
    /// <returns>datatable 房间ID，校区，楼栋名称，楼层，房间编号，房间类型，性别，班级名称，班主任</returns>



    public static DataTable serch_yfpgl(string xq,string dorm,string floor ,string bjbh)
    {
        DataTable bjcx = new DataTable();
        try
        {
            string sql = "SELECT     TOP (500) row_number() over (order by  Fresh_Room.PK_Room_NO desc)  AS 序号, Fresh_Room.PK_Room_NO AS id, Base_Campus.Campus_Name AS 校区, Fresh_Dorm.Name AS 公寓楼名称, Fresh_Room.Floor AS 楼层,                 Fresh_Room.Room_NO AS 房间编号, Fresh_Room_Type.Type_Name AS 房间类型, Fresh_Room.Gender AS 性别, Fresh_Bed.PK_Bed_NO as 床位主键 ,Fresh_Bed.Bed_NO AS 床位编号,                 Fresh_Class.Name AS 班级名称 FROM         Fresh_Dorm FULL OUTER JOIN                      Base_Campus ON Fresh_Dorm.Campus_NO = Base_Campus.Campus_NO FULL OUTER JOIN                      Fresh_Room_Type RIGHT OUTER JOIN                  Fresh_Class INNER JOIN                   Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO RIGHT OUTER JOIN                      Fresh_Bed ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO RIGHT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ON Fresh_Room_Type.PK_Room_Type = Fresh_Room.FK_Room_Type ON     Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO where 1=1";
            if (xq.Trim().Length > 0)
            {
                sql += " and  Base_Campus.Campus_NO='" + xq + "'";

            }
            if (dorm.Trim().Length > 0)
            {
                sql += " and  Fresh_Dorm.PK_Dorm_NO='" + dorm + "' ";

            }
            if (floor.Trim().Length > 0)
            {
                sql += " and Fresh_Room.Floor='" + floor + "' ";

            }
            if (bjbh.Trim().Length > 0)
            {
                sql += " and Fresh_Class.PK_Class_NO='" + bjbh + "' ";

            }
            bjcx = Sqlhelper.Serach(sql + " order by id");

            new c_log().logAdd("dormitory.cs", "serch_yfpgl", sql, "2", "zhangming1");//测试
        }
        catch (Exception err)
        {
            try
            {
                if (logzt) new c_log().logAdd("dormitory.cs", "serch_yfpgl", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }

        return bjcx;

    }


}