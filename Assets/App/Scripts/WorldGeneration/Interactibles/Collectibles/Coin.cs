using UnityEngine;

public class Coin : Interactible
{
    [Header("Settings")]
    [SerializeField] int coinGiven = 1;

    bool isCollected = false;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_AddCoin rseAddCoin;

    public override void OnEntityEnter(GameObject entity)
    {
        if (isCollected) return;

        if (entity.CompareTag("Player"))
        {
            rseAddCoin.Call(coinGiven);
            isCollected = true;
            Destroy(gameObject);
        }
    }

    public override void OnEntityExit(GameObject entity)
    {
        // Do nothing
    }
}