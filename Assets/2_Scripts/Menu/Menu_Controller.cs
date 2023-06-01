namespace Menu
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MenuController : MonoBehaviour
    {

        Camera mainCamera;
        private Transform camTransform;
        AudioSource aud;

        public GameObject locationMarker;

        LevelButton currentlySelectedLevel;


        public List<LevelButton> buttons = new List<LevelButton>();

        [Header("Camera Settings")]
        public float CameraSpeed;

        private void Start()
        {
            Time.timeScale = 1;

          
            currentlySelectedLevel = buttons[PlayerPrefs.GetInt("HighestCompletedLevel")];

            locationMarker.transform.position = currentlySelectedLevel.markerLocation.transform.position;
            locationMarker.transform.rotation = currentlySelectedLevel.markerLocation.transform.rotation;

            mainCamera = Camera.main;
            camTransform = mainCamera.transform;
            aud = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 tempForward = new Vector3(camTransform.forward.x, camTransform.forward.y, camTransform.forward.z);
                camTransform.localPosition += (tempForward * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {


                Vector3 tempForward = new Vector3(camTransform.forward.x, camTransform.forward.y, camTransform.forward.z);
                camTransform.localPosition -= (tempForward * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {

                camTransform.position -= (camTransform.right * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                camTransform.position += (camTransform.right * CameraSpeed * Time.deltaTime);
            }

            mainCamera.transform.position = camTransform.position;
            mainCamera.transform.rotation = camTransform.rotation;


            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;

                if (Physics.Raycast(ray, out hitData, 1000.0f))
                {

                    if (hitData.collider.GetComponent<LevelButton>())
                    {

                        currentlySelectedLevel = hitData.collider.GetComponent<LevelButton>();
                        Debug.Log("selected level" + currentlySelectedLevel.levelToLoad);
                        locationMarker.transform.position = currentlySelectedLevel.markerLocation.transform.position;
                        locationMarker.transform.rotation = currentlySelectedLevel.markerLocation.transform.rotation;

                    }




                }


            }
        }

        public void LoadCurrentlySelectedLevel()
        {
            SceneManager.LoadScene(currentlySelectedLevel.levelToLoad);
            ;
        }


    }
}