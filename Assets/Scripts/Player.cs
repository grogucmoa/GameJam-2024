using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerType = 1;
    public GameManager gameManager;

    //Variables Vitesses
    public float vitesseDeplacement = 5f;
    private Vector3 positionClic;
    public bool isRunning = false;

    //variable range
    public float rayonDeplacement = 5f;
    [SerializeField] private GameObject _range;
    

    void Update()
    {
        if(PlayerType == gameManager.ActiveTour)
        {
            _range.SetActive(true);
            
            if (gameManager.ActiveTour>= 1 && gameManager.ActiveTour <= 4) 
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }

            if (gameManager.ActiveTour>= 5 && gameManager.ActiveTour <= 8) 
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            if (gameManager.ActiveTour>=9 && gameManager.ActiveTour <= 12) 
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }

            if (gameManager.ActiveTour>= 13 && gameManager.ActiveTour <= 16) 
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }

            if (Input.GetMouseButtonDown(0))
            {
                positionClic = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionClic.z = 0;

                if (Vector3.Distance(transform.position, positionClic) <= rayonDeplacement)
                {
                    isRunning = true;
                }
            }

            if (transform.position != positionClic && isRunning == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, positionClic, vitesseDeplacement * Time.deltaTime);

                }
            else if (isRunning == true)
                {
                    _range.SetActive(false);
                    gameManager.ActiveTour += 1;
                    isRunning = false;
                }
        }
    }
}
