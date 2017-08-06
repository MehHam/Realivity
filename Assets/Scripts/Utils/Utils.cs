using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Utils : MonoBehaviour {

	public static Vector3 stringToVector3(string sVector)
	{
		Vector3 result=Vector3.zero;
		if(sVector!=null || sVector!=""){
			//remove the ()
			if(sVector.StartsWith("(" )&& sVector.EndsWith(")"))
			{
				sVector=sVector.Substring(1,sVector.Length-2);
			}

			//split the items

			string[] sArray=sVector.Split(',');
			//store as a Vector3
			result=new Vector3(float.Parse(sArray[0]),float.Parse(sArray[1]),float.Parse(sArray[2]));
		}
		return result;
	}


	public static Vector4 stringToVector4(string sVector)
	{
		Vector4 result=Vector4.zero;
		float coef=1.0f;
		//split the items
		if(sVector !=null){
		string[] sArray=sVector.Split(' ');


			//store as a Vector3
		result=new Vector4(float.Parse(sArray[0]),float.Parse(sArray[1]),float.Parse(sArray[2]),float.Parse(sArray[3]));
		}
		return result;
	}


	public static Vector3 randomVectorGenerator(float accelRandom, float gyroRandom){

		Vector3 vect =new Vector3(Random.Range(-gyroRandom,gyroRandom),Random.Range(-gyroRandom,gyroRandom),Random.Range(-gyroRandom,gyroRandom));
		return vect;
		//return Vector3.one;
	}

	public static int ArraySum(int[] arr){

		int sum = 0;
	     foreach (int item in arr)
	     {
	         sum += item;
	     }
     return sum;
	}

	public static float VectorPostioninCube(float range, int resolution, Vector3 vec, Vector4[] vectors){
		float index ;
		float indexX;
		float indexY;
		float indexZ;

		indexX =((vec.x+range)*resolution/range);
		indexY =((vec.y+range)*resolution/range);
		indexZ =((vec.z+range)*resolution/range);

		//Debug.Log(indexX + "\t"+ indexY+ "\t"+ indexZ);

		index=0;

		List<Vector4> arr = new List<Vector4>();//creating an arraylist to search the corresponding index

		foreach (Vector4 v in vectors)
		{
			arr.Add(v);

			}
		Vector4 vectorXYZ = arr.Find(x=>((x.x==(int)indexX) && (x.y==(int)indexY) && (x.z==(int)indexZ) ) );
		index = vectorXYZ.w;

		return index;
	}


	public static ArrayList stringfromFile(string path){

		/*FileStream fs = new FileStream(path, FileMode.Open);
		string[] str = new string[fs.Length]; 
		string content = "";

		using (StreamReader read = new StreamReader(fs, true))
		{
			content = read.ReadLine();
			Debug.Log(content);
		}*/

		FileInfo theSourceFile = new FileInfo(path);
		StreamReader reader = theSourceFile.OpenText();
		string text="";
		ArrayList str = new ArrayList(); 

	
		while(text!=null){
			
			if(text != null){
				
				text=reader.ReadLine();

				str.Add(stringToVector4(text));

				//positions.Add(Utils.stringToVector3(text));
				//animLine.Enqueue(Utils.stringToVector3(text));

			}else{
				reader.Close();

			}
		}
		str.RemoveAt(str.Count-1);
		return str;
	}

}
