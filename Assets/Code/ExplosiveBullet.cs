using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : bullet
{
    [SerializeField] CircleCollider2D explosiveRadius;
    private Boolean exploding;
    [SerializeField] private SpriteRenderer bulletSprite;
    [SerializeField] private GameObject explosionSprite;
    [SerializeField] private float explosiveRange;

    public void Explosion()
    {
        exploding = true;
        bulletSprite.enabled = false;
        explosionSprite.SetActive(true);
        rb.velocity = Vector2.zero;
    }

    private new void FixedUpdate()
    {
        if (exploding)
        {
            explosiveRadius.radius += 0.2f;
            explosionSprite.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            if (explosiveRadius.radius > explosiveRange)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            base.FixedUpdate();
        }
    }
}
