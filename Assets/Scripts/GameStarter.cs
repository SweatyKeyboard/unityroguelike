using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public UpDown Speed;
    public UpDown BulSpeed;
    public UpDown Range;
    public UpDown Damage;
    public UpDown RoF;
    public UpDown MaxHealth;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameWithData()
    {
        PlayerPrefs.SetInt("SkillsSpeed", Speed.Points);
        PlayerPrefs.SetInt("SkillsBulletSpeed", BulSpeed.Points);
        PlayerPrefs.SetInt("SkillsRoF", RoF.Points);
        PlayerPrefs.SetInt("SkillsDamage", Damage.Points);
        PlayerPrefs.SetInt("SkillsRange", Range.Points);
        PlayerPrefs.SetInt("SkillsMaxHealth", MaxHealth.Points);

        SceneManager.LoadScene("Game");
    }
}
