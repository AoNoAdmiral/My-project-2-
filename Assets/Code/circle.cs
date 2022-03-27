
using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class circle : MonoBehaviour
{
    // Start is called before the first frame update
        public Text healthText;
        public Image healthBar;
        public Text humText;
        public Image humhBar;
        float health, maxHealth = 50;
        float hum, maxhum = 100;
        float lerpSpeed;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        GameObject go = GameObject.Find("MQTT");
        M2MqttUnity.Examples.M2MqttUnityTest speedController = go.GetComponent<M2MqttUnity.Examples.M2MqttUnityTest>();
        health = speedController.Heat;
        healthText.text = health+"°C";
        hum = speedController.Humd;
        humText.text = hum +"%";
        HealthBarFiller();
    }

    void HealthBarFiller(){
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,health / maxHealth,lerpSpeed);
        humhBar.fillAmount = Mathf.Lerp(humhBar.fillAmount,hum / maxhum,lerpSpeed);
        healthBar.color = Color.Lerp(Color.red,Color.green,health / maxHealth);
        humhBar.color = Color.Lerp(Color.red,Color.green,hum / maxhum);
    }
}