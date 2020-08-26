using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    protected float attack = 1.0f;
    [SerializeField]
    private ShooterTurret turretPrefab;
    private ShooterTurret turretPreview;
    private int resourceCurrency;
    public int ResourceCurrency {
        get { return resourceCurrency; }
        set { resourceCurrency = value; GameManager.instance.EventManager.ResourceCurrencyChanged.Invoke(resourceCurrency); }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ResourceCurrency = GameManager.instance.CurrentLevel.StartCurrencies;
        var currentLevel = FindObjectOfType<LevelSettings>();
        if(currentLevel != null)
        {
            ResourceCurrency = currentLevel.StartCurrencies;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (turretPreview != null)
            {
                Destroy(turretPreview.gameObject);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Ground")))
            {
                if(turretPreview == null)
                {
                    turretPreview = Instantiate(turretPrefab);
                }
                turretPreview.gameObject.transform.position = hit.point;
                if (Input.GetMouseButtonDown(0))
                {
                    if(ResourceCurrency >= turretPrefab.PlacementCost)
                    {
                        ResourceCurrency -= turretPrefab.PlacementCost;
                        turretPreview.Activate();
                        turretPreview = null;
                    }
                    else
                    {
                        GameManager.instance.EventManager.InsufficientResources.Invoke();
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    Enemy enemyHit = hit.collider.GetComponent<Enemy>();
                    if (enemyHit != null)
                    {
                        enemyHit.TakeDamage(attack);
                    }
                }
            }
        }
    }
}
