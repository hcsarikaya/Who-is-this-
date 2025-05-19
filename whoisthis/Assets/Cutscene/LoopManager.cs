using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;

    public enum GamePhase { Phase1, Phase2, Phase3 }
    public GamePhase currentPhase = GamePhase.Phase1;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool IsInPhase(GamePhase phase)
    {
        return currentPhase == phase;
    }

    public void ResetLoop()
    {
        currentPhase = GamePhase.Phase1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void AdvanceToPhase(GamePhase phase)
    {
        currentPhase = phase;
    }
}
