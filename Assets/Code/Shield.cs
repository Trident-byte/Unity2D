using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hello");
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            RoundUI.roundUI.updateEnemies();
        }
    }
}
