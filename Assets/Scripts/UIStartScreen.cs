using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickTesting()
    {
        SceneManager.LoadScene("Main");
    }

    /*public void OnClickValidation()
    {
        SceneManager.LoadScene("Validation");
    }*/

    public void OnClickShowWebView()
    {
        WebViewObject webViewObject = FindObjectOfType<WebViewObject>().GetComponent<WebViewObject>();
        if (webViewObject)
        {
            webViewObject.SetVisibility(true);
        }
    }

    public void OnClickCloseWebView()
    {
        WebViewObject webViewObject = FindObjectOfType<WebViewObject>().GetComponent<WebViewObject>();
        if (webViewObject)
        { 
            webViewObject.SetVisibility(false);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
