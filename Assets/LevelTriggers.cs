using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the Inspector
    public string nextSceneName;    // Name of the next scene

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // Auto-assign if not set
        }
    }

    public void PlayTransition()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd; // Subscribe to event
        }
        else
        {
            Debug.LogError("No VideoPlayer assigned to LevelTransition!");
            LoadNextScene(); // Load scene if video is missing
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("SceneTwo");
    }
}
