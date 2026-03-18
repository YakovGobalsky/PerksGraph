using Reflex.Core;
using UnityEngine;

public static class ReflexExtensions {
	public static T RegisterComponentInNewPrefab<T>(this ContainerBuilder builder, T prefab) where T : Component {
		var obj = GameObject.Instantiate(prefab);
		builder.RegisterValue(obj);
		return obj;
	}

	public static T RegisterComponentOnNewGameObject<T>(this ContainerBuilder builder) where T : Component {
		var obj = new GameObject($"({typeof(T).ToString()})").AddComponent<T>();
		builder.RegisterValue(obj);
		return obj;
	}
}