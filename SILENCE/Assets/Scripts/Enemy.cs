using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SynchronizerData;
using System;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] Player player;
    [SerializeField] float hitPoints = 10f;

    [Header("Score")]
    [SerializeField] int scoreValue = 100;

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = player.transform.position - transform.position; //I think this gets the direction and distance from one object to another (vector subtraction).
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //Buhhhhh?
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //BUHHHHHH?????

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (hitPoints <= 0)
        {
            UpdateScore();
            Destroy(gameObject);
            //Explosion();
        }
    }

    private void UpdateScore()
    {
        gameSession.AddToScore(scoreValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer || collision.gameObject.tag != "Player") { return; }

        hitPoints -= damageDealer.GetDamage();
        damageDealer.Hit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer || collision.gameObject.tag != "Player") { return; }

        hitPoints -= damageDealer.GetDamage();
        damageDealer.Hit();
    }
}
