using System.Linq;
using Enemy;
using FMODUnity;
using Player;
using UnityEngine;

namespace Audio
{
    public class FMODMusicSource : MonoBehaviour
    {
        public static FMODMusicSource currentlyPlayingMusic;
        public static FMODMusicSource previouslyPlayingMusic;
        public FMOD.Studio.EventInstance instance;
        public FMODUnity.EventReference music; //music will be your music track & variables to control the music
        [SerializeField] private int playerHealth;
        [SerializeField] private int numberOfEnemies;

        public int PlayerHealth
        {
            get => playerHealth;
            set
            {
                instance.setParameterByName("playerHealthPR", value);
                playerHealth = value;
            }
        }

        public int NumberOfEnemies
        {
            get => numberOfEnemies;
            set
            {
                instance.setParameterByName("currentNumberOfEnemies", value);
                playerHealth = value;
            }
        }

        public void PlayMusic()
        {
            if (currentlyPlayingMusic != null)
                currentlyPlayingMusic.instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            
            previouslyPlayingMusic = currentlyPlayingMusic;
            currentlyPlayingMusic = this;
            instance = FMODUnity.RuntimeManager.CreateInstance(music); // This also works (like tutorial, older style)
            instance.start();
            DetectPlayerHealth();
            DetectNumberOfEnemies();
        }

        public static void PlayPreviousMusic()
        {
            StopMusic();
            
            if (previouslyPlayingMusic == null)
                return;
            
            // Using deconstruction to swap currently playing music with previously playing music
            (currentlyPlayingMusic, previouslyPlayingMusic) = (previouslyPlayingMusic, currentlyPlayingMusic);
            currentlyPlayingMusic.instance.start();
            currentlyPlayingMusic.DetectPlayerHealth();
            currentlyPlayingMusic.DetectNumberOfEnemies();
        }

        public static void StopMusic()
        {
            if (currentlyPlayingMusic != null)
                currentlyPlayingMusic.instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); //Player is dead, stop music
        }

        public void DetectPlayerHealth()
        {
            var health = FindObjectOfType<PlayerHealth>();
            PlayerHealth = health.Health.Value;
        }
        
        public void DetectNumberOfEnemies()
        {
            var list = FindObjectsOfType<EnemyHealth>().ToList();
            NumberOfEnemies = list.Count;
        }
        

    }
}