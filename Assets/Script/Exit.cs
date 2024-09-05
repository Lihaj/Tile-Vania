using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
[SerializeField] float LevelLoadDelay=1f;
void OnTriggerEnter2D(Collider2D other) {

    if(other.tag=="Player"){
        //Delaying the code
        StartCoroutine(LoadNextLevel());
    }
   
    }
    IEnumerator LoadNextLevel(){
        //Delaying the code for 1S
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
      
        //then do this
        int currentSceneIndex= SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=currentSceneIndex+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings){
            nextSceneIndex=0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(currentSceneIndex+1);
    }
}
