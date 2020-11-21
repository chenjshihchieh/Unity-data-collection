using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanPoseData
{
    // local position of each bone.
    public JointData Hips,  Spine, Chest, Neck, Head, Jaw;
    public JointData LeftUpperLeg, LeftLowerLeg, LeftFoot, LeftToes, RightUpperLeg, RightLowerLeg, RightFoot, RightToes;
    public JointData LeftShoulder, LeftUpperArm, LeftLowArm, LeftHand;
    public JointData RightShoulder, RightUpperArm, RightLowArm, RightHand;
    //positiion relative to origin

    public HumanPoseData (demo capturePose)
    {
        Hips = new JointData(capturePose, "Hips");
        Spine = new JointData(capturePose,"Spine");
        Chest = new JointData(capturePose, "Chest");
        Neck = new JointData(capturePose, "Neck");
        Head = new JointData(capturePose, "Head");
        Jaw = new JointData(capturePose, "Jaw");

        LeftUpperLeg = new JointData(capturePose, "LeftUpperLeg");
        LeftLowerLeg = new JointData(capturePose, "LeftLowerLeg");
        LeftFoot = new JointData(capturePose, "LeftFoot");
        LeftToes = new JointData(capturePose, "LeftToes");
        RightUpperLeg = new JointData(capturePose, "RightUpperLeg");
        RightLowerLeg = new JointData(capturePose, "RightLowerLeg");
        RightFoot = new JointData(capturePose, "RightFoot");
        RightToes = new JointData(capturePose, "RightToes");

        LeftShoulder = new JointData(capturePose, "LeftShoulder");
        LeftUpperArm = new JointData(capturePose, "LeftUpperArm");
        LeftLowArm = new JointData(capturePose, "LeftLowArm");
        LeftHand = new JointData(capturePose, "LeftHand");
        RightShoulder = new JointData(capturePose, "RightShoulder");
        RightUpperArm = new JointData(capturePose, "RightUpperArm");
        RightLowArm = new JointData(capturePose, "RightLowArm");
        RightHand = new JointData(capturePose, "RightHand");       
    }
}

[System.Serializable]
public class JointData
{
    public float[] local_p;
    public float[] relative_p;
    public float[] local_R;

    public JointData(demo capturePose, string jointName)
    {
        Transform jointTrans = capturePose.hips; 
        Vector3 relativeP2origin;
        if (jointName == "Hips")
            jointTrans = capturePose.hips;
        else if (jointName == "Spine")
            jointTrans = capturePose.spine;           
        else if (jointName == "Chest")
            jointTrans = capturePose.chest; 
        else if (jointName == "Neck")
            jointTrans = capturePose.neck; 
         else if (jointName == "Head")
            jointTrans = capturePose.head; 
         else if (jointName == "LeftUpperLeg")
            jointTrans = capturePose.leftUpperLeg; 
         else if (jointName == "LeftLowerLeg")
            jointTrans = capturePose.leftLowerLeg; 
         else if (jointName == "LeftFoot")
            jointTrans = capturePose.leftFoot; 
         else if (jointName == "LeftToes")
            jointTrans = capturePose.leftToes; 
         else if (jointName == "RightUpperLeg")
            jointTrans = capturePose.rightUpperLeg; 
         else if (jointName == "RightLowerLeg")
            jointTrans = capturePose.rightLowerLeg; 
         else if (jointName == "RightFoot")
            jointTrans = capturePose.rightFoot; 
         else if (jointName == "RightToes")
            jointTrans = capturePose.rightToes; 
         else if (jointName == "LeftShoulder")
            jointTrans = capturePose.leftShoulder; 
         else if (jointName == "LeftUpperArm")
            jointTrans = capturePose.leftUpperArm; 
          else if (jointName == "LeftLowArm")
            jointTrans = capturePose.leftLowArm; 
         else if (jointName == "LeftHand")
            jointTrans = capturePose.leftHand; 
          else if (jointName == "RightShoulder")
            jointTrans = capturePose.rightShoulder; 
          else if (jointName == "RightUpperArm")
            jointTrans = capturePose.rightUpperArm; 
         else if (jointName == "RightLowArm")
            jointTrans = capturePose.rightLowArm; 
         else if (jointName == "RightHand")
            jointTrans = capturePose.rightHand;           

        local_p = new float[3];
        local_p[0] = jointTrans.localPosition.x;
        local_p[1] = jointTrans.localPosition.y;
        local_p[2] = jointTrans.localPosition.z;

        local_R = new float[3];
        local_R[0] = jointTrans.localRotation.eulerAngles.x;
        local_R[1] = jointTrans.localRotation.eulerAngles.y;
        local_R[2] = jointTrans.localRotation.eulerAngles.z; 

        // Relative position to Origin
        relativeP2origin = GetPosition2Origin(jointTrans);
        relative_p = new float[3];
        relative_p[0] = relativeP2origin.x;
        relative_p[1] = relativeP2origin.y;
        relative_p[2] = relativeP2origin.z;
    }

	private Vector3 GetPosition2Origin(Transform obj)
	{
		if (obj.name == "Hips")
			return new Vector3(0.0f, 0.0f, 0.0f);
		else
		    return obj.localPosition + GetPosition2Origin(obj.parent);
	}
}
