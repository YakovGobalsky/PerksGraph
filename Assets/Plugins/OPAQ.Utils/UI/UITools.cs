using UnityEngine;

namespace Opaq.Utils {
  public class UITools: MonoBehaviour {
		private Rect curSafeArea = new Rect(0, 0, 0, 0);

		public Rect activeSafeArea { get; private set; } = new Rect(0, 0, 0, 0);

		private float offsetTop = 0;
		private float offsetBottom = 0;

		//see: https://docs.unity3d.com/ScriptReference/Screen-safeArea.html
		public event System.Action<Rect> OnSafeAreaChanged;

		private void Awake () {
			CheckSafeAreaChanges();
		}

		private void Update () {
			CheckSafeAreaChanges();
		}

		public void SetOffset(float top, float bottom) {
			offsetTop = top;
			offsetBottom = bottom;

			UpdateSafeArea();
		}

		private void CheckSafeAreaChanges() {
			Rect safeArea = Screen.safeArea;

			if (safeArea != curSafeArea) {
				curSafeArea = safeArea;
				UpdateSafeArea();
			}
		}

		private void UpdateSafeArea() {
			var area = curSafeArea;

			//Debug.Log($"yMin:{area.yMin}, yMax:{area.yMax}");

			area.yMin = Mathf.Max(area.yMin, offsetBottom); // offset calc is ads work
			area.yMax -= offsetTop; //2do: repalce to min
			//area.yMax = Mathf.Min(area.yMax, offsetTop);

			activeSafeArea = area;

			OnSafeAreaChanged?.Invoke(activeSafeArea);
		}

	}
}