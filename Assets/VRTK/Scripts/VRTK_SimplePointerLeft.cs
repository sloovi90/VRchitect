// Simple Pointer|Scripts|0040
namespace VRTK
{
    using UnityEngine;

    /// <summary>
    /// The Simple Pointer emits a coloured beam from the end of the controller to simulate a laser beam. It can be useful for pointing to objects within a scene and it can also determine the object it is pointing at and the distance the object is from the controller the beam is being emitted from.
    /// </summary>
    /// <remarks>
    /// The laser beam is activated by default by pressing the `Touchpad` on the controller. The event it is listening for is the `AliasPointer` events so the pointer toggle button can be set by changing the `Pointer Toggle` button on the `VRTK_ControllerEvents` script parameters.
    ///
    /// The Simple Pointer script can be attached to a Controller object within the `[CameraRig]` prefab and the Controller object also requires the `VRTK_ControllerEvents` script to be attached as it uses this for listening to the controller button events for enabling and disabling the beam. It is also possible to attach the Simple Pointer script to another object (like the `[CameraRig]/Camera (head)`) to enable other objects to project the beam. The controller parameter must be entered with the desired controller to toggle the beam if this is the case.
    /// </remarks>
    /// <example>
    /// `VRTK/Examples/003_Controller_SimplePointer` shows the simple pointer in action and code examples of how the events are utilised and listened to can be viewed in the script `VRTK/Examples/Resources/Scripts/VRTK_ControllerPointerEvents_ListenerExample.cs`
    /// </example>
    public class VRTK_SimplePointerLeft : VRTK_WorldPointer
    {
		

		GameObject pt;//point of lazer
		Collider col;
	
        [Tooltip("The thickness and length of the beam can also be set on the script as well as the ability to toggle the sphere beam tip that is displayed at the end of the beam (to represent a cursor).")]
        public float pointerThickness = 0.002f;
        [Tooltip("The distance the beam will project before stopping.")]
        public float pointerLength = 100f;
        [Tooltip("Toggle whether the cursor is shown on the end of the pointer beam.")]
        public bool showPointerTip = true;
        [Tooltip("A custom Game Object can be applied here to use instead of the default sphere for the pointer cursor.")]
        public GameObject customPointerCursor;
        [Tooltip("The layers to ignore when raycasting.")]
        public LayerMask layersToIgnore = Physics.IgnoreRaycastLayer;
		private float oldDelta;
		public Vector3 oldAngles;
		private bool rayHitflag=false;
		private Vector3 originalScale;
        private GameObject pointerHolder;
        private GameObject pointer;
        private GameObject pointerTip;
        private Vector3 pointerTipScale = new Vector3(0.05f, 0.05f, 0.05f);
        // material of customPointerCursor (if defined)
        private Material customPointerMaterial;
		// Use this for initialization
	
        protected override void OnEnable()
        {
            base.OnEnable();
            InitPointer();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (pointerHolder != null)
            {
                Destroy(pointerHolder);
            }
        }

        protected override void Update()
        {
            base.Update();
			if (pointer.gameObject.activeSelf) {
				Ray pointerRaycast = new Ray (transform.position, transform.forward);
				RaycastHit pointerCollidedWith;
				 var rayHit = Physics.Raycast (pointerRaycast, out pointerCollidedWith, pointerLength, ~layersToIgnore);
				var pointerBeamLength = GetPointerBeamLength (rayHit, pointerCollidedWith);
				SetPointerTransform (pointerBeamLength, pointerThickness);
				col = pointerCollidedWith.collider;
				 pt = col.gameObject.transform.parent.gameObject.GetComponent<VRTK_SimplePointerRight> ().pointerTip;
				if (rayHit && col.gameObject.transform.parent.gameObject == GameObject.Find ("Controller (right)") && (((int)GameObject.Find ("Controller (left)").GetComponentInChildren<test> ().pressed) == 1) && ((int)GameObject.Find ("Controller (right)").GetComponentInChildren<test> ().pressed) == 1) {
					Debug.Log ("Left hit");


					if (rayHitflag == false) {
						Vector3 olddiff = (pt.transform.position - pointerTip.transform.position);
						oldDelta = Mathf.Sqrt(olddiff.x*olddiff.x +olddiff.y*olddiff.y);
						rayHitflag = true;
						return;
					}
					Vector3 diff = (pt.transform.position - pointerTip.transform.position);
					float delta = Mathf.Sqrt(diff.x*diff.x +diff.y*diff.y);
					Debug.Log ("delta="+delta);
					Debug.Log ("olddelta="+oldDelta);
					Debug.Log ("old pos="+pt.transform.position);
					Debug.Log ("new pos="+ pointerTip.transform.position);
					float finalDelta = (delta / oldDelta);
					if (finalDelta > 1.0f && delta>oldDelta+0.1)
						finalDelta = 1.0001f;
					else if(finalDelta<1.0f && delta+0.1<oldDelta)
						finalDelta = 0.9999f;
					oldDelta = delta;
					
					col.transform.localScale *= finalDelta;
					Physics.Raycast (pointerRaycast, out pointerCollidedWith, pointerLength, ~layersToIgnore);
					if (col != pointerCollidedWith.collider) {
						col.gameObject.transform.SetParent (null);
						col.attachedRigidbody.isKinematic = false;
						col = null;
						rayHitflag = false;
					}
				}
				else if(rayHit && col.gameObject.transform.parent.gameObject == GameObject.Find ("Controller (right)") && ((int)GameObject.Find ("Controller (right)").GetComponentInChildren<test> ().pressed) == 3 ){
					
					//oldAngles.x += 0.1f;
					Vector3 axis=Vector3.zero;
					float angleDelta = 0;
					axis= (pt.transform.position - col.gameObject.transform.parent.gameObject.transform.position);
					if (((int)GameObject.Find ("Controller (left)").GetComponentInChildren<test> ().pressed) == 1) {
						axis = Vector3.Cross (new Vector3 (0, 1, 0), axis);
						axis.Normalize ();
						angleDelta = 1;
					} else if (((int)GameObject.Find ("Controller (left)").GetComponentInChildren<test> ().pressed) == 4) {
						axis = Vector3.Cross (new Vector3 (0, 1, 0), axis);
						axis.Normalize ();
						angleDelta = -1;
					} else if (((int)GameObject.Find ("Controller (left)").GetComponentInChildren<test> ().pressed) == 3) {
						axis = new Vector3 (0, 1, 0);
						angleDelta = 1;

					} else {//left should be 2
						axis = new Vector3 (0, 1, 0);
						angleDelta = -1;
					}
					col.transform.RotateAround (col.transform.position, axis, angleDelta);


				} else {
					rayHitflag = false;
					if (col != null) {
						col.gameObject.transform.SetParent (null);
						col.attachedRigidbody.isKinematic = false;
						col = null;
					}

				}

			}
		}

        protected override void InitPointer()
        {
            pointerHolder = new GameObject(string.Format("[{0}]WorldPointer_SimplePointer_Holder", gameObject.name));
            Utilities.SetPlayerObject(pointerHolder, VRTK_PlayerObject.ObjectTypes.Pointer);
            pointerHolder.transform.parent = transform;
            pointerHolder.transform.localPosition = Vector3.zero;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.name = string.Format("[{0}]WorldPointer_SimplePointer_Pointer", gameObject.name);
            Utilities.SetPlayerObject(pointer, VRTK_PlayerObject.ObjectTypes.Pointer);
            pointer.transform.parent = pointerHolder.transform;

            pointer.GetComponent<BoxCollider>().isTrigger = true;
            pointer.AddComponent<Rigidbody>().isKinematic = true;
            pointer.layer = LayerMask.NameToLayer("Ignore Raycast");

            if (customPointerCursor == null)
            {
                pointerTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointerTip.transform.localScale = pointerTipScale;
            }
            else
            {
                Renderer renderer = customPointerCursor.GetComponentInChildren<MeshRenderer>();
                if (renderer)
                {
                    customPointerMaterial = Instantiate(renderer.sharedMaterial);
                }
                pointerTip = Instantiate(customPointerCursor);
                foreach (Renderer mr in pointerTip.GetComponentsInChildren<Renderer>())
                {
                    mr.material = customPointerMaterial;
                }
            }

            pointerTip.transform.name = string.Format("[{0}]WorldPointer_SimplePointer_PointerTip", gameObject.name);
            Utilities.SetPlayerObject(pointerTip, VRTK_PlayerObject.ObjectTypes.Pointer);
            pointerTip.transform.parent = pointerHolder.transform;

            pointerTip.GetComponent<Collider>().isTrigger = true;
            pointerTip.AddComponent<Rigidbody>().isKinematic = true;
            pointerTip.layer = LayerMask.NameToLayer("Ignore Raycast");

            base.InitPointer();

            SetPointerTransform(pointerLength, pointerThickness);
            TogglePointer(false);
        }

        protected override void SetPointerMaterial()
        {
            base.SetPointerMaterial();
            pointer.GetComponent<Renderer>().material = pointerMaterial;
            if (customPointerMaterial != null)
            {
                customPointerMaterial.color = pointerMaterial.color;
            }
            else
            {
                pointerTip.GetComponent<Renderer>().material = pointerMaterial;
            }
        }

        protected override void TogglePointer(bool state)
        {
            state = (pointerVisibility == pointerVisibilityStates.Always_On ? true : state);
            base.TogglePointer(state);
            pointer.gameObject.SetActive(state);

            var tipState = (showPointerTip ? state : false);
            pointerTip.gameObject.SetActive(tipState);

            if (pointer.GetComponent<Renderer>() && pointerVisibility == pointerVisibilityStates.Always_Off)
            {
                pointer.GetComponent<Renderer>().enabled = false;
            }
        }

        private void SetPointerTransform(float setLength, float setThicknes)
        {
            //if the additional decimal isn't added then the beam position glitches
            var beamPosition = setLength / (2 + 0.00001f);

            pointer.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
            pointer.transform.localPosition = new Vector3(0f, 0f, beamPosition);
            pointerTip.transform.localPosition = new Vector3(0f, 0f, setLength - (pointerTip.transform.localScale.z / 2));
            pointerHolder.transform.localRotation = Quaternion.identity;
            base.SetPlayAreaCursorTransform(pointerTip.transform.position);
        }

        private float GetPointerBeamLength(bool hasRayHit, RaycastHit collidedWith)
        {
            var actualLength = pointerLength;

            //reset if beam not hitting or hitting new target
            if (!hasRayHit || (pointerContactTarget && pointerContactTarget != collidedWith.transform))
            {
                if (pointerContactTarget != null)
                {
                    base.PointerOut();
                }

                pointerContactDistance = 0f;
                pointerContactTarget = null;
                destinationPosition = Vector3.zero;

                UpdatePointerMaterial(pointerMissColor);
            }

            //check if beam has hit a new target
            if (hasRayHit)
            {
                pointerContactDistance = collidedWith.distance;
                pointerContactTarget = collidedWith.transform;
                destinationPosition = pointerTip.transform.position;

                UpdatePointerMaterial(pointerHitColor);

                base.PointerIn();
            }

            //adjust beam length if something is blocking it
            if (hasRayHit && pointerContactDistance < pointerLength)
            {
                actualLength = pointerContactDistance;
            }

            return actualLength;
        }
    }
}