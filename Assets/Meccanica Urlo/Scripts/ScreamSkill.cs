using System.Collections;
using UnityEngine;

public class ScreamSkill : MonoBehaviour
{
    public float screamDistanceMax = 5f, waitingTime = 2f, screamSpeed = 2f;
    [SerializeField] private KeyCode screamKey = KeyCode.R;
    [SerializeField] private bool canScream = true;
    [SerializeField] private GameObject screamPrefab, spawnScreamPoint;
    [SerializeField] private string spawnScreamPointName = "ScreamSpawnPoint";

    private Animator animator;

    void Awake()
    {
        spawnScreamPoint = transform.Find(spawnScreamPointName).gameObject;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleScream();
    }

    private void HandleScream()
    {
        if (Input.GetKeyDown(screamKey) && canScream)
        {
            if (animator != null)
                animator.SetTrigger("attack"); // attiva l'animazione

            canScream = false;
            StartCoroutine(ResetCooldown());
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(waitingTime);
        canScream = true;
    }

    // Questa funzione verrà chiamata da un Animation Event durante l'attacco
    public void SpawnScreamPrefab()
    {
        if (screamPrefab != null)
        {
            Instantiate(screamPrefab, spawnScreamPoint.transform.position, transform.rotation);
        }
    }
}
