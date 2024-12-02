using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;


public class DependencyPostProcessor : AssetPostprocessor
{
 
    
   
    

    private static string promptUser = "dubby_cc_promptUser";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
/*
        if (Directory.Exists($"{Application.dataPath}/bd_/AttachToMe")) return;
        
        bool userPrompt = EditorPrefs.GetBool(promptUser, true);

        if (userPrompt == false) return;
        
        // Prompt the user to install the dependencies //TODO: Add bool check here and if someone hits no don't add
        if (EditorUtility.DisplayDialog("Dependencies Missing",
                $"Consent Cards rely on {ConsentCardEditorWindow.fileName}. Do you want to download and install the required dependencies?",
                "Yes", "No"))
        {
            ConsentCardEditorWindow.DownloadAndImportUnityPackage();
        }
        else
        {
            EditorUtility.DisplayDialog("Dependencies Missing",
                $"Consent Cards relies on {ConsentCardEditorWindow.fileName}, you can download them at any time by going to {ConsentCardEditorWindow.menuPath}",
                "Ok");
            EditorPrefs.SetBool(promptUser, false);
        }
        */
    }

    

}
