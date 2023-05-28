using UnityEngine;
using UnityEngine.UI;

namespace DimensionBrothers.Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
            _musicVolumeSlider.value = _audioManager.MusicVolume;
            _sfxVolumeSlider.value = _audioManager.SFXVolume;
        }       

        public void SetMusicVolume(float volume)
        {
            _audioManager.SetMusicVolume(volume);
        }

        public void SetSFXVolume(float volume)
        {
            _audioManager.SetSFXVolume(volume);
        }
    }
}