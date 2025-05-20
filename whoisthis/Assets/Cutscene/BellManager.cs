using System.Collections;
using UnityEngine;

public class BellManager : MonoBehaviour
{
    public AudioSource bellSound;
    public CutsceneInteraction cutsceneInteraction;

    private LoopManager phaseManager;
    private bool phase3Triggered = false;

    void Start()
    {
        phaseManager = FindObjectOfType<LoopManager>();
        StartCoroutine(HandleBellByPhase());
    }

    void Update()
    {
        // Faz 3'e geçildiğinde tek seferlik tetikle
        if (!phase3Triggered && phaseManager != null && phaseManager.currentPhase == LoopManager.GamePhase.Phase3)
        {
            phase3Triggered = true;
            StartCoroutine(HandlePhase3Logic());
        }
    }

    IEnumerator HandleBellByPhase()
    {
        // Faz 1 kontrolü
        if (phaseManager.currentPhase == LoopManager.GamePhase.Phase1)
        {
            yield return new WaitForSeconds(15f);

            if (phaseManager.currentPhase == LoopManager.GamePhase.Phase1)
            {
                TriggerBell();
            }
            else if (phaseManager.currentPhase == LoopManager.GamePhase.Phase2)
            {
                yield return StartCoroutine(HandlePhase2Logic());
            }
        }
        // Faz 2'de başladıysa direkt Faz 2 mantığına geç
        else if (phaseManager.currentPhase == LoopManager.GamePhase.Phase2)
        {
            yield return StartCoroutine(HandlePhase2Logic());
        }
        // Faz 3'te başladıysa direkt tetikle (nadiren olur ama tedbir)
        else if (phaseManager.currentPhase == LoopManager.GamePhase.Phase3)
        {
            phase3Triggered = true;
            yield return StartCoroutine(HandlePhase3Logic());
        }
    }

    IEnumerator HandlePhase2Logic()
    {
        yield return new WaitForSeconds(60f);

        if (phaseManager.currentPhase == LoopManager.GamePhase.Phase2)
        {
            TriggerBell();
        }
        else
        {
            Debug.Log("Faz 2 sırasında faz 3'e geçildi, zil çalmayacak.");
        }
    }

    IEnumerator HandlePhase3Logic()
    {
        yield return new WaitForSeconds(2f);
        TriggerFinalBell();
    }

    void TriggerBell()
    {
        if (bellSound != null)
            bellSound.Play();

        if (cutsceneInteraction != null)
            cutsceneInteraction.isBellRung = true;

        Debug.Log("Kapı çaldı.");
    }

    void TriggerFinalBell()
    {
        

    

        Debug.Log("Oyun Bitti!");
    }
}
