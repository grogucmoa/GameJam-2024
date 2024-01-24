using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerType = 1;
    public GameManager gameManager;
    public int Team = 1;

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

    //variables d'attaques
    public bool isAttack = false;
    public float AttackRange = 5f;
    public GameObject targetObject;
    

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

                }
                else if (isRunning == true)
                {
                    gameManager.ActiveTour += 1;
                    isRunning = false;
                    _range.SetActive(false);
                    Deplace = false;
                }
            }
        }
    }

    public void Deplacement(){
        Deplace = true;
    }

    public void Skip(){
        _actionMenu.SetActive(false);
        gameManager.ActiveTour += 1;
    }

    public void Attack(){
        _actionMenu.SetActive(false);
        if(isAttack == true){
            targetObject.SetActive(false);   
        }
    }

    //verifie si il y a un joueur
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Objet detecté");
        // Vérifie si le collider qui entre a un composant SpriteRenderer
        SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null && PlayerType != gameManager.ActiveTour )
        {
            isAttack = true;
        }
    }

}