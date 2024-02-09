using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    [Range(4,300)] public float delaySpawnSeconds = 10 ;
    public int maxEnemies = 10;
    private GameObject playerEyes;
    private int numEnemies = 0; 

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy(){
        playerEyes = playerPrefab.GetComponentInChildren<Camera>().gameObject;

        float range = Random.Range(0, 360);
        Quaternion randRotation = Quaternion.Euler(new Vector3(0, range, 0));
        GameObject enemy = Instantiate(enemyPrefab, transform.position, randRotation);
        
        enemy.GetComponentInChildren<RotadorCabeza>().target = playerEyes;
        enemy.GetComponentInChildren<EnemigoIA>().estado = EnemigoIA.EstadoEnemigo.Parado;
        numEnemies++;

        yield return new WaitForSeconds(1);

        enemy.GetComponentInChildren<EnemigoIA>().estado = EnemigoIA.EstadoEnemigo.Andando;
        
        yield return null;
    }

    private IEnumerator SpawnEnemies(){
        while(numEnemies < maxEnemies){
            StartCoroutine(SpawnEnemy());
            yield return new WaitForSeconds(delaySpawnSeconds);
        }
    }
}
