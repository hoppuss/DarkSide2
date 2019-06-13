using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


    float yaw;
    float pitch;
    [SerializeField]
    float mouseSensitivity = 3f;
    [SerializeField]
    float distanceFromTargetX;
    [SerializeField]
    float distanceFromTargetY;

    [SerializeField]
    float pitchMin = -40;
    [SerializeField]
    float pitchMax = 80;

    [SerializeField]
    bool lockCursor;

    Vector3 currentRotation;
    [SerializeField]
    float rotationSmoothTime = 0.12f;
    [SerializeField]
    Vector3 rotationSmoothVelocity;

    [SerializeField]
    Transform target; // rotacionar em volta de um alvo, no caso o player

    [SerializeField]
    LayerMask cameraLayerMask;

    // Use this for initialization
    void Start () {

        if(lockCursor)
        {
            //trava o cursor no centro da tela
            Cursor.lockState = CursorLockMode.Locked;
            // deixa o cursor invisivel
            Cursor.visible = false;
        }

       // transform.eulerAngles = target.eulerAngles;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        
        // input do mouse
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        //limita a rotacao no y
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        Vector3 targetRotation = new Vector3(pitch, yaw);

        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
        //fazemos a rotação
        transform.eulerAngles = currentRotation;

        // colocamos a camera numa posicao logo atras do jogador
        transform.position = target.position - transform.forward * distanceFromTargetX + transform.up * distanceFromTargetY;

        CheckWall();


	}

    void CheckWall()
    {
        RaycastHit hit;
        // inicio do raio
        Vector3 raystart = target.position;
        //direção da posição
        Vector3 dir = (transform.position - target.position).normalized;

        //distancia do raio ( que é a distancia do player até a camera
        float dist = Vector3.Distance(transform.position, target.position);

        if(Physics.Raycast(raystart,dir, out hit, dist, cameraLayerMask))
        {

            float hitDistance = hit.distance;

            Vector3 castCenterHit = target.position + (dir.normalized * hitDistance);

            transform.position = castCenterHit;
        }

    }
}
