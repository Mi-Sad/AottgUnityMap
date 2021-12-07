#if UNITY_EDITOR

using System;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Utilities;

public class Utility : EditorWindow
{
    [MenuItem("Window/AoTTg Utility")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(Utility));
	}

	List<UtilityBase> tools = new List<UtilityBase>()
	{
		new Grounderer(),
		new Randomizer(),
		new ArrayCloner()
	};

	Vector2 scroll_pos = Vector2.zero;

	void OnGUI()
	{
		scroll_pos = EditorGUILayout.BeginScrollView(scroll_pos,GUIStyle.none,GUI.skin.verticalScrollbar);
		tools.ForEach(util => util.OnGUI());
		EditorGUILayout.EndScrollView();
	}
}

#endif