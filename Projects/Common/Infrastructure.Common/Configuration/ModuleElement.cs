﻿using System.Configuration;

namespace Infrastructure.Common.Configuration
{
	public class ModuleElement : ConfigurationElement
	{
		[ConfigurationProperty("assemblyFile")]
		public string AssemblyFile
		{
			get { return (string)base["assemblyFile"]; }
			set { base["assemblyFile"] = value; }
		}
	}
}
