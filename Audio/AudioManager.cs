using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace sprint0Test.Audio
{

    public enum SongList
    {
        Title,
        Dungeon,
        Boss
    }
    public class AudioManager
    {
        private static AudioManager _instance;
        public static AudioManager Instance => _instance ??= new AudioManager();

        private static Song currentSong;

        

        public void LoadContent(ContentManager content)
        {
            Song dungeonSong = content.Load<Song>("sanctuary_dungeon");
            Song bossSong = content.Load<Song>("ganons_message");
            Song titleSong = content.Load<Song>("title");

            listToSong[SongList.Title] = titleSong;
            listToSong[SongList.Dungeon] = dungeonSong;
            listToSong[SongList.Boss] = bossSong;
        }

        private Dictionary<SongList, Song> listToSong = new Dictionary<SongList, Song>
        {
        };

        public void SetSong(SongList newSong)
        {
            Song selectedSong = listToSong[newSong];

            if (currentSong != selectedSong)
            {
                MediaPlayer.Stop();
                currentSong = selectedSong;
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(currentSong);
            }
        }

        public static void PauseMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            if (MediaPlayer.State == MediaState.Paused)
                MediaPlayer.Resume();
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public static void Mute()
        {
            MediaPlayer.Volume = 0;
            
        }

        public static void VolumeUp()
        {
            MediaPlayer.Volume = MediaPlayer.Volume + (float)0.1;
        }

        public static void VolumeDown()
        {
            MediaPlayer.Volume = MediaPlayer.Volume - (float)0.1;
        }


    }
}
