
using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class graph : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite CircleSprite;
    private RectTransform graphContainer;
    private List<int> hlist = new List<int>();
    private List<int> x = new List<int>(){30,30,30,30,30};
    private GameObject CreateCircle(Vector2 anchoredPosition){
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().sprite = CircleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        return gameObject;
    }

    private void showGraph(List<int> valueList,List<int> valueList2){
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 20f;
        float xSize = 45f;
        GameObject last = null;
        for(int i = 0; i<valueList.Count; i++){
            float xPosition = i*xSize - 35;
            float yPosition = ((valueList[i]-20)/yMaximum) * graphHeight-100;
            GameObject x = CreateCircle(new Vector2(xPosition, yPosition));
            if(last != null)
                CreateDotConnection(last.GetComponent<RectTransform>().anchoredPosition,x.GetComponent<RectTransform>().anchoredPosition);
            last = x;
        }
        last = null;
        for(int i = 0; i<valueList2.Count; i++){
            float xPosition = i*xSize - 35;
            float yPosition = ((valueList2[i]-10)/yMaximum) * graphHeight-100;
            GameObject x = CreateCircle(new Vector2(xPosition, yPosition));
            if(last != null)
                CreateDotConnection2(last.GetComponent<RectTransform>().anchoredPosition,x.GetComponent<RectTransform>().anchoredPosition);
            last = x;
        }
    }

    private void CreateDotConnection(Vector2 a, Vector2 b){
        GameObject gameObject = new GameObject("dotConnection",typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (b-a).normalized;
        rectTransform.sizeDelta = new Vector2(45,3f);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        float distance = Vector2.Distance(a,b);
        rectTransform.anchoredPosition = a+dir*distance*0.5f;
        rectTransform.localEulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private void CreateDotConnection2(Vector2 a, Vector2 b){
        GameObject gameObject = new GameObject("dotConnection",typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().color = new Color(1,50,50,1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (b-a).normalized;
        rectTransform.sizeDelta = new Vector2(45,3f);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        float distance = Vector2.Distance(a,b);
        rectTransform.anchoredPosition = a+dir*distance*0.5f;
        rectTransform.localEulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir));
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.Find("MQTT");
        M2MqttUnity.Examples.M2MqttUnityTest speedController = go.GetComponent<M2MqttUnity.Examples.M2MqttUnityTest>();
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        foreach (Transform child in graphContainer) { GameObject.Destroy (child.gameObject); } 
        showGraph(speedController.AX,speedController.BX);
        
    }
}
