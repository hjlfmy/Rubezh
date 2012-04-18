﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.530
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FiresecService.SKUD.DataAccess
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Firesec")]
	public partial class FiresecDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertEmployee(Employee instance);
    partial void UpdateEmployee(Employee instance);
    partial void DeleteEmployee(Employee instance);
    partial void InsertPerson(Person instance);
    partial void UpdatePerson(Person instance);
    partial void DeletePerson(Person instance);
    #endregion
		
		public FiresecDataContext() : 
				base(global::FiresecService.SKUD.Properties.Settings.Default.FiresecConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FiresecDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FiresecDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FiresecDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FiresecDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Employee> Employees
		{
			get
			{
				return this.GetTable<Employee>();
			}
		}
		
		public System.Data.Linq.Table<Person> Persons
		{
			get
			{
				return this.GetTable<Person>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetAllEmployees")]
		public ISingleResult<GetAllEmployeesResult> GetAllEmployees()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetAllEmployeesResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.DeleteEmployee")]
		public int DeleteEmployee([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Count", DbType="Int")] ref System.Nullable<int> count)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, count);
			count = ((System.Nullable<int>)(result.GetParameterValue(1)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetEmployeeCard")]
		public ISingleResult<GetEmployeeCardResult> GetEmployeeCard([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] System.Nullable<int> id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((ISingleResult<GetEmployeeCardResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SaveEmployeeCard")]
		public int SaveEmployeeCard([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="Int")] ref System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="NVarChar(MAX)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="NVarChar(MAX)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SecondName", DbType="NVarChar(MAX)")] string secondName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Birthday", DbType="DateTime")] System.Nullable<System.DateTime> birthday, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Sex", DbType="DateTime")] System.Nullable<System.DateTime> sex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Department", DbType="NVarChar(MAX)")] string department, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Position", DbType="NVarChar(MAX)")] string position, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Comment", DbType="NVarChar(MAX)")] string comment)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, lastName, firstName, secondName, birthday, sex, department, position, comment);
			id = ((System.Nullable<int>)(result.GetParameterValue(0)));
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Employee")]
	public partial class Employee : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _PersonId;
		
		private string _Staff;
		
		private string _Comment;
		
		private EntityRef<Person> _Person;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPersonIdChanging(int value);
    partial void OnPersonIdChanged();
    partial void OnStaffChanging(string value);
    partial void OnStaffChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    #endregion
		
		public Employee()
		{
			this._Person = default(EntityRef<Person>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonId", DbType="Int NOT NULL")]
		public int PersonId
		{
			get
			{
				return this._PersonId;
			}
			set
			{
				if ((this._PersonId != value))
				{
					if (this._Person.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPersonIdChanging(value);
					this.SendPropertyChanging();
					this._PersonId = value;
					this.SendPropertyChanged("PersonId");
					this.OnPersonIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Staff", DbType="NVarChar(MAX)")]
		public string Staff
		{
			get
			{
				return this._Staff;
			}
			set
			{
				if ((this._Staff != value))
				{
					this.OnStaffChanging(value);
					this.SendPropertyChanging();
					this._Staff = value;
					this.SendPropertyChanged("Staff");
					this.OnStaffChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="NVarChar(MAX)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Person_Employee", Storage="_Person", ThisKey="PersonId", OtherKey="Id", IsForeignKey=true)]
		public Person Person
		{
			get
			{
				return this._Person.Entity;
			}
			set
			{
				Person previousValue = this._Person.Entity;
				if (((previousValue != value) 
							|| (this._Person.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Person.Entity = null;
						previousValue.Employees.Remove(this);
					}
					this._Person.Entity = value;
					if ((value != null))
					{
						value.Employees.Add(this);
						this._PersonId = value.Id;
					}
					else
					{
						this._PersonId = default(int);
					}
					this.SendPropertyChanged("Person");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Person")]
	public partial class Person : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _LastNmae;
		
		private string _FirstName;
		
		private string _SecondName;
		
		private System.Nullable<System.DateTime> _Birthday;
		
		private System.Nullable<System.DateTime> _Sex;
		
		private string _Comment;
		
		private EntitySet<Employee> _Employees;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnLastNmaeChanging(string value);
    partial void OnLastNmaeChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnSecondNameChanging(string value);
    partial void OnSecondNameChanged();
    partial void OnBirthdayChanging(System.Nullable<System.DateTime> value);
    partial void OnBirthdayChanged();
    partial void OnSexChanging(System.Nullable<System.DateTime> value);
    partial void OnSexChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    #endregion
		
		public Person()
		{
			this._Employees = new EntitySet<Employee>(new Action<Employee>(this.attach_Employees), new Action<Employee>(this.detach_Employees));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastNmae", DbType="NVarChar(MAX)")]
		public string LastNmae
		{
			get
			{
				return this._LastNmae;
			}
			set
			{
				if ((this._LastNmae != value))
				{
					this.OnLastNmaeChanging(value);
					this.SendPropertyChanging();
					this._LastNmae = value;
					this.SendPropertyChanged("LastNmae");
					this.OnLastNmaeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(MAX)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecondName", DbType="NVarChar(MAX)")]
		public string SecondName
		{
			get
			{
				return this._SecondName;
			}
			set
			{
				if ((this._SecondName != value))
				{
					this.OnSecondNameChanging(value);
					this.SendPropertyChanging();
					this._SecondName = value;
					this.SendPropertyChanged("SecondName");
					this.OnSecondNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Birthday", DbType="DateTime")]
		public System.Nullable<System.DateTime> Birthday
		{
			get
			{
				return this._Birthday;
			}
			set
			{
				if ((this._Birthday != value))
				{
					this.OnBirthdayChanging(value);
					this.SendPropertyChanging();
					this._Birthday = value;
					this.SendPropertyChanged("Birthday");
					this.OnBirthdayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sex", DbType="DateTime")]
		public System.Nullable<System.DateTime> Sex
		{
			get
			{
				return this._Sex;
			}
			set
			{
				if ((this._Sex != value))
				{
					this.OnSexChanging(value);
					this.SendPropertyChanging();
					this._Sex = value;
					this.SendPropertyChanged("Sex");
					this.OnSexChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="NVarChar(MAX)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Person_Employee", Storage="_Employees", ThisKey="Id", OtherKey="PersonId")]
		public EntitySet<Employee> Employees
		{
			get
			{
				return this._Employees;
			}
			set
			{
				this._Employees.Assign(value);
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
		
		private void attach_Employees(Employee entity)
		{
			this.SendPropertyChanging();
			entity.Person = this;
		}
		
		private void detach_Employees(Employee entity)
		{
			this.SendPropertyChanging();
			entity.Person = null;
		}
	}
	
	public partial class GetAllEmployeesResult
	{
		
		private int _Id;
		
		private int _PersonId;
		
		private string _LastName;
		
		private string _FirstName;
		
		private string _SecondName;
		
		private System.Nullable<int> _Age;
		
		private string _Department;
		
		private string _Position;
		
		private string _Comment;
		
		public GetAllEmployeesResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL")]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonId", DbType="Int NOT NULL")]
		public int PersonId
		{
			get
			{
				return this._PersonId;
			}
			set
			{
				if ((this._PersonId != value))
				{
					this._PersonId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(MAX)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(MAX)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecondName", DbType="NVarChar(MAX)")]
		public string SecondName
		{
			get
			{
				return this._SecondName;
			}
			set
			{
				if ((this._SecondName != value))
				{
					this._SecondName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Age", DbType="Int")]
		public System.Nullable<int> Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this._Age = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Department", DbType="NVarChar(MAX)")]
		public string Department
		{
			get
			{
				return this._Department;
			}
			set
			{
				if ((this._Department != value))
				{
					this._Department = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Position", DbType="NVarChar(MAX)")]
		public string Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				if ((this._Position != value))
				{
					this._Position = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="NVarChar(MAX)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this._Comment = value;
				}
			}
		}
	}
	
	public partial class GetEmployeeCardResult
	{
		
		private int _Id;
		
		private int _PersonId;
		
		private string _LastName;
		
		private string _FirstName;
		
		private string _SecondName;
		
		private System.Nullable<int> _Age;
		
		private string _Department;
		
		private string _Position;
		
		private string _Comment;
		
		public GetEmployeeCardResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL")]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonId", DbType="Int NOT NULL")]
		public int PersonId
		{
			get
			{
				return this._PersonId;
			}
			set
			{
				if ((this._PersonId != value))
				{
					this._PersonId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(MAX)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(MAX)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecondName", DbType="NVarChar(MAX)")]
		public string SecondName
		{
			get
			{
				return this._SecondName;
			}
			set
			{
				if ((this._SecondName != value))
				{
					this._SecondName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Age", DbType="Int")]
		public System.Nullable<int> Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this._Age = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Department", DbType="NVarChar(MAX)")]
		public string Department
		{
			get
			{
				return this._Department;
			}
			set
			{
				if ((this._Department != value))
				{
					this._Department = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Position", DbType="NVarChar(MAX)")]
		public string Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				if ((this._Position != value))
				{
					this._Position = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="NVarChar(MAX)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this._Comment = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
