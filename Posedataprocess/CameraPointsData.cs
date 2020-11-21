using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraPointsData 
{
    // local position of each bone.
    public ScreenPoints Hips,  Spine, Chest, Neck, Head, Jaw;
    public ScreenPoints LeftUpperLeg, LeftLowerLeg, LeftFoot, LeftToes, RightUpperLeg, RightLowerLeg, RightFoot, RightToes;
    public ScreenPoints LeftShoulder, LeftUpperArm, LeftLowArm, LeftHand;
    public ScreenPoints RightShoulder, RightUpperArm, RightLowArm, RightHand;
   public CameraPointsData(demo capturePose, Camera cam)
    {
        Hips = new ScreenPoints(capturePose, "Hips", cam);
        Spine = new ScreenPoints(capturePose,"Spine", cam);
        Chest = new ScreenPoints(capturePose, "Chest", cam);
        Neck = new ScreenPoints(capturePose, "Neck", cam);
        Head = new ScreenPoints(capturePose, "Head", cam);
        Jaw = new ScreenPoints(capturePose, "Jaw", cam);

        LeftUpperLeg = new ScreenPoints(capturePose, "LeftUpperLeg", cam);
        LeftLowerLeg = new ScreenPoints(capturePose, "LeftLowerLeg", cam);
        LeftFoot = new ScreenPoints(capturePose, "LeftFoot", cam);
        LeftToes = new ScreenPoints(capturePose, "LeftToes", cam);
        RightUpperLeg = new ScreenPoints(capturePose, "RightUpperLeg", cam);
        RightLowerLeg = new ScreenPoints(capturePose, "RightLowerLeg", cam);
        RightFoot = new ScreenPoints(capturePose, "RightFoot", cam);
        RightToes = new ScreenPoints(capturePose, "RightToes", cam);

        LeftShoulder = new ScreenPoints(capturePose, "LeftShoulder", cam);
        LeftUpperArm = new ScreenPoints(capturePose, "LeftUpperArm", cam);
        LeftLowArm = new ScreenPoints(capturePose, "LeftLowArm", cam);
        LeftHand = new ScreenPoints(capturePose, "LeftHand", cam);
        RightShoulder = new ScreenPoints(capturePose, "RightShoulder", cam);
        RightUpperArm = new ScreenPoints(capturePose, "RightUpperArm", cam);
        RightLowArm = new ScreenPoints(capturePose, "RightLowArm", cam);
        RightHand = new ScreenPoints(capturePose, "RightHand", cam);       

    }
}

[System.Serializable]
public class ScreenPoints
{
    public float[] world_p;
    public float[] screen_p;

    public ScreenPoints(demo capturePose, string jointName, Camera cam)
    {
        Transform jointTrans = capturePose.hips; 
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

        //World postion of each joint.
        world_p = new float[3];
        world_p[0] = jointTrans.position.x;
        world_p[1] = jointTrans.position.y;
        world_p[2] = jointTrans.position.z;

        //Convert World position to Camera Screen Position.
        screen_p = new float[3];
        Vector3 screenP_v = cam.WorldToScreenPoint(jointTrans.position);
        screen_p[0] = screenP_v.x;
        screen_p[1] = screenP_v.y;
        screen_p[2] = screenP_v.z; 
    }
}
