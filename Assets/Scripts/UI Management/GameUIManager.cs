using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private TMP_Text livesText, moneyText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private LevelManager levelManager;


    void Awake()
    {
        pd = GameObject.FindAnyObjectByType<PlayerData>();
        livesText = GameObject.Find("LivesText").GetComponent<TMP_Text>();
        moneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();

        winPanel = transform.Find("WinPanel").gameObject;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pd.lives >= 0)
        {
            livesText.text = "Lives: " + pd.lives.ToString();
        }
        else
        {
            livesText.text = "DEAD";
        }

        moneyText.text = "$$$$$: " + pd.money.ToString();

        if (levelManager.levelWon)
        {
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LoadSceneIndexed(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


}
