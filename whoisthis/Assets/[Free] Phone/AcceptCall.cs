using System.Collections;
using UnityEngine;

public class AcceptCall : MonoBehaviour
{
    public GameObject phoneCallUI;      // Panel with Accept/Reject
    public GameObject talkingPhoneUI;   // Talking interface
    public AudioSource callAudioSource; // AudioSource with voice clip

    public LoopManager loopManager;     // Loop manager reference (ata inspector'dan)

    public void OnAcceptCall()
    {
        phoneCallUI.SetActive(false);
        talkingPhoneUI.SetActive(true);

        callAudioSource.Play();

        StartCoroutine(WaitForCallToEnd());
    }

    public void OnRejectCall()
    {
        Debug.Log("Call rejected");

        // Fazı 2 yap
        if (loopManager != null)
        {
            loopManager.SetPhase(LoopManager.GamePhase.Phase2);
        }

        // UI'ları kapat
        phoneCallUI.SetActive(false);
        talkingPhoneUI.SetActive(false);
        phoneCallUI.transform.parent.gameObject.SetActive(false);

        // Hareketi geri aç
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    IEnumerator WaitForCallToEnd()
    {
        yield return new WaitWhile(() => callAudioSource.isPlaying);
        Debug.Log("Call ended");

        talkingPhoneUI.SetActive(false);
        phoneCallUI.transform.parent.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
