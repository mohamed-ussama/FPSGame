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

    
    void Start () {
        StartCoroutine(EnemySpawnRandom());
        

    }

    private IEnumerator EnemySpawnRandom()
    {

        
        while (true)
        {
            
            enemy=Instantiate(prefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            enemy.transform.LookAt(player.transform);
            yield return new WaitForSeconds(1);
            
        }
        
    }
}
