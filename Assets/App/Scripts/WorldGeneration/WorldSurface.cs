using UnityEngine;

[System.Serializable]
struct WorldSurface
{
    public SSO_CaseData caseData;

    [Space(10)]
    public Vector2Int startingPosition;
    public Vector2Int endingPosition;
}