using System.Collections;
using UnityEngine;

public class AcceptCall : MonoBehaviour
{
    public GameObject phoneCallUI;      // Panel with Accept/Reject
    public GameObject talkingPhoneUI;   // Talking interface
    public AudioSource callAudioSource; // AudioSource with voice clip

    public void OnAcceptCall()
    {
        // Hide the incoming call UI
        phoneCallUI.SetActive(false);

        // Show the talking UI
        talkingPhoneUI.SetActive(true);

        // Play the audio
        callAudioSource.Play();

        // Start coroutine to wait until it's done
        StartCoroutine(WaitForCallToEnd());
    }

    IEnumerator WaitForCallToEnd()
    {
        yield return new WaitWhile(() => callAudioSource.isPlaying);
        Debug.Log("Trying to open phone call UI");

        // Call ended — hide the talking UI
        talkingPhoneUI.SetActive(false);
        phoneCallUI.transform.parent.gameObject.SetActive(false);

        // Optionally re-enable movement or cursor here
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
