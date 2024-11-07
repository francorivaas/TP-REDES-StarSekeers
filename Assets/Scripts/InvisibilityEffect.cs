using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisibilityEffect : MonoBehaviour
{
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private GameObject spaceship;
    [SerializeField] private Image Borde;
    [SerializeField] private Image Rombo1;
    [SerializeField] private Image Rombo2;
    [SerializeField] private Image Rombo3;
    [SerializeField] private Image Fondo;
    [SerializeField] private GameObject Pointer;

    public void InvisibilitySwitch(bool _bool)
    {
        spaceship.GetComponent<SpriteRenderer>().enabled = _bool;
        Rombo1.GetComponent<SpriteRenderer>().enabled = _bool;
        Rombo2.GetComponent<SpriteRenderer>().enabled = _bool;
        Rombo3.GetComponent<SpriteRenderer>().enabled = _bool;
        Borde.GetComponent<SpriteRenderer>().enabled = _bool;
        lifeBarFill.GetComponent<SpriteRenderer>().enabled = _bool;
        Fondo.GetComponent<Image>().enabled = _bool;
        Pointer.GetComponent<SpriteRenderer>().enabled = _bool;
    }

}
