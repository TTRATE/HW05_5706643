﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class LoginScripts : MonoBehaviour {

    public string URL;
    public InputField userIDEnter;
    public InputField passwordEnter;

    private string loginUser;
    private string userIDkey;
    private string passwordkey;

    private ButtonManager btn;
    public LeaderBoardScript ldb;
    void Start()
    {
        btn = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        //ldb = GameObject.FindGameObjectWithTag("nameList").GetComponent<LeaderBoardScript>();
    }

   
    public void LoginToBorad()
    {

        userIDkey = userIDEnter.text;
        passwordkey = passwordEnter.text;
        
        loginUser = "http://ec2-52-221-196-106.ap-southeast-1.compute.amazonaws.com:8081/user/login/" + userIDkey + "/" + passwordkey;
        URL = loginUser;

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseBody = new StreamReader(stream).ReadToEnd();

            print(responseBody);


            int pass = 1;
            
            
           UserID[] userIDs = JsonConvert.DeserializeObject<UserID[]>(responseBody);
           print(userIDs[0].off);
             if(userIDs[0].off == 1)
             {
                 if(userIDs[0].off == 1)
                 {
                     print("True");
                     btn.LogToBoard();
                     ldb.ShowList();
                 }
                 else
                 {
                     print("passF");
                 }

             }
             else
             {
                 print("userF");

             }


        }
        catch (WebException ex)
        {

        }
       
    }
}
