using UnityEngine;

public class DiscoveryObject : MonoBehaviour
{
    [SerializeField] private Material trasparentMaterial;
    [SerializeField] private Material originalMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        ChangeMaterial(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Se il player non ha la skill attiva
        if (!GameObject.FindAnyObjectByType<LampSkill>().localLightOn)
        {
            //Cambia il materiale in invisibile
            ChangeMaterial(false);
        }
        //Il collider è attivo solo se la skill è disabilitata
        GetComponent<Collider>().enabled = !GameObject.FindAnyObjectByType<LampSkill>().localLightOn;
    }
    
    //Cambia materiale all'oggetto in modo che sia visibile o invisibile a seconda del booleano
    public void ChangeMaterial(bool mode)
    {
        if (mode)
        {
            GetComponent<MeshRenderer>().material = trasparentMaterial;

        }
        else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }
}
