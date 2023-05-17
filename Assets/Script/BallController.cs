using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance { get; private set;}

    //topa rigidbody nin addforce �zelli�ini kullanarak hareket verice�iz

    public Rigidbody2D rigidbody2D;

    public float speed;

    private void Awake()
    {
        Instance = this; //herkeses a��k bir �ekilde ula�abilmemize yar�yacak en az�ndan public olanlara
    }

    public void OnStart()
    {
        transform.position = Vector2.zero; //top oyun ba�lad���nda her zaman 0 a 0 noktas�nda ba�l�yacak
        rigidbody2D.velocity = Vector2.zero;//sonra top�n h�z�n�da s�f�rlad�k
        rigidbody2D.AddForce(Vector2.down * speed);
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Raket"))
        {
            var racket = collision.transform.GetComponent<RacketController>(); //raketin kodlar�na eri�iyoruz
            var yonvertical = racket.isUp ? -1 : 1; //-1 ile +1 aras�nda de�erler aras�nda oyna -1 a�a�� +1 yukar� oluyor
            var yonhorizontal = (transform.position.x - racket.transform.position.x) / collision.collider.bounds.extents.x; // topun x konumu ile raketin x konumunu ��kard�k b�ylelikle de�er - ise sol + ise sa� tarafta demek oldu ama bu de�er
                                                                                        // �ok k���k geldi�i i�in bunu normalize etmemiz gerekiyordu raketin x boyutunu alarak aradaki fark� buna b�l�yoruz buda bize daha normalize bir de�er veriyor
            rigidbody2D.AddForce(new Vector2(yonhorizontal,yonvertical).normalized*speed);
        }

        if (collision.transform.CompareTag("PlayerGol"))
        {
            //playerskore
            GameManager.Instance.oyuncuskoru++;
            GameManager.Instance.OnGameover();
            rigidbody2D.velocity = Vector2.zero; //topun h�z�n� s�f�r yapt�k 
           //OnStart();
        }
        if (collision.transform.CompareTag("AIGol"))
        {
            //aiskore
            GameManager.Instance.AIScore++;
            GameManager.Instance.OnGameover();
            rigidbody2D.velocity = Vector2.zero; //topun h�z�n� s�f�r yapt�k 
        }
    }



}
