﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamController : MonoBehaviour
{

    int width = 1920;
    int height = 1080;
    int fps = 30;
    Texture2D texture;
    WebCamTexture webcamTexture;
    Color32[] colors = null;


   IEnumerator Init()
   {
       while(true)
       {
           if(webcamTexture.width > 16 && webcamTexture.height >16)
           {
               colors = new Color32[webcamTexture.width * webcamTexture.height];
               texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
               GetComponent<Renderer>().material.mainTexture = texture;
               break;
           }
            yield return null;
       }
   }
    
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        //Debug.Log(devices[0].name);
        webcamTexture = new WebCamTexture(devices[1].name, this.width, this.height, this.fps);
        webcamTexture.Play();

        StartCoroutine (Init());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            CameraDo();
        }
    }

    void CameraDo()
    {
        if (colors != null)
            {
                webcamTexture.GetPixels32(colors);

                int width = webcamTexture.width;
                int height = webcamTexture.height;
                Color32 rc = new Color32();

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height ; y++)
                    {
                        Color32 c = colors [x + y * width];
                            //byte gray = (byte)(0.1f * c.r + 0.7f * c.g + 0.2f * c.b);
                        //rc.r = rc.g = rc.b = gray;
                        //colors [x + y * width] = rc;
                        /*
                        rc.r = (byte)(c.r / 5);
                        rc.g = (byte)(c.g / 5);
                        rc.b = (byte)(c.b / 5);
                        */
                        
                        rc.r = (byte)( 255f * ( Mathf.Pow (0.6f * c.r/255f , 2.5f)));
                        rc.g = (byte)( 255f * ( Mathf.Pow (0.6f * c.g/255f , 2.5f)));
                        rc.b = (byte)( 255f * ( Mathf.Pow (0.6f * c.b/255f , 2.5f)));
                        
                        colors [ x + y * width] = rc;
                    }
                }

                texture.SetPixels32 (colors);
                texture.Apply();

            }
    }
}