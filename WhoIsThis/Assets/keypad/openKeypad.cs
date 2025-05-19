using UnityEngine;
using UnityEngine.UI;

public class openKeypad : MonoBehaviour
{
    public GameObject relatedUIImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("keypad clicked!");
        SubtitleManager.Instance.ShowSubtitle("Enter password", 3f);

        

        if (relatedUIImage != null)
        {
            relatedUIImage.SetActive(true);
            
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
