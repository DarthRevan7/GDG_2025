using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private TMP_Text livesText, moneyText;

    void Awake()
    {
        pd = GameObject.FindAnyObjectByType<PlayerData>();
        livesText = GameObject.Find("LivesText").GetComponent<TMP_Text>();
        moneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(pd.lives >= 0) {
            livesText.text = "Lives: " + pd.lives.ToString();
        } else {
            livesText.text = "DEAD";
        }

        moneyText.text = "$$$$$: " + pd.money.ToString();
    }


}
