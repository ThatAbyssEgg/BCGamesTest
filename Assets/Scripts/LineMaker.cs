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
		public Transform PCs;

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
				screenPosDepth.z = 2f;
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

				// Distributing the followers by counting the points and dividing it by the amount of followers. The result is the number of the point that gets occupied by one follower.
				// For example, for 15 points with 3 followers, points with indexes 0, 5 and 10 will be occupied

				//float occupiedPointNumber = line.positionCount / (PCs.childCount - 1);

				//if (occupiedPointNumber < 1)
				//{

				//}

				// Converting the line points into actual positions & occupying them with followers

				float drawingMin = -0.5f, drawingMax = 1.5f;
				float planeMin = -5f, planeMax = 5f;

				var ratio = (planeMax - planeMin) / (drawingMax - drawingMin);

				for (int i = 1; i < PCs.childCount; i++)
				{
					var childPosition = PCs.GetChild(i).localPosition;
					var drawingPoint = line.GetPosition((i - 1) % (PCs.childCount - 1));
					PCs.GetChild(i).localPosition= new Vector3((drawingPoint.x - drawingMin) * ratio + planeMin, childPosition.y, (drawingPoint.y - 15 - drawingMin) * ratio + planeMin); // Temp solution
				}
			}

		}
	}
}