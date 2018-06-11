using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    Transform[] spawnPoints;
    
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject player;

    GameObject enemy;

    public BoolVariable targetCaught;
    void Start () {
        StartCoroutine(EnemySpawnRandom());
        targetCaught.value = false;

    }

    private IEnumerator EnemySpawnRandom()
    {

        
        while (true)
        {
            
            enemy=Instantiate(prefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            enemy.transform.LookAt(player.transform);
            Destroy(enemy, 3);
            yield return new WaitForSeconds(2);
            targetCaught.value = false;
        }
        
    }
}
