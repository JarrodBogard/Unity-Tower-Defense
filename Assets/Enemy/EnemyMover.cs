using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    // [SerializeField] List<Tile> path = new List<Tile>();
    // [SerializeField] float waitTime = 1f;
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    // void Start()
    void OnEnable()
    {
        // Debug.Log("Starting Start");
        ReturnToStart();
        RecalculatePath(true);
        // StartCoroutine(FollowPath());
        // Debug.Log("Finishing Start");

        // InvokeRepeating("PrintWaypointName", 0, 1f); // will not work
    }

    // void Start()
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    // void FindPath()
    // void RecalculatePath()
    void RecalculatePath(bool resetPath)
    {
        // GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        // foreach (GameObject waypoint in waypoints)
        // {
        //     path.Add(waypoint.GetComponent<Waypoint>());
        // }

        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());

        // path = pathfinder.GetNewPath();
        // GameObject parent = GameObject.FindGameObjectWithTag("Path");
        // foreach (Transform child in parent.transform)
        // {
        //     // path.Add(child.GetComponent<Waypoint>());
        //     Tile waypoint = child.GetComponent<Tile>();
        //     if (waypoint != null)
        //     {
        //         path.Add(waypoint);
        //     }
        // }
    }

    void ReturnToStart()
    {
        // transform.position = path[0].transform.position;
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    IEnumerator FollowPath()
    {
        // foreach (Tile waypoint in path)
        // for (int i = 0; i < path.Count; i++)
        for (int i = 1; i < path.Count; i++)
        {
            // Debug.Log(waypoint.name);
            // gameObject.transform.position = waypoint.transform.position;
            // transform.position = waypoint.transform.position;
            // yield return new WaitForSeconds(waitTime);

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            // Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
        // enemy.StealGold();
        // gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}
