
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    enum EstadoEnemigo
    {
        Parado = 0,
        Andando = 1
    }

    EstadoEnemigo estado;
    private RotadorExtremidades[] rotadores;
    private CharacterController characterController;
    public float speed = 2;
    //--------------------------------
    private Vector3 origin;
    private Vector3 direction;
    public float sphereRadius = 0.5f;
    public float maxDistance = 0.2f;
    public GameObject DEBUGCurrentHitObject; //para debugear
    public float DEBUGCurrentHitDistance; //para debugear

    //---------------------------------
    float vy = -10;
    public float fallSpeedLimit = 20;
    public float gravity = -10;
    private bool isGrounded = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotadores = GetComponentsInChildren<RotadorExtremidades>();
        IniciarAnimacion();
    }

    void Update()
    {
        Fall();
        origin = transform.position + Vector3.up;
        direction = transform.forward;
        Ray rayo = new Ray(origin,direction);
        
        RaycastHit hit;
        if (Physics.SphereCast(rayo,sphereRadius,out hit,maxDistance)){
            DEBUGCurrentHitObject = hit.transform.gameObject;
            DEBUGCurrentHitDistance = hit.distance;
            
            Vector3 reflexionPared = Vector3.Reflect(transform.forward,hit.normal).normalized; 
            /*
            Debug.DrawLine(rayo.origin, hit.point, Color.red);
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.blue);
            Debug.DrawLine(hit.point, hit.point + reflexionPared, Color.white);*/
            transform.LookAt(transform.position + reflexionPared*2);

        } else{
            DEBUGCurrentHitDistance = maxDistance;
            DEBUGCurrentHitObject = null;
        }

        // --------------------------------------------
        switch (estado)
        {
            case EstadoEnemigo.Andando:
                Debug.Log("Se mueve");
                characterController.Move(transform.forward * speed * Time.deltaTime);
                IniciarAnimacion();
                break;
            case EstadoEnemigo.Parado:
                characterController.Move(Vector3.zero);
                PararAnimacion();
                break;
        }

    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Debug.DrawLine(origin,origin+direction*DEBUGCurrentHitDistance);
        Gizmos.DrawWireSphere(origin+direction*DEBUGCurrentHitDistance,sphereRadius);
    }

    private void IniciarAnimacion()
    {
        estado = EstadoEnemigo.Andando;
        foreach (RotadorExtremidades rotador in rotadores)
        {
            rotador.IniciarAnimacion();
        }
    }

    private void PararAnimacion()
    {
        estado = EstadoEnemigo.Parado;
        foreach (RotadorExtremidades rotador in rotadores)
        {
            rotador.PararAnimacion();
        }
    }

    private void Fall()
    {
        if (!isGrounded && vy > -fallSpeedLimit){
            vy = vy + gravity*Time.deltaTime;
            if (vy < -fallSpeedLimit){
                vy = -fallSpeedLimit;
            }
        }

        characterController.Move(new Vector3(
            0,
            vy,
            0
        ) * speed/2 * Time.deltaTime);

        isGrounded = characterController.isGrounded;
    }
    
}
