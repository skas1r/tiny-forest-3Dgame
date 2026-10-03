using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private float fadeDelay = 2.0f;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.canvasRenderer.SetAlpha(0f);
    }

    // Перетворюємо метод Start на корутинний
    private IEnumerator Start()
    {
        // Затримка анімації
        yield return new WaitForSeconds(fadeDelay);
        // Анімація проявлення тексту
        text.CrossFadeAlpha(1f, fadeDuration, false);
    }
}