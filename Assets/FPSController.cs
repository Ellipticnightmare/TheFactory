using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FPSController : MonoBehaviour
{
    public List<string> inventory = new List<string>();
    public Camera camMain;
    public GameObject camObj, lighter, lighterFlame, flashLight, flashLightLight, Inventory;
    public bool light, fLight, inv;
    public string selectedItem;
    public int keyCount, batteryCount;
    public float flashLightPower;
    public Text batText, keyText;
    // Start is called before the first frame update
    void Start()
    {
        camObj = GameObject.FindGameObjectWithTag("MainCamera");
        camMain = camObj.GetComponent<Camera>();
        flashLight.SetActive(false);
        lighter.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        flashLightPower = 60;
    }

    // Update is called once per frame
    void Update()
    {
        #region lightStuff
        if (light)
        {
            flashLightLight.SetActive(true);
            lighterFlame.SetActive(true);
        }
        else
        {
            flashLightLight.SetActive(false);
            lighterFlame.SetActive(false);
        }

        if (flashLightPower > 0)
        {
            if (flashLightLight.activeInHierarchy)
            {
                flashLightPower -= Time.deltaTime;
            }
        }
        if (flashLightPower > 60)
        {
            flashLightPower = 60;
        }
        if (flashLightPower <= 0)
        {
            flashLightLight.SetActive(false);
        }

        if (!inv)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                light = !light;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                light = false;
                if (fLight)
                {
                    fLight = false;
                    flashLight.SetActive(false);
                    lighter.SetActive(true);
                }
                else if (!fLight)
                {
                    fLight = true;
                    lighter.SetActive(false);
                    flashLight.SetActive(true);
                }
            }
        }
        #endregion
        #region movement
        float rV = Input.GetAxisRaw("Mouse Y");
        float rH = Input.GetAxisRaw("Mouse X");

        float mH = Input.GetAxisRaw("Horizontal");
        float mV = Input.GetAxisRaw("Vertical");

        transform.Translate(mH * 5 * Time.deltaTime, 0, mV * 5 * Time.deltaTime);
        transform.Rotate(0, rH * 60 * Time.deltaTime, 0);
        camMain.transform.Rotate(-rV * 60 * Time.deltaTime, 0, 0);
        #endregion
        #region Pickup & Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Casting Ray");
            Ray pickupRay = camMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit pUHit;
            if (Physics.Raycast(pickupRay, out pUHit))
            {
                Debug.Log("Ray hit something: " + pUHit.collider.name);
                if (pUHit.collider.gameObject.CompareTag("Item"))
                {
                    if (pUHit.collider.gameObject.name == "Battery")
                    {
                        batteryCount++;
                    }
                    else if (pUHit.collider.gameObject.name == "Key")
                    {
                        keyCount++;
                    }
                    inventory.Add(pUHit.collider.gameObject.name);
                    Destroy(pUHit.collider.gameObject);
                }
                else
                {
                    if (selectedItem != null && selectedItem != "Battery")
                    {
                        if (pUHit.collider.gameObject.CompareTag("WorldUse"))
                        {
                            if (selectedItem == pUHit.collider.gameObject.GetComponent<WorldInput>().useString)
                            {
                                pUHit.collider.gameObject.GetComponent<WorldInput>().Activate();
                                if (selectedItem == "Key")
                                {
                                    keyCount -= 1;
                                    selectedItem = "Hand";
                                }
                            }
                        }
                    }
                    else if (selectedItem == "Battery")
                    {
                        batteryCount -= 1;
                        flashLightPower += 30;
                        selectedItem = "Hand";
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inv = !inv;
        }
        batText.text = batteryCount.ToString();
        keyText.text = keyCount.ToString();
        Inventory.SetActive(inv);
        Cursor.visible = inv;

        if (inv)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
        #endregion
    }

    public void SelectBattery()
    {
        inv = false;
        selectedItem = "Battery";
    }
    public void SelectKey()
    {
        inv = false;
        selectedItem = "Key";
    }
}