using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
public class FindMissingReferences : EditorWindow
{
    [MenuItem("Tools/Find Missing References")]
    public static void ShowWindow()
    {
        GetWindow<FindMissingReferences>("Find Missing References");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Missing References in Scene"))
        {
            FindInScene();
        }

        if (GUILayout.Button("Find Missing References in Prefabs"))
        {
            FindInPrefabs();
        }
    }

    private void FindInScene()
    {
        GameObject[] gos = FindObjectsOfType<GameObject>();
        int missingCount = 0;

        foreach (GameObject go in gos)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogError("Missing script in GameObject: " + go.name, go);
                    missingCount++;
                }
                else
                {
                    SerializedObject so = new SerializedObject(components[i]);
                    SerializedProperty sp = so.GetIterator();

                    while (sp.NextVisible(true))
                    {
                        if (sp.propertyType == SerializedPropertyType.ObjectReference && sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                        {
                            Debug.LogError("Missing reference found in GameObject: " + go.name + ", Component: " + components[i].GetType().Name, go);
                            missingCount++;
                        }
                    }
                }
            }
        }

        Debug.Log("Total Missing References in Scene: " + missingCount);
    }

    private void FindInPrefabs()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths();
        int missingCount = 0;

        foreach (string path in prefabPaths)
        {
            if (path.EndsWith(".prefab"))
            {
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                Component[] components = go.GetComponentsInChildren<Component>(true);

                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        Debug.LogError("Missing script in Prefab: " + path, go);
                        missingCount++;
                    }
                    else
                    {
                        SerializedObject so = new SerializedObject(components[i]);
                        SerializedProperty sp = so.GetIterator();

                        while (sp.NextVisible(true))
                        {
                            if (sp.propertyType == SerializedPropertyType.ObjectReference && sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                            {
                                Debug.LogError("Missing reference found in Prefab: " + path + ", Component: " + components[i].GetType().Name, go);
                                missingCount++;
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("Total Missing References in Prefabs: " + missingCount);
    }
}
#endif
