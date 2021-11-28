/*
 *  Script Made By Sadico
 *  Allow you to export scene as streaming assets to load em as level
 */

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StreamedSceneExporter : MonoBehaviour
{
    // Build a streamed unity3d file. This contain one Scene that can be downloaded
    // on demand and loaded once its asset bundle has been loaded.

    [MenuItem("Assets/Export Streaming Asset")]
    static void ExportResource()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Export Scene(s)", string.Empty, "UnityMap", "unity3d");
        if (path.Length != 0)
        {
            var objs = Selection.objects;
            if (objs != null) {
                List<string> level_scenes = new List<string>();
                foreach (var obj in objs)
                {
                    var obj_path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
                    if (obj_path.Length > 0 && obj_path.EndsWith(".unity"))
                    {
                        if (!level_scenes.Contains(obj_path))
                            level_scenes.Add(obj_path);

                        Debug.Log("Exporting file: " + obj_path);
                    }

                }
                
                if(level_scenes.Count == 0)
                    throw new System.Exception("No File Found");
                else
                {
                    var levels = level_scenes.ToArray();
                    BuildPipeline.BuildStreamedSceneAssetBundle(
                        levels, path, BuildTarget.StandaloneWindows);
                }
            }
            else
                throw new System.Exception("No Obj Selected");
        }
    }
}
#endif