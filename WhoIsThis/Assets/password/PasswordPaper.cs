using UnityEngine;
using UnityEngine.UI;

public class PasswordPaper : MonoBehaviour
{
    public string passwordFragment;
    public GameObject relatedUIImage; // Assign in Inspector

    private void OnMouseDown()
    {
        Debug.Log("Paper clicked!");

        PasswordManager.Instance.CollectFragment(passwordFragment);

        if (relatedUIImage != null)
        {
            relatedUIImage.SetActive(true);
            Debug.Log("UI Image activated: " + relatedUIImage.name);
        }
        else
        {
            Debug.LogWarning("relatedUIImage not assigned!");
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Optional: disable player movement
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<FirstPersonController>().enabled = false;
        gameObject.SetActive(false);
    }
}
