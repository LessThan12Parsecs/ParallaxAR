using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DatasetLoader : MonoBehaviour
{
    public Transform sourcePrefab;
    float rad = Mathf.PI / 180f;
    public void SpawnCartesianFromPath(string path) 
    {
        string fullPath = Application.persistentDataPath + "/datasets/" +  path + ".csv";
        if ( File.Exists( fullPath ) ) {
            int id = 0;
            string[] starData;
            StreamReader sr = new StreamReader(fullPath);
            // headers = sr.ReadLine().Split(',');
            while (!sr.EndOfStream){
                starData = sr.ReadLine().Split(',');
                // List<float> sourceData = new List<float>();
                // for (int i = 1; i < starData.Length; i++) {
                //     sourceData.Add(float.Parse(starData[i]));
                // }
                // if (sourceData.Count >= 2){
                
                float a = x;
                float b = y;
                float c = 10f / z; //TODO: Check this
                x = (Mathf.Cos(a * rad) * Mathf.Cos(b * rad) * c) / 1;
                y = (Mathf.Sin(a * rad) * Mathf.Cos(b * rad) * c) / 1;
                z = (Mathf.Sin(b * rad) * c) / 1;
                var sourceInstance = Instantiate(sourcePrefab);
                sourceInstance.SetParent(transform);

                    // sourceInstance.hideFlags = HideFlags.HideInHierarchy;
                    // var sourcePoint = sourceInstance.GetComponent<SourcePoint>();
                    // sourcePoint.sourceId = id.ToString();
                    id++;
                    // sourcePoint.data = sourceData;
                    // sourceList.Add(sourceInstance.gameObject);
                // }
            };
            // ReorderPlotsWithParameters(0,1,2);
        }
        else
            Debug.LogError ("CSV file not found");
	}
}
