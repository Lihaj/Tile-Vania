using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField]int playerlives=3;
    [SerializeField]int score=0;

    [SerializeField]TextMeshProUGUI liveText;
    [SerializeField]TextMeshProUGUI scoreText;
   
    void Awake()
    {
        int numGameSessions=FindObjectsOfType<GameSession>().Length;
        if(numGameSessions>1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    void Strat(){
        liveText.text=playerlives.ToString();
        scoreText.text=score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerlives > 1){
            TakeLife();
        }else{
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd){
        score=score+pointsToAdd;
        scoreText.text=score.ToString();
    }

    void ResetGameSession(){
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife(){
        playerlives--;
        int currentSceneIndex= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        liveText.text=playerlives.ToString();
    }
}
