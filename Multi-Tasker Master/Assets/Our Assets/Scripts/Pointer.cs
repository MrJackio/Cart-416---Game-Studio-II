using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

//Script that casts a ray to determine where your controller is pointing. Also turns on and off laser if you are pointed at an object tagged 'menu'
//Written by Jack Harrison

public class Pointer : MonoBehaviour
{

    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_PointerAction;
    public GameObject m_laser;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;



    // Start is called before the first frame update
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);


        // Menu Hits

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (m_PointerAction.GetStateUp(m_Pose.inputSource) && (Physics.Raycast(ray, out hit)) && hit.transform.tag == "Play")
        {
            //Load Level Demo 1
            //Debug.Log("You pressed Play");
            SceneManager.LoadScene("Level 1 (Demo)");
        }

        if (m_PointerAction.GetStateUp(m_Pose.inputSource) && (Physics.Raycast(ray, out hit)) && hit.transform.tag == "Quit")
        {
            //Exit Game
            //Debug.Log("You pressed Quit");
            Application.Quit();
        }

    }


    private bool UpdatePointer()
    {
        // Ray from controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;


        // If it's a Hit
        if (Physics.Raycast(ray, out hit)) //&& (hit.transform.tag == "Play" || hit.transform.tag == "Quit"))
        {
            m_Pointer.transform.position = hit.point;
            if(hit.transform.tag == "Menu" || hit.transform.tag == "Play" || hit.transform.tag == "Quit" )
            {
                m_laser.SetActive(true);
            } else
            {
                m_laser.SetActive(false);
            }
            
            return true;
        }

        //If not hit
        m_laser.SetActive(false);
        return false;
       
    }

}
