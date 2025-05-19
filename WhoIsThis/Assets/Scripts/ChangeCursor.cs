using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Optional: disable player movement
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<FirstPersonController>().enabled = false;
    }

    public void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        // Optional: disable player movement
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<FirstPersonController>().enabled = true;
    }
}
