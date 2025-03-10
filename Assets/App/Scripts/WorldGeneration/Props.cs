using UnityEngine;

public struct Props
{
    public Vector2Int position;

    public SSO_PropsData propsData;
    public Interactible interactible;

    public Props(Vector2Int position, SSO_PropsData propsData, Interactible interactible)
    {
        this.position = position;
        this.propsData = propsData;
        this.interactible = interactible;
    }
}