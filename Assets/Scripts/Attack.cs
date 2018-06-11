using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{


    public GameObject bulletPrefab;
    public float shootSpeed = 300;

    Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootBullet();
        }
    }
    void shootBullet()
    {
        GameObject tempObj;

        tempObj = Instantiate(bulletPrefab) as GameObject;

        tempObj.transform.position = transform.position + cameraTransform.forward;

        Rigidbody projectile = GetComponent<Rigidbody>();

        projectile.velocity = cameraTransform.forward * shootSpeed;
    }
}
//   private Camera camera;
//   void Start () {
//       camera = GetComponent<Camera>();
//   }

//void Update () {
//       //if (Input.GetMouseButtonDown(0))
//       //{

//       //    Vector3 point = new Vector3(camera.pixelWidth/2,camera.pixelHeight/2,0);

//       //    Ray ray = camera.ScreenPointToRay(point);
//       //    RaycastHit hit; 
//       //    if(Physics.Raycast(ray, out hit))
//       //    {
//       //       

//       //    } 
//       //}
//   }