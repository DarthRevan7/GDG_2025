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
        if (!player.GetComponent<LampSkill>().localLightOn)
            return;

        LampSkill inputBase = player.GetComponent<LampSkill>();
        //Casto una sfera per capire che oggetti trovo nel raggio del punto luce
        Collider[] colliders = Physics.OverlapSphere(player.position, inputBase.localLightRadius);
        // Debug.Log("collider.Count = " + colliders.Length.ToString());

        foreach (Collider coll in colliders)
        {

            // Debug.Log("Obj: " + coll.gameObject.name);
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
                //Attiva collider e ripristina materiale.
                element.ChangeMaterial(false);
            }
        }


    }

    void Awake()
    {
        //Trovo gli oggetti da nascondere e li metto in una lista
        discoveryObjects = FindObjectsByType<DiscoveryObject>(FindObjectsSortMode.None).ToList<DiscoveryObject>();

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
    


    private void OnDrawGizmos()
{
    // Assicurati che il player e la skill esistano prima di provare a disegnare
    if (player == null || player.GetComponent<LampSkill>() == null)
    {
        return;
    }

    LampSkill inputBase = player.GetComponent<LampSkill>();

    // Disegna la sfera SOLO se la luce è accesa (opzionale, ma utile)
    if (inputBase.localLightOn)
    {
        // Imposta il colore dei Gizmos che disegnerai subito dopo
        Gizmos.color = Color.yellow;

        // Disegna una sfera vuota (wireframe) alla posizione del player con il raggio della skill
        Gizmos.DrawWireSphere(player.position, inputBase.localLightRadius);

        // Opzionale: Disegna anche i raggi ai collider rilevati nell'ultimo frame di Update
        // (Questo richiede di memorizzare i collider trovati nell'Update,
        // o di rifare un piccolo OverlapSphere qui, che però non è ideale in OnDrawGizmos)
        // Per semplicità, concentrati prima sulla sfera stessa.
    }
}
}
