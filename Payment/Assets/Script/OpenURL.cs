using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    
    public void NicePayURL()
    {
        Application.OpenURL("file:///C:/Users/user/Desktop/pay-test.html");
    }

    public void BaycURL()
    {
        Application.OpenURL("https://boredapeyachtclub.com/#/");
    }
}
