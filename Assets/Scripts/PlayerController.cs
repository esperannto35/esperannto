using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody rigi;
    public GameObject GreenEnemy, Tutorial;
    public bool startBool;   
    public float Speed;
    bool cameraRotatebool;

    float xpositionfloat,yrotatefloat;

    int LevelNumber, MainLevelNumber;
    public GameObject[] Levels;
    public GameObject FailScreen, SuccessScreen;
    public Text LevelText;

    private void Start()
    {
        LevelNumber = PlayerPrefs.GetInt("LevelNumber");
        MainLevelNumber = PlayerPrefs.GetInt("MainLevelNumber");
        Levels[LevelNumber].SetActive(true);
        LevelText.text = "LEVEL " + (MainLevelNumber+1).ToString();
        rigi = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (startBool == false)
            {
                startBool = true;
                animator.SetInteger("Player", 5); //Running Animation
                Invoke("SpawnActivate", 5f);
            }
            Tutorial.gameObject.SetActive(false);
        }
        if (startBool)
        {
            rigi.velocity = new Vector3(0, rigi.velocity.y, Speed);
            

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    xpositionfloat += Input.GetTouch(0).deltaPosition.x / 20f;
                    yrotatefloat += Input.GetTouch(0).deltaPosition.x * 1f;
                }

                transform.rotation = Quaternion.Euler(0, yrotatefloat, 0);
                transform.position = Vector3.Lerp(transform.position, new Vector3(xpositionfloat, transform.position.y, transform.position.z), Time.deltaTime * 6.5f);
            }

            yrotatefloat=Mathf.Lerp(yrotatefloat, 0, Time.deltaTime * 5f);

            //--------------FALLING ANIMATION----------------//

            if (transform.position.y < -4.5f)
            {
                animator.SetInteger("Player", 1);
                Invoke("FShow", 2f);
                Speed = 0;
            }
        }
        if (cameraRotatebool == true)
        {
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.Euler(12, 0, 0), Time.deltaTime *15f);     
        }
    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            rigi.velocity = new Vector3(0, 0,0);
            animator.SetInteger("Player", 3);
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Invoke("FShow", 2f);
        }
        if (target.gameObject.tag == "Heli")
        {
            animator.SetInteger("Player", 4);          
        }
    }
    //--------------JUMPING----------------//
    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "FinishLine")
        {
            animator.SetInteger("Player", 2);
            rigi.AddForce(Vector3.up * 450);

            Camera.main.GetComponent<CamFollow>().enabled = false;
            cameraRotatebool = true;
            CancelInvoke("SpawnActivate");
            Invoke("SShow", 5f);
        }
    }

    void SpawnActivate()
    {
        Instantiate(GreenEnemy, new Vector3(Random.Range(-4.5f, 4f), transform.position.y, transform.position.z - 10), this.transform.rotation);
        Invoke("SpawnActivate", 0.4f);
    }

    public void FShow()
    {
        FailScreen.SetActive(true);
    }
    public void SShow()
    {
        SuccessScreen.SetActive(true);
    }
    public void NextButton()
    {
        if (MainLevelNumber < 2)
        {
            PlayerPrefs.SetInt("LevelNumber", LevelNumber + 1);
            PlayerPrefs.SetInt("MainLevelNumber", MainLevelNumber + 1);
        }
        else
        {
            PlayerPrefs.SetInt("LevelNumber", Random.Range(0, 3));
            PlayerPrefs.SetInt("MainLevelNumber", MainLevelNumber + 1);
        }
        Application.LoadLevel(0);
    }
    public void RestartButton()
    {
        Application.LoadLevel(0);
    }
}
