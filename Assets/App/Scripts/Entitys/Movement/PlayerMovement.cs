using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveTime;

    Vector2Int currentPosition;

    [Header("References")]
    [SerializeField] PlayerInput input;

    [Space(10)]
    // RSO
    // RSF
    [SerializeField] RSF_GetNewWorldPosition rsfGetNewWorldPos;

    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_OnEntityEnter rseOnEntityEnter;
    [SerializeField] RSE_OnEntityExit rseOnEntityExit;

    private void OnEnable()
    {
        input.onMoveInput += OnMoveInput;
    }
    private void OnDisable()
    {
        input.onMoveInput -= OnMoveInput;
    }

    void OnMoveInput(Vector2 input)
    {
        Vector2Int desirePosition = currentPosition + Vector2Int.RoundToInt(input);
        Vector2Int targetPosition = rsfGetNewWorldPos.Call(currentPosition, desirePosition);

        if(targetPosition != currentPosition)
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