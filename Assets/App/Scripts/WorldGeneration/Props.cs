using UnityEngine;

public struct Props
{
    public Vector2Int position;
    public GameObject gameObject;

    public SSO_PropsData propsData;
    public Interactible interactible;

    public Props(Vector2Int position, GameObject gameObject, SSO_PropsData propsData, Interactible interactible)
    {
        this.position = position;
        this.gameObject = gameObject;
        this.propsData = propsData;
        this.interactible = interactible;
    }
}