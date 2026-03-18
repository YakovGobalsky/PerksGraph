using System;

namespace Opaq.Utils.TimeControl {
	public interface ITimeToken: IDisposable {
		public float TimeScale { get; }
		public event System.Action<TimeToken> OnDisposed;
	}
}