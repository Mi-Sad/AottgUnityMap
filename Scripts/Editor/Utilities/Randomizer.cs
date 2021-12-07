#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Utilities
{
    class Randomizer : TargetingUtility
	{
		List<v3Application> applications = new List<v3Application>()
		{new v3Application("Rotation"), new v3Application("Scale"), new v3Application("Position")};
		bool uniform_scale;

		protected override void UtilGUI()
		{
			base.UtilGUI();
			this.applications[0].Dialog_GUI();
			this.uniform_scale = EditorGUILayout.Toggle("Uniform_Scale", this.uniform_scale);
			if(this.uniform_scale)
            {
				EditorGUILayout.LabelField("Only X value will be taken in consideration");

				var scale_appl = this.applications[1];
				var from = scale_appl.Get(0);
				var to = scale_appl.Get(1);
				from.y = from.z = from.x;
				to.y = to.z = to.x;
				scale_appl.Set(from, 0);
				scale_appl.Set(to, 1);
			}
			this.applications[1].Dialog_GUI();
			this.applications[2].Dialog_GUI();

			if (GUILayout.Button("Apply"))
				this.Process();
		}

		void Process()
		{
			foreach (Transform go in this.GetTargets())
			{
				go.transform.position = this.applications[2].Get(go.transform.position);
				go.transform.rotation = Quaternion.Euler(this.applications[0].Get(go.transform.rotation.eulerAngles));
				var scale_to = this.applications[1].Get(go.transform.localScale);
				if (this.uniform_scale)
					scale_to.y = scale_to.z = scale_to.x;
				go.transform.localScale = scale_to;
			}
		}
	}
}
#endif