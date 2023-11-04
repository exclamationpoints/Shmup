using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    private EnemySpawner EnSpawner;
    public Animator animator;
    private bool hasCollided;

    void Start()
    {
        EnSpawner = (EnemySpawner) FindObjectOfType(typeof(EnemySpawner));

        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        y = objTransform.position.y;
        width = objTransform.localScale.x;
        height = objTransform.localScale.y;

        speed = 3.5f;

        damage = 5;
    }

    void Update()
    {
        // only the original projectiles that are being cloned has y that is >= 6
        if (y < 6){
            // move the projectile
            objTransform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            x = objTransform.position.x;
            y = objTransform.position.y;

            // if it fully passed through the right border, destroy it
            if (x > 11.42f + width / 2){
                Destroy(this.gameObject);
            }
        }

        foreach(Enemy enemy in EnSpawner.GetEnemiesAlive()){
            if (IsHitting(enemy)){
                animator.SetBool("hasCollided", true);
                StartCoroutine(deleteObject());
                if(!hasCollided){
                    enemy.TakeDamage(damage);
                    hasCollided = true;
                    speed = -2f;
                }
                break;
            }
        }
    }

    bool IsHitting(Enemy enemy)
    {
        float enemyX = enemy.getX();
        float enemyY = enemy.getY();
        float enemyRadius = enemy.getWidth() / 2;

        float allowance = 0.05f;

        return Mathf.Sqrt((x - enemyX) * (x - enemyX) + 
            (y - enemyY) * (y - enemyY)) <= enemyRadius + allowance;
    }

    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
