using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Transform target;
    public float speed = 5f;
    Vector3 direction;

    float timerSelfDestruct = 0;
    float timerSelfDestruct_Max = 5;

    

    // Use this for initialization
    void Start () {

        timerSelfDestruct = Time.time;
        Transform playerT = GameObject.FindGameObjectWithTag("Player").transform;

        if(playerT && playerT != null)
        {
            foreach (Transform child in playerT)
            {
                if (child.name == "Camera Target")
                {
                    print("Achou");
                    target = child;
                    break;
                }
            }
        }
        
        

        Vector3 input = target.position - transform.position;
        
        //normalizamos para pegar apenas a direção



        direction = input.normalized;

        
    }

    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update () {
    
        
        //transform.LookAt(target);
        transform.Translate(direction * speed * Time.deltaTime);

        if(Time.time >= timerSelfDestruct + timerSelfDestruct_Max)
        {
            if(Vector3.Distance(transform.position, target.transform.position) >= 30f)
            {
                Destroy(gameObject);
            }

                timerSelfDestruct = Time.time;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") || 
            other.transform.CompareTag("Obstacle"))
        {

            Destroy(transform.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Obstacle"))
        {

            Destroy(gameObject);
        }


    }

}
