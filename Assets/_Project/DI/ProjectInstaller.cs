using Opaq.TestProject.Config;
using Opaq.Utils;
using Opaq.Utils.Localization;
using Opaq.Utils.TimeControl;
using Opaq.Utils.UI.BackButton;
using R3;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using UnityEngine.Audio;

using Opaq.TestProject;

public class ProjectInstaller : MonoBehaviour, IInstaller {
	[Header("Scriptables")]
	[SerializeField] private LocalizationManager localizationManager;
	[SerializeField] private AudioMixer _audioMixer;

	[Header("Prefabs")]
	[SerializeField] private SoundManager soundManagerPrefab;
	[SerializeField] private TransitionScreen transitionScreenPrefab;

	public void InstallBindings(ContainerBuilder builder) {
		var playerData = new PlayerData();
		var gameConfig = new GameConfig();

		var playerDataSaver = new SerializatorPlayerPrefsPlayerData(playerData);
		var configSaver = new SerializatorJSON<GameConfig>(ref gameConfig, "config");

		builder.RegisterValue(playerData);
		builder.RegisterValue(gameConfig);

		builder.RegisterType(typeof(TimeScaleControl), Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);

		DontDestroyOnLoad(builder.RegisterComponentInNewPrefab<SoundManager>(soundManagerPrefab).gameObject);

		DontDestroyOnLoad(builder.RegisterComponentInNewPrefab<TransitionScreen>(transitionScreenPrefab).gameObject);

		DontDestroyOnLoad(builder.RegisterComponentOnNewGameObject<BackButtonManager>().gameObject);
		DontDestroyOnLoad(builder.RegisterComponentOnNewGameObject<UITools>().gameObject);

		//Container.BindInterfacesAndSelfTo<SceneParams>().FromInstance(new SceneParams("main", transitionScreen)).AsSingle();

		builder.RegisterValue(localizationManager);
		{
			Debug.Log("Localization created");
			localizationManager.Init(gameConfig.Language.Value);
			localizationManager.Locale.Subscribe((locale) => gameConfig.Language.Value = locale.GetLanguageId());
		}

		gameConfig.SoundVolume.Subscribe((x) => UpdateAudioMixer("sfx", x));
		gameConfig.MusicVolume.Subscribe((x) => UpdateAudioMixer("music", x));
	}

	private void UpdateAudioMixer(string paramName, float val) {
		float dbVolume;
		if (val <= Mathf.Epsilon) {
			dbVolume = -80.0f;
		} else {
			dbVolume = Mathf.Log10(val) * 20f;
		}

		_audioMixer.SetFloat(paramName, dbVolume);
	}

}
