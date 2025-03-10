using UnityEngine;
public class Door : Interactible
{
    [Header("Settings")]
    [SerializeField] int coinRequire;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]
    public override void OnEntityEnter(GameObject entity)
    {
        if (entity.CompareTag("Player"))
        {
            if(entity.TryGetComponent(out PlayerInventory inventory) && inventory.HaveEnoughCoin(coinRequire))
            {
                print("You win");
            }
        }
    }

    public override void OnEntityExit(GameObject entity)
    {
        // Do nothing
    }
}