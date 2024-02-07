
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum FadeMethod{
        Lerp,
        ShoothDamp
    }

    float time = 0;
    [SerializeField] GameObject shotCrackPrefab;
    GameObject shotCrack;
    public float crackSeconds = 1;
    bool shotCrackPlaced = false;
    public FadeMethod fadeMethod = FadeMethod.Lerp;
    public float fadeTime = 2f;
    
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
            //bulletHit = hit;
            transform.localScale = Vector3.zero;
            EnemigoIA enemigo = hit.transform.gameObject.GetComponentInParent<EnemigoIA>();
            if (enemigo){
                Debug.Log("enemigo dado");
            } else {
                StartCoroutine(PlaceShotMark(hit));
            }
        }

        transform.position += transform.forward*40*Time.deltaTime;
    }

    private void spawnCrack(RaycastHit hit){
        shotCrack = Instantiate(shotCrackPrefab) as GameObject;
        shotCrack.transform.position = hit.point + hit.normal*0.01f;
        shotCrack.transform.LookAt(hit.point - hit.normal);
        shotCrack.transform.SetParent(hit.transform, true);
    }

    private IEnumerator PlaceShotMark(RaycastHit hit){
        // Condición para que no se creen varios cracks a la vez en el mismo frame
        if (!shotCrackPlaced){
            spawnCrack(hit);
            shotCrackPlaced = true;
        }
        yield return new WaitForSeconds(crackSeconds);
        
        Material m = shotCrack.GetComponent<Renderer>().material;
        Color alphaColor = new Color(m.color.r, m.color.g, m.color.b, m.color.a);
        
        switch(fadeMethod){
            case FadeMethod.Lerp:
                for(float s=0; s<fadeTime; s+=Time.deltaTime){
                    alphaColor.a = Mathf.Lerp(1, 0, s/fadeTime);
                    m.color = alphaColor;
                    yield return null;
                }

            break;
            case FadeMethod.ShoothDamp:
                float v = 0;
                for(float s=0; s<fadeTime; s+=Time.deltaTime){
                    //alphaColor.a = Mathf.Lerp(1, 0, s/fadeTime);
                    alphaColor.a = Mathf.SmoothDamp(1, 0, ref v, s/fadeTime);
                    m.color = alphaColor;
                    yield return null;
                }

            break;
        }
        
        Destroy(shotCrack);
        shotCrackPlaced = false;
    }

}
