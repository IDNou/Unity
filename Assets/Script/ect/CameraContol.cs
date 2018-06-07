using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    public float CameraSpeed = 16.0f;

    private void Update()
    {
        //Cursor.lockState = CursorLockMode.Confined;
       
        //if(Input.mousePosition.x <= 0)
        //{
        //    Camera.main.transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime);
        //}
        //if(Input.mousePosition.y <= 0)
        //{
        //    Vector3 pos = this.transform.position;
        //    pos.z += -CameraSpeed * Time.deltaTime;
        //    Camera.main.transform.position = pos;
        //}
        //if(Input.mousePosition.x >= Screen.width)
        //{
        //    Camera.main.transform.Translate(Vector3.left * -CameraSpeed * Time.deltaTime);
        //}
        //if(Input.mousePosition.y >= Screen.height)
        //{
        //    Vector3 pos = this.transform.position;
        //    pos.z += CameraSpeed * Time.deltaTime;
        //    Camera.main.transform.position = pos;
        //}
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 pos = this.transform.position;
            pos.z += CameraSpeed * Time.deltaTime;
            Camera.main.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 pos = this.transform.position;
            pos.z += -CameraSpeed * Time.deltaTime;
            Camera.main.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.Translate(Vector3.left * -CameraSpeed * Time.deltaTime);
        }
    }

}
