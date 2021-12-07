#if UNITY_EDITOR

using UnityEngine;
using System;
using UnityEditor;
using Rnd = UnityEngine.Random;

namespace Utilities
{
    abstract class UtilityBase
	{
		protected class v3Application
		{
			string ID;
			bool additive;
			Application type;
			Vector3[] between = new Vector3[2];

			public v3Application(string id)
			{
				this.ID = id;
			}

			public Vector3 Get(Vector3 base_value)
			{
				var value = Vector3.zero;
				switch (type)
				{
					default:
						return base_value;
					case Application.Setted:
						value = this.between[0];
						break;
					case Application.RandomBetween:
						value =  new Vector3(
							Rnd.Range(this.between[0].x, this.between[1].x),
							Rnd.Range(this.between[0].y, this.between[1].y),
							Rnd.Range(this.between[0].z, this.between[1].z)
							);
						break;
				}
				if (this.additive)
					return value + base_value;
				return value;

			}

			public Vector3 Get(int ID)
            {
				return this.between[ID];
            }

			public void Set(Vector3 element, int ID)
            {
				this.between[ID] = element;
            }

			public void Dialog_GUI()
			{
				this.type = (Application)enumPopop<Application>((int)this.type, this.ID);
				switch (this.type)
				{
					case Application.None:
						return;
					case Application.RandomBetween:
						this.between[0] = EditorGUILayout.Vector3Field("From:", this.between[0]);
						this.between[1] = EditorGUILayout.Vector3Field("To:", this.between[1]);
						break;
					case Application.Setted:
						this.between[0] = EditorGUILayout.Vector3Field("Fixed:", this.between[0]);
						break;
				}
				this.additive = EditorGUILayout.Toggle("Additive", this.additive);
			}
		}

		protected bool enabled = false;

		protected virtual void DialogGUI()
		{
			enabled = GUILayout.Toggle(this.enabled, this.GetType().Name);
		}
		protected abstract void UtilGUI();
		public void OnGUI()
		{
			DialogGUI();
			if (this.enabled)
				this.UtilGUI();
			EditorGUILayout.Separator();
		}

		protected static int enumPopop<T>(int element, string label)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(label);
			GUILayout.FlexibleSpace();
			var out_data = EditorGUILayout.Popup((int)element, Enum.GetNames(typeof(T)));
			GUILayout.EndHorizontal();
			return out_data;
		}

		protected static void String(ref string data, string label, int width = 100)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(label);
			GUILayout.FlexibleSpace();
			data = GUILayout.TextField(data, GUILayout.Width(width));
			GUILayout.EndHorizontal();
		}
	}
}
#endif