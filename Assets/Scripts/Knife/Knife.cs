using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public GameObject projectile;

    private float minDamage = 5;
    private float maxDamage = 7;
    public float projectileForce = 15f;
    private bool offCooldown = true;
    public float cooldown = 1f;
    public float spellLevel = 0;

    //Knife direction and angle
    private Vector2 direction = new Vector2(1f, 0);
    private float angle;

    GameObject spell;

    //Get input
    float inputVertical;
    float inputHorizontal;

    //Speed Limiter
    public float speedLimiter = 0.7f;

    // Update is called once per frame
    void Start()
    {
        //StartCoroutine(ShootEnemy());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && offCooldown)
        {
            StartCoroutine(ShootEnemy());
        }
    }
    IEnumerator startCooldown()
    {
        offCooldown = false;
        yield return new WaitForSeconds(cooldown);
        offCooldown = true;
    }

    //void FixedUpdate()
    //{
    //    if (inputHorizontal != 0 || inputVertical != 0)
    //    {
    //        direction = new Vector2(inputHorizontal, inputVertical);
    //    }
    //}

    IEnumerator ShootEnemy()
    {
        StartCoroutine(startCooldown());
        if (spellLevel > 5) spellLevel = 5;

        //yield return new WaitForSeconds(cooldown);
        switch (spellLevel)
        {
            case 0:
                for (int i = 0; i < 2; ++i)
                {
                    SpawnKnife();
                    yield return new WaitForSeconds(0.06f);
                }
                break;
            case 1:
                for (int i = 0; i < 3; ++i)
                {
                    SpawnKnife();
                    yield return new WaitForSeconds(0.06f);
                }
                break;
            case 2:
                minDamage = 14;
                maxDamage = 17;
                for (int i = 0; i < 4; ++i)
                {
                    SpawnKnife();
                    yield return new WaitForSeconds(0.06f);
                }
                break;
            case 3:
                minDamage = 14;
                maxDamage = 17;
                for (int i = 0; i < 4; ++i)
                {
                    SpawnKnife();
                    yield return new WaitForSeconds(0.06f);
                }
                break;
            case 4:
                minDamage = 14;
                maxDamage = 17;
                for (int i = 0; i < 5; ++i)
                {
                    SpawnKnife();
                    yield return new WaitForSeconds(0.06f);
                }
                break;
            case 5:
                minDamage = 14;
                maxDamage = 17;
                for (int i = 0; i < 5; ++i)
                {
                    SpawnKnife();
                    yield return new WaitForSeconds(0.06f);
                }
                break;
        }

        //StartCoroutine(ShootEnemy());

    }

    private void SpawnKnife()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;

        Vector2 currDirection = direction;
        float rand = Random.Range(-0.4f, 0.4f);
        Vector2 knifePos = new Vector2(transform.position.x, transform.position.y);
        if(currDirection.x != 0)
            knifePos = new Vector2(transform.position.x, transform.position.y + rand);
        else if (currDirection.y != 0)
            knifePos = new Vector2(transform.position.x+ rand, transform.position.y);
        else if (currDirection.x != 0 && currDirection.y != 0)
            knifePos = new Vector2(transform.position.x + rand, transform.position.y + rand);

        spell = Instantiate(projectile, knifePos, Quaternion.identity);
        
        angle = Mathf.Atan2(currDirection.y, currDirection.x) * Mathf.Rad2Deg;
        spell.GetComponent<Rigidbody2D>().rotation = angle;
        spell.GetComponent<Rigidbody2D>().velocity = currDirection * projectileForce;
        Debug.Log(currDirection * projectileForce);
        //Limits projectile speed if it's travelling diagonally
        //if (currDirection.x != 0 && currDirection.y != 0) 
        //    spell.GetComponent<Rigidbody2D>().velocity = projectileForce * speedLimiter * currDirection;
        //else 
        //    spell.GetComponent<Rigidbody2D>().velocity = currDirection * projectileForce;
        spell.GetComponent<ProjectileCollision>().damage = Random.Range(minDamage, maxDamage);
    }
}
