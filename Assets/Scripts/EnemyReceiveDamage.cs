using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReceiveDamage : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public GameObject drop;
    public float dropRate = 0.8f;
    [SerializeField]
    GameObject previousDamageSrc;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashDamage());
        CheckDeath();
    }
    private void OnDestroy()
    {
        EnemySpawner.instance.maxSpawn--;
        SpawnDrop();
    }
    IEnumerator FlashDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    //checkOverheal is not really useful for now
    private void checkOverheal()
    {
        if (health > maxHealth) health = maxHealth;
    }

    private void CheckDeath()
    {
        if(health <= 0)
        {
            StartCoroutine(KillEnemy());
        }
    }
    private void SpawnDrop()
    {
        float rand = Random.Range(0f, 1f);
        if(rand <= dropRate)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
    IEnumerator KillEnemy()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (transform.localScale.x > 0f)
        {
            transform.localScale -= new Vector3(1f, 1f, 1f) * 0.3f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
