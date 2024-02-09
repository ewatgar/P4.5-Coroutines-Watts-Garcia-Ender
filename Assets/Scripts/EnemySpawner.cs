using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject playerPrefab;
    private GameObject playerEyes;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy(){
        float rango = Random.Range(0, 360);

        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = transform.position + transform.up*0.01f;
        enemy.transform.localEulerAngles = new Vector3(0, rango, 0);

        playerEyes = playerPrefab.GetComponentInChildren<Camera>().gameObject;

        enemy.GetComponentInChildren<RotadorCabeza>().target = playerEyes;
        enemy.GetComponentInChildren<EnemigoIA>().estado = EnemigoIA.EstadoEnemigo.Parado;
        enemy.transform.SetParent(transform, true);

        yield return new WaitForSeconds(1);

        enemy.GetComponentInChildren<EnemigoIA>().estado = EnemigoIA.EstadoEnemigo.Andando;
        
        yield return null;
    }

    /*private IEnumerator SpawnEnemies(){

    }*/
}
