using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SnapToItem : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;
    public HorizontalLayoutGroup HLG;
    public TMP_Text NameLabel;
    public string[] ItemNames;
    bool isSnapped;
    float snapSpeed;
    public float snapForce;
    // Start is called before the first frame update
    void Start()
    {
        isSnapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        int currentItem = Mathf.RoundToInt((0-contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)));

        if (scrollRect.velocity.magnitude < 50 && !isSnapped) {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)),snapSpeed),
                contentPanel.localPosition.y,
                contentPanel.localPosition.z);
            NameLabel.text = (currentItem-20).ToString();
            if (contentPanel.localPosition.x == 0 -(currentItem * (sampleListItem.rect.width + HLG.spacing))){
                isSnapped = true;
            }
            isSnapped = true;
        
        }
        if(scrollRect.velocity.magnitude > 50)
        {
            NameLabel.text = (currentItem-20).ToString();
            isSnapped = false;
            snapSpeed = 0;
        }
    }
}