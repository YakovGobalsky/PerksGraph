using UnityEngine;

namespace Opaq.Utils.TimeControl {
	public class TimeToken: ITimeToken {
		public float TimeScale { get; private set; }

		public event System.Action<TimeToken> OnDisposed;

		public TimeToken (float scale) {
			TimeScale = scale;
		}

		public void Dispose () {
			OnDisposed?.Invoke(this);
		}
	}
}