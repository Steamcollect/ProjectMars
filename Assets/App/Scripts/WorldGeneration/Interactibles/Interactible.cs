using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    public abstract void OnEntityEnter(GameObject entity);
    public abstract void OnEntityExit(GameObject entity);
}