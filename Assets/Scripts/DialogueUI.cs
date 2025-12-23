using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; 

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float autoHideDelay = 5f;
    [SerializeField] private string dialoguePrefix = "> ";

    [Header("Image")]
    [SerializeField] private Image Danger;

    private Coroutine hideCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        dialogueText.gameObject.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        dialogueText.text = dialoguePrefix + text;
        dialogueText.gameObject.SetActive(true);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    public void showPhoto()
    {
        Danger.gameObject.SetActive(true);
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(autoHideDelay);
        dialogueText.gameObject.SetActive(false);
        Danger.gameObject.SetActive(false);
        hideCoroutine = null;
    }
}
