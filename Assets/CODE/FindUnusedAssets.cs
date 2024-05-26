using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class FindUnusedAssets : EditorWindow
{
    [MenuItem("Tools/Find Unused PNG Files")]
    public static void ShowWindow()
    {
        GetWindow<FindUnusedAssets>("Find Unused PNG Files");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Unused PNG Files"))
        {
            FindUnusedPNGFiles();
        }
    }

    private void FindUnusedPNGFiles()
    {
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        List<string> usedAssets = new List<string>();
        List<string> allPngAssets = new List<string>();

        // Collect all .png files
        foreach (string path in allAssetPaths)
        {
            if (path.EndsWith(".png"))
            {
                allPngAssets.Add(path);
            }
        }

        // Collect all assets used in scenes
        string[] scenePaths = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
        foreach (string scenePath in scenePaths)
        {
            string[] dependencies = AssetDatabase.GetDependencies(scenePath, true);
            usedAssets.AddRange(dependencies);
        }

        // Remove duplicates
        HashSet<string> usedAssetsSet = new HashSet<string>(usedAssets);

        // Find unused PNG files
        List<string> unusedPngAssets = new List<string>();
        foreach (string pngPath in allPngAssets)
        {
            if (!usedAssetsSet.Contains(pngPath))
            {
                long fileSizeInBytes = new FileInfo(pngPath).Length;
                if (fileSizeInBytes > 5 * 1024 * 1024) // Check if file size is greater than 5MB
                {
                    unusedPngAssets.Add(pngPath);
                }
            }
        }

        // Print unused PNG files
        if (unusedPngAssets.Count > 0)
        {
            Debug.Log("Unused PNG files larger than 5MB:");
            foreach (string unusedPng in unusedPngAssets)
            {
                Object asset = AssetDatabase.LoadAssetAtPath<Object>(unusedPng);
                Debug.Log(unusedPng, asset);
            }
        }
        else
        {
            Debug.Log("No unused PNG files larger than 5MB found.");
        }
    }
}

