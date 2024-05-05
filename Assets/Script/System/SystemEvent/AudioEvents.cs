using UnityEngine;
namespace Game.System
{
    struct PlaySoundEvent:IEvent
    {
        public AudioClip clip;
        public PlaySoundEvent(AudioClip clip)
        {
            this.clip = clip;
        }
    }

    struct PlayMusicEvent:IEvent
    {
        public AudioClip clip;
        public PlayMusicEvent(AudioClip clip)
        {
            this.clip = clip;
        }
    }

    struct PauseMusciTriggerEvent:IEvent
    { }
}
