using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	[RequireComponent(typeof(Collider))]
	public class ActivateOptions : MonoBehaviour {

		private Vector3 startingPosition;

		public Material inactiveMaterial;
		public Material gazedAtMaterial;
		public Text txt;
		public  GameObject go; //the game object on which the main script is attached
		

		public int port1;  
		public int port2;  

		void Start() {
			startingPosition = transform.localPosition;
			SetGazedAt(false);
			//go=GameObject.Find("TestCharacter");
			GameObject.Find("Player").transform.position=new Vector3(-40f,0f, 0f);
			go = GameObject.Find("webCam");
			
			port1=2390;
			port2=8080;

		//c= (Canvas)GameObject.Find("Canvas") = Canvas.
		}

	void Update(){

		//Debug.Log(transform.localPosition);


	}

		public void SetGazedAt(bool gazedAt) {

	
		if (inactiveMaterial != null && gazedAtMaterial != null) {
				GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
				Color c;
				c = GetComponent<MeshRenderer>().material.color; 
				c.a = gazedAt ? 1f:0f ;
				GetComponent<MeshRenderer>().material.color = c;
				return;
			}
			//GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
			
			

			
		}

		public void Reset() {
			transform.localPosition = startingPosition;
		}


		public void TeleportRandomly() {
			Vector3 direction = Random.onUnitSphere;
			switcherCubes();
			
		}

		public void switcherCubes(){

			if(this.gameObject.name=="Resolution"){
				if(go.GetComponent<WebcamScript>().isResolutionSwitched)
					go.GetComponent<WebcamScript>().resolution++;
				else
					go.GetComponent<WebcamScript>().resolution--;

					txt.text = " Resolution: " + go.GetComponent<WebcamScript>().resolution;
			}

			if(this.gameObject.name=="OutIn"){
			if(go.GetComponent<WebcamScript>().isVelocitySwitched)
				go.GetComponent<WebcamScript>().velocity += 0.1f;
			else
				go.GetComponent<WebcamScript>().velocity -= 0.1f;

			txt.text = " Velocity : " + go.GetComponent<WebcamScript>().velocity;
			}

			if(this.gameObject.name=="RandomizedVectors"){
				
				int ind = go.GetComponent<WebcamScript>().index;

					switch(ind){

					case(0): ind= 1;
						break;

					case(1): ind= 2;
						break;
					case(2): ind= 0;
						break;

				}

				go.GetComponent<WebcamScript>().index = ind;
				txt.text = " On remote component axis " + ind;
			}

			if(this.gameObject.name=="isStatic"){
			
				bool boolrandom = go.GetComponent<WebcamScript>().isfrequency ; 
				go.GetComponent<WebcamScript>().isfrequency = !boolrandom;

				txt.text = " Frequency mode" + go.GetComponent<WebcamScript>().isfrequency;
			}
			
			if(this.gameObject.name=="Infos"){
				
				if(GameObject.Find("UDPListener").GetComponent<UDPReceive>().port ==port1)
				{
					GameObject.Find("UDPListener").GetComponent<UDPReceive>().port =port2;

				}else{
					GameObject.Find("UDPListener").GetComponent<UDPReceive>().port =port1;

				}
				GameObject.Find("UDPListener").GetComponent<UDPReceive>().init();

			txt.text = " Port: " + GameObject.Find("UDPListener").GetComponent<UDPReceive>().port;
			}



	}
 
		public void textUIChanger(bool isPointed){

			if(isPointed){

			transform.Find("FireBall").gameObject.SetActive(true);
			GameObject.Find("VRCameraUI").transform.Find("Panel").gameObject.SetActive(true);
			GameObject.Find("options").GetComponent<rotate>().constant=0f;
			GetComponent<rotateOptions>().constant=0f;	

			if(this.gameObject.name=="Resolution"){

				txt.text = " Resolution: " + go.GetComponent<WebcamScript>().resolution;

				}

			if(this.gameObject.name=="RandomizedVectors"){
					
				txt.text = " On remote component axis " + go.GetComponent<WebcamScript>().index;
				}

			if(this.gameObject.name=="isStatic"){

				txt.text = " Frequency mode" + go.GetComponent<WebcamScript>().isfrequency + " freq is "+ go.GetComponent<WebcamScript>().timer ;
			}

			if(this.gameObject.name=="Infos"){
				//if(UDPReceive.lastReceivedUDPPacket!="")
				txt.text =  go.GetComponent<WebcamScript>().info;
			}

			}else{
				txt.text = " ";
				transform.Find("FireBall").gameObject.SetActive(false);
				
				GameObject.Find("VRCameraUI").transform.Find("Panel").gameObject.SetActive(false);
				GameObject.Find("options").GetComponent<rotate>().constant=20f;
				GetComponent<rotateOptions>().constant=200f;	
			}
			
		}
		



		// maximize acceleration 
		// check movuino values, and split them
}