using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int playerCount = 0;
    private List<Health> playersHealth = new List<Health>();
    
    private void Start()
    {
        foreach (PlayerController player in Resources.FindObjectsOfTypeAll(typeof(PlayerController)))
        {
            GameObject obj = player.gameObject;
            if (EditorUtility.IsPersistent(obj.transform.root.gameObject) &&
                !(obj.hideFlags == HideFlags.NotEditable || obj.hideFlags == HideFlags.HideAndDontSave))
            {
                continue;
            }
            else
            {
                this.playersHealth.Add(obj.GetComponent<Health>());
            }
        }

        this.playerCount = this.playersHealth.Count;
        this.Register();
    }

    private void OnDestroy()
    {
        this.Unregister();
    }

    private void OnPlayerDealth()
    {
        this.playerCount--;
        if (this.playerCount <= 0f)
        {
            this.GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void Register()
    {
        this.playersHealth.ForEach((p) => { p.onDeath += OnPlayerDealth; });
    }

    private void Unregister()
    {
        this.playersHealth.ForEach((p) => { p.onDeath -= OnPlayerDealth; });
    }
}
