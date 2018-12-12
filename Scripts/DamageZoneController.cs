using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DamageZoneController : MonoBehaviour, IPunObservable
{
    // 需要同步，当房主掉线时，需要将damagezone的权限交由下一个房主
    [Header("Graphics Setting")]
    public GameObject damageZonePrefab;

    private GameObject ZoneIns;
    [SerializeField]
    private float mapSize;
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private float centerX;
    [SerializeField]
    private float centerZ;
    [SerializeField]
    private float zoneRadius;

    [Header("GUI Setting")]
    [SerializeField]
    private Text timerText;

    [Header("Necessary Variables")]
    [SerializeField]
    private float[] waitingRestrictTimes;
    public float TimeLeft { get; private set; }
    public bool counting = false;
    private int restrictLevel;
    [SerializeField]
    private float restrictingTime = 10f;
    [SerializeField]
    private float shrinkRate = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            ZoneIns = PhotonNetwork.InstantiateSceneObject(damageZonePrefab.name, Vector3.zero, Quaternion.identity, 0, null);
            if(ZoneIns == null)
            {
                Debug.LogError("Instantiate Damage Zone Fail");
            }
            ZoneIns.transform.localScale = new Vector3(mapSize * 1.5f, 20, mapSize * 1.5f);
        }
        restrictLevel = 0;
        zoneRadius = mapSize;
        IgnitionDestrict();
    }

    void Update()
    {
        if (counting)
        {
            TimeLeft -= Time.deltaTime;
            TimerGUI(TimeLeft);
            if(TimeLeft <= 0)
            {
                TimeLeft = 0;
                counting = false;
                RestrictZone();
            }
        }
    }

    void IgnitionDestrict()
    {
        CalculateNextZone();
        TimeLeft = waitingRestrictTimes[restrictLevel];
        counting = true;
    }

    private void CalculateNextZone()
    {
        //Calculate Radius and Center of Second;
        float nextZoneRadius = zoneRadius * shrinkRate;
        float r = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        float rand = UnityEngine.Random.Range(0, zoneRadius - nextZoneRadius);
        center = new Vector2(center.x + rand * Mathf.Cos(r), center.y + rand * Mathf.Sin(r));
        zoneRadius = nextZoneRadius;
        
        restrictingTime = waitingRestrictTimes[restrictLevel];
    }
    
    private void TimerGUI(float _value)
    {
        TimeSpan time = TimeSpan.FromSeconds(_value);
        timerText.text = "距离限制安全区的时间剩余 : " + string.Format("{0:D1}分 : {1:D2}秒", time.Minutes, time.Seconds);
    }
    
    private void RestrictingGUI()
    {
        timerText.text = "正在限制安全区中，请尽快进入安全区以避免受到毒气伤害";
    }

    //Using Dowteen to change center and scale of the Zone
    private void RestrictZone()
    {
        RestrictingGUI();
        ZoneIns.transform.DOMove(new Vector3(center.x, 0, center.y), restrictingTime);
        ZoneIns.transform.DOScaleX(zoneRadius, restrictingTime);
        ZoneIns.transform.DOScaleZ(zoneRadius, restrictingTime);
        StartCoroutine(Restricting());
    }
    //Remark time when we shrink the zone;
    IEnumerator Restricting()
    {
        yield return new WaitForSeconds(restrictingTime);
        RestartTimer();
        StopCoroutine(Restricting());
    }

    //Start Next Zone Timer
    private void RestartTimer()
    {
        if(++restrictLevel == waitingRestrictTimes.Length)
        {
            return;
        }
        TimeLeft = waitingRestrictTimes[restrictLevel];
        CalculateNextZone();
        counting = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            
        }
        else
        {

        }
    }
}
