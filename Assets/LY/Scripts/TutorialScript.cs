// Script for having a typewriter effect for UI
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inpector

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{

	//public Light globalLight;
	public GameObject[] glowDots = new GameObject[4];
	public GameObject textBox;
	public GameObject speech;
	public GameObject imageBox;
	private bool firstLine = true;
    AudioManager audioManager;

    TMP_Text _tmpProText;
	string writer;
	string[] script = new string[22] {
		"Worry not my bewildered biped, it is just as easy as ABC",
		"and also calculus and algebra",
		"Hold right click to move the camera around",
		"You can also use the scroll wheel to adjust the zoom levels",
		"Now, let's click on the arrow to the right",
		"This here is the equation table, we use this to draw the path of portals",
		"We will use these portals to help us navigate the deep space",
		"I have already prepared a simple one for you, right here",
		"By modifying the values and limit of equations...",
		"We can change how the portal's pathing is drawn",
		"Now, let's look at more options",
		"Further expand the menu by clicking this button here",
		"These are the templates of other lines we can draw",
		"by virtue of different types of equations, as you can see",
		"To use any of these, simply drag it to the right",
		"The goal is to use these portals to path our way to that black hole",
		"And also collect stars, we need at least one to clear the stage",
		"The rest I will leave up to you",
		"Play around with the equations and its variables",
		"See how the line reacts to each change",
		"And maybe you wouldn't fail your algebra exam next time",
		""};
		


	int scriptOrder = 0;
	[SerializeField] float delayBeforeStart = 0.6f;
	[SerializeField] float timeBtwChars = 0.03f;
	// Use this for initialization
	void Start()
	{
		_tmpProText = GetComponentInChildren<TMP_Text>()!;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (_tmpProText != null)
		{
			writer = _tmpProText.text;
			_tmpProText.text = "";

			StartCoroutine("TypeWriterTMP");
		}
	}

	IEnumerator TypeWriterTMP()
    {
		if (firstLine) 
		{
            yield return new WaitForSeconds(delayBeforeStart);
        }
        firstLine = false;

		foreach (char c in writer)
		{
            audioManager.PlaySFX(audioManager.speech);

            if (_tmpProText.text.Length > 0)
			{
				_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length);
			}
			_tmpProText.text += c;
			yield return new WaitForSeconds(timeBtwChars);
		}
	}

    void Update()
    {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left-click.");
			//globalLight.Intensity = 0.5f;

            StopCoroutine("TypeWriterTMP");
			_tmpProText.text = "";
			writer = script[scriptOrder];
			StartCoroutine("TypeWriterTMP");

			if (scriptOrder < 21)
			{
				scriptOrder++;
			}
			else
			{
				Destroy(textBox);
				Destroy(speech);
				Destroy(imageBox);
			}


			

        }
    }

}
