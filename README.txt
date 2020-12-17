In the CMPT726-Project folder will contain all the files you need to run the unity session for collecting mocap data. When test running, make sure to create a new project. Then, replace the Asset folder with the Asset folder in DataCollectFullSripts.

Just to make sure that everything is in order, I list all the things that the unity session should have once opened. 
-Default Avatar has Demo script attached
-There should be 16 cameras that are in DefaultAvatar
-Each camera should have a 'Postproc', 'Face Character', 'Gather Keypoints 2d' and 'Image Synthesis' script
- Postproc script should have the material 'postprocessing' attached
- the 'postprocessing' material should have the Tutorial/postproc shader attached. 
- For the Image Synthesis script doesnt need any shaders attached. The script itself will pull the necessary shader.
- For the Face Character script, make sure to pick a target body part (I picked the spine).