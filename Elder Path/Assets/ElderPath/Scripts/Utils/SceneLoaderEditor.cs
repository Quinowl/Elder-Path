#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;
/// <summary>
/// With this script you will be able to navigate between scenes in BuildSettings by pressing Control + W.
/// https://gist.github.com/Quinowl
/// </summary>
public class SceneLoaderEditor : EditorWindow
{
    private static List<string> scenePaths;
    private static bool additiveLoad;
    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorBuildSettings.sceneListChanged += LoadScenePaths;
        LoadScenePaths();
    }
    private static void LoadScenePaths()
    {
        scenePaths = new List<string>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) scenePaths.Add(EditorBuildSettings.scenes[i].path);
        scenePaths = scenePaths.Where(x => !x.Contains("Packages/")).ToList();
    }
    // You have to press Control + W (%w)
    [MenuItem("Tools/Load Scene %w")]
    private static void OpenScene()
    {
        if (scenePaths.Count > 0)
        {
            GenericMenu menu = new GenericMenu();
            foreach (var scenePath in scenePaths)
            {
                var sceneName = scenePath.Substring(scenePath.LastIndexOf('/') + 1);
                //
                menu.AddItem(new GUIContent($"🔍 Find plane/{sceneName}/Load"), false, OpenSelectedScene, (scenePath, false));
                menu.AddItem(new GUIContent($"🔍 Find plane/{sceneName}/Add"), false, OpenSelectedScene, (scenePath, true));
                menu.AddItem(new GUIContent($"🔍 Find plane/{sceneName}/Unload"), false, UnloadSelectedScene, sceneName);
                //
                menu.AddItem(new GUIContent($"🔍 Find hierarchy/{scenePath}/Load"), false, OpenSelectedScene, (scenePath, false));
                menu.AddItem(new GUIContent($"🔍 Find hierarchy/{scenePath}/Add"), false, OpenSelectedScene, (scenePath, true));
            }
            menu.ShowAsContext();
        }
        else Debug.LogWarning("No scenes found");
    }
    private static void OpenSelectedScene(object data)
    {
        var parameters = ((string scenePath, bool isAdditive))data;
        var scenePath = parameters.scenePath;
        additiveLoad = parameters.isAdditive;
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) EditorSceneManager.OpenScene(scenePath, additiveLoad ? OpenSceneMode.Additive : OpenSceneMode.Single);
    }
    private static void UnloadSelectedScene(object scenePath)
    {
        try
        {
            EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath((string)scenePath), true);
        }
        catch (System.Exception)
        {
            Debug.Log("This scene it is not loaded");
            throw;
        }
    }
}
#endif