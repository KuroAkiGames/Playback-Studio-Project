using TMPro;
using UnityEngine;

public class FontChangerTMP : MonoBehaviour
{
    public TextMeshProUGUI textMeshProText; // Reference to the TextMeshPro text component
    public TMP_FontAsset newFont; // Reference to the new TMP font

    void Start()
    {
        if (textMeshProText != null && newFont != null)
        {
            textMeshProText.font = newFont;
        }
    }
}
