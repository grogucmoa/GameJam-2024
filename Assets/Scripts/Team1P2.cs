using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team1P2 : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    private Vector3 positionClic;
    public GameManager gameManager;
    public bool isRunning = false;
    
    void Update()
    {
        if (gameManager.joueur=="joueur 1.2") {
            GetComponent<SpriteRenderer>().color = Color.green;
            if (Input.GetMouseButtonDown(0)) // Clic gauche de la souris
            {
                positionClic = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionClic.z = 0; // Assurez-vous que la position z est correcte pour un espace 2D
                isRunning = true;

            }

            if (transform.position != positionClic && isRunning == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, positionClic, vitesseDeplacement * Time.deltaTime);

                }
            else if (isRunning == true)
                {
                    gameManager.joueur = "joueur 1.3";
                }
        }
    }
}