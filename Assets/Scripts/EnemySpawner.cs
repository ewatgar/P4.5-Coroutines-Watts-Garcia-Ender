using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    [Range(4,300)] public float delaySpawnSeconds = 10 ;
    public int maxEnemies = 10;
    public int spawnAnimationSeconds = 3;
    private GameObject playerEyes;
    private int numEnemies = 0; 
    private Vector3 spawnLocation;

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

    private IEnumerator SpawnEnemyV2(){
        spawnLocation = transform.position;
        playerEyes = playerPrefab.GetComponentInChildren<Camera>().gameObject;

        float range = Random.Range(0, 360);
        Quaternion randRotation = Quaternion.Euler(new Vector3(0, range, 0));
        Vector3 underFloor = transform.position;
        underFloor.y = -1; //para que aparezca debajo del suelo
        spawnLocation.y += 0.1f; //para que el zombie salga algo más arriba del suelo
        GameObject enemy = Instantiate(enemyPrefab, underFloor, randRotation);
        enemy.GetComponentInChildren<RotadorCabeza>().target = playerEyes;
        enemy.GetComponentInChildren<EnemigoIA>().estado = EnemigoIA.EstadoEnemigo.Parado;
        numEnemies++;

        float currentSeconds = 0;

        while (currentSeconds < spawnAnimationSeconds){
            currentSeconds += Time.deltaTime;
            enemy.transform.position = Vector3.Lerp(underFloor, spawnLocation, currentSeconds / spawnAnimationSeconds);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        enemy.GetComponentInChildren<EnemigoIA>().estado = EnemigoIA.EstadoEnemigo.Andando;
        
        yield return null;

    }

    private IEnumerator SpawnEnemies(){
        while(numEnemies < maxEnemies){
            StartCoroutine(SpawnEnemyV2());
            yield return new WaitForSeconds(delaySpawnSeconds);
        }
    }
}
