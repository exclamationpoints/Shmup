using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Entity
{
    public bool isInvulnerable;
    private float invulnerableTimer;

    private int score;
    public Text displayText;

    void Start()
    {
        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        y = objTransform.position.y;
        width = objTransform.localScale.x;
        height = objTransform.localScale.y;

        speed = 2.5f;
        shotCoolDown = 0;

        HP = 5 * 20;

        isInvulnerable = false;
        invulnerableTimer = 2.5f;

        score = 0;
    }

    void Update()
    {
        // player horizontal movement
        if (Input.GetKey(KeyCode.RightArrow)){
            x = x + speed * Time.deltaTime;

            // prevent player from passing right border
            if (x + width / 2 > 11.42f){
                x = 11.42f - width / 2;
            }

        } else if (Input.GetKey(KeyCode.LeftArrow)){
            x = x - speed * Time.deltaTime;

            // prevent player from passing left border
            if (x - width / 2 < -11.42f){
                x = -11.42f + width / 2;
            }
        }

        // player vertical movement
        if (Input.GetKey(KeyCode.UpArrow)){
            y = y + speed * Time.deltaTime;

            // prevent player from passing top border
            if (y + height / 2 > 5){
                y = 5 - height / 2;
            }

        } else if (Input.GetKey(KeyCode.DownArrow)){
            y = y - speed * Time.deltaTime;

            // prevent player from passing bottom border
            if (y - height / 2 < -5){
                y = -5 + height / 2;
            }
        }

        // player input to shoot projectiles
        if (Input.GetKey(KeyCode.Space) && shotCoolDown <= 0){
            GameObject projectile = Instantiate(Projectile,
                                new Vector3(x + width / 2 + 0.75f, y, 0),
                                new Quaternion(0,0,0,0));


            shotCoolDown = 0.5f;
        }

        if (shotCoolDown > 0){
            shotCoolDown -= Time.deltaTime;
        }

        objTransform.position = new Vector3(x, y, 0);

        if(isInvulnerable){
            // make player blink
            // tentative code (idk if will work for actual sprites)
            SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();

            float approxTimer = Mathf.Round(invulnerableTimer * 10) / 10;

            if(approxTimer % 2 == 0.5 || approxTimer % 2 == 1.5){
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            } else if(approxTimer % 2 == 0.0 || approxTimer % 2 == 1.0) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);
            }

            invulnerableTimer -= Time.deltaTime;

            if (invulnerableTimer <= 0){
                isInvulnerable = false;
                invulnerableTimer = 2.5f;

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
        }
    }

    public void TakeDamage(int dmg){
        if (!isInvulnerable){
            HP -= dmg;

            if (HP > 0){
                // apply small explosion
            } else {
                // apply big explosion

                Destroy(this.gameObject);
            }
        }    
    }

    public void AddScore(){
        score += 50;

        displayText.text = score.ToString();
    }

    public void SetToInvulnerable(){
        isInvulnerable = true;
    }
}
