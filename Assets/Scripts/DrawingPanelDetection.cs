using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
	public class DrawingPanelDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public static bool IsOverDrawingPanel;

		public void OnPointerEnter(PointerEventData eventData)
		{
			IsOverDrawingPanel = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			IsOverDrawingPanel = false;
		}
	}
}