using UnityEngine;

namespace Opaq.Utils {
	public class SoundManager: MonoBehaviour {
		[Header("AudioSources")]
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioSource musicSource;

		[Header("Files")]
		[SerializeField] private AudioClip _soundClick;

		public void PlayOnce (AudioClip clip) {
			if (clip) {
				audioSource.PlayOneShot(clip);
			}
		}

		public void PlayMusic (AudioClip clip) {
			if (clip) {
				musicSource.clip = clip;
				musicSource.Play();
			}
		}

		public void PlayClickSound () => PlayOnce(_soundClick);
	}
}