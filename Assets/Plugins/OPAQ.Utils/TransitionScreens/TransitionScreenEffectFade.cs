using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Opaq.Utils {
	[RequireComponent(typeof(CanvasGroup))]
	public class TransitionScreenEffectFade: TransitionScreenEffect {
		[SerializeField] private float showTime = 1f;

		[SerializeField] private Image progressFill;
		[SerializeField] private RectTransform progressIcon;

		private CanvasGroup _canvas;

		private void Awake () {
			_canvas = GetComponent<CanvasGroup>();
			SetAlpha(0);
		}

		public override void ShowEffect (Action onDone, Action onComplete = null) {
			//base.ShowEffect(onDone, onComplete);
			StopAllCoroutines();
			StartCoroutine(ShowRoutine(onDone, onComplete));
		}

		private IEnumerator ShowRoutine (Action onDone, Action onComplete = null) {
			for (float t = 0; t <= 1f; t += Time.unscaledDeltaTime / showTime) {
				SetAlpha(Mathf.Sin(t * Mathf.PI / 2f));
				SetProgress(t);
				yield return null;
			}
			SetAlpha(1f);
			SetProgress(1f);
			onDone?.Invoke();

			for (float t = 1; t >= 0f; t -= Time.unscaledDeltaTime / showTime) {
				//SetProgress(0.5f + t / 2f);
				SetAlpha(Mathf.Sin(t * Mathf.PI / 2f));
				yield return null;
			}
			SetAlpha(0f);
			onComplete?.Invoke();
		}

		private void SetAlpha (float alpha) {
			_canvas.alpha = alpha;
		}

		private void SetProgress (float progress) {
			if (progressFill) {
				progressFill.fillAmount = progress;
			}

			if (progressIcon) {
				progressIcon.anchorMin = new Vector2(progress, 0.5f);
				progressIcon.anchorMax = new Vector2(progress, 0.5f);
			}
		}
	}
}