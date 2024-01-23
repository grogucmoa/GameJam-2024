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
    

    void Update()
    {
        if(PlayerType == gameManager.ActiveTour)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            if (Input.GetMouseButtonDown(0)) // Clic gauche de la souris
            {
                positionClic = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionClic.z = 0;
                isRunning = true;

            }

            if (transform.position != positionClic && isRunning == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, positionClic, vitesseDeplacement * Time.deltaTime);

                }
            else if (isRunning == true)
                {
                    gameManager.ActiveTour += 1;
                }
        }
    }
}
