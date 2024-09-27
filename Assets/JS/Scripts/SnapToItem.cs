using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SnapToItem : MonoBehaviour
{
    AudioManager audioManager;
    private bool isPlayingSFX = false;

    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;
    public HorizontalLayoutGroup HLG;
    public TMP_Text NameLabel;
    public string[] ItemNames;
    bool isSnapped;
    float snapSpeed;
    public float snapForce;
    public GameObject LineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        isSnapped = false;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentItem = (0-contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing));

        if (scrollRect.velocity.magnitude < 50 && !isSnapped && NameLabel!=null) {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)),snapSpeed),
                contentPanel.localPosition.y,
                contentPanel.localPosition.z);
            NameLabel.text = (currentItem-70).ToString("F1");
            if (contentPanel.localPosition.x == 0 -(currentItem * (sampleListItem.rect.width + HLG.spacing))){
                isSnapped = true;

            }
            isSnapped = true;


            if (!isPlayingSFX)
            {
                StartCoroutine(PlaySliderSFXSmoothly());
            }


        }
        if (scrollRect.velocity.magnitude > 0 && NameLabel != null)
        {
            NameLabel.text = (currentItem-70).ToString("F1");
            isSnapped = false;
            snapSpeed = 0;
        }
    }

    public void UpdateSlider(float itemIndex)
    {
        // Calculate the target position based on the item index
        float targetPositionX = 0 - (itemIndex * (sampleListItem.rect.width + HLG.spacing));

        // Update the contentPanel's localPosition
        contentPanel.localPosition = new Vector3(
            targetPositionX,
            contentPanel.localPosition.y,
            contentPanel.localPosition.z);

        // Reset snapping variables to ensure smooth snapping in Update()
        isSnapped = false;
        snapSpeed = 0f;
        scrollRect.velocity = Vector2.zero;

        // Update the NameLabel
        NameLabel.text = (itemIndex - 70).ToString("F1");
    }


    private IEnumerator PlaySliderSFXSmoothly()
    {
        isPlayingSFX = true;
        audioManager.PlaySFX(audioManager.menuSlider);  // Play the slider sound effect

        // Wait a short period before allowing the sound to play again (smooth effect)
        yield return new WaitForSeconds(0.1f);  // Adjust this value for smoother sound effect timing

        isPlayingSFX = false;
    }



}