using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject miPrefab;

    [SerializeField]
    private GameObject player;

    float tiempo = 0;
    float momentoSiguienteEnemigo = 1;
    int numInstancias = 0;

    public int enemyCount = 10;
    public float spawnAreaWidth = 20;
    public float spawnAreaHeight = 26;
    public float spawnTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= momentoSiguienteEnemigo && numInstancias<enemyCount){
            EnemigoIA nuevoObjeto = Instantiate(
                miPrefab,
                position: transform.position + new Vector3(
                    Random.Range(-spawnAreaWidth/2, spawnAreaWidth/2),
                    0,
                    Random.Range(-spawnAreaHeight/2, spawnAreaHeight/2)
                ),
                rotation: Quaternion.Euler(new Vector3(0, Random.Range(0,360), 0))
            ).GetComponent<EnemigoIA>();

            nuevoObjeto.SetTarget(player);
            momentoSiguienteEnemigo += spawnTime;
            numInstancias++;
        }
    }
}
