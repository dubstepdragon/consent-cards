#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Cyan.PlayerObjectPool;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.Core;
using VRC.SDK3.Components;
using VRC.SDK3.Editor;
using VRC.SDKBase;

public class ConsentCardEditorWindow : EditorWindow
{
    
    public GameObject CardPrefab;

    public ConsentCardManager ConsentCardManager;
    public CyanPlayerObjectPool ObjectAssigner;
    
    public static int WorldCapacity = 32;
    
    [MenuItem("Consent Card Tools/Consent Card Manager Window")]
    public static void OpenWindow()
    {
        ConsentCardEditorWindow wnd = GetWindow<ConsentCardEditorWindow>();
        wnd.titleContent = new GUIContent("Consent Card Editor Window");

        wnd.ConsentCardManager = FindObjectOfType<ConsentCardManager>();
        wnd.ObjectAssigner = wnd.ConsentCardManager.GetComponentInChildren<CyanPlayerObjectPool>();
        
        
    }

    
    private void OnGUI()
    {
        if (ConsentCardManager == null)
        {
            ConsentCardManager = EditorGUILayout.ObjectField("Consent Card Manager Missing, please Drag and Drop it Here...", (ConsentCardManager)ConsentCardManager, typeof(ConsentCardManager), true) as ConsentCardManager;
            if (ConsentCardManager == null) return;
            ObjectAssigner =  ConsentCardManager.GetComponentInChildren<CyanPlayerObjectPool>();
        }
        
        WorldCapacityInput();
        
        //TODO: Text Input for 3 options
        //TODO: Image Upload and update for 3 Options
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void WorldCapacityInput()
    {
        EditorGUI.BeginChangeCheck();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Enter World Capacity", GUILayout.Width(200));
        WorldCapacity = EditorGUILayout.IntField("", WorldCapacity);
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
        {
          ObjectAssigner.poolSize = WorldCapacity * 2 + 2; //This is according to the tooltip provided in the documentation for Cyan Object assigner.
          EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
#endif