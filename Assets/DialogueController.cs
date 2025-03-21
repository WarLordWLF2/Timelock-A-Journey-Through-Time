using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed;

    public Animator DialogueAnimator;
    private bool StartDialogue = true;
    private bool isTyping = false; // Flag to track if text is still appearing

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // If text is still appearing, instantly complete it
                StopAllCoroutines();
                DialogueText.text = Sentences[Index]; // Show full text instantly
                isTyping = false;
                Index++; // Move to the next sentence on next press
            }
            else
            {
                NextSentence();
            }
        }
    }

    void NextSentence()
    {
        if (Index == 0)
        {
            DialogueAnimator.SetBool("Enter", true);
        }

        if (Index < Sentences.Length)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            DialogueAnimator.SetBool("Exit", true);
            DialogueText.text = "";
            Invoke(nameof(GoToNextScene), 1f);
        }
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene("Video1");
    }

    IEnumerator WriteSentence()
    {
        isTyping = true; // Start typing
        DialogueText.text = ""; 

        foreach (char Character in Sentences[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }

        isTyping = false; // Typing is complete
        Index++;
    }
}