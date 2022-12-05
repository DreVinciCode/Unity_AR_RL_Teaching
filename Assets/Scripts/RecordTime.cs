using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class RecordTime : MonoBehaviour
{
    string filename = ""; 

    [System.Serializable]
    private class UserAction 
    {
        public string id;
        public string date; //currnet time
        public string condition;
        public string key; 
        public float timestamp; 
    }

    private List<UserAction> myUserActionList = new List<UserAction>();
     
    private float gameTimer = 0f;
    
    private void Start()
    {
        //filename = "/Users/victoriachen/Desktop" + "/test.csv"; 
        filename = Application.dataPath + "/record.csv";
        Debug.Log(filename);
        //WriteCSV(); 
        
    }

    private void Update()
    {

        gameTimer += Time.deltaTime; 

        UserAction myUserAction = new UserAction();
        //myUserAction.id = "Victoria"; 
        //myUserAction.date = System.DateTime.Now.ToString();
        //myUserAction.condition = "Control";
        //myUserAction.key = "space";
        //myUserAction.timestamp = gameTimer;

        Event e = Event.current;
        if (e != null && e.isKey)
        {
            Debug.Log(e.keyCode);
            //InsertElement(e.KeyCode.toString(), gameTimer);
            myUserAction.id = "Victoria";
            myUserAction.date = System.DateTime.Now.ToString();
            myUserAction.condition = "Control";
            myUserAction.key = e.keyCode.ToString();
            myUserAction.timestamp = gameTimer;
            myUserActionList.Add(myUserAction);
            WriteCSV();
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //    //myUserAction.key = "space";
        //    //myUserAction.timestamp = gameTimer;
        //    myUserActionList.Add(myUserAction); 
        //    WriteCSV(); 
    }

    public void WriteCSV() 
    {
        if (myUserActionList.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("ID, Date, Condition, Key, Timestamp");
            tw.Close(); 

            tw = new StreamWriter(filename, true); 

            for (int i = 0; i < myUserActionList.Count; i++)
            {
                tw.WriteLine(myUserActionList[i].id + ", " +
                             myUserActionList[i].date + ", " +
                             myUserActionList[i].condition + ", " +
                             myUserActionList[i].key + ", " +
                             myUserActionList[i].timestamp); 
            }
            tw.Close();
            Debug.Log("gameTimer" + gameTimer); 
        }
    }
}
