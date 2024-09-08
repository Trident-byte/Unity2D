using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] protected float lifeTime = 1f;

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    protected void FixedUpdate()
    {
        rb.velocity = transform.up * speed;

    }

    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     Destroy(gameObject);
    // }
}
