using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

public class MouvementFlippers : MonoBehaviour
{
    public static MouvementFlippers Instance { get; private set; }

    public bool isAnimated;
    public  Animator animLeft;
    public Animator animRight;

    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
	public GameObject OverlayObject;
	public float smoothFactor = 5f;
	
	public GUIText debugText;

	private float distanceToCamera = 10f;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        animLeft.enabled = isAnimated;
        if (OverlayObject)
        {
            distanceToCamera = (OverlayObject.transform.position - Camera.main.transform.position).magnitude;
        }
    }
    public void mouvLeftFlipper()
    {
        if(!isAnimated)
            return;
        animLeft.Play("leftflipper", 0);
    }
    public void mouvRightFlipper()
    {
        if (!isAnimated)
            return;
        animRight.Play("rightflipper", 0);
    }

    void Update()
    {
        
        //Activation de l'animation des flippers si utilisation du clavier et pas de la kinect
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mouvLeftFlipper();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mouvRightFlipper();
        }
        //Gestion de la Kinect et position des flippers en fonctions du corps du joueur
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            int iJointIndex = (int)TrackedJoint;

            if (manager.IsUserDetected())
            {
                uint userId = manager.GetPlayer1ID();

                if (manager.IsJointTracked(userId, iJointIndex))
                {
                    Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                    if (posJoint != Vector3.zero)
                    {
                        // 3d position to depth
                        Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);

                        // depth pos to color pos
                        Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                        float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                        float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                        //						Vector3 localPos = new Vector3(scaleX * 10f - 5f, 0f, scaleY * 10f - 5f); // 5f is 1/2 of 10f - size of the plane
                        //						Vector3 vPosOverlay = backgroundImage.transform.TransformPoint(localPos);
                        //Vector3 vPosOverlay = BottomLeft + ((vRight * scaleX) + (vUp * scaleY));

                        if (debugText)
                        {
                            debugText.guiText.text = "Tracked user ID: " + userId;  // new Vector2(scaleX, scaleY).ToString();
                        }

                        if (OverlayObject)
                        {
                            Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                            Vector3 newPosFlippers = new Vector3(vPosOverlay.x, OverlayObject.transform.position.y, OverlayObject.transform.position.z);
                            OverlayObject.transform.position = Vector3.Lerp(OverlayObject.transform.position, newPosFlippers, smoothFactor * Time.deltaTime);
                        }
                    }
                }

            }

        }
    }

}
