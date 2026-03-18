using System.Collections.Generic;
using UnityEngine;

namespace Opaq.Utils.TimeControl {
	public class TimeScaleControl {
		public event System.Action<ITimeToken> OnTimeScaleChanged;
		public event System.Action<ITimeToken> OnTimeTokenAdded;

		private readonly List<ITimeToken> _tokensStack = new(); //да, это список, хоть и стек

		public void SlowdownTime (ITimeToken token) {
			token.OnDisposed += OnTokenDisposed;
			_tokensStack.Add(token);

			UpdateTimeScale();

			OnTimeTokenAdded?.Invoke(token);
		}

		private void OnTokenDisposed (ITimeToken token) {
			_tokensStack.Remove(token);
			UpdateTimeScale();
		}

		private void UpdateTimeScale () {
			if (_tokensStack.Count > 0) {
				var token = _tokensStack[_tokensStack.Count - 1];
				OnTimeScaleChanged?.Invoke(token);
				Time.timeScale = token.TimeScale;
			} else {
				Time.timeScale = 1f;
				OnTimeScaleChanged?.Invoke(null);
			}
		}
	}
}