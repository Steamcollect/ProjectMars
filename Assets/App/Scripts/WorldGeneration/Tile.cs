using UnityEngine;

public struct Tile
{
    public Vector2Int position;
    public GameObject gameObject;

    public SSO_CaseData caseData;
    public Interactible interactible;

    public Tile(Vector2Int position, GameObject gameObject, SSO_CaseData caseData, Interactible interactible)
    {
        this.position = position;
        this.gameObject = gameObject;
        this.caseData = caseData;
        this.interactible = interactible;
    }
}