using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite closed;
    [SerializeField] private Sprite open;
    private SpriteRenderer sprite;
    private BoxCollider2D collider;
    private bool isOpen = false;
    
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<BoxCollider2D>();
    }

    [Button]
    public void Toggle()
    {
        this.isOpen = !this.isOpen;
        this.sprite.sprite = this.isOpen ? this.open : this.closed;
        this.collider.isTrigger = this.isOpen;
    }
}
