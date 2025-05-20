using UnityEngine;
using UnityEngine.UI;

public class note : MonoBehaviour
{
   
    public GameObject relatedUIImage; // Assign in Inspector

    private void OnMouseDown()
    {
        

        if (relatedUIImage != null)
        {
            relatedUIImage.transform.parent.gameObject.SetActive(true);
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
