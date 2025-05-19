using UnityEngine;

public class onClickEv : MonoBehaviour
{
    private void Start()
    {
        //GetComponent<Button>().onClick.AddListener(CloseSelf);
    }

    public void CloseSelf()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = true;
        gameObject.SetActive(false);
    }
}
