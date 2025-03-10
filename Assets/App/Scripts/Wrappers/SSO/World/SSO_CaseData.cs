using UnityEngine;

[CreateAssetMenu(fileName = "SSO_CaseData", menuName = "SSO/World/SSO_CaseData")]
public class SSO_CaseData : ScriptableObject
{
    public string caseName;
    public CaseType caseType;

    [Space(10)]
    public GameObject visual;

    [Header("Debug")]
    public Color caseDebugColor;
}

public enum CaseType
{
    Walkable,
    Solid
}