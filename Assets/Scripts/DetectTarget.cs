using System.Collections;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{


    float radius = 10.0F;
    public BoolVariable startSearching;
    GameObject nearestTarget;
    public BoolVariable startShooting;
    int layerMask;

    void Start()
    {
        startShooting.value = false;
        startSearching.value = true;
    }

    void Update()
    {
        layerMask = 1 << 8;

        NearestTarget();

 
    }

    void NearestTarget()
    {
        if (startSearching.value)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

            Debug.Log(colliders.Length);
            if (colliders.Length > 1)
            {
                nearestTarget = colliders[0].gameObject;

                for (int i = 0; i < colliders.Length - 1; i++)
                {
                    if (Vector3.Distance(transform.position, colliders[i].gameObject.transform.position)
                        > Vector3.Distance(transform.position, colliders[i + 1].gameObject.transform.position))
                    {
                        nearestTarget.transform.position = colliders[i + 1].gameObject.transform.position;
                    }
                }
                FacingEnemy(nearestTarget.transform.position); 

            }
            else if(colliders.Length == 1)
            {
                nearestTarget = colliders[0].gameObject;
                FacingEnemy(nearestTarget.transform.position);
            }
            else
            {
                
            }

        }
        else
        {
        }

    }

    public void FacingEnemy(Vector3 enemyTransform)
    {
        Vector3 direction = (enemyTransform - transform.position).normalized;
        var t = 0f;
        float timeToMove = 1f;
        while (Vector3.Dot(transform.forward,direction)!=1)
        {
            t += Time.deltaTime / timeToMove;
            transform.forward = Vector3.Lerp(transform.forward, direction, t);
        }

        startShooting.value = true;
        startSearching.value = false;
        
    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, 4);

    }
}
