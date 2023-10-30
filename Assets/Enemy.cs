using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private EnemySpawner Spawner;
    private PlayerController Player;

    private Transform Blaster;

    void Start()
    {
        Spawner = (EnemySpawner) FindObjectOfType(typeof(EnemySpawner));
        Player = (PlayerController) FindObjectOfType(typeof(PlayerController));

        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        y = objTransform.position.y;
        width = objTransform.localScale.x;
        height = objTransform.localScale.y;

        speed = 2;

        shotCoolDown = 2;

        HP = 5 * 3;

        Blaster = objTransform.GetChild(0);
    }

    void Update()
    {
        // only the original enemy that is being cloned has y that is >= 6
        if (y < 6){
            objTransform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

            x = objTransform.position.x;

            // if it fully passed through left border, destroy it
            if (x < -11.42f - width / 2){
                Spawner.DeleteEnemy(this);
                Destroy(this.gameObject);
            }

            if(Player != null){
                float xComp = Player.getX() - x;
                float yComp = Player.getY() - y;

                objTransform.rotation = new Quaternion(0, 0, 0, 1);
                objTransform.Rotate(0,0, Mathf.Atan(yComp / xComp) * 180 / Mathf.PI, Space.Self);

                if (xComp > 0){
                    objTransform.Rotate(0, 0, 180, Space.Self);
                }

                if (IsCollidingWithPlayer() && !Player.isInvulnerable){
                    Player.TakeDamage(5);
                    this.TakeDamage(5);

                    Player.SetToInvulnerable();
                }

                shotCoolDown -= Time.deltaTime;

                if (shotCoolDown <= 0){
                    GameObject projectile = Instantiate(Projectile,
                                    new Vector3(Blaster.position.x, Blaster.position.y, 0),
                                    new Quaternion(0,0,0,0));

                    shotCoolDown = 2;
                }
            }
        }
    }

    bool IsCollidingWithPlayer()
    {
        float playerX = Player.getX();
        float playerY = Player.getY();
        float playerWidth = Player.getWidth();
        float playerHeight = Player.getHeight();

        return ((x + width / 2 > playerX - playerWidth / 2) &&
                (playerX + playerWidth / 2 > x - width / 2) &&
                (y + height / 2 > playerY - playerHeight / 2) &&
                (playerY + playerHeight / 2 > y - height / 2));
    }

    public void TakeDamage(int dmg){
        HP -= dmg;

        if (HP > 0){
            // apply small explosion
        } else {
            // apply big explosion

            Spawner.DeleteEnemy(this);
            Destroy(this.gameObject);

            Player.AddScore();
        }
    }
}
