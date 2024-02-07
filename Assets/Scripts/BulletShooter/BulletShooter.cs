
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
            //spawnCrack();
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

    private void spawnCrack(){
        Vector3 p = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(p);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Shootable"))){
                GameObject shotCrack = Instantiate(shotCrackPrefab) as GameObject;
                shotCrack.transform.position = hit.point + hit.normal*0.01f;
                shotCrack.transform.LookAt(hit.point - hit.normal);
                shotCrack.transform.SetParent(hit.transform, true);
            }
    }
}
