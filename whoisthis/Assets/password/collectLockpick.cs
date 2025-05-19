using UnityEngine;

public class collectLockpick : MonoBehaviour
{
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
        

        // Optional: disable player movement
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<PlayerInventory>().hasLockPick = true;
        SubtitleManager.Instance.ShowSubtitle("Lockpick founded!", 3f);
        gameObject.SetActive(false);
    }
}
