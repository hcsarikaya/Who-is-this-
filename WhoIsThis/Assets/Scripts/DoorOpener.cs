using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            animator.SetTrigger("Open");
            isOpen = true;
        }
    }
}
