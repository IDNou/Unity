using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionGen : MonoBehaviour
{
    public float GenTime = 0;
    public GameObject Minion;
    public Transform GenPos;
    private bool isPlaying = true;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("NaelTower").Length <= 0 || GameObject.FindGameObjectsWithTag("UndeadTower").Length <= 0)
        {
            isPlaying = false;
        }

        if (isPlaying)
        {
            if (GenTime <= 0)
            {
                GenTime = 10.0f;
                GameObject GenMinion = Instantiate(Minion, GenPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                //Status Setting = GenMinion.GetComponent<Status>();
                //if (Minion.name == "Orc" || Minion.name == "Barbarian")
                //{
                //    Setting.Level = 1;
                //    Setting.HP = 240.0f;
                //    Setting.MAXHP = 240.0f;
                //    Setting.MP = 0;
                //    Setting.MAXMP = 0;
                //    Setting.ATK = 20.0f;
                //    Setting.DEF = 0;
                //    Setting.SPD = 0;
                //    Setting.EXP = 20;
                //    Setting.CUREXP = 0;
                //    Setting.MAXEXP = -1;
                //}
                //else if (Minion.name == "VoidWaker" || Minion.name == "Dragon")
                //{
                //    Setting.Level = 1;
                //    Setting.HP = 170.0f;
                //    Setting.MAXHP = 170.0f;
                //    Setting.MP = 0;
                //    Setting.MAXMP = 0;
                //    Setting.ATK = 40.0f;
                //    Setting.DEF = 0;
                //    Setting.SPD = 0;
                //    Setting.EXP = 30;
                //    Setting.CUREXP = 0;
                //    Setting.MAXEXP = -1;
                //}
            }
            else
            {
                GenTime -= Time.deltaTime;
            }
        }
    }

}
