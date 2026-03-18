using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Opaq.Utils.UI {
	public class DraggableUIItem: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {
		[SerializeField] private int fingerId;

		[SerializeField] private UnityEvent<Vector3> OnDragBeginEvent;
		[SerializeField] private UnityEvent<Vector3> OnDragEvent;
		[SerializeField] private UnityEvent<Vector3> OnDragEndEvent;

		public event System.Action<Vector3> onDragBegin;
		public event System.Action<Vector3> onDrag;
		public event System.Action<Vector3> onDragEnd;

		public event System.Action<Vector3> onPointerDown;
		public event System.Action<Vector3> onPointerUp;

		public void OnBeginDrag (PointerEventData eventData) {
#if !UNITY_EDITOR && !UNITY_STANDALONE_WIN
			if (fingerId >= 0 && eventData.pointerId != fingerId) {
				return;
			}
#endif
			onDragBegin?.Invoke(eventData.position);
			OnDragBeginEvent?.Invoke(eventData.position);
		}

		public void OnDrag (PointerEventData eventData) {
#if !UNITY_EDITOR && !UNITY_STANDALONE_WIN
			if (fingerId >= 0 && eventData.pointerId != fingerId) {
				return;
			}
#endif
			onDrag?.Invoke(eventData.position);
			OnDragEvent?.Invoke(eventData.position);
		}

		public void OnEndDrag (PointerEventData eventData) {
#if !UNITY_EDITOR && !UNITY_STANDALONE_WIN
			if (fingerId >= 0 && eventData.pointerId != fingerId) {
				return;
			}
#endif
			onDragEnd?.Invoke(eventData.position);
			OnDragEndEvent?.Invoke(eventData.position);
		}

		public void OnPointerDown (PointerEventData eventData) {
			onPointerDown?.Invoke(eventData.position);
		}

		public void OnPointerUp (PointerEventData eventData) {
			onPointerUp?.Invoke(eventData.position);
		}
	}
}