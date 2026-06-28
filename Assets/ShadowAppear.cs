using UnityEngine;

public class ShadowAppear : MonoBehaviour
{
    public GameObject shadowFigure;

    void Start()
    {
        InvokeRepeating("ShowShadow", 10f, 15f);
    }

    void ShowShadow()
    {
        StartCoroutine(ShadowRoutine());
    }

    System.Collections.IEnumerator ShadowRoutine()
    {
        shadowFigure.SetActive(true);

        yield return new WaitForSeconds(4f);

        shadowFigure.SetActive(false);
    }
}