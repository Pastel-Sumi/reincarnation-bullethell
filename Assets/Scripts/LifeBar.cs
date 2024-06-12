using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeBar : MonoBehaviour
{
    public Image lifeBar;
    private float actualLife;
    public float maxLife;

    // Método para actualizar la vida actual
    public void UpdateLife(float newLife)
    {
        actualLife = newLife;
        lifeBar.fillAmount = actualLife / maxLife;
    }
}
