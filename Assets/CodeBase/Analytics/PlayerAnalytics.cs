using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Analytics
{
    // Higher than bootstrapper in execution order
    public class PlayerAnalytics : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void SaveData(string data);

        [DllImport("__Internal")]
        private static extern void LoadData();

        public AnalyticsData AnalyticsData;

        public static PlayerAnalytics Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                transform.parent = null;
                Instance = this;
                DontDestroyOnLoad(gameObject);

                AnalyticsData = new AnalyticsData();
                LoadData();
            }
            else
                Destroy(gameObject);
        }

        public void LoadAnalyticsFromJS(string data)
        {
            AnalyticsData = JsonUtility.FromJson<AnalyticsData>(data);
        }

        public void SaveAnalyticsDataJS()
        {
            string data = JsonUtility.ToJson(AnalyticsData);
            SaveData(data);
        }    
    }

    [Serializable]
    public class AnalyticsData
    {
        public float TotalTime;
        public float Deaths;
        public float Wins;

        public string Name;
    }
}
