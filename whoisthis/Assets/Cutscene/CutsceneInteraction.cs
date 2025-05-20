using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInteraction : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public PlayableDirector finalCutsceneDirector;
    public bool isBellRung = false;
     public AudioSource bellSound;
    
    public bool isPhase3 = false;

    public void RingBell()
    {
        isBellRung = true;
    }

    private void OnMouseDown()
    {
        if (isBellRung)
        {
            cutsceneDirector.Play();

        }
        if (isPhase3)
        {
            
            bellSound.Play();

            finalCutsceneDirector.Play();
           
        }
    }
}
