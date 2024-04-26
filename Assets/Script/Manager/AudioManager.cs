using UnityEngine.Audio;
using UnityEngine;
using Game.System;

namespace Managers
{
    class AudioManager
    {
        public static void PlaySound(string path)
        {
            AudioClip clip= LoadAudioClip(path);
            EventSystem.Send(new PlaySoundEvent(clip));
        }

        public static void PlayMusic(string path)
        {
            AudioClip clip = LoadAudioClip(path);
            EventSystem.Send(new PlayMusicEvent(clip));
        }

        public static void StopMusic()
        {
            EventSystem.Send<PauseMusciTriggerEvent>();
        }

        static AudioClip LoadAudioClip(string path)
        {
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip == null)
            {
                Debug.LogError(path + " 没有找到相应文件");
                return null;
            }
            return clip;
        }

    }
}
