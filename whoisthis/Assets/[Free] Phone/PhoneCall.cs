using System.Collections;
using UnityEngine;

public class PhoneCall : MonoBehaviour
{
    public AudioSource ringSource;
    public Light phoneLight;
    public GameObject phoneCallUI;

    public float flashInterval = 0.5f;
    private bool isRinging = false;

    void Start()
    {
        StartCoroutine(StartRinging());
    }

    IEnumerator StartRinging()
    {
        isRinging = true;
        ringSource.Play();

        while (isRinging)
        {
            phoneLight.enabled = !phoneLight.enabled;
            yield return new WaitForSeconds(flashInterval);
        }

        phoneLight.enabled = false;
    }

    void OnMouseDown()
    {
        if (isRinging)
        {
            StopRinging();
        }
    }

    void StopRinging()
    {
        isRinging = false;
        ringSource.Stop();
        phoneLight.enabled = false;
        phoneCallUI.transform.parent.gameObject.SetActive(true); // Enable full canvas
        phoneCallUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Optional: disable player movement
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<FirstPersonController>().enabled = false;
    }
}
