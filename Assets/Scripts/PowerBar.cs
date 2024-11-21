using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PowerBar : MonoBehaviour
{
    public Slider powerSlider;
    public float chargeRate = 1f;
    private bool isTakingDamage = false;

    //private void Start()
    //{
        //pv = GetComponent<PhotonView>();
        //currentHealth = maxHealth;
    //}

    private void Update()
    {
        if (!isTakingDamage && powerSlider.value < powerSlider.maxValue) powerSlider.value += chargeRate * Time.deltaTime;
    }

    public void ResetPower()
    {
        powerSlider.value = 0;
    }

    public bool IsPowerReady()
    {
        return powerSlider.value >= powerSlider.maxValue;
    }

    public void SetTakingDamage(bool takingDamage)
    {
        isTakingDamage = takingDamage;
    }
}
