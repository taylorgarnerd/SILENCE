using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SynchronizerData;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 100f;
    [SerializeField] float projectileFiringPeriod = 10f;
    [SerializeField] Player player;

    private BeatObserver beatObserver;
    private SpriteRenderer enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        beatObserver = GetComponent<BeatObserver>();
        enemyBody = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)
        {
            Fire();
        }

        var dir = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(laserPrefab, enemyBody.transform.position, enemyBody.transform.rotation) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileSpeed);
    }
}
