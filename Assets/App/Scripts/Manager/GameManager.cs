using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxMove;
    [SerializeField] int maxCoins;

    [Header("References")]
    [SerializeField, SceneName] private string leveMenuName;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] private RSE_Loose rseLoose;

    [Header("Output")]
    [SerializeField] private RSE_UpdateUI rseUpdateUI;
    [SerializeField] private RSO_MoveMax rsoMoveMax;
    [SerializeField] private RSO_CurrentMove rsoCurrentMove;
    [SerializeField] private RSO_CoinsMax rsoCoinsMax;
    [SerializeField] private RSO_CurrentCoins rsoCurrentCoins;

    private void OnEnable()
    {
        rseLoose.Action += LooseGame;
    }

    private void OnDisable()
    {
        rseLoose.Action -= LooseGame;
    }

    private void Start()
    {
        rsoMoveMax.Value = maxMove;
        rsoCurrentMove.Value = 0;

        rsoCoinsMax.Value = maxCoins;
        rsoCurrentCoins.Value = 0;

        rseUpdateUI.Call();
    }

    private void LooseGame(int currentMove)
    {
        if (currentMove > maxMove)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(leveMenuName);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            SceneManager.LoadScene(sceneName);
        }
        else
        {
            rsoCurrentMove.Value = currentMove;

            rseUpdateUI.Call();
        }
    }
}