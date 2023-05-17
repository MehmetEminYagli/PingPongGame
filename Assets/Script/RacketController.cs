using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float movementSpeed = 5f; // Hareket h�z�
    private bool moveRight; // Sa�a m� hareket ediyoruz?
    private bool moveLeft; // Sola m� hareket ediyoruz?
    public float limitPositionX = 1.8f;
    public bool isUp;

    //yapay zeka yap�m�
    public bool isPlayer;
    public float aiSpeed;

    void Update()
    {
        Vector3 newPosition = transform.position;
        if (isPlayer)
        {
            /*  var horizontal = Input.GetAxis("Horizontal");
              newPosition = transform.position + horizontal * speed * Time.deltaTime * Vector3.right;*/
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // �lk dokunmay� al

                // Ekrana dokunma olaylar�n� kontrol et
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = touch.position;
                    // Ekran�n sa� yar�s�na dokunulduysa sa�a hareket et
                    if (touchPosition.x > Screen.width / 2)
                    {
                        moveRight = true;
                        moveLeft = false;
                    }
                    // Ekran�n sol yar�s�na dokunulduysa sola hareket et
                    else if (touchPosition.x < Screen.width / 2)
                    {
                        moveRight = false;
                        moveLeft = true;
                    }
                }

                // Dokunma sonland�
                if (touch.phase == TouchPhase.Ended)
                {
                    moveRight = false;
                    moveLeft = false;
                }
            }

        }
        else
        {                                                                                       
            newPosition.x = Mathf.Lerp(newPosition.x, BallController.Instance.transform.position.x, aiSpeed * Time.deltaTime); //lerp yava��a hareket eder a noktas�ndan b noktas�na verilen h�zda e�itlenerek gider
        }
        
        

        newPosition.x = Mathf.Clamp(newPosition.x, -limitPositionX, limitPositionX);//clamp iki de�er aras�nda kalmas�n� sa�lar minimum ve maksimum de�erler aras�nda kal�r. objeyi o aral�kta tutar

        transform.position = newPosition;
    }

 
    private void FixedUpdate()
    {
        // Hareket etme i�lemini ger�ekle�tir

        // Sa�a hareket
        if (moveRight)
        {
            transform.Translate(Vector3.right * movementSpeed * Time.fixedDeltaTime);
        }
        // Sola hareket
        else if (moveLeft)
        {
            transform.Translate(Vector3.left * movementSpeed * Time.fixedDeltaTime);
        }
    }
}


