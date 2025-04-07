using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HiddenManager : MonoBehaviour
{

    [SerializeField] private List<HiddenObject> hiddenObjects;

    [SerializeField] private Transform player;



    private void DiscoveryMode()
    {
        //Se non è attiva la modalità di scoperta, allora ritorno.
        if(!player.GetComponent<LampSkill>().localLightOn)
            return;

        LampSkill inputBase = player.GetComponent<LampSkill>();
        //Casto una sfera per capire che oggetti trovo nel raggio del punto luce
        Collider[] colliders = Physics.OverlapSphere(player.position, inputBase.localLightRadius);
        Debug.Log("collider.Count = " + colliders.Length.ToString());
        
        foreach(Collider coll in colliders)
        {
            Debug.Log("Obj: " + coll.gameObject.name);
            if(hiddenObjects.Contains(coll.gameObject.GetComponent<HiddenObject>()))
            {
                Debug.Log("Find element in list");
                HiddenObject element;

                //Trova quel GameObject nella lista
                element = hiddenObjects.Find(obj => obj.gameObject.GetInstanceID() == coll.gameObject.GetInstanceID());
                if(element != null)
                {
                    Debug.Log("element.name = " + element.gameObject.name);
                }
                //Attiva collider e ripristina materiale.
                element.ChangeMaterial(true);
            }
        }


    }

    void Awake()
    {
        //Trovo gli oggetti da nascondere e li metto in una lista
        hiddenObjects = GameObject.FindObjectsByType<HiddenObject>(FindObjectsSortMode.None).ToList<HiddenObject>();
        
        //Trovo il player gameobject
        player = GameObject.Find("Player").transform;
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
