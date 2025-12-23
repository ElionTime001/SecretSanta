using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIItemIndicator : MonoBehaviour
{
    public static UIItemIndicator Instance { get; private set; }

    [Header("Images")]
    [SerializeField] private Image imageOne;
    [SerializeField] private Image imageTwo;
    [SerializeField] private Image imageThree;

    [Header("Ship Parts Counter")]
    [SerializeField] private TMP_Text shipPartsText;
    [SerializeField] private int totalShipParts = 3;

    [Header("Interact Prompt")]
    [SerializeField] private TMP_Text interactPromptText;

    [Header("Colors")]
    [SerializeField] private Color darkColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    [SerializeField] private Color litColor = Color.white;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1f;

    private int collectedShipParts = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Initial UI state
        imageOne.color = darkColor;
        imageTwo.color = darkColor;
        imageThree.color = darkColor;

        shipPartsText.text = $"0/{totalShipParts} Ship Parts";
        interactPromptText.gameObject.SetActive(false);
    }

    /* =========================
       IMAGE INDICATORS
       ========================= */

    public void LightUpImageOne()
    {
        StartCoroutine(FadeImage(imageOne));
    }

    public void LightUpImageTwo()
    {
        StartCoroutine(FadeImage(imageTwo));
    }

    public void LightUpImageThree()
    {
        StartCoroutine(FadeImage(imageThree));
    }

    private IEnumerator FadeImage(Image img)
    {
        Color startColor = img.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            img.color = Color.Lerp(startColor, litColor, t);
            yield return null;
        }

        img.color = litColor;
    }

    /* =========================
       SHIP PART COUNTER
       ========================= */

    public void AddShipPart()
    {
        collectedShipParts = Mathf.Clamp(collectedShipParts + 1, 0, totalShipParts);
        shipPartsText.text = $"{collectedShipParts}/{totalShipParts} Ship Parts";
    }

    /* =========================
       INTERACT PROMPT
       ========================= */

    public void ShowInteractPrompt()
    {
        interactPromptText.text = "Interact with E";
        interactPromptText.gameObject.SetActive(true);
    }

    public void HideInteractPrompt()
    {
        interactPromptText.gameObject.SetActive(false);
    }

    public bool HasThreeShipParts()
    {
        return collectedShipParts == 3;
    }
}
