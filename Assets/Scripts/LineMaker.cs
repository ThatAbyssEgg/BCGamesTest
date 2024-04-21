using Radishmouse;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
	public class LineMaker : MonoBehaviour
	{
		public GameObject LinePrefab;
		public Transform Pattern;

		LineRenderer line;
		Vector3 previousPosition;
		bool isNewDrawing = true;

		[SerializeField]
		float minimalDistance = .1f;
		
		void Start()
		{
			line = Instantiate(LinePrefab, Pattern).GetComponent<LineRenderer>();
			previousPosition = transform.position;
		}

		private void Update()
		{
			if (Input.GetMouseButton(0) && DrawingPanelDetection.IsOverDrawingPanel)
			{
				if (isNewDrawing && line != null)
				{
					Destroy(line.gameObject);
					isNewDrawing = false;
					line = Instantiate(LinePrefab, Pattern).GetComponent<LineRenderer>();
				}

				var screenPosDepth = Input.mousePosition;
				screenPosDepth.z = 5f;
				Vector3 currentPosition = Camera.main.ScreenToWorldPoint(screenPosDepth);

				if (Vector3.Distance(currentPosition, previousPosition) > minimalDistance)
				{
					line.positionCount++;
					line.SetPosition(line.positionCount - 1, new Vector3(currentPosition.x, currentPosition.y, currentPosition.z));
					previousPosition = currentPosition;
				}
			}

			if (Input.GetMouseButtonUp(0))
			{				
				isNewDrawing = true;
			}

		}
	}
}