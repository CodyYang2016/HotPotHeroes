using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace sprint0Test.Audio
{

    public enum SoundList
    {
        sword,
        hey,
        door,
        scream,
        horse,
        chicken,
        boss

    }
    public class SoundManager
    {
        private static SoundManager _instance;
        public static SoundManager Instance => _instance ??= new SoundManager();

        

        public void LoadContent(ContentManager content)
        {
            SoundEffect bossHit = content.Load<SoundEffect>("MC_Boss_Hit");
            SoundEffect sword = content.Load<SoundEffect>("MC_Link_Sword");
            SoundEffect horse = content.Load<SoundEffect>("MC_Epona");
            SoundEffect chicken = content.Load<SoundEffect>("MC_Cucco5");
            SoundEffect scream = content.Load<SoundEffect>("MC_Minish_Inaudible");
            SoundEffect door = content.Load<SoundEffect>("MC_Door1");
            SoundEffect hey = content.Load<SoundEffect>("MC_Zelda_Hey");

            listToSound[SoundList.boss] = bossHit;
            listToSound[SoundList.sword] = sword;
            listToSound[SoundList.horse] = horse;
            listToSound[SoundList.chicken] = chicken;
            listToSound[SoundList.scream] = scream;
            listToSound[SoundList.door] = door;
            listToSound[SoundList.hey] = hey;

        }

        private Dictionary<SoundList, SoundEffect> listToSound = new Dictionary<SoundList, SoundEffect>
        {
        };

        public void PlaySound(SoundList Sound)
        {
            SoundEffect currentSound = listToSound[Sound];
            currentSound.Play();
        }

    }
}
