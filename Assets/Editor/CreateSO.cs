using UnityEngine;
using UnityEditor;
using System.IO;

public static class SOCreatorWithHotkey
{
    // puts new SO in rename mode. name it, don't press Enter, press hotkey again to save it and create a new one
    readonly static bool isInRenameMode = true;

    // % = ctrl, # = shift, & = alt, _ = (no modifier key).
    [MenuItem("Assets/Create/SO _F11", false, priority = 65)]
    static void CreateScriptableObject()
    {
        var script = Selection.activeObject as MonoScript;

        if (script != null && script.GetClass().IsSubclassOf(typeof(ScriptableObject)))
        {
            var asset = ScriptableObject.CreateInstance(script.GetClass());
            var path = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(script));

            if (isInRenameMode)
            {
                ProjectWindowUtil.CreateAsset(asset, $"{path}.asset");
                Selection.activeObject = script;
            }
            else
            {
                AssetDatabase.CreateAsset(asset, $"{path}.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = asset;
            }
        }
    }
}