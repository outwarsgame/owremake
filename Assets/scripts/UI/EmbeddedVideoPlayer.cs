using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EmbeddedVideoPlayer : MonoBehaviour
{
	RawImage rawImage;
	VideoPlayer videoPlayer;
	Button button;

	Color pausedColor;

	private void Awake()
	{
		rawImage = GetComponent<RawImage>();
		videoPlayer = GetComponent<VideoPlayer>();
		button = GetComponent<Button>();

		pausedColor = new Color(0.5f, 0.5f, 0.5f, 0.75f);
	}

	private void OnEnable()
	{
		StartCoroutine(PlayVideo());
	}

	private void OnDisable()
	{
		rawImage.color = Color.clear;
	}

	private void Update()
	{
		if( videoPlayer.isPlaying )
			rawImage.color = Color.white;
		else if( !videoPlayer.isPaused )
			rawImage.color = Color.clear;
	}

	IEnumerator PlayVideo()
	{
		videoPlayer.Prepare();

		WaitForSeconds waitForSeconds = new WaitForSeconds(1);

		while (!videoPlayer.isPrepared)
		{
			yield return waitForSeconds;
			break;
		}

		rawImage.texture = videoPlayer.texture;
		videoPlayer.Play();
	}

	public void PlayPause()
	{
		if (videoPlayer.isPaused)
		{
			videoPlayer.Play();
		}	
		else if(videoPlayer.isPlaying)
		{
			videoPlayer.Pause();
			rawImage.color = pausedColor;
		}	
		else
		{
			StartCoroutine(PlayVideo());
		}	
	}
}