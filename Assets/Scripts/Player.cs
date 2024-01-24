//essai
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

	//variables UI
	[SerializeField] private GameObject _actionMenu;
    public bool Deplace = false;
		
    //variable range
    public float rayonDeplacement = 5f;
    [SerializeField] private GameObject _range;
    

    void Update()
    {
        if(PlayerType == gameManager.ActiveTour)
        {
            _actionMenu.SetActive(true);
            if(Deplace == true){
                _range.SetActive(true);
                _actionMenu.SetActive(false);

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
                    _range.SetActive(false);

                }
                else if (isRunning == true)
                {
                    gameManager.ActiveTour += 1;
                    isRunning = false;
                }
            }
        }
    }

    public void Deplacement(){
        Deplace = true;
    }

}
