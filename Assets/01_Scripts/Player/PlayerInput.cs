using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 PlayerMoveInput
    {
        get; 
        private set;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        PlayerMoveInput= new Vector3(h, 0, v);

        
    }
}
