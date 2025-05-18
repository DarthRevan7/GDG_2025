using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DiscoveryManager : MonoBehaviour
{

    [SerializeField] private List<DiscoveryObject> discoveryObjects;

    [SerializeField] private Transform player;



    private void DiscoveryMode()
    {
        //Se non è attiva la modalità di scoperta, allora ritorno.
        if(!player.GetComponent<LampSkill>().localLightOn)
            return;

        LampSkill inputBase = player.GetComponent<LampSkill>();
        //Casto una sfera per capire che oggetti trovo nel raggio del punto luce
        Collider[] colliders = Physics.OverlapSphere(player.position, inputBase.localLightRadius);
        // Debug.Log("collider.Count = " + colliders.Length.ToString());
        
        foreach(Collider coll in colliders)
        {
            Debug.Log("Foreach colliders: " + colliders.Length.ToString());
            if (discoveryObjects.Contains(coll.gameObject.GetComponent<DiscoveryObject>()))
            {
                Debug.Log("Find element in list");
                DiscoveryObject element;

                //Trova quel GameObject nella lista
                element = discoveryObjects.Find(obj => obj.gameObject.GetInstanceID() == coll.gameObject.GetInstanceID());
                if (element != null)
                {
                    Debug.Log("element.name = " + element.gameObject.name);
                }
                //Disattiva collider e ripristina materiale.
                element.ChangeMaterial(true);
            }
        }
    }

    void Awake()
    {
        //Trovo gli oggetti da nascondere e li metto in una lista
        discoveryObjects = GameObject.FindObjectsByType<DiscoveryObject>(FindObjectsSortMode.None).ToList();
        
        //Trovo il player gameobject
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DiscoveryMode();
    }
}
