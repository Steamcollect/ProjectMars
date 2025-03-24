using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Settings")]
    float currentTime = 0;
    [SerializeField] float maxTime = 60;

    [Space(5)]
    [SerializeField, SceneName] private string leveMenuName;

    [Header("References")]

    //[Space(10)]
    // RSO
    [SerializeField] RSO_LevelTime levelTime;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        currentTime = maxTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        levelTime.Value = currentTime;

        if(currentTime <= 0)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(leveMenuName);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            SceneManager.LoadScene(sceneName);
        }
    }
}