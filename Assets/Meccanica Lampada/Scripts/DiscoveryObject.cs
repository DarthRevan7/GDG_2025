using UnityEngine;

//Puoi passare attraverso questo oggetto se hai la LightSkill Attiva.
public class DiscoveryObject : MonoBehaviour
{
    [SerializeField] private Material trasparentMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private float transparencyIndex = 0.55f;
    [SerializeField] private GameObject player;
    

    void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
        
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
        if (!GameObject.FindAnyObjectByType<LampSkill>().localLightOn)
        {
            ChangeMaterial(true);
        }
        // GetComponent<Collider>().enabled = !GameObject.FindAnyObjectByType<LampSkill>().localLightOn;
    }

    //Cambia materiale all'oggetto in modo che sia visibile o invisibile a seconda del booleano
    public void ChangeMaterial(bool mode)
    {
        if (!mode)
        {
            GetComponent<MeshRenderer>().material = trasparentMaterial;
            GetComponent<Collider>().enabled = false;

        }
        else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
            GetComponent<Collider>().enabled = true;
        }
    }
}
