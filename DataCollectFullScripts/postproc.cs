using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;


//behaviour which should lie on the same gameobject as the main camera
public class postproc : MonoBehaviour {
	
	//material that's applied when doing postprocessing
	[FormerlySerializedAs("postprocessMaterial"), SerializeField]
	public Material PostprocessMaterial;
    public Transform keypoint;
    
    private float[] keypoints;
	private int frames_to_skip = 60;
	private bool m_ShouldSaveDepth = false;
    private bool m_SavingDepth = false;
	private int image_numb = 1;
	private bool new_file_annotation = true;
    private bool new_file_coco = true;
	private int count;
	private string prev_camid = "";
	private string current_camid;
    private GameObject avatar;
	private Camera cam;
	private demo charactor;
	private CameraPointsData cameraPtsData;
    
	private void Start(){
		//get the camera and tell it to render a depth texture
		Camera cam = GetComponent<Camera>();
		//cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
	}
	
    void Awake()
	{
		// Get Charactor Object
        avatar = GameObject.Find("DefaultAvatar");
		 if (avatar != null)
		 	print("AVATAR got");
	}
    
	private void Update ()
     {
         //If we aren't already saving a depth texture, save a new one when the space key is pressed
         if (!m_SavingDepth) m_ShouldSaveDepth = true;
         charactor = avatar.GetComponent<demo>();
         
         
     }
	
	
	//method which is automatically called by unity after the camera is done rendering
	void OnRenderImage(RenderTexture source, RenderTexture destination){
		
		 if (m_ShouldSaveDepth)
         {
             //Start saving the depth texture
			 StartCoroutine(Capture(source, destination));
            // StartCoroutine(CaptureDepth(source, destination));
             m_ShouldSaveDepth = false;
         }
		 
		 Graphics.Blit(source, destination/*, PostprocessMaterial*/);
	}
    
	public void save_coco_json(int id, int width, int height, string image_name, string camera){
		image new_image = new image(id, width, height, image_name);
		string coco_json = JsonUtility.ToJson(new_image);
		if(new_file_coco) 
		{
			File.WriteAllText($"../Pictures/ImageJSON/{camera}_image.json", coco_json);
			new_file_coco = false;
		}else{
			File.AppendAllText($"../Pictures/ImageJSON/{camera}_image.json", 
                   Environment.NewLine + coco_json);
		}
		
	}
	
	public IEnumerator Capture(RenderTexture source, RenderTexture destination)
    {
		Camera cam = GetComponent<Camera>();
		
        RenderTexture tmp = RenderTexture.GetTemporary (source.width, source.height, 16, RenderTextureFormat.ARGB32);
		
        Graphics.Blit(source, tmp);
		
		
		//Store the last active render texture and set our source copy as active
        RenderTexture lastActive = RenderTexture.active;
        RenderTexture.active = tmp;
		
        Texture2D image = new Texture2D(source.width, source.height, TextureFormat.ARGB32, false);
        image.ReadPixels(new Rect (0, 0, source.width, source.height), 0, 0);
		
        image.Apply();
		
        //Restore the active render texture and release our temporary tex
        RenderTexture.active = lastActive;
		
        RenderTexture.ReleaseTemporary (tmp);

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);
		
		string image_name = $"rgb_{cam.name}_{image_numb}.png";
        File.WriteAllBytes("../Pictures/Captures/" + image_name, bytes);
		save_coco_json(image_numb, source.width, source.height, image_name, cam.name);
		
        
        
        
        
		annotation new_annotation = new annotation(image_numb, source.width * source.height);
		//Uncomment if we want 3d data as well
		//SendMessageUpwards("save_annotation", new_annotation);
		
		//Uncomment if we only want 2d data
        current_camid = $"{cam.name}{new_annotation.id}";
        if(current_camid != prev_camid){
            cameraPtsData = new CameraPointsData(charactor,cam);
            keypoints = new float[3];
            Vector3 xyz = cam.WorldToScreenPoint(keypoint.transform.position);
            keypoints[0] = xyz.x;
            keypoints[1] = xyz.y;
            keypoints[2] = xyz.z;
            print($"{keypoints[0]}, {keypoints[1]}, {keypoints[2]}");
            new_annotation.keypoints2d = keypoints;
            string annotation_data = JsonUtility.ToJson(new_annotation);
            if(new_file_annotation){
                File.WriteAllText($"../Pictures/AnnotationJSON/{cam.name}_annotation.json", "[" + annotation_data);
                new_file_annotation = false;
                prev_camid = current_camid;
            }else{
                File.AppendAllText($"../Pictures/AnnotationJSON/{cam.name}_annotation.json", 
                     ", " + Environment.NewLine + annotation_data);
                     prev_camid = current_camid;
            }
        }
		//BroadcastMessage("gatherKeypoints2d", new_annotation);
		image_numb += 1;
        m_SavingDepth = false;
		
		GetComponent<ImageSynthesis>().Save($"segmentation_{cam.name}_{image_numb}", 0, 0);
		yield return null;
        /*for(int i = 0; i < frames_to_skip; i++){
			yield return null;
		}*/
    }
	
    
	//code for when collecting depth data but thats not necessary anymore so commenting it out
	//private IEnumerator CaptureDepth (RenderTexture source, RenderTexture destination)
	//{
	//	
	//	Camera cam = GetComponent<Camera>();
	//	//draws the pixels from the source texture to the destination texture
	//	m_SavingDepth = true;
	//	RenderTexture tmp = RenderTexture.GetTemporary (source.width, source.height, 16, RenderTextureFormat.ARGB32);
	//
	//	Graphics.Blit(source, tmp, PostprocessMaterial);
	//
	//	
    //    //Store the last active render texture and set our source copy as active
    //    RenderTexture lastActive = RenderTexture.active;
    //    RenderTexture.active = tmp;
    //    //Copy the active render texture into a normal Texture2D
    //    //Unfortunately readpixels doesn't work with single channel formats, so ARGB32 will have to do
    //    Texture2D tex = new Texture2D (source.width, source.height, TextureFormat.Alpha8, false);
	//	
    //    tex.ReadPixels (new Rect (0, 0, source.width, source.height), 0, 0);
    //    tex.Apply ();
	//	
	//	
    //    //Restore the active render texture and release our temporary tex
    //    RenderTexture.active = lastActive;
    //    RenderTexture.ReleaseTemporary (tmp);
	//
    //    //Encode the texture data into .png formatted bytes
    //    byte[] data = tex.EncodeToPNG();
    //    Destroy(tex);
	//
	//	string depth_file_path = $"../Pictures/DepthImage/depthTexture_{cam.name}_{image_numb}.png";
    //    //Write the texture to a file
    //    File.WriteAllBytes (depth_file_path, data);
	//		
	//	image_numb += 1;
    //    m_SavingDepth = false;
	//	
	//	 //Wait another frame
    //    for(int i = 0; i < frames_to_skip; i++){
	//		yield return null;
	//	}
	//}
}

 