using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private PlayerController Player;

    private Transform objTransform;
    private float x;
    private float y;
    private float width;

    private float offset;

    private SpriteRenderer sr;

    void Start()
    {
        Player = (PlayerController) FindObjectOfType(typeof(PlayerController));

        objTransform = this.gameObject.transform;
        x = objTransform.position.x;
        y = objTransform.position.y;
        width = 1;

        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        x = Player.getX() - offset;
        y = Player.getY() - 0.65f;

        objTransform.localScale = new Vector3(width, objTransform.localScale.y, 0);
        objTransform.position = new Vector3(x, y, 0);

        if (width <= 0.25f){
            sr.color = new Color(1, 0.2706f, 0);
        } else if (width <= 0.5f){
            sr.color = Color.yellow;
        } else if (width <= 0.25f){
            sr.color = Color.cyan;
        }
    }

    public void Configure()
    {
        width -= 0.05f;
        offset = 1.5f * (1 - width) / 2;
    }
}
