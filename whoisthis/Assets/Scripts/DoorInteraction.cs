using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator doorAnimator;
    private bool IsOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsOpen = !IsOpen; 
            doorAnimator.SetBool("IsOpen", IsOpen);
        }
    }
}
