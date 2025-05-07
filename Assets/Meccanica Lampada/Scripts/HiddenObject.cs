using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] private Material originalMaterial, trasparentMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
        // trasparentMaterial = GetComponent<MeshRenderer>().material;
        // Color newColor = trasparentMaterial.color;
        // newColor.a = 0;
        // trasparentMaterial.color = newColor;

        ChangeMaterial(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameObject.FindAnyObjectByType<LampSkill>().localLightOn)
        {
            ChangeMaterial(false);
        }
        GetComponent<Collider>().enabled = GameObject.FindAnyObjectByType<LampSkill>().localLightOn;
    }

    public void ChangeMaterial(bool mode)
    {
        if(!mode)
        {
            GetComponent<MeshRenderer>().material = trasparentMaterial;
            
        }
        else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;
            // GetComponent<MeshCollider>().enabled = true;
        }
    }
}
