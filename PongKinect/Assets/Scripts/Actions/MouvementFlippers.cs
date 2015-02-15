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
    public  Animator animLeft1;
    public Animator animLeft2;
    public Animator animRight1;
    public Animator animRight2;

    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
	public GameObject[] Flippers = new GameObject[2];

    public float[] _rangeMouvementFlippers = new float[2];
	public float smoothFactor = 5f;
	

	private float[] distanceToCamera = {10f,10f};

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        animLeft1.enabled = isAnimated;
        animRight1.enabled = isAnimated;
        animLeft2.enabled = isAnimated;
        animRight2.enabled = isAnimated;
        for (int i = 0; i < Flippers.Length; i++ )
            if (Flippers[i])
                distanceToCamera[i] = (Flippers[i].transform.position - Camera.main.transform.position).magnitude;
    }
    public void mouvLeftFlipper()
    {
        if(!isAnimated)
            return;
        animLeft1.Play("leftflipper", 0);
        animRight2.Play("rightflipper", 0);
    }
    public void mouvRightFlipper()
    {
        if (!isAnimated)
            return;
        animRight1.Play("rightflipper", 0);
        animLeft2.Play("leftflipper", 0);
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
                        for (int i = 0; i < Flippers.Length; i++)
                        {
                            if (Flippers[i])
                            {
                                Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera[i]));
                                Vector3 newPosFlippers = new Vector3(vPosOverlay.x, Flippers[i].transform.position.y, Flippers[i].transform.position.z);
                                Debug.Log(Mathf.Abs(newPosFlippers.x - _rangeMouvementFlippers[0]) + "  " + Mathf.Abs(newPosFlippers.x - _rangeMouvementFlippers[0]));
                                if (_rangeMouvementFlippers[0] <= newPosFlippers.x && _rangeMouvementFlippers[1] >= newPosFlippers.x)
                                    Flippers[i].transform.position = Vector3.Lerp(Flippers[i].transform.position, newPosFlippers, smoothFactor * Time.deltaTime);
                            }
                        }
                    }
                }

            }

        }
    }

}
