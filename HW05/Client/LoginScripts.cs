using System.Collections;
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

    // Update is called once per frame
    public void LoginToBorad()
    {

        userIDkey = userIDEnter.text;
        passwordkey = passwordEnter.text;

        loginUser = "http://localhost:8081/user/login/" + userIDkey;
        URL = loginUser;

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseBody = new StreamReader(stream).ReadToEnd();

            print(responseBody);

           /* UserID log = JsonConvert.DeserializeObject<UserID>(responseBody);
            print(log);*/
            UserID[] userIDs = JsonConvert.DeserializeObject<UserID[]>(responseBody);
            
            if(userIDkey == userIDs[0].userID)
            {
                if(passwordkey == userIDs[0].password)
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
            

            /*for (int i = 0; i < userIDs.Length; i++)
            {
               if(userIDkey == userIDs[i].userID)
                {
                    if(passwordkey == userIDs[i].password)
                    {
                        print("Wellcome");
                        btn.LogToBoard();
                        ldb.ShowList();
                    }
                   
                }
                else
                {
                    print("Nope");
                }
               
            }*/

        }
        catch (WebException ex)
        {

        }
       
    }
}
