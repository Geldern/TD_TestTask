using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public TowerData Tower;
    public Transform Head;
    public Transform MuzzlePoint;
    public GameObject MuzzleFlash;

    private GameObject[] Enemies;
    public GameObject CurrentEnemy;
    public GameObject closest;
    private float closestDist;
    private float Timer;
    
    void Start()
    {
        SearchTarget();
    }

     void Update()
    {
        if (!closest)
        {
            SearchTarget();
        }
        else
        {
            closestDist = Vector3.Distance(closest.transform.position, transform.position);
            if (closestDist <= Tower.Range)
            {
                if (closest.tag == "Enemy")
                {
                    CurrentEnemy = closest;
                    Head.transform.LookAt(CurrentEnemy.transform);
                }
                else SearchTarget();
            }
            else if (closestDist > Tower.Range)
            {
                CurrentEnemy = null;
                closest = null;
            }
        }

        Timer = Mathf.MoveTowards(Timer, 0, Time.deltaTime);
        if (CurrentEnemy != null)
        {
            Shooting();
        }
        else
        {
            SearchTarget();
        }

    }

    void Shooting()
    {
        if (Timer == 0)
        {
            CurrentEnemy.GetComponent<EnemyAI>().CurHealth = CurrentEnemy.GetComponent<EnemyAI>().CurHealth - Tower.Damage;
            Timer = Tower.ShootInterval;
            Instantiate(MuzzleFlash, MuzzlePoint);
        }
    }

    GameObject SearchTarget()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float passedPath = 0;
        foreach (GameObject enemy in Enemies)
        {
                float curPath = enemy.GetComponent<EnemyAI>().PassedPath;
            if (Vector3.Distance(transform.position, enemy.transform.position) <= Tower.Range)
            {
                if (curPath > passedPath)
                {
                    closest = enemy;
                    Debug.DrawLine(transform.position, closest.transform.position, Color.red);
                    passedPath = curPath;
                }
            } 
        }
        return closest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Tower.Range);
    }
}
