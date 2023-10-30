using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Transform objTransform;
    protected float x;
    protected float y;
    protected float width;
    protected float height;

    protected float speed;

    protected int damage;

    bool IsHitting(){return false;}
}
