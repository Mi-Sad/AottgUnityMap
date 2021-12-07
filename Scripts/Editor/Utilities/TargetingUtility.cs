#if UNITY_EDITOR

using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Utilities
{
    abstract class TargetingUtility : UtilityBase
	{
		Targets target;
		string filter = string.Empty;
		Transform parent;
		List<Transform> list = new List<Transform>();

		protected override void UtilGUI()
		{
			this.TargetGUI();
		}

		protected void TargetGUI()
		{
			this.target = (Targets)enumPopop<Targets>((int)this.target, "Targets:");
			switch (this.target)
			{
				case Targets.Selected:
					break;
				case Targets.Name:
					String(ref filter, "Name:");
					break;
				case Targets.Tag:
					String(ref filter, "Tag:");
					break;
				case Targets.ObjectList:
					int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", list.Count));
					while (newCount < list.Count)
						list.RemoveAt(list.Count - 1);
					while (newCount > list.Count)
						list.Add(null);
					for (int i = 0; i < list.Count; i++)
						list[i] = (Transform)EditorGUILayout.ObjectField(list[i], typeof(Transform));
					break;
				case Targets.Child:
					parent = (Transform)EditorGUILayout.ObjectField(parent, typeof(Transform));
					break;
			}
		}

		protected IEnumerable<Transform> GetTargets()
		{
			switch (this.target)
			{
				case Targets.Selected:
					return Selection.objects.Where(obj => obj is GameObject)
						.Select(obj => ((GameObject)obj).transform);
				case Targets.Name:
					return GameObject.FindObjectsOfType<Transform>().Where(go => go.name.Equals(this.filter)).Select(go => go.transform);
				case Targets.Child:
					return (IEnumerable<Transform>)this.parent.GetEnumerator();
				case Targets.Tag:
					return GameObject.FindObjectsOfType<Transform>().Where(go => go.tag.Equals(this.filter)).Select(go => go.transform);
				case Targets.ObjectList:
					return list;
			}
			return null;
		}
	}
}
#endif