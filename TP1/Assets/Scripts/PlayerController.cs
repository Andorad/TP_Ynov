using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float thrustersSpeed;
    [SerializeField]
    private float timeOfThrustersUse;
    [SerializeField]
    private float reloadThrustersSpeed;
    [SerializeField]
    private GameObject visual;
    [SerializeField]
    private Image thrustersFuel;

    private float actualSpeed;
    private bool isUsingThrusters;
    private bool isReloading;
    private bool isDead;

    private void Start()
    {
        actualSpeed = speed;
        thrustersFuel.fillAmount = 100;
    }

    private void Update()
    {
        if (!isDead)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);

            transform.Translate(movement * actualSpeed * Time.deltaTime);

            if(isUsingThrusters)
            {
                thrustersFuel.fillAmount -= 1 / timeOfThrustersUse * Time.deltaTime;
            }

            if(isReloading)
            {
                thrustersFuel.fillAmount += 1 / reloadThrustersSpeed * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isUsingThrusters && !isReloading)
            {
                StartCoroutine(UseThrusters());
            }

            if(moveX != 0 && moveY == 0)
            {
                if(moveX > 0)
                {
                    visual.transform.rotation = Quaternion.Euler(0, 0, -90f);
                }
                else
                {
                    visual.transform.rotation = Quaternion.Euler(0, 0, 90f);
                }
            }
            else if(moveY != 0 && moveX == 0)
            {
                if (moveY > 0)
                {
                    visual.transform.rotation = Quaternion.Euler(0, 0, 0f);
                }
                else
                {
                    visual.transform.rotation = Quaternion.Euler(0, 0, -180f);
                }
            }
            else if(moveX > 0 && moveY > 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, -45f);
            }
            else if (moveX < 0 && moveY < 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, 135f);
            }
            else if (moveX > 0 && moveY < 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, -135f);
            }
            else if(moveX < 0 && moveY > 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, 45f);
            }
        }
    }

    IEnumerator UseThrusters()
    {
        isUsingThrusters = true;
        actualSpeed = thrustersSpeed;
        yield return new WaitForSeconds(timeOfThrustersUse);
        actualSpeed = speed;
        StartCoroutine(ReloadThrusters());
        isUsingThrusters = false;
    }

    IEnumerator ReloadThrusters()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadThrustersSpeed);
        isReloading = false;
    }
}
