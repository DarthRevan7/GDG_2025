using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private List<CollectableObject> money;
    public bool levelWon = false;
    [SerializeField] private int moneyNeeded;


    void Awake()
    {
        pd = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        CollectableObject[] collectables = GameObject.FindObjectsByType<CollectableObject>(FindObjectsSortMode.None);
        money = new List<CollectableObject>();

        foreach (CollectableObject obj in collectables)
        {
            if (obj.objectType == CollectableObject.ObjectType.MONEY)
            {
                money.Add(obj);
            }
        }

        moneyNeeded = money.Count;

    }

    // Update is called once per frame
    void Update()
    {
        levelWon = pd.money == moneyNeeded;
    }
}
