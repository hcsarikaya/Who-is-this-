using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum ItemType { Flash, Pistol, Other }
    public ItemType itemType;

    private void OnMouseDown()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

        if (inventory != null)
        {
            switch (itemType)
            {
                case ItemType.Flash:
                    inventory.PickupFlash();
                    break;
                case ItemType.Pistol:
                    inventory.PickupPistol();
                    break;
            }


            gameObject.SetActive(false);
        }
    }
}
