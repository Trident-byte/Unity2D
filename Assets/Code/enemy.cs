using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform target;
    [SerializeField] protected GameObject healthPrefab;
    [SerializeField] protected GameObject moneyPrefab;
    [SerializeField] protected ParticleSystem damageParticles;
    protected ParticleSystem damageParticlesInstance;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Get the target
        if (!target)
        {
            GetTarget();
        }
        //Rotate to target
        else
        {
            RotateTowardsTarget();
        }

    }

    private void FixedUpdate()
    {
        //Move fowards
        rb.velocity = transform.up * speed;
    }

    protected void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected void RotateTowardsTarget()
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.curPlayer.hit();
            SpawnDamageParticles(other.gameObject.transform.eulerAngles);
            Destroy(this.gameObject);
            target = null;
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            LevelManager.manager.IncreaaseScore(1);
            SpawnDamageParticles(other.gameObject.transform.eulerAngles);
            Debug.Log(other.gameObject.transform.rotation);
            Destroy(other.gameObject);
            int randInt = UnityEngine.Random.Range(0, 100);
            if (randInt < 2)
            {
                Instantiate(healthPrefab, transform.position, Quaternion.identity);
            }
            else if (randInt < 12)
            {
                Instantiate(moneyPrefab, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    protected void SpawnDamageParticles(Vector3 bulletAngle)
    {
        Quaternion angle = Quaternion.Euler(0, 0, bulletAngle.z + 90);
        Debug.Log(angle);
        damageParticlesInstance = Instantiate(damageParticles, transform.position, angle);
    }
}
