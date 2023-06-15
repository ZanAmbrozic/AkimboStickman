using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PauseMusic"))
        {
            if (music.isPlaying)
            {
                music.Pause();
            }
            else
            {
                music.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            music.volume += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            music.volume -= 0.1f;
        }

    }
}
