using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField]
	GameObject Main;

	[SerializeField]
	GameObject SoloCampaign;

	[SerializeField]
	GameObject SingleMission;

	[SerializeField]
	GameObject GroupManeuvers;

	[SerializeField]
	GameObject Options;

	public void loadTestZone()
	{
		SceneManager.LoadScene("TestZone");
	}

	public void showMain()
	{
		Main.SetActive(true);
		SoloCampaign.SetActive(false);
		SingleMission.SetActive(false);
		GroupManeuvers.SetActive(false);
		Options.SetActive(false);
	}

	public void showSoloCampaign()
	{
		Main.SetActive(false);
		SoloCampaign.SetActive(true);
		SingleMission.SetActive(false);
		GroupManeuvers.SetActive(false);
		Options.SetActive(false);
	}

	public void showSingleMission()
	{
		Main.SetActive(false);
		SoloCampaign.SetActive(false);
		SingleMission.SetActive(true);
		GroupManeuvers.SetActive(false);
		Options.SetActive(false);
	}

	public void showGroupManeuvers()
	{
		Main.SetActive(false);
		SoloCampaign.SetActive(false);
		SingleMission.SetActive(false);
		GroupManeuvers.SetActive(true);
		Options.SetActive(false);
	}

	public void showOptions()
	{
		Main.SetActive(false);
		SoloCampaign.SetActive(false);
		SingleMission.SetActive(false);
		GroupManeuvers.SetActive(false);
		Options.SetActive(true);
	}
}
