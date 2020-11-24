In the CMPT726-Project folder will contain all the files you need to run the unity session(?) for collecting mocap data. When test running, go to Assets/Huge Mocap Library/demo/scripts/scenes then click on main.unity. That should start the unity session with all the necessary components set up. Also, in the same place where the CMPT726-Project folder is, there should be a Pictures folder. In the pictures folder should be folders called 'DepthImage', 'ImageJSON', 'Captures', 'Segmentation' and 'AnnotationJSON.' All the produced data will save there. 

Just to make sure that everything is in order, I list all the things that the unity session should have once opened. 
-Default Avatar has Demo script attached
-There should be 16 cameras that are in DefaultAvatar
-Each camera should have a 'Postproc', 'Face Character', 'Gather Keypoints 2d' and 'Image Synthesis' script
- Postproc script should have the material 'postprocessing' attached
- the 'postprocessing' material should have the Tutorial/postproc shader attached. 
- For the Image Synthesis script doesnt need any shaders attached. The script itself will pull the necessary shader.
- For the Face Character script, make sure to pick a target body part (I picked the spine).