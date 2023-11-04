using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private PlayerController Player;
    public Animator animator;

    private float xSpeed;
    private float ySpeed;
    private bool hasCollided;

    void Start()
    {
        Player = (PlayerController) FindObjectOfType(typeof(PlayerController));
        
        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        y = objTransform.position.y;
        width = objTransform.localScale.x;
        height = objTransform.localScale.y;

        speed = 3;
        
        damage = 5;

        xSpeed = Player.getX() - x;
        ySpeed = Player.getY() - y;

        float multiplier = speed / Mathf.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);

        xSpeed *= multiplier;
        ySpeed *= multiplier;

        objTransform.Rotate(0, 0, Mathf.Atan(ySpeed / xSpeed) * 180 / Mathf.PI, Space.Self);

        hasCollided = false;
    }

    void Update()
    {
        // only the original projectiles that are being cloned has y that is >= 6
        if (y < 6){
            // move the projectile
            objTransform.position += new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0);

            x = objTransform.position.x;
            y = objTransform.position.y;

            // if it is fully outside the screen borders, destroy it
            if (x > 11.42f + width / 2 || x < -11.42f - width / 2 || y > 5 + height / 2 || y < -5 - height / 2){
                Destroy(this.gameObject);
            }
        }

        if (IsHitting(Player)){
            animator.SetBool("hasCollided", true);
            StartCoroutine(deleteObject());
            if(!hasCollided){
                Player.TakeDamage(damage);
                hasCollided = true;
            }
        }

        if (Player == null){
            Destroy(this.gameObject);
        }
    }

    bool IsHitting(PlayerController player)
    {
        float playerX = player.getX();
        float playerY = player.getY();
        float playerWidth = player.getWidth();
        float playerHeight = player.getHeight();

        return ((x + width / 2 > playerX - playerWidth / 2) &&
                (playerX + playerWidth / 2 > x - width / 2) &&
                (y + height / 2 > playerY - playerHeight / 2) &&
                (playerY + playerHeight / 2 > y - height / 2));
    }

    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
