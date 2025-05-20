using SojaExiles;
using UnityEngine;

public class collectKey : MonoBehaviour
{
    public GameObject door;
    private LoopManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<LoopManager>();
    }

    private void OnMouseDown()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<PlayerInventory>().hasKey = true;
        door.GetComponent<keyOpenClose>().open = true;

        SubtitleManager.Instance.ShowSubtitle("founded!", 3f);
        gameObject.SetActive(false);

        // FAZ 3'e geç
        if (gameManager != null)
        {
            gameManager.SetPhase(LoopManager.GamePhase.Phase3);
        }
        else
        {
            Debug.LogWarning("PhaseManager bulunamadı!");
        }
    }
}
