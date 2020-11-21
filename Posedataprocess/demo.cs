using UnityEngine;
using System.Collections;
using System.IO;

public class demo : MonoBehaviour {

	public Animator animator;

    public Transform leftUpperLeg, leftLowerLeg, leftFoot, leftToes;
    public Transform rightUpperLeg, rightLowerLeg, rightFoot, rightToes;
    public Transform hips,spine, chest, neck, head, jaw;
    public Transform leftShoulder, leftUpperArm, leftLowArm, leftHand, leftHandindex1, LeftHandMiddle1, leftHandPinky1,
    leftHandRing1, leftHandThumb1;
    public Transform  rightShoulder, rightUpperArm, rightLowArm, rightHand, rightHandindex1, rightHandMiddle1, rightHandPinky1,
    rightHandRing1, rightHandThumb1;

	[SerializeField]private HumanPoseData hp;

	private string path = "./PoseDataCollect.json";

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

	void Start(){
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

		// print("==rightLowerLeg========= "+ GetPosition2Origin(rightLowerLeg));

	}

	void Update(){
		// hp = new HumanPoseData(this);	
        // string human_json = JsonUtility.ToJson(hp);
		// System.IO.File.AppendAllText( "./PoseDataCollect.json", human_json);
		hp = new HumanPoseData(this);	
        string human_json = JsonUtility.ToJson(hp);
		System.IO.File.WriteAllText( "./PoseDataCollect.json", human_json);
	}

	private void Awake(){
		/**
		string json_poses = JsonUtility.ToJson(humanposes);
		System.IO.File.WriteAllText( "./PotionData.json", json_poses); 
		**/
		// Create file
		File.Create(path);

	}
}
