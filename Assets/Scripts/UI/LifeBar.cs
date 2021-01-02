using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] float offset = 0.5f;
    [SerializeField] Color lifeColor = Color.green;

    Character target;
    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        //bar = GetComponentInChildren<Image>();
    }

    void Update()
    {
        transform.position = mainCamera.WorldToScreenPoint(target.transform.position + new Vector3(0, 0, offset));
    }

    public void SetTarget(Character _target)
    {
        target = _target;
        bar.color = lifeColor;
        //GetComponent<Image>().enabled = true;
        //bar.GetComponent<Image>().enabled = true;
    }

    public void SetAmount(float _value)
    {
        bar.fillAmount = _value;
    }
}