using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ReadNote : MonoBehaviour
{
    public GameObject noteUI;
    public GameObject pressEText;

    public Transform player;
    public float interactionDistance = 3f;

    private bool noteShowing = false;
    private float timer = 0f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Show "Press E"
        if (distance <= interactionDistance && !noteShowing)
        {
            pressEText.SetActive(true);

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                noteUI.SetActive(true);
                pressEText.SetActive(false);

                noteShowing = true;
                timer = 5f;
            }
        }
        else if (!noteShowing)
        {
            pressEText.SetActive(false);
        }

        // Countdown for note
        if (noteShowing)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                noteUI.SetActive(false);
                noteShowing = false;
            }
        }
    }
}