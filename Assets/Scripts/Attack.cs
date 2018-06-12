using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    //public Camera camera;
    //Vector3 point;
    int layerMask;

    //bullet variables
    [SerializeField]
    GameObject bulletPrefab;
    GameObject instaniatedObj;

    public float shootSpeed = 200;
    Transform cameraTransform;

    public BoolVariable startSooting;
    void Start()
    {
        //camera = Camera.main;
        //cameraTransform = Camera.main.transform;
        //point = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
    }

    void Update()
    {
        layerMask = 1 << 8;
        //Ray ray = camera.ScreenPointToRay(point);
        //RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, layerMask))
        {
           
            if (startSooting.value==true)
            {
                ShootBullet();
            }
            else
            {

            }
        }
    }
    void ShootBullet()
    {
        
        instaniatedObj = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.Euler(0,0,90)) ;
        
        instaniatedObj.GetComponent<Rigidbody>().velocity = transform.forward * shootSpeed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
