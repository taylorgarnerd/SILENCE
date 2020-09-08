using SynchronizerData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] GameObject forceFieldPrefab;
    [SerializeField] float forceFieldActivateTime = 1f;

    private BeatObserver beatObserver;
    private SpriteRenderer enemyBody;
    private GameObject enemyForceField;

    // Start is called before the first frame update
    void Start()
    {
        beatObserver = GetComponent<BeatObserver>();
        enemyBody = GetComponentInChildren<SpriteRenderer>();
        enemyForceField = Instantiate(forceFieldPrefab, enemyBody.transform.position, enemyBody.transform.rotation) as GameObject;
        enemyForceField.transform.parent = gameObject.transform;
        enemyForceField.SetActive(false);
    }

    //Update is called once per frame
    void Update()
    {
        if ((beatObserver.beatMask & BeatType.UpBeat) == BeatType.UpBeat)
        {
            //Debug.Log("Beat check matched. Activating forcefield...");
            StartCoroutine(ActivateForceField());
        }
    }

    private IEnumerator ActivateForceField()
    {
        //Debug.Log("Inside force field coroutine");
        enemyForceField.SetActive(true);
        yield return new WaitForSeconds(forceFieldActivateTime);
        enemyForceField.SetActive(false);
    }

    //private void Fire()
    //{
    //    GameObject enemyLaser = Instantiate(laserPrefab, enemyBody.transform.position, enemyBody.transform.rotation) as GameObject;
    //    enemyLaser.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
    //}
}
