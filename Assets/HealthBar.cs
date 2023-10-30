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

    void Start()
    {
        Player = (PlayerController) FindObjectOfType(typeof(PlayerController));

        objTransform = this.gameObject.transform;
        x = objTransform.position.x;
        y = objTransform.position.y;
        width = 1;
    }

    void Update()
    {
        x = Player.getX();
        y = Player.getY() - 0.65f;

        objTransform.localScale = new Vector3(width, objTransform.localScale.y, 0);
        objTransform.position = new Vector3(x, y, 0);
    }

    public void Configure()
    {
        width -= 0.05f;
    }
}
