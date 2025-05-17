using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    private bool isOn = false;

    void Start()
    {
        if (flashlight != null)
            flashlight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
