using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance
        {
            get
            {
                if (Instance == null)
                {
                    if ((Instance = FindAnyObjectByType<AudioManager>()) == null)
                    {
                        GameObject container = new GameObject("AudioManager");
                        Instance = container.AddComponent<AudioManager>();
                        DontDestroyOnLoad(container);
                    }
                }
                return Instance;
            }
            set
            {
                Instance = value;
            }
        }

        [Header("Audio Settings")]
        [SerializeField] private int channelCount = 10;
        private List<AudioSource> channels = new List<AudioSource>();

        private void Awake()
        {
            for(int i = 0 ; i < channelCount; i++)
            {
                GameObject container = new GameObject("Channel " + i);
                container.transform.SetParent(transform);
                AudioSource audioSource = container.AddComponent<AudioSource>();
                channels.Add(audioSource);
            }
        }

        public void Play(int channel, AudioClip clip)
        {
            if (channel < 0 || channel >= channelCount)
            {
                Debug.LogError("Channel " + channel + " is out of range");
                return;
            }
            channels[channel].clip = clip;
            channels[channel].Play();
        }
        public void Pause(int channel)
        {
            if (channel < 0 || channel >= channelCount)
            {
                Debug.LogError("Channel " + channel + " is out of range");
                return;
            }
            channels[channel].Pause();
        }
        public void Stop(int channel)
        {
            if (channel < 0 || channel >= channelCount)
            {
                Debug.LogError("Channel " + channel + " is out of range");
                return;
            }
            channels[channel].Stop();
        }
    }   
}