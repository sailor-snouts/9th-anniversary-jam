using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite closed = null;
    [SerializeField] private Sprite open = null;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private bool isOpen = false;
    
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.boxCollider = GetComponent<BoxCollider2D>();
    }

    [Button]
    public void Toggle()
    {
        this.isOpen = !this.isOpen;
        this.sprite.sprite = this.isOpen ? this.open : this.closed;
        this.boxCollider.isTrigger = this.isOpen;
    }
}
