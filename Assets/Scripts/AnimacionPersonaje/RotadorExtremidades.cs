using System;
using Unity.VisualScripting;
using UnityEngine;

public class RotadorExtremidades : MonoBehaviour
{
    public float angMinimo = -30;
    public float angMaximo = 30;
    public float vAngular = 150;
    public float direccion = 1;
    private float anguloTotal = 0;
    private Boolean isWalking = false;

    private Vector3 ultimaPosicion;

    void Start()
    {
        ultimaPosicion = transform.position;
    }


    void Update()
    {
        string nameRoot = transform.root.name;
        switch (nameRoot){
            case "Player":
                //Quiero que Steve ejecutela animación sólo cuando se mueve y no cuando rota también su cámara
                isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
                break;
            case "Zombie":
                isWalking = transform.position != ultimaPosicion;
                break;
        }

        if (isWalking){
            IniciarAnimacion();
        } else{
            PararAnimacion();
        }
        ultimaPosicion = transform.position;
    }

    public void IniciarAnimacion()
    {
        if (anguloTotal >= angMaximo || anguloTotal <= angMinimo){
            direccion *= -1;
            anguloTotal = Mathf.Clamp(anguloTotal,angMinimo,angMaximo);
        }

        float angulo = vAngular * Time.deltaTime;
        anguloTotal += direccion*angulo;

        transform.localEulerAngles = new Vector3(anguloTotal,0,0);
        Debug.Log("Se inicia animación");
    }

    public void PararAnimacion(){
        anguloTotal = 0;
        transform.localEulerAngles = Vector3.zero;
    }
}
