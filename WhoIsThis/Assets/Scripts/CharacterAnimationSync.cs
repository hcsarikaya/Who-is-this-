using UnityEngine;

public class CharacterAnimationSync : MonoBehaviour
{
    public Animator animator; 
    public Transform fpsController; 
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = fpsController.position;
    }

    void Update()
    {
        Vector3 velocity = (fpsController.position - lastPosition) / Time.deltaTime;
        lastPosition = fpsController.position;

      
        Vector3 localVelocity = fpsController.InverseTransformDirection(velocity);
        float h = localVelocity.x;
        float v = localVelocity.z;

        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
    }
}
