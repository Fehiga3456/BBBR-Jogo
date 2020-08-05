using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
   
    Camera cam;//ref da para pegar a ref do mouse
    GameObject currentBallPrefab;
    [Header("Settings")]
    public float pushForce = 1;// forca 
    public float distanciaMax;//distancia lIMIT PARA PARAR
    public int currentErrosInt;
   
    int maxErrosInt = 3;
    GameObject Pos; //POS DO OBJ INSTACIADO
    bool isDragging = false;//verticar ser está puxando

    Vector3 startPoint;//primiero ponto de puxe
    Vector3 endPoint;//ultimo ponto de puxe
    Vector2 direction;//direcao da ball
    Vector2 force;//forca que vai sair
    float distance;//distacia

    GameObject currentInstance; //ISSO É O RESULTADO DA INSTANCIA (IMPORTANTE!)   

    AudioSource audioSource;

    public int numeroDestruidosAlvos;

    public bool triggou;
    bool MenuOpen;
    bool Dragged =  true;
    public  bool instaciou;
    bool podesoltar;
    public bool foiDestuido;
    float distaciaMaxSplide = 12;
    int maxAmmoAzul = 7;
    int currentAmmoAzul;
    int maxAmmoLaranja = 7;
    int currentAmmoLaranja;

    [Header("Referencias")]
    public Text currentAmmoUiAzul;
    public Text currentAmmoUiLaranja;
    public Text currentErros;
    public Text MaxErros;
    public Slider slider;
    public Image barImage;
    public Tragetoria tragetoria;//ref a Tragetoria
    public GameObject BolaAzul;
    public GameObject BolaLaranja;
    public GameObject MenuSom;
    public Slider SliderSom;
    public GameObject recarregar;



    private void Start()
    {
        cam = Camera.main;
        Pos = GameObject.Find("posBola");
        audioSource = GetComponent<AudioSource>();

        currentAmmoAzul = maxAmmoAzul;
        currentAmmoLaranja = maxAmmoLaranja;
   

    }
    private void Update()
    {

        UiUpdate();

        if (Input.GetMouseButtonDown(0) && instaciou == true && !IsMouseOverUIWithIgnores())//Quando o jogador clicar
        {
            
            DesaticvateCurrentRb();
            isDragging = true;
            Dragged = false;
            podesoltar = true;
            OnDragStart();
            triggou = false;
            

        }
       
        if (Input.GetMouseButtonUp(0) && podesoltar == true && instaciou == true)//Quando o jogador soltar o botao
        {
            isDragging = false;
            OnDragEnd();
            
        }
        if (isDragging)
        {
            OnDrag();         
        }    
    }

    void OnDragStart()//pega a posicao inicial do mouse
    {
        DesaticvateCurrentRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        tragetoria.Show();
    }
    void OnDrag()//pega a posicao atual do mouse quando estiver precionado e faz as contas para adicionar forca para a bolinha
    {
        slider.value = distance / distaciaMaxSplide ;
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector3.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;
        Debug.DrawLine(startPoint, endPoint);

        tragetoria.UpdateDots(currentInstance.GetComponent<Ball>().pos, force);
        
        if (Vector3.Distance(Pos.transform.position,endPoint) > distanciaMax)
        {
            currentInstance.transform.position = Pos.transform.position + (endPoint - Pos.transform.position).normalized * distanciaMax;
            
        }
        else
        {
            currentInstance.GetComponent<Rigidbody2D>().position = endPoint;
           
        }
        
    }
    void OnDragEnd()//verifica qual bolinha é e adiciona a forca - quando soltar isso revolver - subtrai da municao;
    {
        if (currentBallPrefab == BolaAzul)
        {
            currentInstance.tag = "BallSaiuAzul";
        }
        if (currentBallPrefab == BolaLaranja)
        {
            currentInstance.tag = "BallSaiuLaranja";
        }
       
        currentInstance.GetComponent<Ball>().ActivateRb();
        currentInstance.GetComponent<Ball>().Push(force);
        tragetoria.Hide();
        Dragged = true;
        instaciou = false;
        slider.value = 0;
 

        if (currentBallPrefab == BolaAzul)
        {
            currentAmmoAzul--;
        }
        if (currentBallPrefab == BolaLaranja)
        {
            currentAmmoLaranja--;
        }
      
     
    }

    void ActivateCurrentRb()//RB ATIVADO
    {
        currentInstance.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    void DesaticvateCurrentRb()//RB DESATIVADO
    {
        currentInstance.GetComponent<Rigidbody2D>().isKinematic = true;
        currentInstance.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        currentInstance.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void ChangeTheBolinhaAzul()//MUDA A BOLINHA PARA A AZUL E INSTACIA ELA 
    {
        if (currentBallPrefab == BolaLaranja || currentBallPrefab == BolaAzul && instaciou == true)
        {
            DestroirBallAntes();
        }
        if (Dragged == true)
        {
            currentBallPrefab = BolaAzul;
            currentInstance = Instantiate(currentBallPrefab, Pos.transform);
            instaciou = true;
            podesoltar = false;
            foiDestuido = false;
            PerdeuJogo();

        }        
    }
    public void ChangeTheBolinhaLaranja()//MUDA A BOLINHA PARA A ROXA(ANTIGA LARANJA) E INSTACIA ELA 
    {
        if (currentBallPrefab == BolaAzul || currentBallPrefab == BolaLaranja && instaciou == true)
        {
            DestroirBallAntes();
            
        }
        if (Dragged == true)
        { 
            currentBallPrefab = BolaLaranja;
            currentInstance = Instantiate(currentBallPrefab, Pos.transform);
            instaciou = true;
            podesoltar = false;
            foiDestuido = false;
            PerdeuJogo();

        }
    }


    bool isMouseOverUI()//VERIFICA SE O MOUSE ESTA SOBRE UMA UI
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    bool IsMouseOverUIWithIgnores()//VERIFICA EM QUE OBJETO O MOUSE ESTA E DEIXA PASSAR OBJETOS DA UI COM O SCRIPT <PAINEL> DELES
    {
        PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
        pointerEvent.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEvent, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.GetComponent<Painel>() != null)
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultsList.Count > 0;
    }

   
    public void HabilitaMenu()//Botao que habilita menu de config
    {
        MenuSom.SetActive(true);
        Time.timeScale = 0f;
        MenuOpen = true;

    }
    public void DesabilitaMenu()//Botao que desabilita menu de config
    {
        MenuSom.SetActive(false);
        Time.timeScale = 1f;
    }


    void UiUpdate()//FAZ TODOS AS COISAS DA UI (QUASE TODAS) / VERIFICA SE O JOGADOR PERDEU OU GANHOU
    {
        currentAmmoUiAzul.text = currentAmmoAzul.ToString();
        currentAmmoUiLaranja.text = currentAmmoLaranja.ToString();
        currentErros.text = currentErrosInt.ToString();
        MaxErros.text = maxErrosInt.ToString();

        if (MenuOpen == true)
        {
            audioSource.volume = SliderSom.value;
        }

        if (instaciou == false)
        {
            recarregar.SetActive(true);
           
        }
        else
        {
            recarregar.SetActive(false);
        }
        if (currentErrosInt == maxErrosInt)
        {
            SceneManager.LoadScene("Menu 1");
        }

        if (numeroDestruidosAlvos == 5)
        {
            SceneManager.LoadScene("Menu 2");
            Debug.Log("Voce Ganhou");
        }


    }

    void PerdeuJogo()
    {
        if (currentAmmoAzul == 0 || currentAmmoLaranja == 0)
        {
            SceneManager.LoadScene("Menu 1");
            Debug.Log("vocePerdeu");
        }
    
    }


    public void DestroirBallAntes()//DESTROI OBJ COM A TAG BALL
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject obj in allObjects)
        {

            Destroy(obj);
            Debug.Log("DESTURUI - Ball");
        }
    }

    public void DestroirBallDepois()//MESMA COISA/ NAO ESTA SENDO USADO!
    {

        GameObject[] allObjectsAZUL = GameObject.FindGameObjectsWithTag("BallSaiuAzul");
        foreach (GameObject obj in allObjectsAZUL)
        {

            Destroy(obj);
            Debug.Log("DESTURUI - BallSaiuAzul");
        }
        GameObject[] allObjectsLARANJA = GameObject.FindGameObjectsWithTag("BallSaiuAzul");
        foreach (GameObject obj in allObjectsLARANJA)
        {

            Destroy(obj);
            Debug.Log("DESTURUI - BallSaiu");
        }
    }
}
