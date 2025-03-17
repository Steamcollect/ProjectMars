using UnityEngine;

public class PowerUp : Interactible
{
    [Header("Settings")]
    [SerializeField] float timeEffect = 3;

    bool isCollected = false;

    [Header("Output")]
    [SerializeField] RSE_GhostMode rseGhostMode;

    public override void OnEntityEnter(GameObject entity)
    {
        if (isCollected) return;

        if (entity.CompareTag("Player"))
        {
            rseGhostMode.Call(timeEffect);
            isCollected = true;
            gameObject.SetActive(false);
        }
    }

    public override void OnEntityExit(GameObject entity)
    {
        // Do nothing
    }
}