// using UnityEngine;
// using UnityEngine.Video;
// using UnityEngine.SceneManagement;

// public class LevelTransition : MonoBehaviour
// {
//     public VideoPlayer videoPlayer;
//     public string nextSceneName;

//     void Start()
//     {
//         videoPlayer.Play(); // Start video playback
//         videoPlayer.loopPointReached += LoadNextScene; // Wait for video to finish
//     }

//     void LoadNextScene(VideoPlayer vp)
//     {
//         SceneManager.LoadScene(nextSceneName); // Load the next scene
//     }
// }
