using System;

[System.Serializable]
public class image 
{
	public int id;
	public int width, height;
	public string file_name;
	public string date_captured = DateTime.Now.ToString("yyyy-MM-dd");
	
	public image(int idNum, int widthSize, int heightSize, string file)
	{
		id = idNum;
		width = widthSize;
		height = heightSize;
		file_name = file;
	}
}