using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;


using System.Collections.Generic;


using UnityEngine;


using TMPro;


public class VRInteractableDebug : MonoBehaviour

{

    public TMP_Text tmp_text;

    public void ResetText()

    {

        tmp_text.text = "";

    }

    public void RayHover()

    {

        tmp_text.text = "RayHover";

        Debug.Log("RayHover");

    }

    public void RaySelect()

    {

        tmp_text.text = "RaySelect";

        Debug.Log("RaySelect");

    }

    public void RayFocus()

    {

        tmp_text.text = "RayFocus";

        Debug.Log("RayFocus");

    }

    public void RayActivate()

    {

        tmp_text.text = "RayActivate";

        Debug.Log("RayActivate");

    }

}