using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyData Enemy;
    [HideInInspector]
    public int CurHealth;

    [HideInInspector]
    public Transform Waypoints;
    [HideInInspector]
    public Transform CurrentWaypoint;
    [HideInInspector]
    public int WaypointNum = -1;
    [HideInInspector]
    public float LifeTime;
    [HideInInspector]
    public float PassedPath;
    private GameManager gameManager;
    void Start()
    {
        CurHealth = Enemy.Health;
        Waypoints = GameObject.Find("WayPoints").transform;
        NextWaypoint();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        LifeTime = LifeTime + Time.deltaTime;
        PassedPath = LifeTime * Enemy.Speed;
        if (CurHealth <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        Vector3 dir = CurrentWaypoint.transform.position - transform.position;
        dir.y = 0;

        float speed = Time.deltaTime * Enemy.Speed;

        transform.LookAt(CurrentWaypoint);
        transform.Translate(Vector3.forward * speed);

        if (dir.magnitude <= speed)
            NextWaypoint();

    }

    void Die()
    {
        gameObject.tag = "Dead";
        gameManager.PlayerMoney = gameManager.PlayerMoney + Random.Range(Enemy.MinKillingReward, Enemy.MaxKillingReward);
        GameObject.DestroyImmediate(this.gameObject);
    }

    void NextWaypoint()
    {
        WaypointNum++;
        if (WaypointNum >= Waypoints.childCount)
        {
            gameManager.PlayerHealth = gameManager.PlayerHealth - Enemy.Damage;
            this.gameObject.SetActive(false);
            return;
        }

        CurrentWaypoint = Waypoints.GetChild(WaypointNum);
    }
}
