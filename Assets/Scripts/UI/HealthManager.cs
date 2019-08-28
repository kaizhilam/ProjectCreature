using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	private float _MaxHealth;
	private float _Health;

	private void Start()
	{
		_MaxHealth = Player.HP;
	}

	private void Update()
	{
		UpdateHealthBar();
		UpdateHealthText();
	}

	private void UpdateHealthBar()
	{
		_Health = Player.HP;
		Transform healthBar = transform.GetChild(0); //REFERENCE HEATHBAR
		healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(20.48f * _Health, 534); //SET HEALTH BAR WIDTH
		//Debug.Log(_Health);
	}

	private void UpdateHealthText()
	{
		Transform heathText = transform.GetChild(2);
		heathText.GetComponent<Text>().text = Mathf.Round(_Health) + "/" + _MaxHealth;
	}
}
