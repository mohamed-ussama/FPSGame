using System.Collections;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{


    float radius = 10.0F;
    public BoolVariable targetCaught;
    public GameObject nearestTarget;

    int layerMask;

    void Start()
    {

        targetCaught.value = false;
    }

    void Update()
    {
        layerMask = 1 << 8;

        NearestTarget();

        

    }


    void NearestTarget()
    {
        if (!targetCaught.value)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

            Debug.Log(colliders.Length);
            if (colliders.Length > 0)
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
                StartCoroutine(FacingEnemy(nearestTarget.transform.position)); 

                
                targetCaught.value = true;
                Debug.Log("caught");

            }

        }
        else
        {
            Debug.Log("released");
        }

    }

    public IEnumerator FacingEnemy(Vector3 enemyTransform)
    {
        Vector3 direction = (enemyTransform - transform.position).normalized;

        var t = 0f;
        float timeToMove = 1;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.forward = Vector3.Lerp(transform.forward, direction, t);
            yield return null;
        }

        Debug.LogError("obj rotated");

    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, 4);

    }
}
