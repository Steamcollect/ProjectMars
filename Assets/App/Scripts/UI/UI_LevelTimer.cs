using TMPro;
using UnityEngine;
public class UI_LevelTimer : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] TMP_Text timerTxt;

    [Space(10)]
    // RSO
    [SerializeField] RSO_LevelTime levelTime;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Update()
    {
        timerTxt.text = "Time left: " + levelTime.Value.ToString("F1");
    }
}