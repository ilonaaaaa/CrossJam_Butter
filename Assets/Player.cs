using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Touch touch;
    public float speedLaunch;
    Vector2 startTapAngle;
    public float distanceMin = 100;
    public GameObject butterSlide;

    public Rigidbody rb;

    private AudioSource _butterSlide;

    GameManager gameManager;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _butterSlide = butterSlide.GetComponent<AudioSource>();
    }

    //45 135 225 315 

    void Update()
    {
        //if (canMove == true){}
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                startTapAngle = new Vector2(
                   touch.position.x,
                   touch.position.y);

            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 tapAngle = new Vector2(
                    touch.position.x,
                    touch.position.y);

                Debug.Log("Distance: " + Vector2.Distance(startTapAngle, tapAngle));
                
                if (Vector2.Distance(startTapAngle, tapAngle) < distanceMin)
                {
                    return;
                }

                float angleFinal = Angle((tapAngle - startTapAngle).normalized);
                Debug.Log(angleFinal);


                if (angleFinal > 45 && angleFinal < 135)
                {
                    // Droite
                    rb.velocity = speedLaunch * new Vector3(1, 0, 0);
                    FindObjectOfType<GameManager>().MvtCheck();

                }
                if (angleFinal > 225 && angleFinal < 315)
                {
                    // Gauche
                    rb.velocity = speedLaunch * new Vector3(-1, 0, 0);
                    FindObjectOfType<GameManager>().MvtCheck();
                }

                if (angleFinal >= 315 || angleFinal <= 45)
                {
                    // Haut
                    rb.velocity = speedLaunch * new Vector3(0, 1, 0);
                    FindObjectOfType<GameManager>().MvtCheck();
                }
                if (angleFinal > 135 && angleFinal < 225)
                {
                    // Bas
                    rb.velocity = speedLaunch * new Vector3(0, -1, 0);
                    FindObjectOfType<GameManager>().MvtCheck();
                }
            }

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            Destroy(other.gameObject);

            FindObjectOfType<GameManager>().WinCheck();
            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _butterSlide.Play();
        }
    }

    public static float Angle(Vector2 vector2)
    {
        if (vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
        }
    }

}
