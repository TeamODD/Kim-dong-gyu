using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartJumpSoundScript : MonoBehaviour
{
    private AudioSource StartJump;
    void Start()
    {
        StartJump = GetComponent<AudioSource>();
    }
}
