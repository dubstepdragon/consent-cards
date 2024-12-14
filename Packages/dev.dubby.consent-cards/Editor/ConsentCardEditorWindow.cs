#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ConsentCardEditorWindow : EditorWindow
{
    
    public const string fileName = "AttachToMe-v1.2.0.unitypackage";
    
    public static string scriptingDefine = "USING_ATTACH_TO_ME";

    public const string menuPath = "Consent Cards/Tools/Import Dependencies";
    
    //[MenuItem(menuPath)]
    public static void DownloadAndImportUnityPackage()
    {
        try
        {
            
            string filePath = Path.Combine($"{Path.GetDirectoryName(Application.dataPath)}/Packages/dev.dubby.consent-cards/Editor/Dependencies", fileName);
            // Import the package
            Debug.Log("Importing package...");
            AssetDatabase.ImportPackage(filePath, true);
            
            AddScriptingDefines(BuildTargetGroup.Standalone);
            AddScriptingDefines(BuildTargetGroup.Android);
  
        }
        catch (System.Exception e)
        {
            EditorUtility.ClearProgressBar();
            Debug.LogError($"Failed to download the package: {e.Message}");
        }
    }

    private static void AddScriptingDefines(BuildTargetGroup group)
    {
        string newDefines = $"{PlayerSettings.GetScriptingDefineSymbolsForGroup(group)};{scriptingDefine}";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(group, newDefines);
    }

}
#endif