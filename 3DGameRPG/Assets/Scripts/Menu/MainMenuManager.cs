using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button newGameButton;
    public Button loadGameButton;
    public Button settingButton;
    public Button exitButton;

    public AudioClip hoverSound; 
    public AudioClip clickSound; 

    private Color normalColor = Color.white;      
    private Color hoverColor = Color.yellow;     
    private Color clickColor = Color.green;     

    void Start()
    {
        AssignButtonEvents(newGameButton);
        AssignButtonEvents(loadGameButton);
        AssignButtonEvents(settingButton);
        AssignButtonEvents(exitButton);
    }

    void AssignButtonEvents(Button button)
    {
        var textMeshPro = button.GetComponentInChildren<TextMeshProUGUI>();
        var rectTransform = button.GetComponent<RectTransform>();
        var audioSource = button.GetComponent<AudioSource>();

        if (textMeshPro == null || audioSource == null)
        {
            Debug.LogWarning($"Button {button.name} is missing a TextMeshProUGUI or AudioSource component.");
            return;
        }

        // X? lý khi click
        button.onClick.AddListener(() =>
        {
            // Phát âm thanh click
            audioSource.PlayOneShot(clickSound);

            LeanTween.value(gameObject, textMeshPro.color, clickColor, 0.2f)
                     .setOnUpdate((Color value) => textMeshPro.color = value)
                     .setOnComplete(() =>
                     {
                         LeanTween.value(gameObject, textMeshPro.color, normalColor, 0.2f)
                                  .setOnUpdate((Color value) => textMeshPro.color = value);
                     });
        });

        // Hover: Thay ??i màu và Scale
        EventTriggerUtility.AddHoverEvent(button.gameObject,
            () =>
            {
                // Khi hover vào: phát âm thanh và ??i màu
                audioSource.PlayOneShot(hoverSound);

                LeanTween.value(gameObject, textMeshPro.color, hoverColor, 0.2f)
                         .setOnUpdate((Color value) => textMeshPro.color = value);
                LeanTween.scale(rectTransform, Vector3.one * 1.1f, 0.2f).setEase(LeanTweenType.easeOutBack);
            },
            () =>
            {
                // Khi hover ra: tr? l?i màu và kích th??c ban ??u
                LeanTween.value(gameObject, textMeshPro.color, normalColor, 0.2f)
                         .setOnUpdate((Color value) => textMeshPro.color = value);
                LeanTween.scale(rectTransform, Vector3.one, 0.2f).setEase(LeanTweenType.easeInBack);
            });
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("Chap0");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}


