using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerType = 1;
    public GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    public Transform playerTransform;

    //Variables Vitesses
    public float vitesseDeplacement = 5f;
    private Vector3 positionClic;
    public bool isRunning = false;

	//variables UI
	[SerializeField] private GameObject _actionMenu;
    [SerializeField] private GameObject _MoveButton;
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
    public bool isCastle = false;
    public bool Bomb = false;
    
    private Collider2D target = null;

    void Update()
    {
        if(PlayerType == gameManager.ActiveTour)
        {
            // Passer le tour si un joueur est desactivé/dead
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer.enabled == false){
                gameManager.ActiveTour += 1;
            }
            else{
                _actionMenu.SetActive(true);
            
            // Deplacer le joueur
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
                    _MoveButton.SetActive(false);
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

    public void Spell(){
        if (isInRange == true && target != null && Bomb == true){
            Destroy(target.gameObject);
            _actionMenu.SetActive(false);
            gameManager.ActiveTour += 1;
            gameManager.Game -= 1;
        }
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

        if (other.GetComponent<Player>().PlayerType != gameManager.ActiveTour && other.GetComponent<Player>().isCastle == true)
        {
            Bomb = true;
            isInRange = true;
            target = other;
        }

    }

    public void kill(GameObject target){
        
        if (target.GetComponent<Player>().isCastle != true)
        {
            target.GetComponent<SpriteRenderer>().enabled = false;

            if(target.GetComponent<Player>().PlayerType <=4){
                //tp to spawn Bleu
                TeleporterJoueur(4.5f, 4.43f);
                target.GetComponent<SpriteRenderer>().enabled = true;

            }

            if(target.GetComponent<Player>().PlayerType <=8){
                //tp to spawn Rouge
            }

            if(target.GetComponent<Player>().PlayerType <=12){
                //tp to spawn Jaune
            }

            if(target.GetComponent<Player>().PlayerType <=16){
                //tp to spawn Vert
            }
            target = null;
        }
        
    }

    public void Destroy(GameObject target){
        
        if (target.GetComponent<Player>().isCastle == true)
        {
            target.GetComponent<SpriteRenderer>().enabled = false;
            target = null;
        }
        
    }

    void TeleporterJoueur(float x, float y)
    {
        // Créer un nouveau vecteur avec les coordonnées spécifiées
        Vector3 nouvellePosition = new Vector3(x, y, 0f);

        // Appliquer la nouvelle position au transform du joueur
        target.GetComponent<Transform>().position = nouvellePosition;
        print("sa marche");
    }

}