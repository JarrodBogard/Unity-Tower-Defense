using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    // [SerializeField] GameObject towerPrefab;
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    // public bool GetIsPlaceable()
    // {
    //     return isPlaceable;
    // }

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
