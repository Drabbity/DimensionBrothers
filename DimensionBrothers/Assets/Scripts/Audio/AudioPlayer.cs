using UnityEngine;

namespace DimensionBrothers.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = AudioManager.Instance;
        }

        public void PlayAudio(string audioName)
            => _audioManager.PlaySound(audioName);
    }
}