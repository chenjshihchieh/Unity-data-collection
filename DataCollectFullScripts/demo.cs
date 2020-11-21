using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class demo : MonoBehaviour {

	public bool new_file = true;
	public void save_annotation(annotation new_annotation)
	{
		int state_id = 0;
		foreach(string state in states){
			state_id ++;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName(state)){
				new_annotation.category_id = state_id;
				new_annotation.keypoints3d = hp.keypoints.keypoints3d;
				new_annotation.jointrotation3d = hp.keypoints.rotationJoint3d;
				BroadcastMessage("gatherKeypoints2d", new_annotation);
			}
		}
		
	}
	
	public Animator animator;
	public string[] states = new string[]{"143_01", "143_02", "143_04", "143_07"};
	
	//////////////
	public Transform leftUpperLeg, leftLowerLeg, leftFoot, leftToes;
    public Transform rightUpperLeg, rightLowerLeg, rightFoot, rightToes;
    public Transform hips,spine, chest, neck, head, jaw;
    public Transform leftShoulder, leftUpperArm, leftLowArm, leftHand, leftHandindex1, LeftHandMiddle1, leftHandPinky1,
    leftHandRing1, leftHandThumb1;
    public Transform  rightShoulder, rightUpperArm, rightLowArm, rightHand, rightHandindex1, rightHandMiddle1, rightHandPinky1,
    rightHandRing1, rightHandThumb1;

	[SerializeField]
	private HumanPoseData hp;
	////////////
	
	void Start() 
	{
		 //Get each joint object from "HumanBodyBones" 
        animator = GetComponent<Animator>();
        
        // local position of each bone. 
		hips = animator.GetBoneTransform(HumanBodyBones.Hips);
		spine = animator.GetBoneTransform(HumanBodyBones.Spine);
		chest = animator.GetBoneTransform(HumanBodyBones.Chest);
		neck = animator.GetBoneTransform(HumanBodyBones.Neck);
		head = animator.GetBoneTransform(HumanBodyBones.Head);
		jaw =  animator.GetBoneTransform(HumanBodyBones.Jaw);

		leftUpperLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
		leftLowerLeg = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
		leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
		leftToes = animator.GetBoneTransform(HumanBodyBones.LeftToes);
		rightUpperLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
		rightLowerLeg = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
		rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
		rightToes = animator.GetBoneTransform(HumanBodyBones.RightToes);

		leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);
		leftUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
		leftLowArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
		leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);

		rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
		rightUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
		rightLowArm = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
		rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
	}
	
	private void Update () 
	{
		hp = new HumanPoseData(this);
		
	}
	
	void OnGUI() {

		GUILayout.BeginVertical("box");
		if (GUILayout.Button("movements")) {
			animator.SetTrigger("movements");
		}
		if (GUILayout.Button("sports")) {
			animator.SetTrigger("sports");
		}
		if (GUILayout.Button("martial arts")) {
			animator.SetTrigger("martialarts");
		}
		GUILayout.FlexibleSpace();
		GUILayout.Box("This is just a tiny sample of the 2534 animations inside of this library.");
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
	}
}
