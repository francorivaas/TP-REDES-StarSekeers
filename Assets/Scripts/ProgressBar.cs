using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float currentValue = 100f;
    [SerializeField] private Image radialIndicatorUI = null;
    [SerializeField] private Image ImageIcon = null;
    [SerializeField] private float speed;
    private bool shouldUpdate = false;
    [SerializeField] private List<Sprite> Icons;

    private void Start()
    {
        radialIndicatorUI.color = new Color(255, 255, 255, 0);
        ImageIcon.enabled = false;
        currentValue = 0;
    }

    public void Set(Color _color, int Listpos)
    {
        radialIndicatorUI.color = _color;
        ImageIcon.sprite = Icons[Listpos];
        currentValue = 100f;
        ImageIcon.enabled = true;
    }
    void Update()
    {
        if (currentValue >= 0)
        {
            currentValue -= speed * Time.deltaTime;
            radialIndicatorUI.fillAmount = currentValue / 100;
        }
        else
        {
            ImageIcon.enabled = false;
        }
    }
}
