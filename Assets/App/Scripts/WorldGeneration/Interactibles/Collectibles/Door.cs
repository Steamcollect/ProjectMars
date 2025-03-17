using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : Interactible
{
    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private RSO_CoinsMax rsoCoinsMax;

    public override void OnEntityEnter(GameObject entity)
    {
        if (entity.CompareTag("Player"))
        {
            if(entity.TryGetComponent(out PlayerInventory inventory) && inventory.HaveEnoughCoin(rsoCoinsMax.Value))
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = currentSceneIndex + 1;

                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                Debug.Log("Not Enough Coins!");
            }
        }
    }

    public override void OnEntityExit(GameObject entity)
    {
        // Do nothing
    }
}