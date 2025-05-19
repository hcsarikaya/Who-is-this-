using SojaExiles;
using UnityEngine;

public class collectKey : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject door;
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
        playerObj.GetComponent<PlayerInventory>().hasKey = true;
        door.GetComponent<keyOpenClose>().open = true;
        SubtitleManager.Instance.ShowSubtitle(" founded!", 3f);
        gameObject.SetActive(false);
    }
}
