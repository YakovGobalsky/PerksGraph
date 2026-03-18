using R3;

namespace Opaq.TestProject.Config {
	public interface IPlayerDataStorage {
		public void RegisterValue (string key, ReactiveProperty<int> property, int defaultValue = 0);
	}
}