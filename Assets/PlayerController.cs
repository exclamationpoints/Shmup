using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Entity
{
    public bool isInvulnerable;
    private float invulnerableTimer;
    public AudioSource playerShoot;
    public AudioSource playerDie;
    public AudioSource playerHurt;
    public AudioSource enemyDie;
    public AudioSource BGM;
    public AudioSource gameOverSound;
    public Animator animator;
    private bool isDead;
    public SpriteRenderer sr;
    public SpriteRenderer blackScreen;
    public SpriteRenderer gameOver;
    private float chaosCount;

    private int score;
    private int scoreProbability;
    public Text displayText;

    public HealthBar healthBar;
    private List<string> quips;

    void Start()
    {
        objTransform = this.gameObject.transform;

        x = objTransform.position.x;
        y = objTransform.position.y;
        width = objTransform.localScale.x;
        height = objTransform.localScale.y;

        speed = 6f;
        shotCoolDown = 0;

        HP = 5 * 20;
        isDead = false;

        isInvulnerable = false;
        invulnerableTimer = 2;

        score = 0;

        quips = new List<string>{"Hello, [[WORLD]] ! ! !",
            "[[Hyperlink Blocked]].",
            "HAEAHAEAHAEAHAEAH!!",
            "WOW!!! I'M SO [Proud] OF YOU, I COULD [Killed] YOU!",
            "[Heaven], are you WATCHING?",
            "H  E  A  V  E  N",
            "WHY DON'T YOU [[Show it off?]]",
            "[[BIG SHOT!!!]]",
            "[[BIG SHOT!!!!!]]",
            "[[BIG SHOT!!!!!!!]]",
            "GONE DOWN THE [[Drain]] [[Drain]]??",
            "HEY      EVERY      !! IT'S ME!!!",
            "[Press F1 For] HELP",
            "WRONG ANSWER!!! WRONG!!! WRONG!!! WRONG!!! TRY AGAIN!!!",
            "YOUR A BIGSHOT!! SAVING THE WORLD!!",
            "PLEASURE DOING BUSINESS WITH YOU KID!!!",
            "DON'T YOU WANNA BE A [Big Shot]!?",
            "WAIT!! [$!?!] THE PRESSES! ",
            "Smells like KROMER.",
            "[   404   ]",
            "TAKE THIS DEAL AND YOU WILL [[Die]]!! IT'S THAT GOOD!!!",
            "SOON I'LL EVEN SURPASS THAT DAMNED [[Clown Around Town!]]",
            "I'M THE [[It Burns! Ow! Stop! Help Me! It Burns!]] GUY!",
            "I'LL GET SO. I'LL GET SO. I'LL GET SO. I'LL GET SO. I'LL GET SO. I'LL GET SO.",
            "WE DON'T NEED ANY [[Man, Woman, or Child]] [[At Half Price]]!!",
            "[[Warning! If you consent to the terms and agreements,]] ",
            "I'VE ALWAYS BEEN A MAN OF THE [PIPIS]. A REAL [PIPIS] PERSON!",
            "THE NUMBERS DOES'NT [[$!?!]] MATTER ?!"
        };
    }

    void Update()
    {
        if (!isDead){
            // player horizontal movement
            if (Input.GetKey(KeyCode.RightArrow)){
                x = x + speed * Time.deltaTime;

                // prevent player from passing right border
                if (x + width / 2 > 10f){
                    x = 10f - width / 2;
                }

            } else if (Input.GetKey(KeyCode.LeftArrow)){
                x = x - speed * Time.deltaTime;

                // prevent player from passing left border
                if (x - width / 2 < -10f){
                    x = -10f + width / 2;
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


                shotCoolDown = 0.36f;
                playerShoot.Play();
            }

            if (shotCoolDown > 0){
                shotCoolDown -= Time.deltaTime;
            }

            objTransform.position = new Vector3(x, y, 0);

            if(isInvulnerable){
                // make player blink
                // tentative code (idk if will work for actual sprites)

                float approxTimer = Mathf.Round(invulnerableTimer * 10) / 10;

                if(approxTimer % 2 == 0.0 || approxTimer % 2 == 1.0){
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);
                } else if(approxTimer % 2 == 0.5 || approxTimer % 2 == 1.5) {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                }

                invulnerableTimer -= Time.deltaTime;

                if (invulnerableTimer <= 0){
                    isInvulnerable = false;
                    invulnerableTimer = 2.5f;

                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                }
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!isInvulnerable && isDead == false){
            HP -= dmg;

            healthBar.Configure();

            if (HP > 0){
                // apply small explosion
                playerHurt.Play();
                chaosCount += 0.25f;

            } else {
                // stop sounds
                BGM.volume = 0f;
                playerShoot.volume = 0f;
                playerHurt.volume = 0f;
                enemyDie.volume = 0f;

                // animate
                animator.SetBool("isDead", true);
                isDead = true;
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 1);
                playerDie.Play();
                StartCoroutine(playGameOver());
            }
        }    
    }

    public void AddScore()
    {
        score += Random.Range(10, 100);
        scoreProbability = Random.Range(1,11);

        if(scoreProbability <= 9f-chaosCount){
            displayText.text = score.ToString();
        }
        else{
            displayText.text = quips[Random.Range(0,quips.Count)];
        }
    }

    public void SetToInvulnerable()
    {
        isInvulnerable = true;
    }

    IEnumerator playGameOver()
    {
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < 10; i++) {
            gameOver.color = new Color(gameOver.color.r, gameOver.color.g, gameOver.color.b, gameOver.color.a+0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        gameOver.color = new Color(gameOver.color.r, gameOver.color.g, gameOver.color.b, gameOver.color.a+0.1f);        
        gameOverSound.Play();
    }
}
