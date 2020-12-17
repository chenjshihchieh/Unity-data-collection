using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class info
{
	public int year = 2020;
	public string description = "CMPT732 Unity Project Data";
	public string contributor = "Zhu (Charles) Chen, Shih-Chieh (Jack) Chen, Duo Xu, Payam Nikdel";
	public string date_created = DateTime.Now.ToString("yyyy-MM-dd");
}

[System.Serializable]
public class category
{
	public int id;
	public string name;
	public string supercategory = "pose";
	public string[] keypoints = {"Hips", "Spine", "Chest", "Neck", "Head", 
								"LeftUpperLeg", "LeftLowerLeg", "LeftFoot", "LeftToes", 
								"RightUpperLeg", "RightLowerLeg", "RightFoot", "RightToes", 
								"LeftShoulder", "LeftUpperArm", "LeftForeArm", "LeftHand",
								"RightShoulder", "RightUpperArm", "RightForeArm", "RightHand"};
	public string[][] skeleton = new string[][]
	{
		new string[] {"Hips", "Spine"},
		new string[] {"Hips", "LeftUpperLeg"},
		new string[] {"Hips", "RightUpperLeg"},
		new string[] {"Spine", "Chest"},
		new string[] {"Chest", "LeftShoulder"},
		new string[] {"Chest", "Neck"},
		new string[] {"Chest", "RightShoulder"},
		new string[] {"LeftShoulder", "LeftArm"},
		new string[] {"LeftArm", "LeftForeArm"},
		new string[] {"LeftForeArm", "LeftHand"},
		new string[] {"RightShoulder", "RightArm"},
		new string[] {"RightArm", "RightForeArm"},
		new string[] {"RightForeArm", "RightHand"},
		new string[] {"Neck", "Head"},
		new string[] {"LeftUpperLeg", "LeftLowerLeg"},
		new string[] {"LeftLowerLeg", "LeftFoot"},
		new string[] {"LeftFoot", "LeftToes"},
		new string[] {"RightUpperLeg", "RightLowerLeg"},
		new string[] {"RightLowerLeg", "RightFoot"},
		new string[] {"RightFoot", "RightToes"}
		
	};
	
	public category(int state_id, string state)
	{
		int id = state_id;
		string name = state;
	}
}

[System.Serializable]
public class COCOFormat 
{
	public info info = new info();
	public List<image> images;
	public annotation[] annotations;
	
}
