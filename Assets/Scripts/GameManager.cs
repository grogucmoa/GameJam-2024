using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public string joueur = "";
    public int ActiveTour = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ActiveTour = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(ActiveTour == 17)
        {
            ActiveTour = 1;
        }

        if (ActiveTour >= 1 && ActiveTour <= 4) 
        {
            joueur = "J1";
        }

        if (ActiveTour >= 5 && ActiveTour <= 8) 
        {
            joueur = "J2";
        }

        if (ActiveTour >= 9 && ActiveTour <= 12) 
        {
            joueur = "J3";
        }

        if (ActiveTour >= 13 && ActiveTour <= 16) 
        {
            joueur = "J4";
        }
    }
}
