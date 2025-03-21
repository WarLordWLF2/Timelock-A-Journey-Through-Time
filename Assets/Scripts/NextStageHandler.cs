using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class NextStageHandler : MonoBehaviour
{
    public float videoTime; 

    void Start(){
        StartCoroutine(nextStage());
    }
    // alt shift f
    IEnumerator nextStage(){
        yield return new WaitForSeconds(videoTime); 
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log("Scene name" + sceneName);
        string nextScene = "";
        switch (sceneName)
        {
            case "Video1":
                nextScene = "SceneOne";
                break;
            case "Video2":
                nextScene = "SceneTwo";
                break;
            case "Video3":
                nextScene = "SceneThree";
                break;
            case "Video4":
                nextScene = "SceneFour";
                break;
            case "Video5":
                nextScene = "SceneFive";
                break;
            case "Video6":
                nextScene = "MainMenu";
                break;
            case "GameOverScene":
                nextScene = "MainMenu";
                break;
        }
        SceneManager.LoadScene(nextScene);
    }
}
