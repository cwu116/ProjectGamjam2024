using Game.System;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.System
{
    public class AudioSystem : BaseSystem
    {
        public AudioSource soundSource;
        public AudioSource musicSource;

        public override void InitSystem()
        {
            RegisterEvents();
        }

        void RegisterEvents()
        {
            EventSystem.Register<PlaySoundEvent>(PlaySound);
            EventSystem.Register<PlayMusicEvent>(PlayMusic);
            EventSystem.Register<PauseMusciTriggerEvent>(PauseMusic);
        }

        ~AudioSystem()
        {
            EventSystem.UnRegister<PlaySoundEvent>(PlaySound);
            EventSystem.UnRegister<PlayMusicEvent>(PlayMusic);
            EventSystem.UnRegister<PauseMusciTriggerEvent>(PauseMusic);
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
