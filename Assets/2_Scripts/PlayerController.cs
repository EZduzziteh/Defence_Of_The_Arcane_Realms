namespace Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;
    public class PlayerController : MonoBehaviour
    {
        //ui eleements


        public Tower_Base currentlySelectedTowerBase;

        UI_Manager uiManager;

        GameObject cameraArm;
        private Transform camTransform;
        AudioSource aud;
        [SerializeField]
        AudioClip spendGoldSoundEffect;
        [SerializeField]
        AudioClip addGoldSoundEffect;

        [SerializeField]
        GameObject selectedTurretIndicator;
        public bool isCastingFireball = false;
        public bool isCastingWindBlast = false;
        public GameObject fireBall;

        public GameObject windBlast;
        [Header("Player Settings")]
        public int gold = 250;
        public float mana = 0;
        public float maxMana = 100;
        public float manaRegenRate = 1.0f;

        [Header("Camera Settings")]
        public float CameraSpeed = 10.0f;
        public float CameraRotationSpeed = 25.0f;

        private void Awake()
        {

            aud = GetComponent<AudioSource>();
            uiManager = FindObjectOfType<UI_Manager>();
        }
        private void Start()
        {

            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.managedSoundSources.Add(aud);
            aud.volume = audMan.soundEffectVolume;
            //#TODO subscribe to audiomanager onchanged event

            cameraArm = Camera.main.transform.parent.gameObject;
            camTransform = cameraArm.transform;
            uiManager.SetGoldTextValue(gold);

            uiManager.SetMaxMana(maxMana);
            uiManager.SetCurrentMana(mana);
        }

        // Update is called once per frame
        void Update()
        {
            if (mana < maxMana)
            {
                mana += Time.deltaTime * manaRegenRate;
                if (mana >= maxMana)
                {
                    mana = maxMana;
                }
                uiManager.SetCurrentMana(mana);
            }

            HandleCamera();

            



            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                uiManager.TogglePauseMenu();
            }


            if (Input.GetMouseButtonDown(0))
            {


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;


                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //if we clicked on a ui element, dont continue with the raytrace.
                    return;
                }

                //otherwise, raytrace
                if (Physics.Raycast(ray, out hitData, 1000.0f))
                {


                    if (isCastingFireball)
                    {

                        GameObject.Instantiate(fireBall, new Vector3(hitData.point.x, hitData.point.y + 10.0f, hitData.point.z), fireBall.transform.rotation);
                        isCastingFireball = false;
                        FindObjectOfType<CursorManager>().SetDefaultCursor();
                        return;
                    }

                    if (isCastingWindBlast)
                    {
                        GameObject.Instantiate(windBlast, new Vector3(hitData.point.x, hitData.point.y, hitData.point.z), windBlast.transform.rotation);
                        isCastingWindBlast = false;
                        FindObjectOfType<CursorManager>().SetDefaultCursor();
                        return;
                    }

                    if (hitData.collider.gameObject.GetComponent<Tower_Base>())
                    {




                        Tower_Base tower = hitData.collider.gameObject.GetComponent<Tower_Base>();



                        selectedTurretIndicator.SetActive(true);
                        selectedTurretIndicator.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y+2.8f, tower.transform.position.z);

                        if (currentlySelectedTowerBase == tower)
                        {
                            if (currentlySelectedTowerBase.turretLevel > 0)
                            {
                                currentlySelectedTowerBase.currentTurret.HideTurretRange();

                            }

                                uiManager.CloseAllMenus();

                            

                            selectedTurretIndicator.SetActive(false);
                            ClearCurrentTowerBase();

                        }
                        else
                        {
                           
                            ClearCurrentTowerBase();
                            uiManager.CloseAllMenus();
                            currentlySelectedTowerBase = tower;

                            selectedTurretIndicator.SetActive(true);
                            selectedTurretIndicator.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y + 2.8f, tower.transform.position.z);
                            //if a turret has been built
                            if (tower.turretLevel > 0)
                            {
                                uiManager.CloseAllMenus();
                                if (currentlySelectedTowerBase.currentTurret)
                                {
                                    uiManager.UpdateTurretUpgradeMenu(currentlySelectedTowerBase.currentTurret);
                                }
                                else
                                {
                                    Debug.Log("no current turret");
                                }

                                selectedTurretIndicator.SetActive(true);
                                uiManager.ShowTurretUpgradeMenu();


                                tower.currentTurret.ShowTurretRange();
                            }
                            else
                            {
                                uiManager.CloseAllMenus();

                                selectedTurretIndicator.SetActive(true);
                                uiManager.ShowBuildTurretMenu();
                            }
                        }
                        


                    }






                }


            }

            if (Input.GetMouseButtonDown(1))
            {
                if (currentlySelectedTowerBase)
                {
                    if (currentlySelectedTowerBase.currentTurret)
                    {
                        currentlySelectedTowerBase.currentTurret.HideTurretRange();
                    }
                }
                if (isCastingFireball)
                {

                    isCastingFireball = false;
                    FindObjectOfType<CursorManager>().SetDefaultCursor();
                    return;
                }
                if (isCastingWindBlast)
                {
                    isCastingWindBlast = false;
                    FindObjectOfType<CursorManager>().SetDefaultCursor();
                    return;
                }
            }
        }

        internal void TurnOffIndicator()
        {
            selectedTurretIndicator.SetActive(false);
        }

        private void HandleCamera()
        {
            //#TODO move this to a camera controller script

            if (Input.GetKey(KeyCode.W))
            {
                camTransform.localPosition += (camTransform.forward * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {

                camTransform.localPosition -= (camTransform.forward * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {

                camTransform.position -= (camTransform.right * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                camTransform.position += (camTransform.right * CameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Q))
            {

                camTransform.Rotate(camTransform.up, CameraRotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {

                camTransform.Rotate(camTransform.up, (-CameraRotationSpeed) * Time.deltaTime);
            }

            cameraArm.transform.position = camTransform.position;
            cameraArm.transform.rotation = camTransform.rotation;

            //end camera controls
        }

        public void ClearCurrentTowerBase()
        {
            if (currentlySelectedTowerBase)
            {
                currentlySelectedTowerBase = null;
                selectedTurretIndicator.SetActive(false);
            }
        }
        public void EndLevel()
        {
            int starsEarned = 0;
            Health nexus = FindObjectOfType<Nexus>().GetComponent<Health>();
            if (nexus.GetCurrentHealth() == nexus.GetMaxHealth())
            {
                starsEarned = 3;
            }
            else if (nexus.GetCurrentHealth() >= (nexus.GetMaxHealth() / 2))
            {
                starsEarned = 2;
            }
            else
            {
                starsEarned = 1;
            }

            String currentLevelString = SceneManager.GetActiveScene().name;

            String saveString = "Stars_Earned_" + currentLevelString;

            PlayerPrefs.SetInt(saveString, starsEarned);


            SceneManager.LoadScene("Menu_Select");
        }

        public bool TrySpendMana(float amount)
        {
            if (amount <= mana)
            {

                mana -= amount;
                uiManager.SetCurrentMana(mana);
                return true;
            }
            else
            {

                Debug.Log("not enough mana");
                return false;
            }
        }
        public bool TrySpendGold(int amount)
        {
            if (amount <= gold)
            {
                gold -= amount;
                uiManager.SetGoldTextValue(gold);
                aud.clip = spendGoldSoundEffect;
                aud.Play();
                return true;
            }
            else
            {
                Debug.Log("not enough gold");
                return false;
            }
        }

        public void AddGold(int amount)
        {
            gold += amount;
            uiManager.SetGoldTextValue(gold);
            aud.clip = addGoldSoundEffect;
            aud.Play();
        }
    }
}