using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject instructionsPanel;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button closeButton;

    [Header("Audio (Optional)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    void Start()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(false);

        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClick);

        if (instructionsButton != null)
            instructionsButton.onClick.AddListener(OnInstructionsButtonClick);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    void OnPlayButtonClick()
    {
        PlayButtonSound();

        // Load Gameplay Scene
       
        SceneManager.LoadScene("SampleScene");
    }

    public void OnInstructionsButtonClick()
    {
        PlayButtonSound();
        Debug.Log("Instructions button clicked!");

        if (instructionsPanel != null)
        {
            Debug.Log("Showing instructions panel");

            instructionsPanel.SetActive(true);

            AnimatePanel(true);
        }
        else
        {
            Debug.LogError("Instructions Panel is null!");
        }
    }
    void OnCloseButtonClick()
    {
        PlayButtonSound();

        if (instructionsPanel != null)
        {
            AnimatePanel(false);
        }
    }

    void AnimatePanel(bool show)
    {
        if (instructionsPanel == null) return;

 
        CanvasGroup canvasGroup = instructionsPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = instructionsPanel.AddComponent<CanvasGroup>();

        if (show)
        {
            // Fade in
            canvasGroup.alpha = 0f;
            instructionsPanel.SetActive(true);
            StartCoroutine(FadePanel(canvasGroup, 0f, 1f, 0.3f));
        }
        else
        {
            // Fade out
            StartCoroutine(FadePanel(canvasGroup, 1f, 0f, 0.3f, () => {
                instructionsPanel.SetActive(false);
            }));
        }
    }

    System.Collections.IEnumerator FadePanel(CanvasGroup canvasGroup, float startAlpha,
                                             float endAlpha, float duration,
                                             System.Action onComplete = null)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        onComplete?.Invoke();
    }

    void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    // close panel by ESC
    void Update()
    {
        if (instructionsPanel != null && instructionsPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnCloseButtonClick();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log($"Panel active: {instructionsPanel.activeSelf}");
            Debug.Log($"Panel gameObject active: {instructionsPanel.gameObject.activeInHierarchy}");
        }
    }

    void OnDestroy()
    {
        // Cleanup
        if (playButton != null)
            playButton.onClick.RemoveAllListeners();

        if (instructionsButton != null)
            instructionsButton.onClick.RemoveAllListeners();

        if (closeButton != null)
            closeButton.onClick.RemoveAllListeners();
    }
}
