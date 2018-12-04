using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIVM : MonoBehaviour
{

    public Health healthObject;
    public Slider slider;

    private float CurrentHealth
    {
        get { return (float)healthObject.CurrentHealth / healthObject.StartHealth; }
    }
	// Use this for initialization
	void Start () {
        healthObject.OnHealthChanged += ChangeSlider;
	}

    void ChangeSlider()
    {
        StartCoroutine(HealthDecresed());
    }


    IEnumerator HealthDecresed()
    {
        while (slider.value != CurrentHealth)
        {
            slider.value = Mathf.Lerp(slider.value, CurrentHealth, Time.deltaTime);
            yield return 0;
        }
    }
}
