using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

public class WorldManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] WorldSurface[] surfaces;
    [SerializeField] PropsSurface[] propsSurfaces;

    [Header("References")]
    Dictionary<Vector2Int, Tile> cases = new();
    Dictionary<Vector2Int, Props> props = new();

    List<Vector2Int> groundPos = new List<Vector2Int>();

    [Space(10)]
    // RSO
    // RSF
    [SerializeField] RSF_GetNewWorldPosition rsfGetNewWorldPos;

    // RSP

    [Header("Input")]
    [SerializeField] RSE_OnEntityEnter rseOnEntityEnter;
    [SerializeField] RSE_OnEntityExit rseOnEntityExit;
    [SerializeField] RSE_CurrentTypeTile rseCurrentTypeTile;

    [Header("Output")]
    [SerializeField] RSO_GhostMode rsoGhostMode;
    [SerializeField] RSE_LooseWall rseLooseWall;

    private void OnEnable()
    {
        rsfGetNewWorldPos.Action += GetNewTile;

        rseOnEntityEnter.Action += OnEntityEnter;
        rseOnEntityExit.Action += OnEntityExit;
        rseCurrentTypeTile.Action += CurrentTypeTile;
    }
    private void OnDisable()
    {
        rsfGetNewWorldPos.Action -= GetNewTile;

        rseOnEntityEnter.Action -= OnEntityEnter;
        rseOnEntityExit.Action -= OnEntityExit;
        rseCurrentTypeTile.Action -= CurrentTypeTile;
    }

    private void Start()
    {
        SetupWorldSurfaces();
        SetupPropsSurfaces();
    }
    void SetupWorldSurfaces()
    {
        if (surfaces.Length == 0) return;

        for (int i = 0; i < surfaces.Length; i++)
        {
            foreach (Vector2Int position in GetAllPositionsInSurface(surfaces[i]))
            {
                GameObject go = Instantiate(surfaces[i].caseData.visual, new Vector3(position.x, 0, position.y), Quaternion.identity);
                go.transform.SetParent(transform);

                Tile tile = new Tile(position, go, surfaces[i].caseData, go.GetComponent<Interactible>());

                if (tile.caseData.caseType == CaseType.Walkable)
                {
                    go.SetActive(false);
                }

                if (tile.caseData.caseType == CaseType.Walkable && position != Vector2.zero)
                {
                    groundPos.Add(position);
                }

                cases.Add(position, tile);
            }
        }
    }
    void SetupPropsSurfaces()
    {
        if(propsSurfaces.Length <= 0) return;

        foreach (PropsSurface propsSurface in propsSurfaces)
        {
            if (groundPos.Count <= 0) return;

            for (int i = 0; i < propsSurface.count; i++)
            {
                int index = groundPos.GetRandomIndex();
                Vector2Int position = groundPos[index];
                groundPos.RemoveAt(index);

                GameObject go = Instantiate(propsSurface.propsData.visual, new Vector3(position.x, 0,position.y), Quaternion.identity);
                go.transform.SetParent(transform);

                Props _props = new Props(position, go, propsSurface.propsData, go.GetComponent<Interactible>());
                go.SetActive(false);
                props.Add(position, _props);
            }
        }
    }

    Vector2Int[] GetAllPositionsInSurface(WorldSurface surface)
    {
        List<Vector2Int> positions = new();

        int xMin = Mathf.Min(surface.startingPosition.x, surface.endingPosition.x);
        int xMax = Mathf.Max(surface.startingPosition.x, surface.endingPosition.x);
        
        int yMin = Mathf.Min(surface.startingPosition.y, surface.endingPosition.y);
        int yMax = Mathf.Max(surface.startingPosition.y, surface.endingPosition.y);

        for (int i = xMin; i <= xMax; i++)
        {
            for (int j = yMin; j <= yMax; j++)
            {
                positions.Add(new Vector2Int(i, j));
            }
        }

        return positions.ToArray();
    }

    void CurrentTypeTile(Vector2Int currentPosition)
    {
        switch (cases[currentPosition].caseData.caseType)
        {
            case CaseType.Walkable:
                break;
            case CaseType.Solid:
                rseLooseWall.Call();
                break;
            default: 
                break;
        }
    }

    Vector2Int GetNewTile(Vector2Int currentPosition, Vector2Int desirePosition)
    {
        if (!cases.ContainsKey(desirePosition))
        {
            return currentPosition;
        }

        if(!rsoGhostMode.Value)
        {
            switch (cases[desirePosition].caseData.caseType)
            {
                case CaseType.Walkable:
                    cases[desirePosition].gameObject.SetActive(true);
                    return cases[desirePosition].position;

                case CaseType.Solid:
                    return currentPosition;

                default: return currentPosition;
            }
        }
        else
        {
            cases[desirePosition].gameObject.SetActive(true);
            return cases[desirePosition].position;
        }
    }

    void OnEntityEnter(GameObject entity, Vector2Int position)
    {
        if (cases.ContainsKey(position))
        {
            Tile _tile = cases[position];
            if (_tile.interactible != null)
            {
                _tile.interactible.OnEntityEnter(entity);
            }
        }
        
        if (props.ContainsKey(position))
        {
            Props _props = props[position];
            if (_props.interactible != null)
            {
                _props.gameObject?.SetActive(true);
                _props.interactible.OnEntityEnter(entity);
            }
        }
    }
    void OnEntityExit(GameObject entity, Vector2Int position)
    {
        if (cases.ContainsKey(position))
        {
            Tile _tile = cases[position];
            if(_tile.interactible != null)
            {
                _tile.interactible.OnEntityExit(entity);
            }
        }

        if (props.ContainsKey(position))
        {
            Props _props = props[position];
            if (_props.interactible != null)
            {
                _props.interactible.OnEntityExit(entity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (surfaces.Length > 0)
        {
            for (int i = 0; i < surfaces.Length; i++)
            {
                if (surfaces[i].caseData == null) continue;

                Gizmos.color = surfaces[i].caseData.caseDebugColor;

                Vector3 center = new Vector3(
                    (surfaces[i].startingPosition.x + surfaces[i].endingPosition.x) / 2f,
                    0,
                    (surfaces[i].startingPosition.y + surfaces[i].endingPosition.y) / 2f);

                Vector3 size = new Vector3(
                    Mathf.Abs(surfaces[i].endingPosition.x - surfaces[i].startingPosition.x) + 1,
                    0,
                    Mathf.Abs(surfaces[i].endingPosition.y - surfaces[i].startingPosition.y) + 1
                );

                Gizmos.DrawCube(center, size);
            }
        }
    }
}