using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeyboard : MonoBehaviour
{
    public float offset = 100.0f; //Amount to move the form up
    public float dialogOff;
    Vector2 startPos;
    Vector2 endPos;
    float x, y, width, height;
    public TouchScreenKeyboard touchKeyboard;
    RectTransform rt;
    bool firstTime;
    void Start()
    {
        rt = transform.GetComponent<RectTransform>();
        startPos = rt.position;
    }

    void Update()
    {
        print(rt.anchoredPosition);
        if (TouchScreenKeyboard.visible ){
#if UNITY_ANDROID
            using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");
                using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
                {
                    View.Call("getWindowVisibleDisplayFrame", Rct);

                    int height = Rct.Call<int>("height");
                    int width = Rct.Call<int>("width");

                    int systemHeight = Display.main.systemHeight;
                    int systemWidth = Display.main.systemWidth;

                    offset = Screen.height - Rct.Call<int>("height");
                }
            }
            rt.anchoredPosition = Vector2.Lerp(new Vector2(0, 0), new Vector2 (0, offset+ (0.3f*(0.5f*offset))), 1);
            endPos = new Vector2(0, offset + (0.3f * (0.5f * offset)));
            firstTime = true;
        }
        else if(firstTime && !TouchScreenKeyboard.visible)
        {
            rt.anchoredPosition = Vector2.Lerp(endPos, new Vector2(0, 0), 1);
            
        }
#endif
    }
}