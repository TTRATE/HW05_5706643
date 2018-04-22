using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


public class GameClinet : MonoBehaviour {

	public string URL = "http://localhost:8081/user/login/CGM/CGM";
    //http://localhost:8081/user/id
    void Start () 
	{
		try
		{
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseBody = new StreamReader(stream).ReadToEnd();
            print(responseBody);
            
            
            
            LoginUserCheck[] userIDs = JsonConvert.DeserializeObject<LoginUserCheck[]>(responseBody); 
            print(userIDs[0].off);
            
        }
        catch(WebException ex)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
