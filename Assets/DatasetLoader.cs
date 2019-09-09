using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DatasetLoader))]
public class DatasetLoaderEditor : Editor {
	public override void OnInspectorGUI (){
		DrawDefaultInspector ();
		DatasetLoader manager = (DatasetLoader) target;
		if (GUILayout.Button("Load Dataset"))
			manager.SpawnPlotFromPath();
	}
}
#endif

public class DatasetLoader : MonoBehaviour
{
    public Transform sourcePrefab;
    public string datasetPath;
    float rad = Mathf.PI / 180f;
    public void SpawnPlotFromPath() 
    {
        string fullPath = Application.persistentDataPath + "/datasets/" +  datasetPath + ".csv";
        if ( File.Exists( fullPath ) ) {
            // int id = 0;
            string[] starData;
            StreamReader sr = new StreamReader(fullPath);
            // headers = sr.ReadLine().Split(',');
            while (!sr.EndOfStream){
                starData = sr.ReadLine().Split(',');
                float x = float.Parse(starData[1]); //1 ra
                float y = float.Parse(starData[2]); //2 dec
                float z = float.Parse(starData[3]); //3 parallax
                // List<float> sourceData = new List<float>();
                // for (int i = 1; i < starData.Length; i++) {
                //     sourceData.Add(float.Parse(starData[i]));
                // }
                // if (sourceData.Count >= 2){
                
                float a = x;
                float b = y;
                float c = 10f / z;

                x = (Mathf.Cos(a * rad) * Mathf.Cos(b * rad) * c);
                y = (Mathf.Sin(a * rad) * Mathf.Cos(b * rad) * c);
                z = (Mathf.Sin(b * rad) * c);

                var sourceInstance = Instantiate(sourcePrefab);
                sourceInstance.SetParent(transform);
                sourceInstance.transform.localPosition = new Vector3(x,y,z);

                // sourceInstance.hideFlags = HideFlags.HideInHierarchy;
                // var sourcePoint = sourceInstance.GetComponent<SourcePoint>();
                // sourcePoint.sourceId = id.ToString();
                // id++;
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
