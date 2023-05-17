using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance { get; private set;}

    //topa rigidbody nin addforce özelliðini kullanarak hareket vericeðiz

    public Rigidbody2D rigidbody2D;

    public float speed;

    private void Awake()
    {
        Instance = this; //herkeses açýk bir þekilde ulaþabilmemize yarýyacak en azýndan public olanlara
    }

    public void OnStart()
    {
        transform.position = Vector2.zero; //top oyun baþladýðýnda her zaman 0 a 0 noktasýnda baþlýyacak
        rigidbody2D.velocity = Vector2.zero;//sonra topýn hýzýnýda sýfýrladýk
        rigidbody2D.AddForce(Vector2.down * speed);
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Raket"))
        {
            var racket = collision.transform.GetComponent<RacketController>(); //raketin kodlarýna eriþiyoruz
            var yonvertical = racket.isUp ? -1 : 1; //-1 ile +1 arasýnda deðerler arasýnda oyna -1 aþaðý +1 yukarý oluyor
            var yonhorizontal = (transform.position.x - racket.transform.position.x) / collision.collider.bounds.extents.x; // topun x konumu ile raketin x konumunu çýkardýk böylelikle deðer - ise sol + ise sað tarafta demek oldu ama bu deðer
                                                                                        // çok küçük geldiði için bunu normalize etmemiz gerekiyordu raketin x boyutunu alarak aradaki farký buna bölüyoruz buda bize daha normalize bir deðer veriyor
            rigidbody2D.AddForce(new Vector2(yonhorizontal,yonvertical).normalized*speed);
        }

        if (collision.transform.CompareTag("PlayerGol"))
        {
            //playerskore
            GameManager.Instance.oyuncuskoru++;
            GameManager.Instance.OnGameover();
            rigidbody2D.velocity = Vector2.zero; //topun hýzýný sýfýr yaptýk 
           //OnStart();
        }
        if (collision.transform.CompareTag("AIGol"))
        {
            //aiskore
            GameManager.Instance.AIScore++;
            GameManager.Instance.OnGameover();
            rigidbody2D.velocity = Vector2.zero; //topun hýzýný sýfýr yaptýk 
        }
    }



}
