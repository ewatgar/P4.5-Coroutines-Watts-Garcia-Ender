
using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject shotCrackPrefab;
    GameObject cannonPoint;
    Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        cannonPoint = searchCannon();
    }

    void OnGUI(){
        int size = 12;
        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            spawnBullet();
        }
    }

    private GameObject searchCannon()
    {
        GameObject cannon = null;
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach(Transform child in children){
            if(child.name == "Cannon"){
                cannon = child.gameObject;
            }

        }
        return cannon;
    }

    private void spawnBullet(){
        Instantiate(
            bulletPrefab,
            position: cannonPoint.transform.position,
            rotation: cannonPoint.transform.rotation
        );
    }
}
