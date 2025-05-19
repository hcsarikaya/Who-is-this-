using UnityEngine;

public class BellManager : MonoBehaviour
{
    public CutsceneInteraction cutscene;

    void Start()
    {
        if (cutscene == null)
            cutscene = FindObjectOfType<CutsceneInteraction>();

        switch (LoopManager.Instance.currentPhase)
        {
            case LoopManager.GamePhase.Phase1:
                Invoke("RingBellPhase1", 20f); 
                break;

            case LoopManager.GamePhase.Phase2:
                Invoke("RingBellPhase2", 60f); 
                break;
        }
    }

    void RingBellPhase1()
    {
        cutscene.isBellRung = true;
        Debug.Log("Phase 1: Kapı çalıyor.");
    }

    void RingBellPhase2()
    {
        cutscene.isBellRung = true;
        Debug.Log("Phase 2: Kapı çalıyor.");
    }
}
