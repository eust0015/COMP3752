using System.Collections;
using Audio;
using UnityEngine;

namespace UI.TimeProgression
{
    public class Sun : MonoBehaviour
    {
        private GameManager _g;
        [SerializeField] private Animator sunAnimator;
        [SerializeField] private int minutesInADay;
        private static readonly int Night = Animator.StringToHash("night");
        private static readonly int Moving = Animator.StringToHash("moving");
        [SerializeField] private FMODAudioSource startDaySound;
        [SerializeField] private FMODAudioSource startNightSound;
        
        public Animator SunAnimator
        {
            get => sunAnimator;
            private set => sunAnimator = value;
        }

        public int MinutesInADay
        {
            get => minutesInADay;
            set => minutesInADay = value;
        }

        private int SecondsInADay => minutesInADay * 60;
        private float AnimationSpeed => 1.0f / minutesInADay;

        public FMODAudioSource StartDaySound
        {
            get => startDaySound;
            set => startDaySound = value;
        }

        public FMODAudioSource StartNightSound
        {
            get => startNightSound;
            set => startNightSound = value;
        }
        
        private void Start()
        {
            var timer = FindObjectOfType<Timer>();
            if (timer != null)
                MinutesInADay = timer._timerStart / 60;
            _g = GameManager.current;
            _g.onRunStart += OnRunStart;
            _g.onTimerPlay += OnTimerPlay;
            _g.onTimerPause += OnTimerPause;
            _g.onTimerComplete += OnTimerComplete;
            SunAnimator.speed = AnimationSpeed;
            OnRunStart();
        }

        private void OnRunStart()
        {
            SunAnimator.SetBool(Night, false);
            SunAnimator.SetBool(Moving, true);
            StartDaySound.PlaySound();
            //StartCoroutine(DayCoroutine());
        }

        IEnumerator DayCoroutine()
        {
            yield return new WaitForSeconds(SecondsInADay);
            OnTimerComplete();
        }
        
        IEnumerator NightCoroutine()
        {
            yield return new WaitForSeconds(SecondsInADay);
            OnNightComplete();
        }
        
        private void OnTimerPlay()
        {
            
        }

        private void OnTimerPause()
        {

        }

        private void OnTimerComplete()
        {
            SunAnimator.SetBool(Night, true);
            StartNightSound.PlaySound();
            StartCoroutine(NightCoroutine());
        }

        private void OnNightComplete()
        {
            SunAnimator.SetBool(Night, false);
            StartCoroutine(DayCoroutine());
        }
        
        private void OnDestroy()
        {
            _g.onRunStart -= OnRunStart;
            _g.onTimerPlay -= OnTimerPlay;
            _g.onTimerPause -= OnTimerPause;
            _g.onTimerComplete -= OnTimerComplete;
        }
    }
}