using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider = null;
    private Image fill;
    private Camera camera;
    [SerializeField] private Gradient gradient = null;

    private void OnEnable()
    {
        this.camera = Camera.main;
        this.slider = GetComponent<Slider>();
        this.fill = this.slider.fillRect.GetComponent<Image>();
        this.fill.color = this.gradient.Evaluate(1f);
    }

    public void SetMaxHealth(float value)
    {
        this.slider.maxValue = value;
        this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
    }

    public void SetHealth(float value)
    {
        this.slider.value = value;
        this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
    }

    private void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + this.camera.transform.forward);
    }
}
