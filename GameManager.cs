# GameManager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Button startGameButton, shopButton, acceptButton, ReturnFromButton;
    Image backgroundImage, chousenPrefabImage;
    GameObject[] prefabsForShop;
    GameObject chousenPrefab;
    int chousePrefabInShop, buffChousePrefabInShop;
    Camera ownCamera;
    int countPrefabsForShop = 3;
    bool canIWorkWithPrefab = false;
    string scene;

    void Start()
    {
        InitializationObjects();
        DisableAllObjectsFromShop();
    }

    void Update()
    {
        if(scene == "Shop")
        {
            ActiveAllObjetsInShop();
            ChousePrefabInWorld();
        }
    }

    private void InitializationObjects()
    {
        startGameButton = GameObject.Find("Button (1)").GetComponent<Button>(); 
        shopButton = GameObject.Find("Button").GetComponent<Button>();
        acceptButton = GameObject.Find("Button (2)").GetComponent<Button>();
        ReturnFromButton = GameObject.Find("Button (3)").GetComponent<Button>();
        backgroundImage = GameObject.Find("Image").GetComponent<Image>(); 
        prefabsForShop = GameObject.FindGameObjectsWithTag("Prefabs");
        ownCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        chousenPrefabImage = GameObject.Find("Image (1)").GetComponent<Image>(); 
    }

    public void GameMenu()
    {
        scene = "";
        DisableAllObjectsFromShop();
        startGameButton.gameObject.SetActive(true);
        shopButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        scene = "";
        DisableAllObjectFromGame();
        prefabsForShop[chousePrefabInShop].SetActive(true);
        prefabsForShop[chousePrefabInShop].transform.position = new Vector3(371, 42, -3);
    }

    public void Shop()
    {
        scene = "Shop";
    }

    public void ChousenPrefab()
    {
        chousePrefabInShop = buffChousePrefabInShop;
        GameMenu();
    }

    private void DisableAllObjectFromGame()
    {
        startGameButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        ReturnFromButton.gameObject.SetActive(true);
    }

    private void DisableAllObjectsFromShop()
    {
        acceptButton.gameObject.SetActive(false);
        ReturnFromButton.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        chousenPrefabImage.gameObject.SetActive(false);
        ReturnFromButton.gameObject.SetActive(false);

        for (int i = 0; i < countPrefabsForShop; i++) prefabsForShop[i].SetActive(false);
    }

    private void ActiveAllObjetsInShop()
    {
        startGameButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(true);
        chousenPrefabImage.gameObject.SetActive(true);
        //ReturnFromButton.gameObject.SetActive(true);
        for (int i = 0; i < countPrefabsForShop; i++) prefabsForShop[i].SetActive(true);

        SeparationOfGameObject();
    }

    private void SeparationOfGameObject()
    {
        Vector3 buffVector3 = new Vector3(371, 40, -3);

        for (int i = 0; i < countPrefabsForShop; i++)
        {
            buffVector3.y = buffVector3.y + 2.2f;
            prefabsForShop[i].transform.position = buffVector3;
        }
    }

    private void ChousePrefabInWorld()
    {
        Vector3 prefabVectro3, mouseVector3 = new Vector3(0, 0, 0);
        float distantionFromMouseToPrefabX, distantionFromMouseToPrefabY;
        Ray ray;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            ray = ownCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            mouseVector3 = hit.point;
            if (mouseVector3.z != 0)
            {
                for (int i = 0; i < countPrefabsForShop; i++)
                {
                    prefabVectro3 = prefabsForShop[i].transform.position;
                    distantionFromMouseToPrefabX = System.Math.Abs(mouseVector3.x - prefabVectro3.x);
                    distantionFromMouseToPrefabY = System.Math.Abs(mouseVector3.y - prefabVectro3.y);

                    if (distantionFromMouseToPrefabX <= 0.5 && distantionFromMouseToPrefabY <= 0.5)
                    {
                        acceptButton.gameObject.SetActive(true);
                        buffChousePrefabInShop = i;
                        SetChousenImage(buffChousePrefabInShop);
                        break;
                    }
                    else
                    {
                        acceptButton.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void SetChousenImage(int buffChousePrefabInShop)
    {
        Vector3 buffVector3 = chousenPrefabImage.transform.position;

        chousenPrefabImage.gameObject.SetActive(true);
        buffVector3.x = prefabsForShop[buffChousePrefabInShop].transform.position.x - 20;
        buffVector3.y = 30 * (buffChousePrefabInShop+1);
        chousenPrefabImage.transform.position = buffVector3;
    }
}
