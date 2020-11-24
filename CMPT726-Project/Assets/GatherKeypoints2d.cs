// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GatherKeypoints2d : MonoBehaviour {
	private bool new_file = true;
	private int count; 
	private GameObject avatar; 
	private Camera cam;
	private demo charactor;
	private CameraPointsData cameraPtsData;
	
	
	void Start(){
		//Get Camera Component
		cam = GetComponent<Camera>();
	}

	void Awake()
	{
		// Get Charactor Object
         avatar = GameObject.Find("DefaultAvatar");
		 if (avatar != null)
		 	print("AVATAR got");
	}

	void Update()
	{
		//Capture real time pose data
        // 1.1 local position
		charactor = avatar.GetComponent<demo>();
	}

	public void gatherKeypoints2d(annotation new_annotation) {
		//Write data to file as Json format
		
		cameraPtsData = new CameraPointsData(charactor,cam);	
		new_annotation.keypoints2d = cameraPtsData.keypoints2d;
        string annotation_data = JsonUtility.ToJson(new_annotation);
		if(new_file){
					File.WriteAllText("../Pictures/AnnotationJSON/annotation.json", annotation_data);
					new_file = false;
				}else{
					File.AppendAllText("../Pictures/AnnotationJSON/annotation.json", 
						 Environment.NewLine + annotation_data);
				}
	}
}