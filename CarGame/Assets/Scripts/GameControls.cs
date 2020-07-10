using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControls : MonoBehaviour
{
    private GameObject car;
    public bool isStart = false;
    public bool isOver = false;
    private Text timeLabel, startLabel, bestLabel;
    private float time = 0;
    private bool isGameCompleted = false;
    private GameObject[] CarComputer;
    private string type = GAMETYPE.SINGLE.ToString();
    // Start is called before the first frame update
    [SerializeField]
    private Transform[] m_PlayTransform;
    private Transform startPoint;
    private Text rankTxt;
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Car");
        type = PlayerPrefs.GetString("GAMETYPE");
        if(type == GAMETYPE.SINGLE.ToString())
        {
            CarComputer = GameObject.FindGameObjectsWithTag("CarComputer");
            foreach(GameObject item in CarComputer)
            {
                item.SetActive(false);
            }
          
        }
        car = GameObject.FindGameObjectWithTag("Car");
        timeLabel = GameObject.Find("Time").GetComponent<Text>();
        startLabel = GameObject.Find("Start").GetComponent<Text>();
        bestLabel = GameObject.Find("Best").GetComponent<Text>();
        rankTxt = GameObject.Find("Rank").GetComponent<Text>();
        startPoint = GameObject.Find("StartPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGameCompleted || this.name != "Finish")
        {
           
            return;
        }
         
        time += Time.deltaTime;
        if (time <= 2)
        {
            startLabel.text = "Ready";
        }
        else
        {
            if (time <= 3)
            {
                startLabel.text = "GO";
            }
            else
            {
                isStart = true;
                if (!isOver)
                {
                    startLabel.text = "";
                    timeLabel.text = (Mathf.RoundToInt(time) - 3).ToString();
                }
                else
                {
                    if(type != GAMETYPE.SINGLE.ToString())
                    {
                        return;
                    }
                    time = int.Parse(timeLabel.text);
                    startLabel.text = "Your Time" + timeLabel.text + " '' ";
                    if (PlayerPrefs.HasKey("bestTime"))
                    {
                        float best = PlayerPrefs.GetFloat("bestTime");
                        if(time < best)
                        {
                            PlayerPrefs.SetFloat("bestTime", Mathf.RoundToInt(time));
                            bestLabel.text = "You are the best!";
                        }
                        else
                        {
                            bestLabel.text = "Best Time: " + best + "''";
                        }
                    }
                    else
                    {
                        bestLabel.text = "You are the best!";
                        PlayerPrefs.SetFloat("bestTime", Mathf.RoundToInt(time));
                    }
                    isGameCompleted = true;
                }
            }
        }
        Ranks();
    }

    private void Ranks()
    {
        if (type != GAMETYPE.SINGLE.ToString())
        {

            float a = 0f;
            float b = Vector3.Dot(m_PlayTransform[0].forward, m_PlayTransform[1].position) >0? 
                Vector3.Distance(m_PlayTransform[0].position, m_PlayTransform[1].position):
                -Vector3.Distance(m_PlayTransform[0].position, m_PlayTransform[1].position);
            float c = Vector3.Dot(m_PlayTransform[0].forward, m_PlayTransform[2].position) > 0 ?
                Vector3.Distance(m_PlayTransform[0].position, m_PlayTransform[2].position) :
                -Vector3.Distance(m_PlayTransform[0].position, m_PlayTransform[2].position);
            float d = Vector3.Dot(m_PlayTransform[0].forward, m_PlayTransform[3].position) > 0 ?
                Vector3.Distance(m_PlayTransform[0].position, m_PlayTransform[3].position) :
                -Vector3.Distance(m_PlayTransform[0].position, m_PlayTransform[3].position);

            //  Debug.Log("play" + a + "  " + b + "  " + c + "  " + d);
            string[] names = {m_PlayTransform[0].name, m_PlayTransform[1].name, m_PlayTransform[2].name, m_PlayTransform[3].name};
            float[] distance = { a, b, c, d };
            int i, j;  
            float temp1;
            string temp2;
            for (i = 0; i < distance.Length - 1; i++)
            {
                for (j = i + 1; j < distance.Length; j++)
                {
                    if (distance[i] > distance[j]) 
                    {
                      //  Debug.Log(distance[i]);
                        temp1= distance[i]; 
                        distance[i] = distance[j];
                        distance[j] = temp1; 
                        temp2 = names[i];
                        names[i] = names[j];
                        names[j] = temp2;
                    }
                }
            }
          //  Debug.Log("1"+distance[0]+names[0]);
            rankTxt.text = string.Format("第一名:{0}\n第二名:{1}\n第三名:{2}\n第四名:{3}", names[0], names[1], names[2], names[3]);
        }
        #region
        //float[] d = { a, b, c };
        //float dis = 0f;
        //float max = ((a > b ? a : b) > c) ? (a > b ? a : b) : c;//大Max = round > c ? round : c
        //float middle = ((a > b ? a : b) > c) ? ((a < b ? a : b) > c ? (a < b ? a : b) : c) : (a > b ? a : b);
        //float min = ((a > b ? a : b) > c) ? ((a < b ? a : b) > c ? c : (a < b ? a : b)) : (a < b ? a : b);
        //if (max > 0 && middle > 0 && min > 0)
        //{
        //    for (int i = 0; i < d.Length - 1; i++)
        //    {
        //        if (max == d[i])
        //        {
        //            name[0]=m_PlayTransform[i].name;
        //        }
        //        else if (middle == d[i])
        //        {
        //            name[1] = m_PlayTransform[i].name;
        //        }
        //        else if (min == d[i])
        //        {
        //            name[2] = m_PlayTransform[i].name;
        //        }
        //    }
        //    name[3] = m_PlayTransform[0].name;

        //}
        //else
        //{
        //    if (max < 0)
        //    {
        //        name[0] = m_PlayTransform[0].name;
        //        for (int i = 0; i < d.Length - 1; i++)
        //        {
        //            if (max == d[i])
        //            {
        //                name[1] = m_PlayTransform[i].name;
        //            }
        //            else if (middle == d[i])
        //            {
        //                name[2] = m_PlayTransform[i].name;
        //            }
        //            else if (min == d[i])
        //            {
        //                name[3] = m_PlayTransform[i].name;
        //            }
        //        }

        //    }
        //    else if (max > 0 && middle < 0)
        //    {

        //        for (int i = 0; i < d.Length - 1; i++)
        //        {
        //            if (max == d[i])
        //            {
        //                name[0] = m_PlayTransform[i].name;
        //                name[1] = m_PlayTransform[0].name;
        //            }
        //            else if (middle == d[i])
        //            {
        //                name[2] = m_PlayTransform[i].name;
        //            }
        //            else if (min == d[i])
        //            {
        //                name[3] = m_PlayTransform[i].name;
        //            }
        //        }
        //    }
        //    else if (middle > 0 && min < 0)
        //    {
        //        for (int i = 0; i < d.Length - 1; i++)
        //        {
        //            if (max == d[i])
        //            {
        //                name[0] = m_PlayTransform[i].name;

        //            }
        //            else if (middle == d[i])
        //            {
        //                name[1] = m_PlayTransform[i].name;
        //                name[2] = m_PlayTransform[0].name;
        //            }
        //            else if (min == d[i])
        //            {
        //                name[3] = m_PlayTransform[i].name;
        //            }
        //        }
        //    }

        //}
        #endregion
       
    }
    
    [HideInInspector]
    public int Rank = 1;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Collider_Bottom")
        {
            if(this.name == "Finish")
            {
                isOver = true;
                if(PlayerPrefs.GetString("GAMETYPE") == GAMETYPE.SINGLE.ToString())
                {

                }
                else
                {
                    startLabel.text = "Your Rank: " + Rank.ToString();
                }
            }
        }
        if (other.gameObject.name == "Collider_BottomComputer")
        {
            if(this.name == "Finish")
            {
                Rank++;
            }
        }
    }
}
