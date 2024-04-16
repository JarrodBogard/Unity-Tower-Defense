using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    // [SerializeField] GameObject towerPrefab;
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    // public bool GetIsPlaceable()
    // {
    //     return isPlaceable;
    // }

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isPlaceable)
            {
                // Debug.Log(transform.name);
                // Instantiate(towerPrefab, transform.position, Quaternion.identity);
                bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
                isPlaceable = !isPlaced;
                // isPlaceable = false;
            }
        }
    }

    // void OnMouseDown()
    // void OnMouseUp()
    // {
    //     Debug.Log(transform.name);
    // }
}
