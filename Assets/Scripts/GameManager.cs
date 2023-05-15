using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject bird;
    public GameObject obstacle;
    public Transform spawnPoint;
    [SerializeField] private AssetReferenceGameObject obstacleReference;

    [Header("Variables")]
    public float spawnDelay;
    private  int score = 0;


    [Header("GUI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;
    public GameObject playButton;
    public GameObject touchButton;

    [Header("Audio")]
    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip scoreS;


    private void Start()
    {
        UpdateHighscoreText();
        InvokeRepeating("SkyBoxMovement", .00f, 0.01f * Time.deltaTime);

    }
 
    void SkyBoxMovement()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);
    }

    #region ScoresFunctions
    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString(); 
        aS.PlayOneShot(scoreS);
        CheckHighscore();
    }

    void CheckHighscore()
    {
        if(score> PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    void UpdateHighscoreText()
    {
        HighScoreText.text = $"{PlayerPrefs.GetInt("Highscore", 0)}";
    }

    #endregion

    #region ObstacleFunctions
    IEnumerator SpawnObstacles()
    {
        while(true)
        {
            
         
            yield return new WaitForSeconds(spawnDelay);

            AsyncOperationHandle<GameObject> asyncOperationHandle=
            Addressables.LoadAssetAsync<GameObject>("Assets/Prefab/Obstacle.prefab");
            

            asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
        }
    }    

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if(asyncOperationHandle.Status==AsyncOperationStatus.Succeeded)
        {
            float height = Random.Range(-3f, 3f);
            Instantiate(asyncOperationHandle.Result, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y+height, spawnPoint.transform.position.z), Quaternion.identity);
        }
        else
        {
            Debug.Log("Failed to load!");
        }
    }
    #endregion


    public void GameStart()
    {
        playButton.SetActive(false);
        scoreText.text = "0";
        touchButton.SetActive(true);
        bird.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        StartCoroutine(SpawnObstacles());
        InvokeRepeating("ScoreUp", 4f,spawnDelay);


    }
}
