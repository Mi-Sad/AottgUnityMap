#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

namespace Utilities
{
    class Grounderer : TargetingUtility
	{
		List<v3Application> applications = new List<v3Application>()
		{new v3Application("Offset")};
		protected override void UtilGUI()
		{
			base.UtilGUI();
			this.applications.ForEach(a => a.Dialog_GUI());
			if (GUILayout.Button("Apply"))
				this.Process();
		}

		void Process()
		{
			foreach (Transform go in this.GetTargets())
			{
				var goPos = go.transform.position;
				var ray = new Ray(goPos, Vector3.down);
				var hits = Physics.RaycastAll(ray, 200);
				if (hits.Length > 0)
				{
					var pos = hits[0].point;
					pos += this.applications[0].Get(Vector3.zero);
					go.transform.position = pos;
				}
			}
		}
	}
}
#endif