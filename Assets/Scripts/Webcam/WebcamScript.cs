using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

using UnityEngine.Video;

public class WebcamScript : MonoBehaviour {

	public WebCamTexture webcam;
	private Texture tex;
	public Texture2D _TextureFromCamera ;
	[Range(1,10)]
	public int resolution = 1; 
	private int currentResolution;
	public Camera cam;
	/*[Range(0.0001f,1f)]
	public float _ColorAmplification = 1f;*/

	//public GameObject go;
	public GameObject go2;

	//private Vector3[] names;
	private int[,] names;
	private Color[] colors;

	public string info;

	private Color[] coul = new Color[7]{ new Color(240,70,250)/*violet*/, new Color(75f,0f,130f),/*new Color(49,0,246),*//*indigo*/Color.blue, Color.green, Color.yellow, Color.red, new Color(255,0,0,0.01f)  };
	//private Color[] coul = new Color[8]{new Color(255,0,0,0.01f), Color.red,/*indigo*/Color.yellow, new Color(255f, 130f, 0f),Color.green, Color.blue, new Color(75f,0f,130f), new Color(240,130,240)/*violet*/ };

	[Range(0,1)]
	public static float frequency ;


	[Range(0.001f,0.99999f)]
	public float velocity;//(normalized to the speed of light)

	public float coeffRelat=1f;

	public bool isResolutionSwitched;
	public bool isVelocitySwitched;
	
	private WebCamDevice[] devices;

	//public Text txt;

	public bool isfrequency; 

	public int index;

		public float timer; 

	// Use this for initialization
	void Start () {
		 
		index = 0;
		velocity = 0.5f;
	
		isfrequency= false;
		isResolutionSwitched = true;
		isVelocitySwitched = true;

		timer = 0f;

		webcam = new WebCamTexture();
		
		//goUDP = GameObject.Find("UDPListener");

		create();


	}
	
	// Update is called once per frame
	void Update () {

			resolutionSwitcher();
			velocitySwitcher() ;

			velocity = remoteFrequency(isfrequency, UDPReceive.lastReceivedUDPPacket.Split('\t'), index,timer, velocity);
		
			coeffRelat = (velocity-0.50f)*(Mathf.Cos(Mathf.Deg2Rad*cam.transform.eulerAngles.y)) + 0.5f;

			StartCoroutine(CaputureWebcamAsVideo(resolution, coeffRelat));
	}

	public void resolutionSwitcher(){

		if(resolution==10){
			isResolutionSwitched= false;
		}
		if(resolution==1)
			isResolutionSwitched= true;

	}

		public void velocitySwitcher(){

			if(velocity>=0.9999f){
				isVelocitySwitched= false;
			}
			if(velocity<=0.01f)
				isVelocitySwitched= true;

		}


		public float remoteFrequency(bool isRandom, string[] arr, int index, float frequencyTimer, float vitesse){ //
		float velocity =0; 
		
			if(arr.Length!=1 && arr!= null){
			if(!isRandom){

				velocity = 0.5f- Utils.stringToVector3(arr[1])[index];

					if(velocity>=1)
						velocity =0.9999f;
					
					if(velocity<=0)
						velocity =0.001f;

				info =" Velocity is on remote : " + velocity;

			} else{
				if(arr.Length==4){

						if(Utils.stringToVector3(arr[1])[index] == 0f){
							timer = Time.timeSinceLevelLoad - timer ;
				
							if(timer>=1 && timer!=0f) //the limit is 60Hz is max speed 1*c where c is the speed of light
							velocity = 1f/timer;
						else
							velocity = 1f;
						}
						timer = Time.timeSinceLevelLoad;
					
				timer =0f;
				info= "Frequency " + velocity;

				}
			}
			}else{
				velocity = vitesse;
			}
		
			return velocity;
	}


	void create (){

		//names = new Vector3[ 640* 480]; 
		names = new int [640,480]; 
		colors = new Color[640*480];

		int k =0;
		
		GetComponent<RawImage>().texture = webcam ;
		
		devices = WebCamTexture.devices;
		webcam.deviceName = devices[0].name;
		webcam.Play();

		_TextureFromCamera = new Texture2D(webcam.width, webcam.height);
		go2 = GameObject.Find("VRCameraUI"); 
		go2.GetComponent<RawImage>().texture = _TextureFromCamera;
	

		for (int i=0; i< webcam.height; i++){

			for (int j=0; j < webcam.width; j++){

				names[j,i] = k;//new Vector3(k,i,j);
				colors[k] = Color.black;//new Color((i)/(1.0f*webcam.width), (j)/(1.0f*webcam.height), 1,1);
				k++;
			}
		}

		_TextureFromCamera.SetPixels(colors);
		
	}

	IEnumerator CaputureWebcamAsVideo(int resolution, float coeffRelat){  //resolution represents here the binning value
		// Use this for initialization
		yield return new WaitForEndOfFrame();

		Color c;
		//for (int k=0;k < colors.Length; k++){
		for (int i=0; i<= webcam.height-resolution; i=i+resolution){

			for (int j=0; j <= webcam.width-resolution; j=j+resolution){

			c = gradShift(coul, webcam.GetPixel(j,i), coeffRelat);
				
			for(int l=0; l< resolution; l++){
					for(int m=0; m < resolution; m++)
						colors[names[j+m,i+l]] = c;
				}
			}		
		}

		_TextureFromCamera.SetPixels(colors);
		_TextureFromCamera.Apply();
	}

	private Color gradShift(Color[] coul, Color c, float velocity){
	
		float lum = 0.2126f*c.r + 0.7152f*c.g + 0.0722f*c.b;

		int ixi = (lum < 0.5f)? 0:1 ;
		int ix = (int)(velocity*(coul.Length-2)) + ixi;
					
		float a = (lum - ixi*0.5f)/0.5f;

		//return (coul[ix]*(1- a) + coul[ix+1]*(a));
		return (coul[ix]*(1- 0.5f*a) + coul[ix+1]*(a));


	}



}
