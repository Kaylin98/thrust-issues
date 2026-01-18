using System.Collections;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class LevelAnnouncer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] TextMeshProUGUI levelText;

    [Header("Timing Settings")]
    [SerializeField] float displayTime = 2f;
    [SerializeField] float fadeSpeed = 1f;

    CanvasGroup canvasGroup;
    Material textMat;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        textMat = levelText.fontMaterial; 
        
        string sceneName = SceneManager.GetActiveScene().name;
        
        // Defaults
        levelText.color = Color.white;
        levelText.enableWordWrapping = false;
        levelText.transform.localScale = Vector3.one; 
        levelText.transform.localPosition = Vector3.zero; 
        canvasGroup.alpha = 1; 
        levelText.text = ""; 

        switch (sceneName)
        {
            case "Outpost Zero":
                // Cyan + Typewriter (Tech)
                SetGlow(new Color32(0, 255, 255, 255), 0.2f); 
                StartCoroutine(TypewriterEffect(sceneName));
                break;

            case "The Abyss Shaft":
                // Purple + Fly Through (Deep)
                SetGlow(new Color32(157, 0, 255, 255), 0.25f);
                levelText.text = sceneName; 
                StartCoroutine(ZoomThroughEffect());
                break;

            case "The Dust Bowl":
                // Orange + Rising Heat (Hot)
                SetGlow(new Color32(255, 69, 0, 255), 1.0f);
                levelText.text = sceneName;
                StartCoroutine(ScorchEffect());
                break;

            default:
                levelText.text = sceneName;
                SetGlow(Color.white, 0.2f);
                StartCoroutine(FadeOutOnly());
                break;
        }
    }

    // --- HELPER TO SET GLOW ---
    void SetGlow(Color color, float power)
    {
        levelText.fontSharedMaterial = textMat; 
        textMat.EnableKeyword(ShaderUtilities.Keyword_Glow);
        textMat.SetColor(ShaderUtilities.ID_GlowColor, color);
        textMat.SetFloat(ShaderUtilities.ID_GlowPower, power);
        textMat.SetFloat(ShaderUtilities.ID_GlowOuter, 0.5f);
    }

    // --- EFFECT 1: SCORCH (Dust Bowl) ---
    IEnumerator ScorchEffect()
    {
        float timer = 0f;
        float duration = 2.5f;
        
        // Start Position: Slightly lower (rising heat)
        Vector3 startPos = new Vector3(0, -50, 0); 
        Vector3 endPos = Vector3.zero;

        // Start Color: Black (Burnt silhouette)
        levelText.color = Color.black; 

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // 1. Move Upwards (Rising Heat)
            levelText.transform.localPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0, 1, progress));

            // 2. Color "Cools Down" (Black -> White)
            levelText.color = Color.Lerp(Color.black, Color.white, progress);

            // 3. Glow "Settles" (Blinding 1.0 -> Normal 0.2)
            float currentGlow = Mathf.Lerp(1.0f, 0.2f, progress);
            textMat.SetFloat(ShaderUtilities.ID_GlowPower, currentGlow);

            yield return null;
        }
        
        yield return new WaitForSeconds(displayTime);
        StartCoroutine(FadeOutOnly());
    }

    // --- EFFECT 2: ZOOM THROUGH (The Abyss) ---
    IEnumerator ZoomThroughEffect()
    {
        float timer = 0f;
        float duration = 3.0f; 
        Vector3 startScale = Vector3.one;     
        Vector3 endScale = new Vector3(10, 10, 10); 

        yield return new WaitForSeconds(0.5f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            levelText.transform.localScale = Vector3.Lerp(startScale, endScale, progress * progress);

            if (progress > 0.5f)
            {
                float fadeProgress = (progress - 0.5f) * 2f; 
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeProgress);
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }

    // --- EFFECT 3: TYPEWRITER (Outpost Zero) ---
    IEnumerator TypewriterEffect(string textToType)
    {
        levelText.characterSpacing = 10;
        foreach (char letter in textToType)
        {
            levelText.text += letter;
            yield return new WaitForSeconds(0.1f); 
        }
        yield return new WaitForSeconds(displayTime);
        StartCoroutine(FadeOutOnly());
    }

    // --- FADE OUT ---
    IEnumerator FadeOutOnly()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}