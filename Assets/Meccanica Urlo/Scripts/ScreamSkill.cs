using System.Collections;
using UnityEngine;

public class ScreamSkill : MonoBehaviour
{

    public float screamDistanceMax = 5f, waitingTime = 2f, screamSpeed = 2f;
    [SerializeField] private KeyCode screamKey = KeyCode.R;
    [SerializeField] private bool canScream = true;
    [SerializeField] private GameObject screamPrefab, spawnScreamPoint;
    [SerializeField] private string spawnScreamPointName = "ScreamSpawnPoint";

    private IEnumerator SpawnScream()
    {
        if(screamPrefab != null)
        {
            GameObject screamGO = Instantiate(screamPrefab, spawnScreamPoint.transform.position, transform.rotation);
            // screamGO.transform.forward = spawnScreamPoint.transform.forward;
        }
        yield return new WaitForSeconds(waitingTime);
        canScream = true;

    }
    private void HandleScream()
    {
        if(Input.GetKeyDown(screamKey) && canScream)
        {
            StartCoroutine(SpawnScream());
            canScream = false;
        }
    }

    void Awake()
    {
        spawnScreamPoint = transform.Find(spawnScreamPointName).gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleScream();
    }
}
