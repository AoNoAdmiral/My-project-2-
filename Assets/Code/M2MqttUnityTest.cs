/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class M2MqttUnityTest : M2MqttUnityClient
    {
        //[Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        //public bool autoTest = true; 
        //[Header("User Interface")]
        //public InputField consoleInputField;
        //public Toggle encryptedToggle;
        public InputField addressInputField;
        public InputField portInputField;
        public InputField userInputField;
        public InputField pwdInputField;
        public InputField topicInputField;

        //public Button connectButton;
        //public Button disconnectButton;
        //public Button testPublishButton;
        //public Button clearButton;
        public string Topic;
        public string Machine_Id;
        public string Topic_to_Subcribe="";
        public string msg_received_from_topic="";
        public Image text_displaya;
        public Image text_displayb;
        public Image text_displaya1;
        public Image text_displayb1;
        public Button PRESS;
        public GameObject Page1;
        public GameObject Page2;
        public GameObject a1;
        public GameObject b1;
        public GameObject a2;
        public GameObject b2;
        public int Heat;
        public int Humd;
        public List<int> AX = new List<int>();
        public List<int> BX = new List<int>();
        private List<string> eventMessages = new List<string>();
        private bool updateUI = false;
        private bool connect = false;
        private int a = 0;
        //private void Awake()
        //{
        //    Topic_to_Subcribe = Topic + Machine_Id;
        //}
	public void UpdateBeforeConnect(){

            this.brokerAddress = "mqttserver.tk";
            this.brokerPort = 1883;
            this.mqttUserName = "bkiot";
            this.mqttPassword = "12345678";
            // this.brokerAddress = addressInputField.text;
            // this.brokerPort = 1883;
            // this.mqttUserName = userInputField.text;
            // this.mqttPassword = pwdInputField.text;
	}
        public void TestPublish()
        {
            if (connect){
                string x = "/bkiot/1914472/status";
                if (a%100==0)
                    client.Publish(x, System.Text.Encoding.UTF8.GetBytes("{\"temperature\": \"30\", \"humidity\": \"30\"}"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                a=a+1;
            }
            AddUiMessage("Test message published.");
        }
       
        public void SetBrokerAddress(string brokerAddress)
        {
           if (addressInputField)
           {
               this.brokerAddress = brokerAddress;
           }
        }

        //public void SetBrokerPort(string brokerPort)
        //{
        //    if (portInputField && !updateUI)
        //    {
        //        int.TryParse(brokerPort, out this.brokerPort);
        //    }
        //}

        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }


        //public void SetUiMessage(string msg)
        //{
        //    if (consoleInputField != null)
        //    {
        //        consoleInputField.text = msg;
        //        updateUI = true;
        //    }
        //}

        public void AddUiMessage(string msg)
        {
            //if (consoleInputField != null)
            //{
            //    consoleInputField.text += msg + "\n";
            //    updateUI = true;
            //}
        }

        protected override void OnConnecting()
        {
            base.OnConnecting();
            //SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {

            base.OnConnected();
            SubscribeTopics();
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { "/bkiot/1914472/status" }, new byte[] {0});
            connect = true;
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { "/bkiot/1914472/status" });
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            AddUiMessage("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            AddUiMessage("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            AddUiMessage("CONNECTION LOST!");
        }

        private void UpdateUI()
        {
            //if (client == null)
            //{
            //    if (connectButton != null)
            //    {
            //        connectButton.interactable = true;
            //        disconnectButton.interactable = false;
            //        testPublishButton.interactable = false;
            //    }
            //}
            //else
            //{
            //    if (testPublishButton != null)
            //    {
            //        testPublishButton.interactable = client.IsConnected;
            //    }
            //    if (disconnectButton != null)
            //    {
            //        disconnectButton.interactable = client.IsConnected;
            //    }
            //    if (connectButton != null)
            //    {
            //        connectButton.interactable = !client.IsConnected;
            //    }
            //}
            //if (addressInputField != null && connectButton != null)
            //{
            //    addressInputField.interactable = connectButton.interactable;
            //    addressInputField.text = brokerAddress;
            //}
            //if (portInputField != null && connectButton != null)
            //{
            //    portInputField.interactable = connectButton.interactable;
            //    portInputField.text = brokerPort.ToString();
            //}
            //if (encryptedToggle != null && connectButton != null)
            //{
            //    encryptedToggle.interactable = connectButton.interactable;
            //    encryptedToggle.isOn = isEncrypted;
            //}
            //if (clearButton != null && connectButton != null)
            //{
            //    clearButton.interactable = connectButton.interactable;
            //}
            //updateUI = false;
        }

        protected override void Start()
        {
            //SetUiMessage("Ready.");
            PRESS.onClick.AddListener(Connect);
            Topic_to_Subcribe = Topic + Machine_Id;
            updateUI = true;
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
            a1.SetActive(true);
            b1.SetActive(true);
            a2.SetActive(false);
            b2.SetActive(false);
            string msg = System.Text.Encoding.UTF8.GetString(message);
            msg_received_from_topic = msg;
            char[] charsToTrim1 = {'{','}',':'};
            string[] b = msg.Split('\"');
            Heat = Int16.Parse(b[3].Trim(charsToTrim1));
            Humd = Int16.Parse(b[7].Trim(charsToTrim1));
            if(AX.Capacity > 5)
                AX.RemoveAt(0);
            AX.Add(Heat);
            if(BX.Capacity > 5)
                BX.RemoveAt(0);
            BX.Add(Humd);
            StoreMessage(msg);
        }

        public string getUpdate(){
            return msg_received_from_topic;
        }

        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(string msg)
        {
            AddUiMessage("Received: " + msg);
        }

        protected override void Update()
        {
            if(!connect)
                UpdateBeforeConnect();
            TestPublish();
            base.Update(); // call ProcessMqttEvents()
            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
            if (updateUI)
            {
                UpdateUI();
            }
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            //if (autoTest)
            //{
            //    autoConnect = true;
            //}
        }
    }
}
