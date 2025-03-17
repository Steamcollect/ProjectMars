using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField, SceneName] private string levelStartName;

    [Header("Output")]
    [SerializeField] private RSE_Start rseStart;
    [SerializeField] private RSE_Quit rseQuit;

    private void OnEnable()
    {
        rseStart.Action += StartGame;
        rseQuit.Action += QuitGame;
    }

    private void OnDisable()
    {
        rseStart.Action -= StartGame;
        rseQuit.Action -= QuitGame;
    }

    private void StartGame()
    {
        string scenePath = AssetDatabase.GUIDToAssetPath(levelStartName);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        SceneManager.LoadScene(sceneName);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}