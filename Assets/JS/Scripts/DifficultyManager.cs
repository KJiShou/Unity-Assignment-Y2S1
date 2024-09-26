using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TMP_Text[] difficultyTexts = new TMP_Text[3];  // Array of text options for difficulty (e.g., Easy, Medium, Hard)
    public TMP_FontAsset hoverFontAsset;  // Font to apply when hovering
    public TMP_FontAsset defaultFontAsset;  // Default font asset
    private TMP_Text currentText;  // Track the currently hovered or clicked text
    public Color defaultColor = Color.white;  // Default color of the text
    public Color selectedColor = Color.yellow;  // Color to apply to selected difficulty

    private bool isTextSelected = false;  // Whether a difficulty is selected
    private int selectedIndex = -1;  // Track the selected difficulty index

    void Start()
    {
        difficultyTexts[GameManager.Instance.difficulty - 1].color = selectedColor;
        difficultyTexts[GameManager.Instance.difficulty - 1].font = hoverFontAsset;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isTextSelected)
        {
            TMP_Text hoveredText = eventData.pointerEnter.GetComponent<TMP_Text>();
            if (hoveredText != null)
            {
                currentText = hoveredText;
                currentText.font = hoverFontAsset;  // Change to hover font
                currentText.ForceMeshUpdate();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isTextSelected)
        {
            TMP_Text hoveredText = eventData.pointerEnter.GetComponent<TMP_Text>();
            if (hoveredText != null)
            {
                currentText = hoveredText;
                currentText.font = defaultFontAsset;  // Revert to default font
                currentText.ForceMeshUpdate();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TMP_Text clickedText = eventData.pointerClick.GetComponent<TMP_Text>();

        if (clickedText != null)
        {
            // Reset all difficulty texts to default state before selecting a new one
            ResetDifficultyTexts();

            currentText = clickedText;
            selectedIndex = System.Array.IndexOf(difficultyTexts, currentText);  // Get the index of the clicked difficulty
            isTextSelected = true;

            // Apply selected font and color
            currentText.font = hoverFontAsset;
            currentText.color = selectedColor;
            currentText.ForceMeshUpdate();

            // Set difficulty based on index (if index is 0, difficulty is 1, and so on)
            GameManager.Instance.difficulty = selectedIndex + 1;

            Debug.Log("Selected difficulty: " + GameManager.Instance.difficulty);
        }
    }

    private void ResetDifficultyTexts()
    {
        foreach (TMP_Text text in difficultyTexts)
        {
            text.font = defaultFontAsset;  // Reset font to default
            text.color = defaultColor;  // Reset color to default
            text.ForceMeshUpdate();
        }
    }
}
