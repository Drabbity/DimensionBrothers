using DimensionBrothers.Other;
using UnityEngine;
using UnityEngine.Audio;

namespace DimensionBrothers.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private const string _MUSIC_VOLUME_KEY = "MUSIC_VOLUME";
        private const string _SFX_VOLUME_KEY = "SFX_VOLUME";

        public float MusicVolume { get; private set; }
        public float SFXVolume { get; private set; }

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioClip _musicClip;
        [SerializeField] private AudioSource _sfxSource;

        [SerializeField] private SerializableStringDictionary<AudioClip> _sfx = new SerializableStringDictionary<AudioClip>();

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _musicAudioName;
        [SerializeField] private string _sfxAudioName;

        private void Start()
        {
            _musicSource.clip = _musicClip;
            _musicSource.Play();
            LoadVolumeSettings();
        }

        public void PlaySound(string soundName)
        {
            if(_sfx.TryGetValue(soundName, out var audioClip))
                _sfxSource.PlayOneShot(audioClip, 1f);
        }

        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(_musicAudioName, Mathf.Log10(volume) * 20);
            MusicVolume = volume;
            PlayerPrefs.SetFloat(_MUSIC_VOLUME_KEY, volume);
        }

        public void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat(_sfxAudioName, Mathf.Log10(volume) * 20);
            SFXVolume = volume;
            PlayerPrefs.SetFloat(_SFX_VOLUME_KEY, volume);
        }

        private void LoadVolumeSettings()
        {
            float musicVolume;
            float sfxVolume;

            if (PlayerPrefs.HasKey(_MUSIC_VOLUME_KEY))
                musicVolume = PlayerPrefs.GetFloat(_MUSIC_VOLUME_KEY);
            else
                musicVolume = 1;

            if (PlayerPrefs.HasKey(_SFX_VOLUME_KEY))
                sfxVolume = PlayerPrefs.GetFloat(_SFX_VOLUME_KEY);
            else
                sfxVolume = 1;

            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
        }
    }
}
