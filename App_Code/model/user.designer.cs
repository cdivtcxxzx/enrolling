﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace model
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="zypt_data")]
	public partial class userDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void Insertyonghqx(yonghqx instance);
    partial void Updateyonghqx(yonghqx instance);
    partial void Deleteyonghqx(yonghqx instance);
    #endregion
		
		public userDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["zypt_dataConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public userDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public userDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public userDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public userDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<yonghqx> yonghqx
		{
			get
			{
				return this.GetTable<yonghqx>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.yonghqx")]
	public partial class yonghqx : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _guid;
		
		private string _yhqx;
		
		private string _lsz;
		
		private string _yhid;
		
		private string _xm;
		
		private string _lxdh;
		
		private string _email;
		
		private string _yxdm;
		
		private string _pjjxbmdm;
		
		private string _pjbmdm;
		
		private string _mm;
		
		private string _uumzw;
		
		private string _banji;
		
		private string _kecheng;
		
		private string _yhdh;
		
		private System.Nullable<System.DateTime> _dltime;
		
		private System.Nullable<int> _fwcs;
		
		private string _yhzp;
		
		private string _file_json;
		
		private string _privacy;
		
		private string _qq;
		
		private string _gender;
		
		private string _summary;
		
		private string _user_status;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnguidChanging(string value);
    partial void OnguidChanged();
    partial void OnyhqxChanging(string value);
    partial void OnyhqxChanged();
    partial void OnlszChanging(string value);
    partial void OnlszChanged();
    partial void OnyhidChanging(string value);
    partial void OnyhidChanged();
    partial void OnxmChanging(string value);
    partial void OnxmChanged();
    partial void OnlxdhChanging(string value);
    partial void OnlxdhChanged();
    partial void OnemailChanging(string value);
    partial void OnemailChanged();
    partial void OnyxdmChanging(string value);
    partial void OnyxdmChanged();
    partial void OnpjjxbmdmChanging(string value);
    partial void OnpjjxbmdmChanged();
    partial void OnpjbmdmChanging(string value);
    partial void OnpjbmdmChanged();
    partial void OnmmChanging(string value);
    partial void OnmmChanged();
    partial void OnuumzwChanging(string value);
    partial void OnuumzwChanged();
    partial void OnbanjiChanging(string value);
    partial void OnbanjiChanged();
    partial void OnkechengChanging(string value);
    partial void OnkechengChanged();
    partial void OnyhdhChanging(string value);
    partial void OnyhdhChanged();
    partial void OndltimeChanging(System.Nullable<System.DateTime> value);
    partial void OndltimeChanged();
    partial void OnfwcsChanging(System.Nullable<int> value);
    partial void OnfwcsChanged();
    partial void OnyhzpChanging(string value);
    partial void OnyhzpChanged();
    partial void Onfile_jsonChanging(string value);
    partial void Onfile_jsonChanged();
    partial void OnprivacyChanging(string value);
    partial void OnprivacyChanged();
    partial void OnqqChanging(string value);
    partial void OnqqChanged();
    partial void OngenderChanging(string value);
    partial void OngenderChanged();
    partial void OnsummaryChanging(string value);
    partial void OnsummaryChanged();
    partial void Onuser_statusChanging(string value);
    partial void Onuser_statusChanged();
    #endregion
		
		public yonghqx()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guid", DbType="NVarChar(50)")]
		public string guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((this._guid != value))
				{
					this.OnguidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("guid");
					this.OnguidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_yhqx", DbType="NVarChar(MAX)")]
		public string yhqx
		{
			get
			{
				return this._yhqx;
			}
			set
			{
				if ((this._yhqx != value))
				{
					this.OnyhqxChanging(value);
					this.SendPropertyChanging();
					this._yhqx = value;
					this.SendPropertyChanged("yhqx");
					this.OnyhqxChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lsz", DbType="NVarChar(MAX)")]
		public string lsz
		{
			get
			{
				return this._lsz;
			}
			set
			{
				if ((this._lsz != value))
				{
					this.OnlszChanging(value);
					this.SendPropertyChanging();
					this._lsz = value;
					this.SendPropertyChanged("lsz");
					this.OnlszChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_yhid", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string yhid
		{
			get
			{
				return this._yhid;
			}
			set
			{
				if ((this._yhid != value))
				{
					this.OnyhidChanging(value);
					this.SendPropertyChanging();
					this._yhid = value;
					this.SendPropertyChanged("yhid");
					this.OnyhidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_xm", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string xm
		{
			get
			{
				return this._xm;
			}
			set
			{
				if ((this._xm != value))
				{
					this.OnxmChanging(value);
					this.SendPropertyChanging();
					this._xm = value;
					this.SendPropertyChanged("xm");
					this.OnxmChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lxdh", DbType="NVarChar(50)")]
		public string lxdh
		{
			get
			{
				return this._lxdh;
			}
			set
			{
				if ((this._lxdh != value))
				{
					this.OnlxdhChanging(value);
					this.SendPropertyChanging();
					this._lxdh = value;
					this.SendPropertyChanged("lxdh");
					this.OnlxdhChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email", DbType="NVarChar(80)")]
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this.OnemailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("email");
					this.OnemailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_yxdm", DbType="NVarChar(50)")]
		public string yxdm
		{
			get
			{
				return this._yxdm;
			}
			set
			{
				if ((this._yxdm != value))
				{
					this.OnyxdmChanging(value);
					this.SendPropertyChanging();
					this._yxdm = value;
					this.SendPropertyChanged("yxdm");
					this.OnyxdmChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pjjxbmdm", DbType="NVarChar(MAX)")]
		public string pjjxbmdm
		{
			get
			{
				return this._pjjxbmdm;
			}
			set
			{
				if ((this._pjjxbmdm != value))
				{
					this.OnpjjxbmdmChanging(value);
					this.SendPropertyChanging();
					this._pjjxbmdm = value;
					this.SendPropertyChanged("pjjxbmdm");
					this.OnpjjxbmdmChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pjbmdm", DbType="NVarChar(MAX)")]
		public string pjbmdm
		{
			get
			{
				return this._pjbmdm;
			}
			set
			{
				if ((this._pjbmdm != value))
				{
					this.OnpjbmdmChanging(value);
					this.SendPropertyChanging();
					this._pjbmdm = value;
					this.SendPropertyChanged("pjbmdm");
					this.OnpjbmdmChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mm", DbType="NVarChar(MAX)")]
		public string mm
		{
			get
			{
				return this._mm;
			}
			set
			{
				if ((this._mm != value))
				{
					this.OnmmChanging(value);
					this.SendPropertyChanging();
					this._mm = value;
					this.SendPropertyChanged("mm");
					this.OnmmChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_uumzw", DbType="NVarChar(MAX)")]
		public string uumzw
		{
			get
			{
				return this._uumzw;
			}
			set
			{
				if ((this._uumzw != value))
				{
					this.OnuumzwChanging(value);
					this.SendPropertyChanging();
					this._uumzw = value;
					this.SendPropertyChanged("uumzw");
					this.OnuumzwChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_banji", DbType="NVarChar(50)")]
		public string banji
		{
			get
			{
				return this._banji;
			}
			set
			{
				if ((this._banji != value))
				{
					this.OnbanjiChanging(value);
					this.SendPropertyChanging();
					this._banji = value;
					this.SendPropertyChanged("banji");
					this.OnbanjiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_kecheng", DbType="NVarChar(50)")]
		public string kecheng
		{
			get
			{
				return this._kecheng;
			}
			set
			{
				if ((this._kecheng != value))
				{
					this.OnkechengChanging(value);
					this.SendPropertyChanging();
					this._kecheng = value;
					this.SendPropertyChanged("kecheng");
					this.OnkechengChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_yhdh", DbType="NVarChar(50)")]
		public string yhdh
		{
			get
			{
				return this._yhdh;
			}
			set
			{
				if ((this._yhdh != value))
				{
					this.OnyhdhChanging(value);
					this.SendPropertyChanging();
					this._yhdh = value;
					this.SendPropertyChanged("yhdh");
					this.OnyhdhChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dltime", DbType="DateTime")]
		public System.Nullable<System.DateTime> dltime
		{
			get
			{
				return this._dltime;
			}
			set
			{
				if ((this._dltime != value))
				{
					this.OndltimeChanging(value);
					this.SendPropertyChanging();
					this._dltime = value;
					this.SendPropertyChanged("dltime");
					this.OndltimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fwcs", DbType="Int")]
		public System.Nullable<int> fwcs
		{
			get
			{
				return this._fwcs;
			}
			set
			{
				if ((this._fwcs != value))
				{
					this.OnfwcsChanging(value);
					this.SendPropertyChanging();
					this._fwcs = value;
					this.SendPropertyChanged("fwcs");
					this.OnfwcsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_yhzp", DbType="NVarChar(MAX)")]
		public string yhzp
		{
			get
			{
				return this._yhzp;
			}
			set
			{
				if ((this._yhzp != value))
				{
					this.OnyhzpChanging(value);
					this.SendPropertyChanging();
					this._yhzp = value;
					this.SendPropertyChanged("yhzp");
					this.OnyhzpChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_file_json", DbType="NVarChar(MAX)")]
		public string file_json
		{
			get
			{
				return this._file_json;
			}
			set
			{
				if ((this._file_json != value))
				{
					this.Onfile_jsonChanging(value);
					this.SendPropertyChanging();
					this._file_json = value;
					this.SendPropertyChanged("file_json");
					this.Onfile_jsonChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_privacy", DbType="NVarChar(50)")]
		public string privacy
		{
			get
			{
				return this._privacy;
			}
			set
			{
				if ((this._privacy != value))
				{
					this.OnprivacyChanging(value);
					this.SendPropertyChanging();
					this._privacy = value;
					this.SendPropertyChanged("privacy");
					this.OnprivacyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_qq", DbType="NVarChar(50)")]
		public string qq
		{
			get
			{
				return this._qq;
			}
			set
			{
				if ((this._qq != value))
				{
					this.OnqqChanging(value);
					this.SendPropertyChanging();
					this._qq = value;
					this.SendPropertyChanged("qq");
					this.OnqqChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gender", DbType="NVarChar(10)")]
		public string gender
		{
			get
			{
				return this._gender;
			}
			set
			{
				if ((this._gender != value))
				{
					this.OngenderChanging(value);
					this.SendPropertyChanging();
					this._gender = value;
					this.SendPropertyChanged("gender");
					this.OngenderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_summary", DbType="NVarChar(255)")]
		public string summary
		{
			get
			{
				return this._summary;
			}
			set
			{
				if ((this._summary != value))
				{
					this.OnsummaryChanging(value);
					this.SendPropertyChanging();
					this._summary = value;
					this.SendPropertyChanged("summary");
					this.OnsummaryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_user_status", DbType="NVarChar(50)")]
		public string user_status
		{
			get
			{
				return this._user_status;
			}
			set
			{
				if ((this._user_status != value))
				{
					this.Onuser_statusChanging(value);
					this.SendPropertyChanging();
					this._user_status = value;
					this.SendPropertyChanged("user_status");
					this.Onuser_statusChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
