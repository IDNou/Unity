using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    public float CameraSpeed = 16.0f;
    public bool isControlStop = false;
    private bool isPlaying = true;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("NaelTower").Length <= 0 || GameObject.FindGameObjectsWithTag("UndeadTower").Length <= 0)
            isPlaying = false;

        if (isPlaying)
        { 
            if (isControlStop)
            {
                CameraMoveToPlayer();

                if (Vector3.Distance(new Vector3(45, 5, -4), this.transform.position) < 0.5f)
                {
                    this.transform.position = new Vector3(45, 5, -4);
                    isControlStop = false;
                }
            }
            else
            {
                //한계를 정해놔야함 안그러면 밑도끝도없이 이동함
                if (Input.mousePosition.x <= 0)
                {
                    if (Camera.main.transform.position.x > -8.5f)
                        Camera.main.transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime);
                }
                if (Input.mousePosition.y <= 0)
                {
                    Vector3 pos = this.transform.position;
                    if (pos.z > -8.5f)
                    {
                        pos.z += -CameraSpeed * Time.deltaTime;
                        Camera.main.transform.position = pos;
                    }
                }
                if (Input.mousePosition.x >= Screen.width)
                {
                    if (Camera.main.transform.position.x < 45.0f)
                        Camera.main.transform.Translate(Vector3.left * -CameraSpeed * Time.deltaTime);
                }
                if (Input.mousePosition.y >= Screen.height)
                {
                    Vector3 pos = this.transform.position;
                    if (pos.z < 30.0f)
                    {
                        pos.z += CameraSpeed * Time.deltaTime;
                        Camera.main.transform.position = pos;
                    }
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Vector3 pos = this.transform.position;
                    if (pos.z < 30.0f)
                    {
                        pos.z += CameraSpeed * Time.deltaTime;
                        Camera.main.transform.position = pos;
                    }
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    Vector3 pos = this.transform.position;
                    if (pos.z > -8.5f)
                    {
                        pos.z += -CameraSpeed * Time.deltaTime;
                        Camera.main.transform.position = pos;
                    }
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (Camera.main.transform.position.x > - 8.5f)
                        Camera.main.transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if(Camera.main.transform.position.x < 45.0f)
                        Camera.main.transform.Translate(Vector3.left * -CameraSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void CameraMoveToPlayer()
    {
        Vector3 CameraDir = new Vector3(45, 5, -4) - this.transform.position;
        //CameraDir.Normalize();
        this.transform.Translate(CameraDir * Time.deltaTime);
    }
}
