using System;
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
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private AudioSource laserAudio;
    [SerializeField] private AudioSource walking;
    [SerializeField] private float dashSpeed = 5;
    [SerializeField] private GameObject playerUI;
    private Boolean canDash;
    public float curHealth = 0;
    public float coins = 0;

    private float mx;
    private float my;
    private Rigidbody2D rb;

    private float fireTimer;
    private float dashTimer;
    private Vector2 mosPos;
    [SerializeField] protected ParticleSystem damageParticles;
    [SerializeField] protected ParticleSystem dashParticles;
    protected ParticleSystem damageParticlesInstance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireRate;
        curPlayer = this;
        curHealth = maxHealth;
        canDash = false;
        coins = 20;
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxis("Horizontal");
        my = Input.GetAxis("Vertical");
        mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mosPos.y - transform.position.y, mosPos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && dashTimer <= 0f && canDash)
        {
            Dash();
            dashTimer = dashCooldown;
            DashBar.dashbar.changeBar(dashCooldown, Mathf.Min(dashCooldown - dashTimer, dashCooldown));
        }
        else if (canDash)
        {
            dashTimer -= Time.deltaTime;
            DashBar.dashbar.changeBar(dashCooldown, Mathf.Min(dashCooldown - dashTimer, dashCooldown));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx, my).normalized * speed;
        if (rb.velocity != Vector2.zero && !walking.isPlaying)
        {
            walking.Play();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        laserAudio.Play();

    }

    private void Dash()
    {
        Vector3 dashVector = new Vector3(mx, my, 0).normalized;
        Instantiate(dashParticles, transform.position, Quaternion.Euler(dashVector.x, dashVector.y, dashVector.z));
        transform.position += new Vector3(mx, my, 0).normalized * dashSpeed;
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
        if (curHealth < 1)
        {
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

    public void activateDash()
    {
        canDash = true;
        PlayerUI playerUIScript = playerUI.GetComponent<PlayerUI>();
        playerUIScript.ActivateBar();
    }

    protected void SpawnDamageParticles(Vector3 bulletAngle)
    {
        Quaternion angle = Quaternion.Euler(0, 0, bulletAngle.z + 180);
        damageParticlesInstance = Instantiate(damageParticles, transform.position, angle);
    }

    public void setSpeed(double multiplier)
    {
        speed = Mathf.Max((int)(speed * multiplier), 10);
    }

    public void increaseFireRate(double multiplier)
    {
        fireRate = Mathf.Min((float)(fireRate * multiplier), (float)0.1);
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

    public void switchBulletType(GameObject newBullet)
    {
        bulletPrefab = newBullet;
    }
}
