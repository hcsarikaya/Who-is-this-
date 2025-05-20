using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SojaExiles
{ 
public class lockOpenClose : MonoBehaviour
{
    public Animator openandclose;
    public bool open;
    public Transform Player;

    public GameObject lockpickCanvas;
    
    GameObject playerObj;

    public bool unlocked = false;

    private PlayerInventory inventory;

    public Camera playerCamera;
    public Camera lockpickCamera;

    void Start()
    {
        open = false;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        inventory = playerObj.GetComponent<PlayerInventory>();
    }

    void OnMouseOver()
    {
        if (Player)
        {
            float dist = Vector3.Distance(Player.position, transform.position);
            if (dist < 15 && Input.GetMouseButtonDown(0))
            {
                if (!unlocked)
                {
                    if (inventory.hasLockPick)
                    {
                        lockpickCanvas.SetActive(true); // Trigger the lockpick minigame
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        lockpickCamera.enabled = true;
                        playerCamera.enabled = false;


                        playerObj.GetComponent<FirstPersonController>().enabled = false;
                    }
                    else
                    {
                            SubtitleManager.Instance.ShowSubtitle("Door is locked...", 3f);
                        }
                }
                else
                {
                    if (!open)
                    {
                        StartCoroutine(opening());
                    }
                    else
                    {
                        StartCoroutine(closing());
                    }
                }
            }
        }
    }

    

    IEnumerator opening()
    {
        print("you are opening the door");
        SubtitleManager.Instance.ShowSubtitle("you are opening the door...", 3f);

        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        print("you are closing the door");
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }

    public void UnlockDoor()
    {
        unlocked = true;
        lockpickCanvas.SetActive(false);
    }
}
}
