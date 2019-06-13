using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    float rotationSpeed = 180;
	
	// Update is called once per frame
	void Update () {

        //transform.localScale += Vector3.left * Time.deltaTime;
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
    }
}
