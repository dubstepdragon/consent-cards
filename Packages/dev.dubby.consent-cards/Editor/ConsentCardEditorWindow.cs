#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.Core;
using VRC.SDK3.Components;
using VRC.SDK3.Editor;
using VRC.SDKBase;

public class ConsentCardEditorWindow : EditorWindow
{
    
    public GameObject CardPrefab;

    public static ConsentCardManager ConsentCardManager;
    public static VRCObjectPool CardParent;
    
    public static int WorldCapacity = 32;
    
    [MenuItem("Consent Card Tools/Consent Card Manager Window")]
    public static void OpenWindow()
    {
        ConsentCardEditorWindow wnd = GetWindow<ConsentCardEditorWindow>();
        wnd.titleContent = new GUIContent("Consent Card Editor Window");

        ConsentCardManager = FindObjectOfType<ConsentCardManager>();
        CardParent = ConsentCardManager.GetComponentInChildren<VRCObjectPool>();
        
        
    }

    
    private void OnGUI()
    {
        if (ConsentCardManager == null)
        {
            ConsentCardManager = EditorGUILayout.ObjectField("Consent Card Manager Missing, please Drag and Drop it Here...", (ConsentCardManager)ConsentCardManager, typeof(ConsentCardManager), true) as ConsentCardManager;
            if (ConsentCardManager == null) return;
            CardParent =  ConsentCardManager.GetComponentInChildren<VRCObjectPool>();
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
            if (!CardPrefab) return;
            
            List<GameObject> forPool = new List<GameObject>();

            for (int i = 0; i < CardParent.transform.childCount; i++)
            {
                DestroyImmediate(CardParent.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < WorldCapacity; i++)
            {
                float progress = (float)CardParent.transform.childCount / (float)WorldCapacity;
                EditorUtility.DisplayProgressBar("Consent Card Editor Window Process", "Adjusting Consent Card number...", progress);
                GameObject spawnedCard = PrefabUtility.InstantiatePrefab(CardPrefab, CardParent.transform) as GameObject;
                if (!spawnedCard)
                {
                    Debug.LogError("Error Spawning Card Prefab, Check References");
                    return;
                }
                spawnedCard.SetActive(false);
                forPool.Add(spawnedCard);
            }
            
            CardParent.Pool = forPool.ToArray();
            
            EditorUtility.ClearProgressBar();
        }
    }
}
#endif