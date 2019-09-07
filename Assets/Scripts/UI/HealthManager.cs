using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	private float _MaxHealth;
	private float _Health;
	private Vector2 _HealthBarSize;

	private void Start()
	{
		_MaxHealth = Player.HP;
		_HealthBarSize = transform.GetChild(2).GetComponent<RectTransform>().sizeDelta;
	}

	private void Update()
	{
		UpdateHealthBar();
		UpdateHealthText();
	}

	private void UpdateHealthBar()
	{
		_Health = Player.HP;
		Transform healthBar = transform.GetChild(2); //REFERENCE HEATHBAR
		healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(_HealthBarSize.x *_Health/_MaxHealth, 160); //SET HEALTH BAR WIDTH
		//Debug.Log(_Health);
	}

	private void UpdateHealthText()
	{
		Transform heathText = transform.GetChild(1);
		heathText.GetComponent<Text>().text = Mathf.Round(_Health) + "/" + _MaxHealth;
	}
}
