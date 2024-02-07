using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingItem : MonoBehaviour
{
    public enum MetodoAnimacion{
        Lerp,
        LerpSmooth,
        SmoothDamp,
        LerpSmoothStep
    }

    public MetodoAnimacion metodoAnimacion = MetodoAnimacion.LerpSmoothStep;
    MetodoAnimacion? metodoActual = null;
    IEnumerator actual = null;

    public float amplitud = 0.75f;
    public float velocidadAngular = 180;
    public float umbralRebote = 0.15f;
    public float rapidez = 1f;
    Vector3 posInicial;

    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        StartCoroutine(AnimacionGiro());
    }

    // Update is called once per frame
    void Update()
    {
        if (metodoActual!=metodoAnimacion){
            metodoActual = metodoAnimacion;
            if (actual!=null) StopCoroutine(actual);  
            transform.position = posInicial;

            switch(metodoAnimacion){
                case MetodoAnimacion.Lerp:
                    actual = AnimacionBouncingLerp();
                break;
                case MetodoAnimacion.LerpSmooth:
                    actual = AnimacionBouncingLerpSmooth();
                break;
                case MetodoAnimacion.SmoothDamp:
                    actual = AnimacionBouncingSmoothDamp();
                break;
                case MetodoAnimacion.LerpSmoothStep:
                    actual = AnimacionBouncingLerpSmoothStep();  
                break;
            }
            StartCoroutine(actual);    
        }        
    }

    // Coroutine de bouncing lineal sin usar Lerp
    IEnumerator AnimacionBouncing(){
        Vector3 posArriba = posInicial + new Vector3(0,amplitud/2,0);
        Vector3 posAbajo = posInicial - new Vector3(0,amplitud/2,0);
        Vector3 posActual = posInicial;

        int dir = 1;

        // Pausa de dos segundos antes de que comience la animación
        yield return new WaitForSeconds(2);

        while(true){
            if (dir==1 && posActual.y >= posArriba.y || dir==-1 && posActual.y <= posAbajo.y){
                dir = -dir;
            }
            posActual += dir * Vector3.up*Time.deltaTime;
            transform.position = posActual;
            transform.localEulerAngles += Vector3.up*90*Time.deltaTime;

            yield return null;
        }

    }

    // Coroutine de bouncing lineal usando Lerp
    IEnumerator AnimacionBouncingLerp(){
        while(true){
            Vector3 posAbajo = posInicial - new Vector3(0,amplitud/2,0);
            Vector3 posArriba = posInicial + new Vector3(0,amplitud/2,0);

            for(float t=0; t < 1; t+=Time.deltaTime*rapidez ){
                transform.position = Vector3.Lerp(posAbajo, posArriba, t);
                yield return null;
            }
            for(float t=0; t < 1; t+=Time.deltaTime*rapidez ){
                transform.position = Vector3.Lerp(posArriba, posAbajo, t);
                yield return null;
            }
        }

    }
    
    // Coroutine de bouncing suavizado con Lerp, no recomendable porque genera problemas de Overshooting
    IEnumerator AnimacionBouncingLerpSmooth(){
        while(true){
            Vector3 posAbajo = posInicial - new Vector3(0,amplitud/2,0);
            Vector3 posArriba = posInicial + new Vector3(0,amplitud/2,0);

            while(Vector3.Distance(transform.position, posArriba)>umbralRebote){
                transform.position = Vector3.Lerp(transform.position, posArriba, Time.deltaTime*rapidez);
                yield return null;
            }
            while(Vector3.Distance(transform.position, posAbajo)>umbralRebote){
                transform.position = Vector3.Lerp(transform.position, posAbajo, Time.deltaTime*rapidez);
                yield return null;
            }
        }
    }

    // Coroutine de bouncing suavizado con SmoothDamp
    IEnumerator AnimacionBouncingSmoothDamp(){
        Vector3 v = Vector3.zero;
        while(true){
            Vector3 posAbajo = posInicial - new Vector3(0,amplitud/2,0);
            Vector3 posArriba = posInicial + new Vector3(0,amplitud/2,0);

            while(Vector3.Distance(transform.position, posArriba)>umbralRebote){
                transform.position = Vector3.SmoothDamp(transform.position, posArriba, ref v, rapidez/2);
                yield return null;
            }
            while(Vector3.Distance(transform.position, posAbajo)>umbralRebote){
                transform.position = Vector3.SmoothDamp(transform.position, posAbajo, ref v, rapidez/2);
                yield return null;
            }
        }
    }
    // Coroutine de bouncing suavizado con Lerp y SmoothStep
    IEnumerator AnimacionBouncingLerpSmoothStep(){
        while(true){
            Vector3 posAbajo = posInicial - new Vector3(0,amplitud/2,0);
            Vector3 posArriba = posInicial + new Vector3(0,amplitud/2,0);

            for(float t=0; t < 1; t+=Time.deltaTime*rapidez ){
                transform.position = Vector3.Lerp(posAbajo, posArriba, Mathf.SmoothStep(0,1,t));
                yield return null;
            }
            for(float t=0; t < 1; t+=Time.deltaTime*rapidez ){
                transform.position = Vector3.Lerp(posArriba, posAbajo, Mathf.SmoothStep(0,1,t));
                yield return null;
            }
        }
    }
    
    IEnumerator AnimacionGiro(){
        while(true){
            transform.localEulerAngles += Vector3.up*velocidadAngular*Time.deltaTime;
            yield return null;
        }
    }
}
