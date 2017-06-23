using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter2
{
    public class NewBehaviourScript : MonoBehaviour
    {
        GameObject nyScript;

        // Use this for initialization
        void Start()
        {
            nyScript = GameObject.Find("Script");
            if (nyScript != null)
                Debug.Log(nyScript.transform.parent);
        }

        // Update is called once per frame
        //void Update()
        //{
        //    if (Input.GetKeyUp(KeyCode.O))
        //    {
        //        nyScript.callGameOverEvent();
        //        this.enabled = false;
        //    }

        //}
    }

}
