using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    
    private bool _isDead = false;
    public bool IsDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }


    public int maxHealth = 100;
    public int health = 100;
    public int kills = 0;
    
    public delegate void OnChangePlayerInfo();
    public OnChangePlayerInfo OnStart;
    public OnChangePlayerInfo OnDeath;
    public OnChangePlayerInfo OnRespawn;
    
    void Start()
    {
        OnStart += Setup;
        OnRespawn += Setup;
    }

    void Update()
    {
        
    }

    private void Setup()
    {
        health = maxHealth;
        OnDeath += DeathMessage;
        IsDead = false;
    }

    [PunRPC]
    public void RPC_GetDamage(int _damage, int _KillerID)
    {
        if (IsDead)
        {
            return;
        }
        health -= _damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        if(health <= 0)
        {
            OnDeath();
            PhotonView pv = PhotonView.Find(_KillerID);
            if (!pv.isMine)
            {
                pv.RPC("RPC_AddKills", PhotonTargets.All);
            }
            //Died ParticleSystem Play

            StartCoroutine(Respawn());
        }
    }
    
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        OnRespawn();
        Debug.Log(transform.name + " player respawn.");
        StopCoroutine(Respawn());
    }

    [PunRPC]
    public void RPC_AddKills()
    {
        kills += 1;
    }

    void DeathMessage()
    {
        IsDead = true;
        Debug.Log("Death");
    }
    
}
