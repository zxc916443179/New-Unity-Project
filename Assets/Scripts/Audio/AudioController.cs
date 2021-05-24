using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unity.TPS.Audio {
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        public AudioClip[] clips;
        public float PlayDelay;

        float lastPlayTime;
        public AudioSource audioSource;
        private void Start() {
            lastPlayTime = -PlayDelay;
        }

        public void Play() {
            if (Time.time < lastPlayTime + PlayDelay) return;
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(clip);
            lastPlayTime = Time.time;
        }
        public void Stop() {
            lastPlayTime = -PlayDelay;
            audioSource.Stop();
        }
    }
}
