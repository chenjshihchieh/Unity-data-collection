[System.Serializable]
public class annotation
{
	public int id, category_id;
	public float area;
	public int iscrowd = 0;
	public float[] keypoints2d, keypoints3d, jointrotation3d;
	public int num_keypoints = 21;
	
	public annotation(int new_id, float new_area)
	{
		id = new_id;
		area = new_area;
	}
}
