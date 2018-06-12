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
    List<GameObject> bulletList;
    void Start()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < 18; i++)
        {
            GameObject bulletObj = (GameObject)Instantiate(bulletPrefab);
            bulletObj.SetActive(false);
            bulletList.Add(bulletObj);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            mode = AttackMode.Linear;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            mode = AttackMode.Parabolic;
        }
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
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].transform.position = transform.position + transform.forward;
                bulletList[i].transform.rotation = Quaternion.Euler(0, 0, 90);
                bulletList[i].SetActive(true);
                Rigidbody bulletrig = bulletList[i].GetComponent<Rigidbody>();
                
                if (mode == AttackMode.Linear)
                {

                    bulletrig.velocity = transform.forward * shootSpeed;
                }
                else if (mode == AttackMode.Parabolic)
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

                    bulletrig.velocity = new Vector3(xValue * speed, yValue * speed);

                    
                }
                break;
            }
        }
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
