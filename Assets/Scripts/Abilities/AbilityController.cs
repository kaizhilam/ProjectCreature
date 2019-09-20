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
    public AudioSource pickupSoundAudioSrc;
    public AudioClip pickupSound;


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
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            DashImage.fillAmount = 1;
            _player.AddComponent<AbilityDash>();
        }
        else if (abilityCrystal.gameObject.CompareTag("DoubleJump"))
        {
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            DJumpImage.fillAmount = 1;
            AbilityDoubleJump newComp = _player.AddComponent<AbilityDoubleJump>();
            playSound();
            newComp.wingPrefab = wingPrefab;
        }
        else if (abilityCrystal.gameObject.CompareTag("WallClimb"))
        {
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            ClimbImage.fillAmount = 1;
            _player.AddComponent<AbilityWallClimb>();
        }
        else if (abilityCrystal.gameObject.CompareTag("Grapple"))
        {
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            GrappleImage.fillAmount = 1;
            _player.AddComponent<AbilityGrapple>();
        }
    }

    public void playSound()
    {
        pickupSoundAudioSrc.pitch = 1.0f;
        pickupSoundAudioSrc.clip = pickupSound;
        pickupSoundAudioSrc.Stop();
        pickupSoundAudioSrc.Play();
    }
}