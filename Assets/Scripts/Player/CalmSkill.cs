using System.Collections;
using UnityEngine;

public class CalmSkill : MonoBehaviour
{

    [SerializeField] private float duration = 2.0f, elapsedTime, reloadTime = 5f;
    [SerializeField] private KeyCode activationkey = KeyCode.T;
    [SerializeField] private PlayerData pd;

    IEnumerator SkillReload()
    {
        yield return new WaitForSeconds(reloadTime);
        elapsedTime = 0;
    }

    private void SkillTrigger()
    {
        if (Input.GetKeyDown(activationkey) && elapsedTime == 0)
        {
            pd.invulnerable = true;
        }
    }

    private void SkillCountdown()
    {
        if (pd.invulnerable)
        {
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime >= duration)
        {
            pd.invulnerable = false;
            //elapsedTime = 0;
            StartCoroutine(SkillReload());
        }
    }

    void Awake()
    {
        pd = GameObject.FindAnyObjectByType<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillTrigger();
        SkillCountdown();
    }
}
