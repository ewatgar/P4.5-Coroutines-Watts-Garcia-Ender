
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    [SerializeField]
    GameObject bulletPrefab, cannonPoint;
    Camera cam;

    void Start()
    {
        cam = GetComponentInParent<Camera>();
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
            Instantiate(
                bulletPrefab,
                position: cannonPoint.transform.position,
                rotation: cannonPoint.transform.rotation
            ); 
        }
    }
}
