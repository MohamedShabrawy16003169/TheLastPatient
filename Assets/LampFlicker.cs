using UnityEngine;
using System.Collections;

public class LampFlicker : MonoBehaviour
{
    public Light lampLight;

    bool flickering = false;

    public void StartFlicker()
    {
        if (!flickering)
        {
            flickering = true;
            StartCoroutine(FlickerRoutine());
        }
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // wait random time before flicker
            yield return new WaitForSeconds(Random.Range(2f, 8f));

            // random number of flickers
            int flickerCount = Random.Range(2, 6);

            for (int i = 0; i < flickerCount; i++)
            {
                lampLight.intensity = 0f;

                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));

                lampLight.intensity = 40f;

                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            }
        }
    }
}