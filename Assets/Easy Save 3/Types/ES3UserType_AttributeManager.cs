using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("fasterShootingUpgradeLevel", "widerShootingUpgradeLevel", "firePowerUpgradeLevel")]
	public class ES3UserType_AttributeManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AttributeManager() : base(typeof(AttributeManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (AttributeManager)obj;
			
			writer.WritePrivateField("fasterShootingUpgradeLevel", instance);
			writer.WritePrivateField("widerShootingUpgradeLevel", instance);
			writer.WritePrivateField("firePowerUpgradeLevel", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (AttributeManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "fasterShootingUpgradeLevel":
					instance = (AttributeManager)reader.SetPrivateField("fasterShootingUpgradeLevel", reader.Read<System.Int32>(), instance);
					break;
					case "widerShootingUpgradeLevel":
					instance = (AttributeManager)reader.SetPrivateField("widerShootingUpgradeLevel", reader.Read<System.Int32>(), instance);
					break;
					case "firePowerUpgradeLevel":
					instance = (AttributeManager)reader.SetPrivateField("firePowerUpgradeLevel", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AttributeManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AttributeManagerArray() : base(typeof(AttributeManager[]), ES3UserType_AttributeManager.Instance)
		{
			Instance = this;
		}
	}
}