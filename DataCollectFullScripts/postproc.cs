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
	
	private int frames_to_skip = 30;
	private bool m_ShouldSaveDepth = false;
    private bool m_SavingDepth = false;
	private int image_numb = 1;
	private bool new_file = true;
	
	private void Start(){
		//get the camera and tell it to render a depth texture
		Camera cam = GetComponent<Camera>();
		cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
	}
	
	private void Update ()
     {
         //If we aren't already saving a depth texture, save a new one when the space key is pressed
         if (!m_SavingDepth) m_ShouldSaveDepth = true;
     }
	
	
	//method which is automatically called by unity after the camera is done rendering
	void OnRenderImage(RenderTexture source, RenderTexture destination){
		
		 if (m_ShouldSaveDepth)
         {
             //Start saving the depth texture
			 StartCoroutine(Capture(source, destination));
             StartCoroutine(CaptureDepth(source, destination));
             m_ShouldSaveDepth = false;
         }
		 
		 Graphics.Blit(source, destination/*, PostprocessMaterial*/);
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
        File.WriteAllBytes($"../Pictures/Captures/{cam.name}rgbTexture{image_numb}.png", bytes);
		GetComponent<ImageSynthesis>().Save($"segmentation{image_numb}", 0, 0);
		for(int i = 0; i < frames_to_skip; i++){
			yield return null;
		}
    }
	
	private IEnumerator CaptureDepth (RenderTexture source, RenderTexture destination)
	{
		
		Camera cam = GetComponent<Camera>();
		//draws the pixels from the source texture to the destination texture
		m_SavingDepth = true;
		RenderTexture tmp = RenderTexture.GetTemporary (source.width, source.height, 16, RenderTextureFormat.ARGB32);

		Graphics.Blit(source, tmp, PostprocessMaterial);

		
        //Store the last active render texture and set our source copy as active
        RenderTexture lastActive = RenderTexture.active;
        RenderTexture.active = tmp;
        //Copy the active render texture into a normal Texture2D
        //Unfortunately readpixels doesn't work with single channel formats, so ARGB32 will have to do
        Texture2D tex = new Texture2D (source.width, source.height, TextureFormat.Alpha8, false);
		
        tex.ReadPixels (new Rect (0, 0, source.width, source.height), 0, 0);
        tex.Apply ();
		
		
        //Restore the active render texture and release our temporary tex
        RenderTexture.active = lastActive;
        RenderTexture.ReleaseTemporary (tmp);
 
        //Encode the texture data into .png formatted bytes
        byte[] data = tex.EncodeToPNG();
        Destroy(tex);
 
        //Write the texture to a file
        File.WriteAllBytes ($"../Pictures/DepthImage/{cam.name}depthTexture{image_numb}.png", data);
		image new_image = new image(image_numb, source.width, source.height, $"rgb{image_numb}.png");
		string coco_json = JsonUtility.ToJson(new_image);
		if(new_file) 
		{
			File.WriteAllText("../Pictures/ImageJSON/rgbImage.json", coco_json);
			new_file = false;
		}else{
			File.AppendAllText("../Pictures/ImageJSON/rgbImage.json", 
                   Environment.NewLine + coco_json);
		}
		SendMessageUpwards("save_annotation", new annotation(image_numb, source.width * source.height));
		image_numb += 1;
        m_SavingDepth = false;
		
		 //Wait another frame
        for(int i = 0; i < frames_to_skip; i++){
			yield return null;
		}
	}
}

 