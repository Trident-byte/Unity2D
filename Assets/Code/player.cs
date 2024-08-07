using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private int speed = 10;
    public static player curPlayer;

    //Gun vars
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float maxHealth = 10;
    public float curHealth = 0;
    public float coins = 0;

    private float mx;
    private float my;
    private Rigidbody2D rb;

    private float fireTimer;
    private Vector2 mosPos;
    [SerializeField] protected ParticleSystem damageParticles;
    protected ParticleSystem damageParticlesInstance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireRate;
        curPlayer = this;
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxis("Horizontal");
        my = Input.GetAxis("Vertical");
        mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mosPos.y - transform.position.y, mosPos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * speed;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Hit();
            SpawnDamageParticles(other.gameObject.transform.eulerAngles);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Health"))
        {
            curHealth = Mathf.Min(10, curHealth + 1);
            Destroy(other.gameObject);
            HealthBar.healthBar.changeBar(maxHealth, curHealth);
        }

        else if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            CoinText.coinText.updateCounter(coins);

        }
    }

    public void Hit()
    {
        curHealth--;
        // Debug.Log(curHealth);
        if (curHealth < 1)
        {
            Debug.Log("ending");
            LevelManager.manager.GameOver();
        }
        HealthBar.healthBar.changeBar(maxHealth, curHealth);
    }

    public bool GetPowerUP(GameObject powerUP, float cost)
    {
        if (coins > cost)
        {
            GameObject newPower = Instantiate(powerUP, transform.position, Quaternion.identity);
            newPower.transform.SetParent(transform);
            Spending(cost);
            return true;
        }
        return false;
    }

    protected void SpawnDamageParticles(Vector3 bulletAngle)
    {
        Quaternion angle = Quaternion.Euler(0, 0, bulletAngle.z + 90);
        damageParticlesInstance = Instantiate(damageParticles, transform.position, angle);
    }

    public void setSpeed(double multiplier)
    {
        speed = (int)(speed * multiplier);
    }

    public void Spending(float cost)
    {
        coins -= cost;
        CoinText.coinText.updateCounter(coins);
    }

    public float ReturnMoney()
    {
        return coins;
    }
}
