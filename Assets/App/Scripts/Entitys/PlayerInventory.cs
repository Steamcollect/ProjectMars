using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int coinsAmount;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddCoin rseAddCoin;

    //[Header("Output")]

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
    }

    public bool HaveEnoughCoin(int coinRequire)
    {
        return coinsAmount >= coinRequire;
    }
}