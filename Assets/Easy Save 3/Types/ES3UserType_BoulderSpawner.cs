using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("spawnableBoulders")]
	public class ES3UserType_BoulderSpawner : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BoulderSpawner() : base(typeof(BoulderSpawner)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BoulderSpawner)obj;
			
			writer.WritePrivateField("spawnableBoulders", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BoulderSpawner)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "spawnableBoulders":
					instance = (BoulderSpawner)reader.SetPrivateField("spawnableBoulders", reader.Read<System.Collections.Generic.List<Boulder>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BoulderSpawnerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BoulderSpawnerArray() : base(typeof(BoulderSpawner[]), ES3UserType_BoulderSpawner.Instance)
		{
			Instance = this;
		}
	}
}