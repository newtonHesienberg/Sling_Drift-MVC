using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarView : MonoBehaviour
{
    [Header("Graphics & UI")]

    public AudioClip blastSFX;
    public GameObject blastVFX, blastVFX2;
    float SFXSoundVolume = 0.7f;
    public GameObject deathUI;

    public AudioSource engineSFX;

    public Text scoreText;

    public LineRenderer lineRenderer;

    public GameObject[] nodes;
    public int index = -1;
    public float zAisValue;
    public float angular_velocity;
    public float drivingAngle;
    public float drivingSpeed;
    public float rope_radius;

    [Header("Car Effect")]

    public TrailRenderer[] trailRenderers;

    public AudioSource driftSound;

    [Header("Refrences")]

    public CarController carController;


    // Start is called before the first frame update
    void Start()
    {
        engineSFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DriftState()
    {
        lineRenderer.enabled = true;

        transform.RotateAround(nodes[index].transform.position, Vector3.forward, angular_velocity * Time.deltaTime);
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineRenderer.SetPosition(1, new Vector3(nodes[index].transform.position.x, nodes[index].transform.position.y, 0f));


        if (transform.rotation.eulerAngles.z >= zAisValue - 7)
        {
            if (zAisValue == 90)
                drivingAngle = zAisValue + 90;
            else if (zAisValue == -90)
                drivingAngle = -zAisValue;
            else if (zAisValue == -180)
                drivingAngle = -zAisValue;
            else if (zAisValue == 0)
                drivingAngle = -zAisValue;
            else if (zAisValue == 180)
                drivingAngle = zAisValue + 90;

            StartCoroutine(FixOreintation());
        }


    }

    IEnumerator FixOreintation()
    {
        yield return new WaitForSeconds(1);
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.x, -90);
    }

    public void NoDriftState()
    {
        lineRenderer.enabled = false;
        transform.position += new Vector3(Mathf.Sin(drivingAngle * (Mathf.PI / 180)) * drivingSpeed * Time.deltaTime, Mathf.Cos(drivingAngle * (Mathf.PI / 180)) * drivingSpeed * Time.deltaTime, 0);

        rope_radius = Vector3.Distance(nodes[index].transform.position, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("T1"))
        {
            angular_velocity = 140;
            zAisValue = 90;
            index++;
            carController.score++;
        }
        if (collision.CompareTag("T2"))
        {
            zAisValue = -90;
            index++;
            carController.score++;
        }
        if (collision.CompareTag("T3"))
        {
            carController.removableBridge.SetActive(true);
            angular_velocity = -140;
            zAisValue -= 90;
            index++;
            carController.score++;
        }
        if (collision.CompareTag("T4"))
        {
            angular_velocity = 140;
            zAisValue = 0;
            index++;
            carController.score++;
        }
        if (collision.CompareTag("T5"))
        {
            angular_velocity = -140;
            zAisValue -= 90;
            index++;
            carController.score++; ;
        }
        if (collision.CompareTag("T6"))
        {
            angular_velocity = 140;
            zAisValue = 0;
            index++;
            carController.score++;
        }
        if (collision.CompareTag("T7"))
        {
            angular_velocity = -140;
            zAisValue = 180;
            carController.minDistance += .5f;
            index++;
            carController.score++;
        }
        if (collision.CompareTag("T8"))
        {
            angular_velocity = -140;
            carController.minDistance -= .5f;
            index = -1;
        }
        if (collision.CompareTag("RemoveBridge"))
        {
            carController.removableBridge.SetActive(false);
        }

        if (collision.CompareTag("Track"))
        {
            Blast();
        }

    }


    public void Blast()
    {
        Instantiate(blastVFX, transform.position, Quaternion.identity);
        Instantiate(blastVFX2, transform.position, Quaternion.identity);
        deathUI.SetActive(true);
        AudioSource.PlayClipAtPoint(blastSFX, Camera.main.transform.position, SFXSoundVolume);
        Destroy(gameObject);
        Time.timeScale = 0f;
    }
    public void startEmiiter()
    {
        if (carController.driftFlag) return;
        foreach (TrailRenderer t in trailRenderers)
        {
            t.emitting = true;
        }

        driftSound.Play();
        carController.driftFlag = true;
    }

    public void stopEmitter()
    {
        if (!carController.driftFlag) return;
        foreach (TrailRenderer t in trailRenderers)
        {
            t.emitting = false;
        }

        driftSound.Stop();
        carController.driftFlag = false;
    }
}
