using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool hasLockPick = false;
    public bool hasKey = false;
    public bool hasPistol = false;
    public bool hasFlash = false;

    public GameObject flashInHand;
    public GameObject pistolInHand;

    public void PickupFlash()
    {
        hasFlash = true;
        if (flashInHand != null)
            flashInHand.SetActive(true);
    }

    public void PickupPistol()
    {
        hasPistol = true;
        if (pistolInHand != null)
            flashInHand.SetActive(false);
            pistolInHand.SetActive(true);
            
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
