using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManeuversInManager : MonoBehaviour
{
	[SerializeField]
	GameObject Roster;

	[SerializeField]
	GameObject Armory;

	[SerializeField]
	GameObject Teams;

	[SerializeField]
	GameObject Battlefield;

	[SerializeField]
	GameObject Setup;

	[SerializeField]
	GameObject Restrictions;

	public void showRoster()
	{
		Roster.SetActive(true);
		Armory.SetActive(false);
		Teams.SetActive(false);
		Battlefield.SetActive(false);
		Setup.SetActive(false);
		Restrictions.SetActive(false);
	}

	public void showArmory()
	{
		Roster.SetActive(false);
		Armory.SetActive(true);
		Teams.SetActive(false);
		Battlefield.SetActive(false);
		Setup.SetActive(false);
		Restrictions.SetActive(false);
	}

	public void showTeams()
	{
		Roster.SetActive(false);
		Armory.SetActive(false);
		Teams.SetActive(true);
		Battlefield.SetActive(false);
		Setup.SetActive(false);
		Restrictions.SetActive(false);
	}

	public void showBattlefield()
	{
		Roster.SetActive(false);
		Armory.SetActive(false);
		Teams.SetActive(false);
		Battlefield.SetActive(true);
		Setup.SetActive(false);
		Restrictions.SetActive(false);
	}

	public void showSetup()
	{
		Roster.SetActive(false);
		Armory.SetActive(false);
		Teams.SetActive(false);
		Battlefield.SetActive(false);
		Setup.SetActive(true);
		Restrictions.SetActive(false);
	}

	public void showRestrictions()
	{
		Roster.SetActive(false);
		Armory.SetActive(false);
		Teams.SetActive(false);
		Battlefield.SetActive(false);
		Setup.SetActive(false);
		Restrictions.SetActive(true);
	}
}
