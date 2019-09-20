using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    private GameObject _player;
    public Image DashImage;
    public Image DJumpImage;
    public Image ClimbImage;
    public Image GrappleImage;
    public GameObject wingPrefab;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        DashImage.fillAmount = 0;
        DJumpImage.fillAmount = 0;
        ClimbImage.fillAmount = 0;
        GrappleImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider abilityCrystal)
    {
        if (abilityCrystal.gameObject.CompareTag("Dash"))
        {
            abilityCrystal.gameObject.SetActive(false);
            DashImage.fillAmount = 1;
            _player.AddComponent<AbilityDash>();
        }
        else if (abilityCrystal.gameObject.CompareTag("DoubleJump"))
        {
            abilityCrystal.gameObject.SetActive(false);
            DJumpImage.fillAmount = 1;
            AbilityDoubleJump newComp = _player.AddComponent<AbilityDoubleJump>();
            newComp.wingPrefab = wingPrefab;
        }
        else if (abilityCrystal.gameObject.CompareTag("WallClimb"))
        {
            abilityCrystal.gameObject.SetActive(false);
            ClimbImage.fillAmount = 1;
            _player.AddComponent<AbilityWallClimb>();
        }
        else if (abilityCrystal.gameObject.CompareTag("Grapple"))
        {
            abilityCrystal.gameObject.SetActive(false);
            GrappleImage.fillAmount = 1;
            _player.AddComponent<AbilityGrapple>();
        }
    }
}