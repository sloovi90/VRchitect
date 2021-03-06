using UnityEngine;
using System.Collections;
using VRTK;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(SteamVR_TrackedObject))]
public class test : MonoBehaviour {
	public enum ops {NONE=0,UP,RIGHT,LEFT,DOWN};
	public ops pressed=ops.NONE;
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	RadialMenu menu;

	RadialMenu LeftMenu;
	public bool showingMenu=false;
	// Use this for initialization

	public void padUpButtonHandler(){
		pressed = ops.UP;
		Collider col = GameObject.Find ("Controller (right)").GetComponent<VRTK_SimplePointerRight> ().col;
		if (this.gameObject.transform.parent.gameObject == GameObject.Find ("Controller (left)")) {
			

			//bool rayhit = GameObject.Find ("Controller (right)").GetComponent<VRTK_SimplePointerRight> ().rayHit;
			if ((GameObject.Find ("Controller (right)").GetComponentInChildren<test> ().pressed) == ops.UP && col != null) {
				col.transform.localScale *= 1.01f;
			}
			if ((GameObject.Find ("Controller (right)").GetComponentInChildren<test> ().pressed) == ops.LEFT && col != null) {
				GameObject pt = col.gameObject.transform.parent.gameObject.GetComponent<VRTK_SimplePointerRight> ().pointerTip;
				Vector3 axis= (pt.transform.position - col.gameObject.transform.parent.gameObject.transform.position);
				axis = Vector3.Cross (new Vector3 (0, 1, 0), axis);
				axis.Normalize ();
				int angleDelta = 5;
				col.transform.RotateAround (col.bounds.center, axis, angleDelta);
				
			}

		}
		if (this.gameObject.transform.parent.gameObject == GameObject.Find ("Controller (right)")) {
			
			menu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("ScaleUp");
			menu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			menu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			menu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("ScaleDown");

			LeftMenu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("ScaleUp");
			LeftMenu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			LeftMenu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			LeftMenu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("ScaleDown");


		}
		if ((GameObject.Find ("Controller (right)").GetComponentInChildren<test> ().pressed) == ops.RIGHT && col != null) {
			GameObject pt = col.gameObject.transform.parent.gameObject.GetComponent<VRTK_SimplePointerRight> ().pointerTip;
			Vector3 axis= (pt.transform.position - col.gameObject.transform.parent.gameObject.transform.position);
			axis.Normalize ();
			col.gameObject.transform.position += 0.1f*axis;
		}

	
			

	}
	public void TurnOnOffMenu(GameObject controller){
		Vector3 newPos = GameObject.Find ("Canvas").gameObject.transform.localPosition;
		if(!showingMenu)
			newPos.z = 1.5f;
		else
			newPos.z = -1.5f;

		GameObject.Find ("Canvas").gameObject.transform.localPosition = newPos;
		showingMenu = !showingMenu;
//		controller.GetComponent<VRTK_SimplePointerRight> ().menuOn = !controller.GetComponent<VRTK_SimplePointerRight> ().menuOn;
		menu.menuButtons[0].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("X_icon");
		menu.menuButtons[1].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("left");
		menu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
		menu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("right");

		LeftMenu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite =Resources.Load<UnityEngine.Sprite> ("X_icon");
		LeftMenu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite =    Resources.Load<UnityEngine.Sprite> ("left");
		LeftMenu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
		LeftMenu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite     = Resources.Load<UnityEngine.Sprite> ("right");
	}

	public void padDownButtonHandler(){
		pressed = ops.DOWN;
	
		GameObject rightController = GameObject.Find ("Controller (right)");
		if (this.gameObject.transform.parent.gameObject == rightController) {
			menu.menuButtons[0].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("X_icon");
			menu.menuButtons[1].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("left");
			menu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			menu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("right");

			LeftMenu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite =Resources.Load<UnityEngine.Sprite> ("X_icon");
			LeftMenu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite =    Resources.Load<UnityEngine.Sprite> ("left");
			LeftMenu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			LeftMenu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite     = Resources.Load<UnityEngine.Sprite> ("right");


		}
		Collider col = rightController.GetComponent<VRTK_SimplePointerRight> ().col;
		if ((rightController.GetComponentInChildren<test> ().pressed) == ops.UP && col != null) {
			col.transform.localScale *= 0.99f;
		}

		if ((rightController.GetComponentInChildren<test> ().pressed) == ops.LEFT && col!=null) {
			GameObject pt = col.gameObject.transform.parent.gameObject.GetComponent<VRTK_SimplePointerRight> ().pointerTip;
			Vector3 axis= (pt.transform.position - col.gameObject.transform.parent.gameObject.transform.position);
			axis = Vector3.Cross (new Vector3 (0, 1, 0), axis);
			axis.Normalize ();
			int angleDelta = 5;

			col.transform.RotateAround (col.bounds.center, axis, -angleDelta);

		}
		if ((rightController.GetComponentInChildren<test> ().pressed) == ops.RIGHT && col != null) {
			GameObject pt = col.gameObject.transform.parent.gameObject.GetComponent<VRTK_SimplePointerRight> ().pointerTip;
			Vector3 axis= (pt.transform.position - col.gameObject.transform.parent.gameObject.transform.position);
			axis.Normalize ();
			col.gameObject.transform.position -= 0.1f*axis;
		}

	}




	public void padRightButtonHandler(){
		pressed = ops.RIGHT;
		GameObject rightController = GameObject.Find ("Controller (right)");
		Collider col = rightController.GetComponent<VRTK_SimplePointerRight> ().col;
	/*	if (rightController.GetComponent<VRTK_SimplePointerRight> ().menuOn && this.gameObject.transform.parent.gameObject == GameObject.Find ("Controller (left)")) {
			GameObject.Find ("GameController").GetComponent<ScrollRectSnap_CS> ().ScrollRight ();
			Debug.Log ("scroll menu right");
		}*/
		if ((rightController.GetComponentInChildren<test> ().pressed) == ops.LEFT && col!=null ) {
			Vector3 axis = new Vector3 (0, 1, 0);
			int angleDelta = 5;
			col.transform.RotateAround (col.bounds.center, axis, -angleDelta);
		}
		if (this.gameObject.transform.parent.gameObject ==rightController) {

			menu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("PushIcon");
			menu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			menu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("X_icon");
			menu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("PullIcon");
			LeftMenu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite  = Resources.Load<UnityEngine.Sprite> ("PushIcon");
			LeftMenu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite  = Resources.Load<UnityEngine.Sprite> ("X_icon");
			LeftMenu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite  = Resources.Load<UnityEngine.Sprite> ("X_icon");
			LeftMenu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite  = Resources.Load<UnityEngine.Sprite> ("PullIcon");

		}
		if (rightController.GetComponentInChildren<test> ().pressed == ops.DOWN) {
			GameObject cameraRig=GameObject.Find("[CameraRig]");
			cameraRig.transform.RotateAround(cameraRig.transform.position,Vector3.up,10);

		}


	}
	public void padLeftButtonHandler(){
		pressed = ops.LEFT;
		GameObject rightController = GameObject.Find ("Controller (right)");

/*		if (rightController.GetComponent<VRTK_SimplePointerRight> ().menuOn  && this.gameObject.transform.parent.gameObject == GameObject.Find ("Controller (left)")) {
			GameObject.Find ("GameController").GetComponent<ScrollRectSnap_CS> ().ScrollLeft ();
			Debug.Log ("scroll menu left");
		}*/
		if (this.gameObject.transform.parent.gameObject == rightController) {

			menu.menuButtons[0].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("UpRotateIcon");
			menu.menuButtons[1].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("LeftRotateIcon");
			menu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("DownRotateIcon");
			menu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("rightRotateIcon");
			LeftMenu.menuButtons [0].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite  =  Resources.Load<UnityEngine.Sprite> ("UpRotateIcon");
			LeftMenu.menuButtons [1].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite  =  Resources.Load<UnityEngine.Sprite> ("LeftRotateIcon");
			LeftMenu.menuButtons [2].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite   = Resources.Load<UnityEngine.Sprite> ("DownRotateIcon");
			LeftMenu.menuButtons [3].GetComponentInChildren<RadialButtonIcon> (true).gameObject.GetComponent<Image> ().sprite    = Resources.Load<UnityEngine.Sprite> ("rightRotateIcon");

		}

		Collider col = GameObject.Find ("Controller (right)").GetComponent<VRTK_SimplePointerRight> ().col;
		//bool rayhit = GameObject.Find ("Controller (right)").GetComponent<VRTK_SimplePointerRight> ().rayHit;

		if ((GameObject.Find ("Controller (right)").GetComponentInChildren<test> ().pressed) == ops.LEFT && col!=null) {
			Vector3 axis = new Vector3 (0, 1, 0);
			int angleDelta = 5;
			col.transform.RotateAround (col.bounds.center, axis, angleDelta);
		}
		if (rightController.GetComponentInChildren<test> ().pressed == ops.DOWN) {
			GameObject cameraRig=GameObject.Find("[CameraRig]");
			cameraRig.transform.RotateAround(cameraRig.transform.position,Vector3.up,-10);

		}


	}


	// Use this for initialization
	void Start () {
		menu = GameObject.Find ("Controller (left)").GetComponentInChildren<RadialMenu> ();
		LeftMenu = GameObject.Find ("LeftMenu").GetComponentInChildren<RadialMenu> (true);


	}
	
	// Update is called once per frame
	void Update () {
		/*menuRight = GameObject.Find ("RightMenu").GetComponentInChildren<RadialMenu> ();
		Debug.Log (GameObject.Find ("RightMenu"));
		menuRight.menuButtons[0].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("UpRotateIcon");
		menuRight.menuButtons[1].GetComponentInChildren<RadialButtonIcon>().gameObject.GetComponent<Image>().sprite  = Resources.Load<UnityEngine.Sprite> ("LeftRotateIcon");
		menuRight.menuButtons [2].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("DownRotateIcon");
		menuRight.menuButtons [3].GetComponentInChildren<RadialButtonIcon> ().gameObject.GetComponent<Image> ().sprite = Resources.Load<UnityEngine.Sprite> ("rightRotateIcon");
*/
	}
}
