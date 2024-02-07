
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float time = 0;
    
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
        if (Physics.Raycast(ray, out hit, 0.5f, LayerMask.GetMask("Walls","Enemy"))){
            transform.localScale = Vector3.zero;
            EnemigoIA enemigo = hit.transform.gameObject.GetComponentInParent<EnemigoIA>();
            if (enemigo){
                Debug.Log("enemigo dado");
            }
        }

        transform.position += transform.forward*40*Time.deltaTime;
    }
}
