// using System;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Content;
// using Microsoft.Xna.Framework.Input;
// using Microsoft.Xna.Framework.Media;
// using System.Collections.Generic;

// namespace sprint0Test.Audio
// {

//     public enum SoundList
//     {
//         Dungeon,
//         Boss
//     }
//     public class SoundManager
//     {
//         private static SoundManager _instance;
//         public static SoundManager Instance => _instance ??= new SoundManager();

//         private static Sound currentSong;

        

//         public void LoadContent(ContentManager content)
//         {
//             Song dungeonSong = content.Load<Song>("Audio/sanctuary_dungeon");
//             Song bossSong = content.Load<Song>("Audio/ganons_message");

//             listToSound[SoundList.Dungeon] = dungeonSong;
//             listToSound[SoundList.Boss] = bossSong;
//         }

//         private Dictionary<SoundList, Sound> listToSound = new Dictionary<SoundList, Sound>
//         {
//         };

//         public void PlaySound(SoundList Sound)
//         {
//             MediaPlayer.Stop();
//             currentSong = listToSound[Sound];
//             MediaPlayer.IsRepeating = true;
//             MediaPlayer.Play(currentSong);
//         }

//     }
// }
