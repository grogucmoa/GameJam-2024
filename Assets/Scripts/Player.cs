using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerType = 1;
    public GameManager gameManager;
    private SpriteRenderer spriteRenderer;

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
    public bool isInRange = false;
    public GameObject targetObject;

    //variables du chateau
    public int Team = 0;
    
    private Collider2D target = null;

    void Update()
    {
        if(PlayerType == gameManager.ActiveTour)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer.enabled == false){
                gameManager.ActiveTour += 1;
            }
            else{
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
    }

    public void Deplacement(){
        Deplace = true;
    }

    public void Skip(){
        _actionMenu.SetActive(false);
        gameManager.ActiveTour += 1;
    }

    public void Attack(){
        if(isInRange == true && target != null){
            kill(target.gameObject);
            _actionMenu.SetActive(false);
            gameManager.ActiveTour += 1;
        }
        print("is in range : " + isInRange);
    }

    //verifie si il y a un joueur
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>().PlayerType != gameManager.ActiveTour)
        {
            
            isInRange = true;
            target = other;
        }
        else{
            isInRange = false;
            target = null;
        }

    }

    public void kill(GameObject target){
        
        if (target.GetComponent<Player>().Team == 1){
            gameManager.Game -= 1;
        }
        target.GetComponent<SpriteRenderer>().enabled = false;
        target = null;
        
    }

}