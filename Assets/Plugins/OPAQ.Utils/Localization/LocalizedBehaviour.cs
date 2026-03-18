using UnityEngine;
using Reflex.Attributes;
using R3;
using System;

namespace Opaq.Utils.Localization {
	public class LocalizedBehaviour: MonoBehaviour {
		private IDisposable subscriptions;

		[Inject] protected LocalizationManager localizationManager;

		protected virtual void OnEnable () {
			subscriptions = localizationManager.Locale.Subscribe(UpdateLocalizedInfo);
		}

		protected virtual void OnDisable () {
			subscriptions.Dispose();
		}

		protected virtual void UpdateLocalizedInfo (Locale locale) {
		}
	}
}