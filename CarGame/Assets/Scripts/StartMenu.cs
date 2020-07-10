using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GAMETYPE { SINGLE, MULTI };
public class StartMenu : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;
    public GAMETYPE gameType = GAMETYPE.SINGLE;
    // Start is called before the first frame update
    void Start()
    {
        Button m_play = panelA.transform.Find("Play").GetComponent<Button>();
        Button m_quit = panelA.transform.Find("Quit").GetComponent<Button>();
        Button m_single = panelB.transform.Find("Single").GetComponent<Button>();
        Button m_multi = panelB.transform.Find("Multi").GetComponent<Button>();
        m_play.onClick.AddListener(delegate ()
        {
            playA();
        });
        m_single.onClick.AddListener(delegate ()
        {
            playSingle();
        });
        m_multi.onClick.AddListener(delegate ()
        {
            playMulti();
        });
        m_quit.onClick.AddListener(delegate ()
        {
            Quit();
        });
        panelB.SetActive(false);
    }

    public void playA()
    {
        panelA.SetActive(false);
        panelB.SetActive(true);

    }

    public void playSingle()
    {
        gameType = GAMETYPE.SINGLE;
        PlayerPrefs.SetString("GAMETYPE", "SINGLE");
        SceneManager.LoadScene(1);
    }

    public void playMulti()
    {
        gameType = GAMETYPE.MULTI;
        PlayerPrefs.SetString("GAMETYPE", "MULTI");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
