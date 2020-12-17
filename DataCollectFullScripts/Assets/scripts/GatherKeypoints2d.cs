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
	private string prev_camid = "";
	private string current_camid;
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

	/*public void gatherKeypoints2d(annotation new_annotation) {
		//Write data to file as Json format
		current_camid = $"{cam.name}{new_annotation.id}";
		if(current_camid == prev_camid) return;
		cameraPtsData = new CameraPointsData(charactor,cam);	
		new_annotation.keypoints2d = cameraPtsData.keypoints2d;
        string annotation_data = JsonUtility.ToJson(new_annotation);
		if(new_file){
			File.WriteAllText($"../Pictures/AnnotationJSON/{cam.name}_annotation.json", annotation_data);
			new_file = false;
			prev_camid = current_camid;
		}else{
			File.AppendAllText($"../Pictures/AnnotationJSON/{cam.name}_annotation.json", 
				 ", " + Environment.NewLine + annotation_data);
				 prev_camid = current_camid;
		}
	}*/
}