using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float fireRate;
    float temp_currentFireRate;

    bool temp_isShooted;

    public GameObject viewFinder;
    GameObject temp_viewFinder;

    private void Awake()
    {
        temp_currentFireRate = fireRate;
    }

    private void Start()
    {
        if (viewFinder)
        {
            temp_viewFinder = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !temp_isShooted)
        {
            Shot(mousePos);
        }

        if(temp_isShooted)
        {
            temp_currentFireRate -= Time.deltaTime;

            if(temp_currentFireRate <= 0)
            {
                temp_isShooted = false;
                temp_currentFireRate = fireRate;
            }

            GameGUIController.Ins.UpdateFireRateFilled(temp_currentFireRate / fireRate);
        }

        if (temp_viewFinder)
        {
            temp_viewFinder.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
    }

    void Shot(Vector3 mousePos)
    {
        temp_isShooted = true;

        Vector3 shotDirection = Camera.main.transform.position - mousePos;

        shotDirection.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shotDirection);

        if(hits != null &&  hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if(hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();

                    if (bird)
                    {
                        bird.BirdDie();
                    }
                }
            }
        }

        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }
}
