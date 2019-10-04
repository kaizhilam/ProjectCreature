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
    private HelpfulTextScript textScript;


    // Start is called before the first frame update
    void Start()
    {
        textScript = GameObject.Find("HelpfulText").GetComponent<HelpfulTextScript>();
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
            if (!_player.GetComponent<AbilityDash>())
            {
                DashImage.fillAmount = 1;
                _player.AddComponent<AbilityDash>();
                textScript.DisplayHelpfulMessage("Press Q to Dash!");
            }
            
        }
        else if (abilityCrystal.gameObject.CompareTag("DoubleJump"))
        {
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            if (!_player.GetComponent<AbilityDoubleJump>())
            {
                DJumpImage.fillAmount = 1;
                AbilityDoubleJump newComp = _player.AddComponent<AbilityDoubleJump>();
                playSound();
                newComp.wingPrefab = wingPrefab;
                textScript.DisplayHelpfulMessage("Press Space twice to double jump!");
            }

        }
        else if (abilityCrystal.gameObject.CompareTag("WallClimb"))
        {
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            if (!_player.GetComponent<AbilityWallClimb>())
            {
                ClimbImage.fillAmount = 1;
                _player.AddComponent<AbilityWallClimb>();
                textScript.DisplayHelpfulMessage("Hold Space against a wall to climb it!");
            }

        }
        else if (abilityCrystal.gameObject.CompareTag("Grapple"))
        {
            playSound();
            abilityCrystal.gameObject.SetActive(false);
            if (!_player.GetComponent<AbilityGrapple>())
            {
                GrappleImage.fillAmount = 1;
                _player.AddComponent<AbilityGrapple>();
                textScript.DisplayHelpfulMessage("Right click to Grapple!");
            }
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