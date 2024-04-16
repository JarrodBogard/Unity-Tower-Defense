using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15f;
    Transform target;
    // [SerializeField] Transform target;

    // void Start()
    // {
    //     target = FindObjectOfType<EnemyMover>().transform;

    // }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
        // if (target != null) { AimWeapon(); } else { return; } // doesn't work
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon()
    {
        if (target)
        {

            float targetDistance = Vector3.Distance(transform.position, target.position);

            weapon.LookAt(target);

            if (targetDistance <= range)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;

    }
}



// void Update()
//     {
//         if (FindObjectOfType<Enemy>() != null)
//         {
//             FindClosestTarget();
//             AimWeapon();
//         }
//         else
//         {
//             Attack(false);
//         }
//     }

// using System;

// [SerializeField] Transform weapon;
// Transform enemy;
// bool searchingEnemy = false;

// void Start()
// {
//     try
//     {
//         enemy = FindObjectOfType<EnemyMover>().transform;
//     }

//     catch (NullReferenceException)
//     {

//         Debug.Log("There is no enemy");
//     }

// }
// void Update()
// {
//     AimWeapon();
// }

//   void AimWeapon()
//     {
//         if (enemy != null)
//         {
//             weapon.LookAt(enemy);
//         }
//         else
//         {
//             if(!searchingEnemy)
//             {
//                 StartCoroutine(EnemyFinder());
//             }
//         }
//     }

// IEnumerator EnemyFinder()
// {
//     while(enemy == null)
//     {
//         searchingEnemy = true;
//         try
//         {
//             enemy = FindObjectOfType<EnemyMover>().transform;
//         }
//         catch(NullReferenceException)
//         {
//             Debug.Log("Searching for enemy");
//         }
//         yield return new WaitForSeconds(1);
//     }
//     searchingEnemy = false;
// }