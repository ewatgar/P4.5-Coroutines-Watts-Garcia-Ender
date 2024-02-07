
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Camera cam;
    public GameObject shotCrackPrefab;

    void OnGUI(){ // Método que se invoca para dibujar el interfaz de usuario
        int size = 30; // Tamaño de fuente para dibujar el asterisco
        float posX = cam.pixelWidth/2 - size/4; // Calculamos el punto central
        float posY = cam.pixelHeight/2 - size/2; // de la pantalla.
        GUI.Label(new Rect(posX, posY, size, size), "+"); // Se dibuja un asterisco en esa
    } 

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
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
}
