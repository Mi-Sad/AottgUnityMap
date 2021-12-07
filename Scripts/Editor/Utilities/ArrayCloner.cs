#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace Utilities
{
    class ArrayCloner : TargetingUtility
    {
		List<v3Application> applications = new List<v3Application>()
		{new v3Application("Rotation Offset"), new v3Application("Scale Increase"), new v3Application("Position Offset")};
		bool rotation_relative, use_transform, keep_name;
		Transform rotation_transform;
		Vector3 rotation_point;
		int how_many_copies;

		protected override void UtilGUI()
		{
			base.UtilGUI();
			this.how_many_copies = EditorGUILayout.IntField("Copies Number:", this.how_many_copies);
			this.keep_name = EditorGUILayout.Toggle("Keep Original Name", this.keep_name);
			this.applications[0].Dialog_GUI();
			this.rotation_relative = EditorGUILayout.Toggle("Relative", this.rotation_relative);
			if (this.rotation_relative)
			{
				this.use_transform = EditorGUILayout.Toggle("Use Transform", this.rotation_relative);

				if (this.use_transform)
				{
					rotation_transform = (Transform)EditorGUILayout.ObjectField(rotation_transform, typeof(Transform));
					this.rotation_point = rotation_transform.position;
				}
				else
					this.rotation_point = EditorGUILayout.Vector3Field("Position", this.rotation_point);
			}
			this.applications[1].Dialog_GUI();
			this.applications[2].Dialog_GUI();
			if (GUILayout.Button("Apply"))
				this.Process();
		}

		void Process()
		{
			if (how_many_copies > 0)
			{
				Dictionary<GameObject, Vector3> last_scales = new Dictionary<GameObject, Vector3>();
				foreach (var go in this.GetTargets().Select(t => t.gameObject))
				{
					last_scales.Add(go, go.transform.localScale);
				}

				var scale_factor = this.applications[1].Get(Vector3.one);

				for (int i = 1; i <= how_many_copies; i++)
				{
					foreach (var go in this.GetTargets().Select(t => t.gameObject))
					{
						var copy = (GameObject)GameObject.Instantiate(go);
						if (this.keep_name)
							copy.name = go.name;
						else
							copy.name = go.name + "." + i;

						copy.transform.position = go.transform.position + i * this.applications[2].Get(Vector3.zero);
						var rotateOfEuler = i * this.applications[0].Get(Vector3.zero);
						copy.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles + rotateOfEuler);
						if (this.rotation_relative)
						{
							var rotateOf = Quaternion.Euler(rotateOfEuler);
							var position_rot_add = rotateOf * (copy.transform.position - this.rotation_point);
							copy.transform.position = copy.transform.position + position_rot_add;
						}

						var last_scale = last_scales[go];
						last_scale.x *= scale_factor.x;
						last_scale.y *= scale_factor.y;
						last_scale.z *= scale_factor.z;
						last_scales[go] = last_scale;
						go.transform.localScale = last_scale;
					}
				}
			}
			else
				Debug.LogError("Copy number isn't positive!");
		}
	}
}
#endif