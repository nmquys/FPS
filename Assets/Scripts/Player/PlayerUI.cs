using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    public Text playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateText(string promptMessange)
    {
        promptText.text = promptMessange;
    }

    public void UpdateHealth(float healthValue)
    {
        playerHealth.text = healthValue.ToString("F0");
    }
}
