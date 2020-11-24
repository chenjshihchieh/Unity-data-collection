using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanPoseData
{
	public JointData keypoints;
	public string state;
	public int state_id;

    public HumanPoseData (demo capturePose)
    {
        keypoints = new JointData(capturePose);
    }
}

[System.Serializable]
public class JointData
{
	public float[] keypoints3d;
	public float[] rotationJoint3d;

    public JointData(demo capturePose)
    {
		
        Transform jointTrans = capturePose.hips; 
        Vector3 relativeP2origin;
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
		
		
		
		keypoints3d = new float[63];
		for(int i = 0; i < 21; i++)
			{
				int fir_pos = i * 3;
				int sec_pos = fir_pos + 1;
				int thi_pos = sec_pos + 1;
				relativeP2origin = GetPosition2Origin(keypoint_positions[i]);
				keypoints3d[fir_pos] = relativeP2origin.x;
				keypoints3d[sec_pos] = relativeP2origin.y;
				keypoints3d[thi_pos] = relativeP2origin.z;
			}
		
		rotationJoint3d = new float[63];
		for(int i = 0; i < 21; i++)
			{
				int fir_pos = i * 3;
				int sec_pos = fir_pos + 1;
				int thi_pos = sec_pos + 1;
				rotationJoint3d[fir_pos] = keypoint_positions[i].localRotation.eulerAngles.x;
				rotationJoint3d[sec_pos] = keypoint_positions[i].localRotation.eulerAngles.y;
				rotationJoint3d[thi_pos] = keypoint_positions[i].localRotation.eulerAngles.z;
			}
    }

	private Vector3 GetPosition2Origin(Transform obj)
	{
		if (obj.name == "Hips")
			return new Vector3(0.0f, 0.0f, 0.0f);
		else
		    return obj.localPosition + GetPosition2Origin(obj.parent);
	}
}