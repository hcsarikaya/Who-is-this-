using UnityEngine;

public class Phase3Trigger : MonoBehaviour
{
    private LoopManager loopManager;
    public CutsceneInteraction cutsceneInteraction;

    void Start()
    {
        loopManager = FindObjectOfType<LoopManager>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {

            cutsceneInteraction.isPhase3 = true;
            
            gameObject.SetActive(false);
        }
    }
}
