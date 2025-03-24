using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveTime;

    Vector2Int currentPosition;
    int currentMove = 0;
    Material startMaterial;

    [Header("References")]
    [SerializeField] PlayerInput input;
    [SerializeField] Material materialGhost;

    [Space(10)]
    // RSO
    // RSF
    [SerializeField] RSF_GetNewWorldPosition rsfGetNewWorldPos;

    // RSP

    [Header("Input")]
    [SerializeField] RSE_GhostMode rseGhostMode;

    [Header("Output")]
    [SerializeField] RSE_OnEntityEnter rseOnEntityEnter;
    [SerializeField] RSE_OnEntityExit rseOnEntityExit;
    [SerializeField] RSO_MoveMax rsoMoveMax;
    [SerializeField] RSE_Loose rseLoose;
    [SerializeField] RSO_GhostMode rsoGhostMode;
    [SerializeField] RSE_CurrentTypeTile rseCurrentTypeTile;
    [SerializeField] RSE_UpdateUIGhost rseUpdateUIGhost;

    private void OnEnable()
    {
        input.onMoveInput += OnMoveInput;
        rseGhostMode.Action += GhostMode;
    }
    private void OnDisable()
    {
        transform.DOKill();

        input.onMoveInput -= OnMoveInput;
        rseGhostMode.Action -= GhostMode;
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
    
    private void Start()
    {
        startMaterial = GetComponent<MeshRenderer>().material;
        rsfGetNewWorldPos.Call(currentPosition, currentPosition);

        Invoke("LateStart", 0.1f);
    }

    void LateStart()
    {
        rseOnEntityEnter.Call(gameObject, Vector2Int.RoundToInt(transform.position));
    }

    void OnMoveInput(Vector2 input)
    {
        Vector2Int desirePosition = currentPosition + Vector2Int.RoundToInt(input);
        Vector2Int targetPosition = rsfGetNewWorldPos.Call(currentPosition, desirePosition);

        if(targetPosition != currentPosition)
        {
            currentMove++;

            rseLoose.Call(currentMove);

            if(currentMove <= rsoMoveMax.Value)
            {
                rseOnEntityExit.Call(gameObject, currentPosition);

                transform.DOKill();
                transform.DOMove(
                    new Vector3(targetPosition.x, transform.position.y, targetPosition.y),
                    moveTime).
                    OnComplete(() =>
                    {
                        rseOnEntityEnter.Call(gameObject, targetPosition);
                    });

                currentPosition = targetPosition;
            }
        }
    }

    void GhostMode(float timer)
    {
        StartCoroutine(GhostModeTimer(timer));
    }

    IEnumerator GhostModeTimer(float timer)
    {
        rsoGhostMode.Value = true;

        float elapsedTime = 0f;

        rseUpdateUIGhost.Call(timer);
        GetComponent<MeshRenderer>().material = materialGhost;

        while (elapsedTime < timer)
        {
            rseUpdateUIGhost.Call(timer - elapsedTime);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        rsoGhostMode.Value = false;
        rseUpdateUIGhost.Call(0);
        GetComponent<MeshRenderer>().material = startMaterial;

        rseCurrentTypeTile.Call(currentPosition);
    }
}