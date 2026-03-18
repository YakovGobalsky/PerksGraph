using UnityEngine;

namespace Opaq.Utils.Localization {
	[RequireComponent(typeof(SpriteRenderer))]
	public class LocalizedSprite: LocalizedBehaviour {
		[System.Serializable]
		public class ImageVariant {
			public Locale language;
			public Sprite sprite;
		}

		[SerializeField] private ImageVariant[] imageVariants;

		private SpriteRenderer _sr;

		private void Awake () {
			_sr = GetComponent<SpriteRenderer>();
		}

		protected override void UpdateLocalizedInfo (Locale locale) {
			foreach (var variant in imageVariants) {
				if (variant.language == locale) {
					_sr.sprite = variant.sprite;
					return;
				}
			}
		}
	}
}
