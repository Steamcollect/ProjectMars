using TMPro;
using UnityEngine;
public class UIGame : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private TextMeshProUGUI textMove;
    [SerializeField] private TextMeshProUGUI textGhost;
    [SerializeField] private GameObject panelTextGhost;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] private RSE_UpdateUI rseUpdateUI;
    [SerializeField] private RSE_UpdateUIGhost rseUpdateUIGhost;

    [Header("Output")]
    [SerializeField] private RSO_MoveMax rsoMoveMax;
    [SerializeField] private RSO_CurrentMove rsoCurrentMove;
    [SerializeField] private RSO_CoinsMax rsoCoinsMax;
    [SerializeField] private RSO_CurrentCoins rsoCurrentCoins;

    private void OnEnable()
    {
        rseUpdateUI.Action += UpdateUI;
        rseUpdateUIGhost.Action += UpdateGhostUI;
    }

    private void OnDisable()
    {
        rseUpdateUI.Action -= UpdateUI;
        rseUpdateUIGhost.Action -= UpdateGhostUI;
    }

    private void UpdateUI()
    {
        textCoins.text = $"Coins: {rsoCurrentCoins.Value}/{rsoCoinsMax.Value}";
        textMove.text = $"Moves: {rsoCurrentMove.Value}/{rsoMoveMax.Value}";
    }

    private void UpdateGhostUI(float value)
    {
        if(value > 0)
        {
            panelTextGhost.SetActive(true);
        }
        else
        {
            panelTextGhost.SetActive(false);
        }

        textGhost.text = $"Ghost: {value:0.00}s";
    }
}