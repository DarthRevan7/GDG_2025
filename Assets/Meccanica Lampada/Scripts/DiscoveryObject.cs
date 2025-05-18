using UnityEngine;

//Puoi passare attraverso questo oggetto se hai la LightSkill Attiva.
public class DiscoveryObject : MonoBehaviour
{
    [SerializeField] private Material trasparentMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private float transparencyIndex = 0.55f;
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;

        player = GameObject.FindGameObjectWithTag("Player");
        //Creo un materiale trasparente ad hoc.
        trasparentMaterial = new Material(originalMaterial);
        Color oldCol = originalMaterial.color;
        Color newColor = new Color(oldCol.r, oldCol.g, oldCol.b, transparencyIndex);
        trasparentMaterial.SetColor("_BaseColor", newColor);
        trasparentMaterial.SetOverrideTag("_SurfaceType", "Transparent");

        ChangeMaterial(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Se il player non ha la skill attiva
        if (!player.GetComponent<LampSkill>().localLightOn)
        {
            //Setta il materiale visibile
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
