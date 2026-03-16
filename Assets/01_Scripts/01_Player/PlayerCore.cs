using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public CharacterController Controller {  get; private set; }
    public static PlayerCore Instance;
    void Awake()
    {
        Controller = GetComponent<CharacterController>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
