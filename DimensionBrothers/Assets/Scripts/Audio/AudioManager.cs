using DimensionBrothers.Other;
using UnityEngine;

namespace DimensionBrothers.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _musicClip;
        [SerializeField] private AudioSource _sfxSource;

        [SerializeField] private SerializableStringDictionary<AudioClip> _sfx = new SerializableStringDictionary<AudioClip>();

        private void Start()
        {
            _musicSource.clip = _musicClip;
            _musicSource.Play();
        }

        public void PlaySound(string soundName)
        {
            if(_sfx.TryGetValue(soundName, out var audioClip))
                _sfxSource.PlayOneShot(audioClip, 1f);
        }
    }
}
