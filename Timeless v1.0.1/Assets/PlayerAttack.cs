using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackObject;

    public Sprite idleSprite;
    private SpriteRenderer spriteRender;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           print("tuvieja");
        }
       
    }
}

