using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRaycasting : MonoBehaviour
{
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(r, out hit)){
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.blue);

            Vector3 reflejo = Vector3.Reflect(transform.forward, hit.normal);
            
            Debug.DrawRay(hit.point, reflejo, Color.yellow);
        }

    }
}
