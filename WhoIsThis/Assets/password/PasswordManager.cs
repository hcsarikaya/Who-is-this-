using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordManager : MonoBehaviour
{
    public static PasswordManager Instance;

    public List<string> collectedFragments = new List<string>();
    public int requiredFragments = 5;

    public GameObject passwordRevealUI;
    public Text passwordText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectFragment(string fragment)
    {
        if (!collectedFragments.Contains(fragment))
            collectedFragments.Add(fragment);

        if (collectedFragments.Count >= requiredFragments)
            ShowPassword();
    }

    void ShowPassword()
    {
        string fullPassword = string.Join("", collectedFragments);
        Debug.Log("Full password: " + fullPassword);

        if (passwordRevealUI != null)
            passwordRevealUI.SetActive(true);

        if (passwordText != null)
            passwordText.text = "Password: " + fullPassword;
    }
}
