using UnityEngine;

public struct Tile
{
    public Vector2Int position;

    public SSO_CaseData caseData;
    public Interactible interactible;

    public Tile(Vector2Int position, SSO_CaseData caseData, Interactible interactible)
    {
        this.position = position;
        this.caseData = caseData;
        this.interactible = interactible;
    }
}