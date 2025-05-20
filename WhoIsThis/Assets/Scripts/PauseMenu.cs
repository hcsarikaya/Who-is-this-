using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuCanvas; 
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.SetActive(true);
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            playerObj.GetComponent<ChangeCursor>().ActivateCursor();
        }
    }

    void ToggleMenu()
    {
        isPaused = !isPaused;
        menuCanvas.SetActive(isPaused);

        
        

        // Optional: Lock/unlock the cursor
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }
}
