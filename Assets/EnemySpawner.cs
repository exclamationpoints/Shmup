using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    private List<Enemy> enemiesAlive;

    private float enemyWidth;
    private float enemyHeight;

    private float spawnCooldown;
    private float cooldownSpeedup;

    void Start()
    {
        enemiesAlive = new List<Enemy>();

        enemyWidth = Enemy.transform.localScale.x;
        enemyHeight = Enemy.transform.localScale.y;

        spawnCooldown = 1.5f;
        cooldownSpeedup = 0f;
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown <= 0){
            Enemy enemy = Instantiate(Enemy,
                            new Vector3(11.42f + enemyWidth,
                                Random.Range(-5 + enemyHeight, 5 - enemyHeight), 0),
                            new Quaternion(0,0,0,0)).GetComponent<Enemy>();

            enemiesAlive.Add(enemy);

            spawnCooldown = 3f-cooldownSpeedup;

            if(cooldownSpeedup <= 2){
                cooldownSpeedup += 0.125f;
            }
        }
    }

    public void DeleteEnemy(Enemy enemy){
        enemiesAlive.Remove(enemy);
    }

    public List<Enemy> GetEnemiesAlive(){
        return enemiesAlive;
    }
}
