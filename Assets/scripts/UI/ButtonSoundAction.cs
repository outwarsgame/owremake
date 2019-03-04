using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonSoundAction : MonoBehaviour
{
	/// <summary>
	/// The specific action of the aimed button should be implemented in this overriden function in the daughter class.
	/// </summary>
	protected abstract void DoIt(); 

	[SerializeField]
	List<AudioSource> AudioSources = new List<AudioSource>();

	/// <summary>
	/// This should be in the base Canvas of which this button depends.
	/// </summary>
	[SerializeField]
	GraphicRaycaster grc;

	float maxClipLength = 0.0f;

	void Start()
	{
		foreach(AudioSource asou in AudioSources)
			if (asou.clip.length > maxClipLength)
				maxClipLength = asou.clip.length;
	}

	/// <summary>
	/// Call this when the button is clicked.
	/// </summary>
	public void ButtonClick()
	{
		StartCoroutine("WaitForSound");
	}

	IEnumerator WaitForSound()
	{
		grc.enabled = false;

		foreach (AudioSource asou in AudioSources)
			asou.Play();

		yield return new WaitForSeconds(maxClipLength);

		DoIt();
	}
}