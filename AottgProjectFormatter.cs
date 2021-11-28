#if UNITY_EDITOR

using System;
using System.Text;
using UnityEngine;
using UnityEditor;

public class AottgProjectFormatter : EditorWindow
{
	private const int FIRST_CUSTOM_LAYER = 8;

	public static void setLayers()
    {
		var asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0];
		if (asset != null)
		{ // sanity checking
			var so = new SerializedObject(asset);

			var aottg_layers = new string[]
			{
				"Players", //8
				"Ground","EnemyBox","EnemyAABB","FX","NetworkObject",
				"InnerContact","noPhysics","PlayerAttackBox",
				"PlayerHitBox","EnemyAttackBox","EnemyHitBox"
			};

			var iteration_steps = aottg_layers.Length;

			for (int i = 0; i < iteration_steps; i++)
			{
				string nm = "User Layer " + (FIRST_CUSTOM_LAYER + i);
				SerializedProperty sp = so.FindProperty(nm);

				if (sp == null)
					Debug.LogWarning("["+ nm + "] Null Slot!!");
				else if(!aottg_layers[i].Equals(sp.stringValue))
                {
					sp.stringValue = aottg_layers[i];
				}
			}

			so.ApplyModifiedProperties();
			so.Update();
		}

	}

	public static void setTags()
	{
		var asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0];
		if (asset != null)
		{ // sanity checking
			var so = new SerializedObject(asset);
			var tags = so.FindProperty("tags");

			string[] aottg_tags = { "Untagged",
				"titanneck","playerHitbox","playerRespawn",
				"titanRespawn","collectable","playerRespawn2",
				"titanRespawn2","erenHitbox","titaneye",
				"titanankle","route"
			};

			tags.ClearArray();
			
			var iteration_steps = aottg_tags.Length;
			tags.arraySize = iteration_steps + 1;

			for (int i = 0; i < iteration_steps; i++)
			{
				var sp = tags.GetArrayElementAtIndex(i);

                if (sp==null)
					tags.InsertArrayElementAtIndex(i);
				else if (sp.stringValue.Equals(aottg_tags[i]))
					continue;

				tags.GetArrayElementAtIndex(i).stringValue = aottg_tags[i];
			}

			so.ApplyModifiedProperties(); 
			so.Update();
		}
	}

	[MenuItem("Window/Tags Creator")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(AottgProjectFormatter));
	}

	void OnGUI()
	{
		if (GUILayout.Button("Set Aottg Tags"))
		{
			setTags();
		}
		if(GUILayout.Button("Set Aottg Layers"))
        {
			setLayers();
        }
	}
}

#endif