
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float time = 0;
    [SerializeField] GameObject shotCrackPrefab;
    
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 2){
            Destroy(gameObject);
            return;
        }

        // Detectamos si la bala impacta
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f, LayerMask.GetMask("Shootable"))){
            transform.localScale = Vector3.zero;
            EnemigoIA enemigo = hit.transform.gameObject.GetComponentInParent<EnemigoIA>();
            if (enemigo){
                Debug.Log("enemigo dado");
            } else {
                spawnCrack(hit);
            }
        }

        transform.position += transform.forward*40*Time.deltaTime;
    }

    private void spawnCrack(RaycastHit hit){
        GameObject shotCrack = Instantiate(shotCrackPrefab) as GameObject;
        shotCrack.transform.position = hit.point + hit.normal*0.01f;
        shotCrack.transform.LookAt(hit.point - hit.normal);
        shotCrack.transform.SetParent(hit.transform, true);
    }
}
