using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkView
{
	class ConfigHelper
	{
		readonly static System.Configuration.Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

		public static string AdapterName 
		{ 
			get
			{
                return Config.AppSettings.Settings["nameAdapter"].Value;
			}
			set
			{
				SaveValue("nameAdapter", value);
			}
		}

        public static ModelColor BackgroundColor
		{
            get { return CreateColorModel("backgroundColor"); }
            set { SaveValue("backgroundColor", value.ToString()); }
        }
		public static ModelColor DownloadColor
		{
			get { return CreateColorModel("downloadColor"); }
			set { SaveValue("downloadColor", value.ToString()); }
		}
		public static ModelColor UploadColor
		{
			get { return CreateColorModel("uploadColor"); }
			set { SaveValue("uploadColor", value.ToString()); }
		}

		public static int PositionTop
		{
			get
			{
				return int.Parse(Config.AppSettings.Settings["positionTop"].Value);
			}
			set
			{
				SaveValue("positionTop", value.ToString());
			}
		}
		public static int PositionLeft
		{
			get
			{
				return int.Parse(Config.AppSettings.Settings["positionLeft"].Value);
			}
			set
			{
				SaveValue("positionLeft", value.ToString());
			}
		}
		public static int RefreshInterval
		{
			get
			{
				return int.Parse(Config.AppSettings.Settings["refreshInterval"].Value);
			}
			set
			{
				SaveValue("refreshInterval", value.ToString());
			}
		}

		static void SaveValue(string cfgKey, string v)
		{
			Config.AppSettings.Settings.Remove(cfgKey);
			Config.AppSettings.Settings.Add(cfgKey, v);
			Config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		static ModelColor CreateColorModel(string cfgKey)
		{
			var cfgStringSplitted = Config.AppSettings.Settings[cfgKey].Value.Split(',');
			return new ModelColor
			{
				Red = byte.Parse(cfgStringSplitted[0]),
				Green = byte.Parse(cfgStringSplitted[1]),
				Blue = byte.Parse(cfgStringSplitted[2]),
				Alpha = byte.Parse(cfgStringSplitted[3]),
			};
		}
	}
}
