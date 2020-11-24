using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraPointsData 
{
    // local position of each bone.
	public float[] keypoints2d;
	
	public CameraPointsData(demo capturePose, Camera cam)
    {
		Transform jointTrans = capturePose.hips; 
        Transform[] keypoint_positions = {
		capturePose.hips
        , capturePose.spine          
        , capturePose.chest
        , capturePose.neck
        , capturePose.head
        , capturePose.leftUpperLeg
        , capturePose.leftLowerLeg
        , capturePose.leftFoot
        , capturePose.leftToes
        , capturePose.rightUpperLeg
        , capturePose.rightLowerLeg
        , capturePose.rightFoot
        , capturePose.rightToes
        , capturePose.leftShoulder
        , capturePose.leftUpperArm
        , capturePose.leftLowArm
        , capturePose.leftHand
        , capturePose.rightShoulder
        , capturePose.rightUpperArm
        , capturePose.rightLowArm
        , capturePose.rightHand};           
		
		keypoints2d = new float[63];
		for(int i = 0; i < 21; i++)
			{
				int fir_pos = i * 3;
				int sec_pos = fir_pos + 1;
				int thi_pos = sec_pos + 1;
				Vector3 screenP_v = cam.WorldToScreenPoint(keypoint_positions[i].position);
				keypoints2d[fir_pos] = screenP_v.x;
				keypoints2d[sec_pos] = screenP_v.y;
				keypoints2d[thi_pos] = screenP_v.z;
			}
    }
}