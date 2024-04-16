using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    // [SerializeField] float waitTime = 1f;
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    // void Start()
    void OnEnable()
    {
        // Debug.Log("Starting Start");
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        // Debug.Log("Finishing Start");

        // InvokeRepeating("PrintWaypointName", 0, 1f); // will not work
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        // GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        // foreach (GameObject waypoint in waypoints)
        // {
        //     path.Add(waypoint.GetComponent<Waypoint>());
        // }

        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            // path.Add(child.GetComponent<Waypoint>());
            Tile waypoint = child.GetComponent<Tile>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    IEnumerator FollowPath()
    {
        foreach (Tile waypoint in path)
        {
            // Debug.Log(waypoint.name);
            // gameObject.transform.position = waypoint.transform.position;
            // transform.position = waypoint.transform.position;
            // yield return new WaitForSeconds(waitTime);

            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
