using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum AttackMode
    {
        Linear,
        Parabolic
    }
    
    int layerMask;

    //bullet variables
    [SerializeField]
    GameObject bulletPrefab;
    GameObject instaniatedObj;

    public float shootSpeed = 200;

    public BoolVariable startSooting;
    public AttackMode mode;

    public Vector3 target;
    public Transform throwPoint;
    //public GameObject ball;
    public float speed = 1;
    public float timeTillHit = 1f;
    RaycastHit hit;
    void Start()
    {
        
    }

    void Update()
    {
        layerMask = 1 << 8;

        
        if (Physics.Raycast(transform.position, transform.forward, out hit, layerMask))
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
        if (mode==AttackMode.Linear)
        {
            instaniatedObj = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.Euler(0, 0, 90));

            instaniatedObj.GetComponent<Rigidbody>().velocity = transform.forward * shootSpeed;
        }
        else if(mode == AttackMode.Parabolic)
        {
            target = hit.transform.position;
            float xdistance;
            xdistance = target.x - throwPoint.position.x;

            float ydistance;
            ydistance = target.y - throwPoint.position.y;

            float throwAngle;

            throwAngle = Mathf.Atan((ydistance + 4.905f * (timeTillHit * timeTillHit)) / xdistance);

            float totalValue = xdistance / (Mathf.Cos(throwAngle) * timeTillHit);

            float xValue, yValue;
            xValue = totalValue * Mathf.Cos(throwAngle);
            yValue = totalValue * Mathf.Sin(throwAngle);

            GameObject bulletInstance = Instantiate(bulletPrefab, throwPoint.position, 
                Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

            Rigidbody2D rigid;
            rigid = bulletInstance.GetComponent<Rigidbody2D>();
            rigid.velocity = new Vector2(xValue * speed, yValue * speed);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
