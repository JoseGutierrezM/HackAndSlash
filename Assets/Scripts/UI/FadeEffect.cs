using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    Color render;
    public Image image;

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        /*for (float fade = 0; fade <= 1; fade += 0.05f)
        {
            render.a = fade;
            image.color = render;
            yield return new WaitForSeconds(0.05f);
        }*/

        for (float fade = 1; fade >= 0; fade -= 0.05f)
        {
            render.a = fade;
            image.color = render;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        for (float fade = 0; fade <= 1; fade += 0.05f)
        {
            render.a = fade;
            image.color = render;
            yield return new WaitForSeconds(0.05f);
        }
        //gameObject.SetActive(false);
    }
}