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
		//Transform jointTrans = capturePose.Hips; 
        Transform[] keypoint_positions = {
		capturePose.Hips
        , capturePose.Spine          
        , capturePose.Chest
        , capturePose.Neck
        , capturePose.Head
        , capturePose.LeftUpLeg
        , capturePose.LeftLeg
        , capturePose.LeftFoot
        , capturePose.LeftToes
        , capturePose.RightUpLeg
        , capturePose.RightLeg
        , capturePose.RightFoot
        , capturePose.RightToes
        , capturePose.LeftShoulder
        , capturePose.LeftArm
        , capturePose.LeftForeArm
        , capturePose.LeftHand
        , capturePose.RightShoulder
        , capturePose.RightArm
        , capturePose.RightForeArm
        , capturePose.RightHand};

        
		keypoints2d = new float[63];
		for(int i = 0; i < 21; i++)
			{
				int fir_pos = i * 3;
				int sec_pos = fir_pos + 1;
				int thi_pos = sec_pos + 1;
				Vector3 screenP_v = cam.WorldToScreenPoint(keypoint_positions[i].transform.position);
				keypoints2d[fir_pos] = screenP_v.x;
				keypoints2d[sec_pos] = screenP_v.y;
				keypoints2d[thi_pos] = screenP_v.z;
			}
    }
}