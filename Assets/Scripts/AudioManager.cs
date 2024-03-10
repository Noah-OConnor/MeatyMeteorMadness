using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(sounds[6].clip.length - sounds[6].source.time);
        sounds[5].source.Play(); // 5 is in_game_music_start
        sounds[6].source.Stop(); // 6 is menu_music_loop
        yield return new WaitForSeconds(sounds[5].clip.length);
        sounds[4].source.Play(); // 4 is in_game_music_loop
        sounds[5].source.Stop();
    }

    public IEnumerator Death()
    {
        Stop(6); // 6 is menu_music_loop
        Stop(5); // 5 is in_game_music_start
        Stop(4); // 4 is in_game_music_loop
        yield return new WaitForSeconds(1);
        Play(1); // 1 is death_music_start
        yield return new WaitForSeconds(sounds[1].clip.length);
        sounds[0].source.Play(); // 0 is death_music_loop
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No sound found");
            return;
        }
        s.source.Play();
    }

    public void Play (int num)
    {
        Sound s = sounds[num];
        if (s == null)
        {
            Debug.Log("No sound found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No sound found");
            return;
        }
        s.source.Stop();
    }

    public void Stop(int num)
    {
        Sound s = sounds[num];
        if (s == null)
        {
            Debug.Log("No sound found");
            return;
        }
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No sound found");
            return;
        }
        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No sound found");
            return;
        }
        s.source.UnPause();
    }
}
