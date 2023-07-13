using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("index", "crackedCount", "isUnlocked")]
	public class ES3UserType_BoulderI : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BoulderI() : base(typeof(BoulderI)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (BoulderI)obj;
			
			writer.WriteProperty("index", instance.index, ES3Type_int.Instance);
			writer.WriteProperty("crackedCount", instance.crackedCount, ES3Type_int.Instance);
			writer.WriteProperty("isUnlocked", instance.isUnlocked, ES3Type_bool.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (BoulderI)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "index":
						instance.index = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "crackedCount":
						instance.crackedCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "isUnlocked":
						instance.isUnlocked = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new BoulderI();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_BoulderIArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BoulderIArray() : base(typeof(BoulderI[]), ES3UserType_BoulderI.Instance)
		{
			Instance = this;
		}
	}
}