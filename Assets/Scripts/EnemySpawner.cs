using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject zombie;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy(){


        yield return null;
    }

    private void spawnZombie(){
        zombie = Instantiate(zombie) as GameObject;
        zombie.transform.position = transform.position + transform.up*0.01f;
        zombie.transform.LookAt(hit.point - hit.normal);
        zombie.transform.SetParent(transform, true);
    }
}
