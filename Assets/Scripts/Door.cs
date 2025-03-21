using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public CoinManage fm;
    [SerializeField] private bool unlockDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unlockDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fm.fragCount == 1)
        {
            unlockDoor = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "SceneOne" && unlockDoor)
        {
            SceneManager.LoadScene("Video2");
        } else if (sceneName == "SceneTwo" && unlockDoor) {
            SceneManager.LoadScene("Video3");
        }else if (sceneName == "SceneThree" && unlockDoor ) {
            SceneManager.LoadScene("Video4");
        }else if (sceneName == "SceneFour" && unlockDoor) {
            SceneManager.LoadScene("Video5");
        }else if (sceneName == "SceneFive" && unlockDoor) {
            SceneManager.LoadScene("Video6");
        }
    }
}
