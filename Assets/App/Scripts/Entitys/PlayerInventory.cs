using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    int coinsAmount;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddCoin rseAddCoin;

    [Header("Output")]
    [SerializeField] private RSE_UpdateUI rseUpdateUI;
    [SerializeField] private RSO_CurrentCoins rsoCurrentCoins;

    private void OnEnable()
    {
        rseAddCoin.Action += AddCoin;
    }
    private void OnDisable()
    {
        rseAddCoin.Action -= AddCoin;
    }

    void AddCoin(int amount)
    {
        coinsAmount += amount;
        rsoCurrentCoins.Value = coinsAmount;
        rseUpdateUI.Call();
    }

    public bool HaveEnoughCoin(int coinRequire)
    {
        return coinsAmount >= coinRequire;
    }
}