using Game.System;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.System
{
    public class AudioSystem : BaseSystem
    {
        AudioSource soundSource;
        AudioSource musicSource;

        public override void InitSystem()
        {
            soundSource = Camera.main.transform.Find("SoundSource").GetComponent<AudioSource>();
            musicSource = Camera.main.transform.Find("MusicSource").GetComponent<AudioSource>();
            RegisterEvents();
        }

        void RegisterEvents()
        {
            EventSystem.Register<PlaySoundEvent>(PlaySound);
            EventSystem.Register<PlayMusicEvent>(PlayMusic);
            EventSystem.Register<PauseMusciTriggerEvent>(PauseMusic);
        }

        private void PlaySound(PlaySoundEvent obj)
        {
            soundSource.PlayOneShot(obj.clip);
        }
        private void PlayMusic(PlayMusicEvent obj)
        {
            if (musicSource.isPlaying)
                musicSource.Stop();
            musicSource.clip = obj.clip;
            musicSource.Play();
        }
        private void PauseMusic(PauseMusciTriggerEvent obj)
        {
            musicSource.Pause();
        }
    }
}
