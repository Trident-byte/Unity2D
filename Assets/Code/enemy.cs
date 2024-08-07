using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            player.curPlayer.Hit();
            SpawnDamageParticles(other.gameObject.transform.eulerAngles);
            Destroy(this.gameObject);
            target = null;
            RoundUI.roundUI.updateEnemies();
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Hit(other.gameObject);
            RoundUI.roundUI.updateEnemies();
        }
    }

    public void Hit(GameObject attacker)
    {
        LevelManager.manager.IncreaaseScore(1);
        SpawnDamageParticles(attacker.transform.eulerAngles);
        Destroy(attacker);
        int randInt = UnityEngine.Random.Range(0, 100);
        if (randInt < 2)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        else if (randInt < 50)
        {
            Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected void SpawnDamageParticles(Vector3 bulletAngle)
    {
        Quaternion angle = Quaternion.Euler(0, 0, bulletAngle.z + 90);
        damageParticlesInstance = Instantiate(damageParticles, transform.position, angle);
    }
}
