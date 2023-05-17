using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float movementSpeed = 5f; // Hareket hýzý
    private bool moveRight; // Saða mý hareket ediyoruz?
    private bool moveLeft; // Sola mý hareket ediyoruz?
    public float limitPositionX = 1.8f;
    public bool isUp;

    //yapay zeka yapýmý
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
                Touch touch = Input.GetTouch(0); // Ýlk dokunmayý al

                // Ekrana dokunma olaylarýný kontrol et
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = touch.position;
                    // Ekranýn sað yarýsýna dokunulduysa saða hareket et
                    if (touchPosition.x > Screen.width / 2)
                    {
                        moveRight = true;
                        moveLeft = false;
                    }
                    // Ekranýn sol yarýsýna dokunulduysa sola hareket et
                    else if (touchPosition.x < Screen.width / 2)
                    {
                        moveRight = false;
                        moveLeft = true;
                    }
                }

                // Dokunma sonlandý
                if (touch.phase == TouchPhase.Ended)
                {
                    moveRight = false;
                    moveLeft = false;
                }
            }

        }
        else
        {                                                                                       
            newPosition.x = Mathf.Lerp(newPosition.x, BallController.Instance.transform.position.x, aiSpeed * Time.deltaTime); //lerp yavaþça hareket eder a noktasýndan b noktasýna verilen hýzda eþitlenerek gider
        }
        
        

        newPosition.x = Mathf.Clamp(newPosition.x, -limitPositionX, limitPositionX);//clamp iki deðer arasýnda kalmasýný saðlar minimum ve maksimum deðerler arasýnda kalýr. objeyi o aralýkta tutar

        transform.position = newPosition;
    }

 
    private void FixedUpdate()
    {
        // Hareket etme iþlemini gerçekleþtir

        // Saða hareket
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


