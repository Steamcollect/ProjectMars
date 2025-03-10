using UnityEngine;

[CreateAssetMenu(fileName = "SSO_PropsData", menuName = "SSO/World/SSO_PropsData")]
public class SSO_PropsData : ScriptableObject
{
    public string caseName;

    [Space(10)]
    public GameObject visual;

    [Header("Debug")]
    public Color debugColor;
}