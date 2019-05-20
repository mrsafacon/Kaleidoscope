using UnityEngine;
using System.Collections;

public class ScreenTouch : MonoBehaviour { //touch motions designed for mobile / touchscreen
	CaptureCam screen;

	Vector2 oldTouchVector;
	float oldTouchDistance;

	Vector2 center;

	Vector2?[] oldTouchPositions = {
		null,
		null
	};



	void Start () {
		center = new Vector2(Screen.width / 2, Screen.height / 2);
		screen = GetComponent<CaptureCam>();
	}
	

	void Update () {
		switch(Input.touchCount){
		case 0:
			oldTouchPositions[0] = null;
			oldTouchPositions[1] = null;
			break;
		case 1:
			//rotation
			if(oldTouchPositions[0] == null){
				oldTouchPositions[0] = Input.GetTouch(0).position - center;
				oldTouchPositions[1] = null;
			} else {
				Vector2 newTouchPosition = Input.GetTouch(0).position - center;

				//figure direction and get vel
				Vector2 diffVector = newTouchPosition - oldTouchVector;
				Vector2 absDiffVector = new Vector2(Mathf.Abs(diffVector.x), Mathf.Abs(diffVector.y));
				if(absDiffVector.x > absDiffVector.y){
					float vel = diffVector.x;
					if(newTouchPosition.y < 0) screen.Rotate(VelocityDirection.clockwise, vel);
					else if (newTouchPosition.y > 0)  screen.Rotate(VelocityDirection.counterclockwise, vel);
				} else if(absDiffVector.y > absDiffVector.x){
					float vel = diffVector.y;
					if(newTouchPosition.x < 0) screen.Rotate(VelocityDirection.clockwise, vel);
					else if (newTouchPosition.x > 0)  screen.Rotate(VelocityDirection.counterclockwise, vel);
				} else {
					//zero vel
				}

				oldTouchPositions[0] = newTouchPosition;
				oldTouchPositions[1] = null;
			}
			break;
		case 2:
			//zoom
			if(oldTouchPositions[1] == null){
				oldTouchPositions[0] = Input.GetTouch(0).position;
				oldTouchPositions[1] = Input.GetTouch(1).position;
				oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
				oldTouchDistance = oldTouchVector.magnitude;
			} else {
				Vector2[] newTouchPositions = {
					Input.GetTouch(0).position,
					Input.GetTouch(1).position
				};
				Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
				float newTouchDistance = newTouchVector.magnitude;
				screen.Zoom(newTouchDistance - oldTouchDistance);

				oldTouchPositions[0] = newTouchPositions[0];
				oldTouchPositions[1] = newTouchPositions[1];
				oldTouchVector = newTouchVector;
				oldTouchDistance = newTouchDistance;
			}
			break;
		}
	}
}
