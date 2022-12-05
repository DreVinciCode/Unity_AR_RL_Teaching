using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DisplayRecordedData : MonoBehaviour
{

    public string[] filenames;
    private string filepath;

    public GameObject LocalParent;
    public GameObject PrefabMarker;

    private void Start()
    {
        //filepath = Application.dataPath + filename;
        filepath = Application.dataPath + "/record_success.csv";
        ReadCSVFile();
    }



    private void ReadCSVFile()
    {
        StreamReader streamReader = new StreamReader(filepath);
        bool endOfFile = false;

        while(!endOfFile)
        {
            string data_string = streamReader.ReadLine();
            if(data_string == null)
            {
                endOfFile = true;
                break;
            }

            var data_values = data_string.Split(',');

            var x_pos = float.Parse(data_values[0]);
            var y_pos = float.Parse(data_values[1]);
            var z_pos = float.Parse(data_values[2]);
            Vector3 position = new Vector3(x_pos, y_pos, z_pos);
            GameObject clone = Instantiate(PrefabMarker, LocalParent.transform);
            clone.transform.localPosition = position;


            /*
            for (int i = 0; i < data_values.Length; i++)
            {


                //Debug.Log(test);
                //Vector3 position = new Vector3(float.Parse(data_values[i]), float.Parse(data_values[i+1]), float.Parse(data_values[i+2]));
                //Debug.Log(position);
                //Instantiate();
            }
            */
        }
    }
}
