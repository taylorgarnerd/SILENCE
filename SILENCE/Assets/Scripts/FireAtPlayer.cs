using SynchronizerData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 5f;
    //[SerializeField] float projectileFiringPeriod = 10f;

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
        if ((beatObserver.beatMask & BeatType.UpBeat) == BeatType.UpBeat)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(laserPrefab, enemyBody.transform.position, enemyBody.transform.rotation) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
    }
}
