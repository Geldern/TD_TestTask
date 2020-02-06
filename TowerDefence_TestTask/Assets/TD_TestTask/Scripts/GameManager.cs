using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int PlayerMoney;
    public int PlayerHealth;
    public Transform buildGUI;
    public GameObject[] Towers;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI WinText;
    public TextMeshProUGUI LooseText;
    public Button RetryButton;
    public Transform SellButton;
    [HideInInspector]
    public Transform selectedBase;
    [HideInInspector]
    public Transform curTower;

    void Start()
    {
        Time.timeScale = 1;
        TextMeshProUGUI sellText = SellButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void Update()
    {
        MoneyText.text = "Money: " + PlayerMoney;
        HealthText.text = "Health: " + PlayerHealth;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == ("BuildingBase") && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (hit.collider.GetComponent<BaseController>().IsBuilded == false)
                    {
                        buildGUI.transform.position = Input.mousePosition;
                        buildGUI.gameObject.SetActive(true);
                        selectedBase = hit.transform;
                        SellButton.gameObject.SetActive(false);

                    }
                    else
                    {
                        selectedBase = hit.transform;
                        SellButton.transform.position = Input.mousePosition;
                        SellButton.gameObject.SetActive(true);
                        buildGUI.gameObject.SetActive(false);
                        SellButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sell" + curTower.GetComponent<TowerController>().Tower.SellPrice + "$";
                    }
                }
                else if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    buildGUI.gameObject.SetActive(false);
                    SellButton.gameObject.SetActive(false);
                }
            }
        }

        if (selectedBase != null && selectedBase.childCount > 0)
        {
            curTower = selectedBase.GetChild(0);
        }

        if (PlayerHealth <= 0)
        {
            LooseGame();
        }
    }

    public void BuildTower(string TowerName)
    {
        if(selectedBase.GetComponent<BaseController>().IsBuilded == false)
        {
            foreach (GameObject tower in Towers)
            {
                if (tower.name == TowerName)
                {
                    if (tower.GetComponent<TowerController>().Tower.BuildPrice <= PlayerMoney)
                    {
                        GameObject.Instantiate(tower, selectedBase.position, selectedBase.rotation, selectedBase);
                        selectedBase.GetComponent<BaseController>().IsBuilded = true;
                        PlayerMoney = PlayerMoney - tower.GetComponent<TowerController>().Tower.BuildPrice;
                        buildGUI.gameObject.SetActive(false);
                        selectedBase = null;
                    }
                    else Debug.Log("No enough money");
                }
            }
            
        }
       
    }

    public void SellTower()
    {
        PlayerMoney = PlayerMoney + curTower.GetComponent<TowerController>().Tower.SellPrice;
        GameObject.Destroy(curTower.gameObject);
        selectedBase.GetComponent<BaseController>().IsBuilded = false;
        SellButton.gameObject.SetActive(false);
        selectedBase = null;
    }

    public void WinGame()
    {
        WinText.gameObject.SetActive(true);
        RetryButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void LooseGame()
    {
        LooseText.gameObject.SetActive(true);
        RetryButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level");
    }
    
}