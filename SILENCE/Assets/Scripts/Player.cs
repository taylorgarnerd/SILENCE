using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [Header("Player")]
    [SerializeField] float shipSpeed = 10f;
    [SerializeField] float tiltAngle = 60.0f;
    [SerializeField] float rotationSpeed = 60f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 100f;
    [SerializeField] float projectileFiringPeriod = 10f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Rigidbody2D shipRb;
    Vector2 leftStickInput;
    Vector2 rightStickInput;
    Vector3 zeroVector = new Vector3(0f, 0f, 0f);
    Vector3 curRotation = new Vector3(0f, 0f, 0f);
    Vector3 projectileVector;

    IEnumerator firingCoroutine;
    bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        shipRb = GetComponent<Rigidbody2D>();
        firingCoroutine = FireContinuously();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();

        //--->
        Vector2 curMovement = leftStickInput * shipSpeed * Time.deltaTime;

        shipRb.MovePosition(shipRb.position + curMovement);

        SetCurrentRotation();

        if (!curRotation.Equals(zeroVector))
        {
            Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);
            //Debug.Log("playerRotation is: " + playerRotation.ToString());
            shipRb.SetRotation(playerRotation);
        }
        //<----

        Fire();
    }

    private void Fire()
    {
        if (!curRotation.Equals(zeroVector) && !isFiring)
        {
            //Debug.Log("Setting isFiring to true and starting coroutine");
            isFiring = true;
            StartCoroutine(firingCoroutine);
        }
        if (curRotation.Equals(zeroVector) && isFiring)
        {
            //Debug.Log("Setting isFiring to false and stopping coroutine");
            isFiring = false;
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
            //Debug.Log("Creating a laser");
            laser.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
            //Play audio example - AudioSource.PlayClipAtPoint(laserAudio, Camera.main.transform.position, laserAudioVolume);
            yield return new WaitForSecondsRealtime(projectileFiringPeriod);
        }
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(Input.GetAxisRaw("LeftStickHorizontal"), -Input.GetAxisRaw("LeftStickVertical"));
        rightStickInput = new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical"));
    }

    private void SetCurrentRotation()
    {
        curRotation = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
        //Debug.Log("curRotation is: " + curRotation.ToString());
    }

    //----Old FixedUpdate call -------//
    //private void FixedUpdate()
    //{
    //    Vector2 curMovement = leftStickInput * shipSpeed * Time.deltaTime;

    //    shipRb.MovePosition(shipRb.position + curMovement);

    //    SetCurrentRotation();

    //    if (!curRotation.Equals(zeroVector))
    //    {
    //        Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);
    //        //Debug.Log("playerRotation is: " + playerRotation.ToString());
    //        shipRb.SetRotation(playerRotation);
    //    }
    //}

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
