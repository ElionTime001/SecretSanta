using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public static EndGameController Instance { get; private set; }

    [Header("End Game Canvas")]
    public GameObject endGameCanvas;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (endGameCanvas != null)
            endGameCanvas.SetActive(false);
    }

    public void TriggerEndGame()
    {
        Debug.Log("TriggerEndGame called");

        if (endGameCanvas == null)
        {
            Debug.LogError("EndGameCanvas is NOT assigned in Inspector!");
            return;
        }

        endGameCanvas.SetActive(true);
        Debug.Log("EndGameCanvas activated successfully!");
    }
}
