using UnityEngine;
using UnityEngine.Playables;

public class CutsceneInteraction : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    private bool isBellRung = false;

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
    }
}
