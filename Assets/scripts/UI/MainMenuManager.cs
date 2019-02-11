using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField]
	GameObject Main;

	[SerializeField]
	GameObject GroupManeuvers;

	public void loadTestZone()
	{
		SceneManager.LoadScene("TestZone");
	}

	public void showMain()
	{
		Main.SetActive(true);
		GroupManeuvers.SetActive(false);
	}

	public void showGroupManeuvers()
	{
		Main.SetActive(false);
		GroupManeuvers.SetActive(true);
	}
}
