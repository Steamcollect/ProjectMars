using TMPro;
using UnityEngine;
public class UIGame : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private TextMeshProUGUI textMove;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] private RSE_UpdateUI rseUpdateUI;

    [Header("Output")]
    [SerializeField] private RSO_MoveMax rsoMoveMax;
    [SerializeField] private RSO_CurrentMove rsoCurrentMove;
    [SerializeField] private RSO_CoinsMax rsoCoinsMax;
    [SerializeField] private RSO_CurrentCoins rsoCurrentCoins;

    private void OnEnable()
    {
        rseUpdateUI.Action += UpdateUI;
    }

    private void OnDisable()
    {
        rseUpdateUI.Action -= UpdateUI;
    }

    private void UpdateUI()
    {
        textCoins.text = $"Coins: {rsoCurrentCoins.Value}/{rsoCoinsMax.Value}";
        textMove.text = $"Moves: {rsoCurrentMove.Value}/{rsoMoveMax.Value}";
    }
}