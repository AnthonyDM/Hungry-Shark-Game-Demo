using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int scoreAmount;
    public float speed;
    public float fuseTime = 10;
    public bool badFish;
    public Rigidbody rigidBody;

    public void Start()
    {
        speed += Random.value;
    }

    public void FixedUpdate()
    {
        rigidBody.velocity = transform.forward * speed;
        Destroy(gameObject, fuseTime);
    }
}
