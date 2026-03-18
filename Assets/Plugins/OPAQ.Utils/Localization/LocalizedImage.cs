using UnityEngine;
using UnityEngine.UI;

namespace Opaq.Utils.Localization {
	[RequireComponent(typeof(Image))]
	public class LocalizedImage: LocalizedBehaviour {
		[System.Serializable]
		public class ImageVariant {
			public Locale language;
			public Sprite sprite;
		}

		[SerializeField] private ImageVariant[] imageVariants;

		private Image image;

		private void Awake () {
			image = GetComponent<Image>();
		}

		protected override void UpdateLocalizedInfo (Locale locale) {
			foreach (var variant in imageVariants) {
				if (variant.language == locale) {
					image.sprite = variant.sprite;
					return;
				}
			}
		}
	}
}