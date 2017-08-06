using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                  
        [SerializeField] private Material m_ClickedMaterial;               
        [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;


        private void Awake ()
        {
            m_Renderer.material = m_NormalMaterial;
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("Show over state");
            m_Renderer.material = m_OverMaterial;

			if(transform.name == "options"){
				transform.GetComponent<ActivateWebcam>().UIdestroyer(false);
			
			}else{
				//transform.GetComponent<ActivateWebcam>().UIdestroyer(false);
				Color c = GameObject.Find("VRCameraUI").GetComponent<RawImage>().color;
				c.a = 0.2f;
				GameObject.Find("VRCameraUI").GetComponent<RawImage>().color =c;

				transform.GetComponent<ActivateOptions>().textUIChanger(true);

			}
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;

			if(transform.name == "options"){
				transform.GetComponent<ActivateWebcam>().UIdestroyer(true);

			}else{
			//transform.GetComponent<ActivateWebcam>().UIdestroyer(true);
			transform.GetComponent<ActivateOptions>().textUIChanger(false);
			Color c = GameObject.Find("VRCameraUI").GetComponent<RawImage>().color;
			c.a = 1f;
				GameObject.Find("VRCameraUI").GetComponent<RawImage>().color = c;
			}
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
			transform.GetComponent<ActivateOptions>().TeleportRandomly();

        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}