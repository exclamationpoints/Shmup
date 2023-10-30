using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject Projectile;

    protected Transform objTransform;
    protected float x;
    protected float y;
    protected float width;
    protected float height;

    protected float speed;
    protected float shotCoolDown;

    protected int HP;

    public float getX(){
        return x;
    }

    public float getY(){
        return y;
    }

    public float getWidth(){
        return width;
    }

    public float getHeight(){
        return height;
    }
}
