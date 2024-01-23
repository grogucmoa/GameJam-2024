using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    private Vector3 positionClic;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche de la souris
        {
            positionClic = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionClic.z = 0; // Assurez-vous que la position z est correcte pour un espace 2D
        }
    }

    void FixedUpdate()
    {
        if (transform.position != positionClic)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionClic, vitesseDeplacement * Time.deltaTime);
        }
    }
}