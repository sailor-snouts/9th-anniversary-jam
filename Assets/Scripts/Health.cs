using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float max = 100;
    [SerializeField] private float current = 100;
    [SerializeField] private bool showGui = false;
    [ShowIf("showGui")]
    [SerializeField] private GameObject gui = null;
    [ShowIf("showGui")]
    [SerializeField] private Transform guiPosition = null;
    private HealthBar healthBar = null;
    public UnityAction onDeath = null;

    private void Start()
    {
        if (this.showGui)
        {
            GameObject obj = Instantiate(this.gui, this.guiPosition);
            this.healthBar = obj.GetComponentInChildren<HealthBar>();
            this.healthBar.SetMaxHealth(this.max);
            this.healthBar.SetHealth(this.current);
        }
    }

    public void Damage(float dmg)
    {
        this.current = Mathf.Clamp(this.current - dmg, 0f, this.max);

        if (this.showGui)
        {
            this.healthBar.SetHealth(this.current);
        }

        if (this.current <= 0f)
        {
            this.onDeath.Invoke();
        }
    }

    public bool IsAlive()
    {
        return this.current > 0f;
    }

    public float GetMax()
    {
        return this.max;
    }

    public float GetCurrent()
    {
        return this.current;
    }

    public float GetCurrentNormalized()
    {
        return this.current / this.max;
    }
}
