using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSkillPointsBtn : MonoBehaviour
{
    public Player player;
    public AddSkillPoints addSkillPoints;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character").GetComponent<Player>();
        addSkillPoints = GetComponentInParent<AddSkillPoints>();
    }
    public void AddAGI()
    {
        player.AGI += 1;
        addSkillPoints.SetAGI(player.AGI);
        player.statusPoints -= 1;
        addSkillPoints.SetStatusPoint(player.statusPoints);
    }
    public void AddSTR()
    {
        player.STR += 1;
        addSkillPoints.SetSTR(player.STR);
        player.statusPoints -= 1;
        addSkillPoints.SetStatusPoint(player.statusPoints);
    }

    public void AddPOW()
    {
        player.POW += 1;
        addSkillPoints.SetPOW(player.POW);
        player.statusPoints -= 1;
        addSkillPoints.SetStatusPoint(player.statusPoints);
    }
}
