using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickbackEffect : MonoBehaviour
{

    [System.Serializable]
    public struct kickBackEffect
    {
        public float amount;
        public float speed;
    }

    private WeaponSway ws;
    private Shotgun shotgun;
    public List<kickBackEffect> slotEffects = new List<kickBackEffect>();
    public float amount = 3.0f;
    public float speed = 1.0f;

    private Vector3 initPosition;
    private float counter;

    Vector3 currentPosition;

    private void Awake()
    {
        initPosition = transform.localPosition;
        ws = GetComponent<WeaponSway>();
        shotgun = GetComponent<Shotgun>();
    }


    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime * speed;

                  
        if (counter <= 1.0f)
        {
            currentPosition = Vector3.Lerp(currentPosition, initPosition, counter);
            transform.localPosition = currentPosition;
        }
        else
        {
            ws.enabled = true;
        }
    }

    public void DoKickback()
    {
        if (shotgun.canShoot)
        {
            initPosition = transform.localPosition;
            currentPosition = initPosition - new Vector3(0, 0, amount);
            counter = 0.0f;
            ws.enabled = false;
        }
    }
}