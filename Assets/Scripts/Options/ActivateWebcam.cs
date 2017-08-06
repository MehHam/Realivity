using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateWebcam : MonoBehaviour {

	private GameObject go;
	private GameObject go2;
	private GameObject go3;

	private Color c; 
	// Use this for initialization
	public void UIdestroyer(bool gazedAt){

		go = GameObject.Find("Canvas");
		go2 = GameObject.Find("VRCameraUI");
		go3 = GameObject.Find("options");

		go.transform.Find("webCam").gameObject.SetActive(gazedAt);
		go3.transform.Find("RotatingHalo").gameObject.SetActive(!gazedAt);

		c = go2.GetComponent<RawImage>().color;

		if(!gazedAt){
			c.a = 0f;
			go.transform.Find("webCam").GetComponent<WebcamScript>().webcam.Stop();

			//Debug.Log("webcam is " +  go.transform.Find("webCam").GetComponent<WebcamScript>().webcam.isPlaying);
			//go2.GetComponent<RawImage>().texture= null;
		}else{
			c.a = 1f;
			go.transform.Find("webCam").GetComponent<WebcamScript>().webcam.Play();
			//Debug.Log("webcam is "+ go.transform.Find("webCam").GetComponent<WebcamScript>().webcam.isPlaying);
			}
		go2.GetComponent<RawImage>().color = c;

		}

}
