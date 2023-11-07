using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] zombiePrefabs;
    [SerializeField] private Transform[] lanes;
    [SerializeField] private float min_ObstacleDelay = 10f, max_ObstacleDelay = 40f;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TextMeshProUGUI final_Score;
    private PlayerHealth playerHealth;
    private float halfGroundSize;
    private BaseController playerController;

    private TextMeshProUGUI score_Text;
    private int zombie_Kill_Count;


    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        playerHealth = playerController.GetComponent<PlayerHealth>();

        StartCoroutine("GenerateObstacles");

        score_Text = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator GenerateObstacles()
    {
        //Hvis playerController ikke findes, break coroutine og giv fejl.
        if (playerController == null)
        {
            Debug.Log("Player Controller er ikke initialiseret");
            yield break; // Stop coroutine, da playerController ikke er initialiseret.
        }
        /*
                //Hvis playerController.speed.z == 0, stop coroutine og lav en ny for at teste om speed er blevet højere
                if (playerController.speed.z == 0)
                {
                    Debug.Log("playerController speed er 0, GenerateObstacles kaldes igen for at checke");
                    StartCoroutine("GenerateObstacles");
                    yield break;
                }
        */

        //Afvent om playerController.speed.z bliver højere end 0, før der spawnes obstacles
        while (playerController.speed.z == 0)
        {
            Debug.Log("playerController.speed.z == 0. Waiting for speed to rise");
        }


        float timer = Random.Range(min_ObstacleDelay, max_ObstacleDelay) / playerController.speed.z;

        Debug.Log("min_ObstacleDelay: " + min_ObstacleDelay);
        Debug.Log("max_ObstacleDelay: " + max_ObstacleDelay);
        Debug.Log("playerController.speed.z: " + playerController.speed.z);
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);
        Debug.Log("Created en Obstacle");

        StartCoroutine("GenerateObstacles");
    }

    private void CreateObstacles(float zPos)
    {
        int r = Random.Range(0, 10);
        Debug.Log("Create Obstacle");

        if (0 <= r && r < 7)
        {
            int obstacleLane = Random.Range(0, lanes.Length);

            AddObstacle(new Vector3(lanes[obstacleLane].transform.position.x, 0f, zPos), Random.Range(0, obstaclePrefabs.Length));

            int zombieLane = 0;

            if (obstacleLane == 0)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2;
            }
            else if (obstacleLane == 1)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2;
            }
            else if (obstacleLane == 2)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0;
            }

            AddZombies(new Vector3(lanes[obstacleLane].transform.position.x, 0.15f, zPos));
        }
    }

    private void AddObstacle(Vector3 position, int type)
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;

        switch (type)
        {
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170, 0f);
                break;
        }
        obstacle.transform.position = position;
    }
    private void AddZombies(Vector3 position)
    {
        int count = Random.Range(0, 3) + 1;

        for (int i = 0; i < count; i++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * i);
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], position + shift * i, Quaternion.identity);
        }
    }

    public void IncreaseScore()
    {
        zombie_Kill_Count++;
        score_Text.text = zombie_Kill_Count.ToString();
    }

    public void ResetScore()
    {
        zombie_Kill_Count = 0;
        score_Text.text = zombie_Kill_Count.ToString();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Gameover()
    {
        Time.timeScale = 0;
        gameoverPanel.SetActive(true);
        final_Score.text = "Killed: " + zombie_Kill_Count.ToString();
    }

    public void RestartGame()
    {
        ResetScore();
        playerHealth.RestartGame();

        Time.timeScale = 1;

        SceneManager.LoadScene("Gameplay");
        gameoverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}
