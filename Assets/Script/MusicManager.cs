using TMPro;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [Header("References")]
    public AudioSource musicSource;
    public Button toggleButton;
    public TextMeshProUGUI buttonText;

    private bool isMusicOn = true;

    void Start()
    {
        // Load saved preference
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;

        // Set initial state
        musicSource.enabled = isMusicOn;
        UpdateButtonText();

        // Add listener
        toggleButton.onClick.AddListener(ToggleMusic);
    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.enabled = isMusicOn;

        // Save preference
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);

        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = isMusicOn ? "Music: ON" : "Music OFF";
        }
    }
}