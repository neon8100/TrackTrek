using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {


    public SpriteRenderer fill;
    public float maxValue;
    public float currentValue;
    public float percent;

    public void LateUpdate()
    {
        percent = currentValue / maxValue;

        fill.size = new Vector2(percent, 1);
    }

}
