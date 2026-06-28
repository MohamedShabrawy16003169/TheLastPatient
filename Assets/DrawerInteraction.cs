using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DrawerInteraction : MonoBehaviour
{
    public Transform drawer;

    public TextMeshProUGUI interactText;
    public TextMeshProUGUI readText;

    public AudioSource whisperSound;
    public AudioSource distortionSound;

    public LampFlicker lampFlicker;

    public GameObject distortionImage;

    // ENDING SCREEN
    public GameObject endingScreen;

    private bool playerInRange = false;
    private bool opened = false;
    private bool reading = false;

    void Start()
    {
        interactText.gameObject.SetActive(false);
        readText.gameObject.SetActive(false);

        // HIDE DISTORTION AT START
        distortionImage.SetActive(false);

        // HIDE ENDING SCREEN AT START
        endingScreen.SetActive(false);
    }

    void Update()
    {
        if (playerInRange)
        {
            // FIRST INTERACTION
            if (!opened)
            {
                interactText.text = "Press E to Open Drawer";

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    opened = true;

                    Debug.Log("TRYING TO FLICKER");

                    // START FLICKER
                    lampFlicker.StartFlicker();

                    // OPEN DRAWER
                    drawer.localPosition += new Vector3(0.5f, 0f, 0f);

                    // CHANGE MESSAGE
                    interactText.text = "Press E to Read Card";

                    // PLAY WHISPER AFTER 3 SECONDS
                    Invoke("PlayWhisper", 3f);

                    // SHOW DISTORTION AFTER 10 SECONDS
                    Invoke("ShowDistortion", 10f);
                }
            }

            // SECOND INTERACTION
            else if (opened && !reading)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    reading = true;

                    interactText.gameObject.SetActive(false);

                    // SHOW PATIENT CARD TEXT
                    readText.gameObject.SetActive(true);

                    // HIDE AFTER 6 SECONDS
                    Invoke("HideReadText", 6f);
                }
            }
        }
    }

    void PlayWhisper()
    {
        if (whisperSound != null)
        {
            whisperSound.Play();
        }
    }

    void ShowDistortion()
    {
        distortionImage.SetActive(true);

        if (distortionSound != null)
        {
            distortionSound.Play();
        }

        // SHOW ENDING SCREEN AFTER 5 SECONDS
        Invoke("ShowEnding", 5f);
    }

    void ShowEnding()
    {
        endingScreen.SetActive(true);
    }

    void HideReadText()
    {
        readText.gameObject.SetActive(false);
        reading = false;

        if (playerInRange)
        {
            interactText.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            interactText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            interactText.gameObject.SetActive(false);
            readText.gameObject.SetActive(false);
        }
    }
}