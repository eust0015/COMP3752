using Audio;
using Player;
using UI.AbilityInventory;
using UI.ItemInventory;
using UI.RelicInventory;
using UnityEngine;
using Random = System.Random;

namespace UI.Event
{

    public class LoveTesterEventOptionUI : EventOptionUI
    {
        [SerializeField] private int chanceToWin;
        [SerializeField] private int loseHealthAmount;
        [SerializeField] private Transform messageContainer;
        [SerializeField] private GameObject insufficientPaymentMessagePrefab;
        [SerializeField] private GameObject loseMessagePrefab;
        [SerializeField] private FMODAudioSource winSound;
        public int ChanceToWin
        {
            get => chanceToWin;
            private set => chanceToWin = value;
        }

        public int LoseHealthAmount
        {
            get => loseHealthAmount;
            set => loseHealthAmount = value;
        }

        public Transform MessageContainer
        {
            get => messageContainer;
            set => messageContainer = value;
        }

        public GameObject InsufficientPaymentMessagePrefab
        {
            get => insufficientPaymentMessagePrefab;
            set => insufficientPaymentMessagePrefab = value;
        }

        public GameObject LoseMessagePrefab
        {
            get => loseMessagePrefab;
            set => loseMessagePrefab = value;
        }

        public FMODAudioSource WinSound
        {
            get => winSound;
            set => winSound = value;
        }

        public override void Choose()
        {
            if (InsufficientPayment())
            {
                ShowMessage(InsufficientPaymentMessagePrefab);
                return;
            }
            
            Random rnd = new Random();
            
            if (ChanceToWin >= rnd.Next(1, 101))
                Win();
            else
                Lose();
        }

        public bool InsufficientPayment()
        {
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            return health.Health.Value < LoseHealthAmount + 1;
        }

        public void Win()
        {
            WinSound.PlaySound();
            Option.Choose();
            foreach (var ability in AbilityList)
            {
                AbilityInventoryUI abilityInventory = FindObjectOfType<AbilityInventoryUI>();
                abilityInventory.AddAbility(ability);
            }
            foreach (var relic in RelicList)
            {
                RelicInventoryUI relicInventory = FindObjectOfType<RelicInventoryUI>();
                relicInventory.AddInventoryRelicUI(relic);
            }
            foreach (var item in ItemList)
            {
                ItemInventoryUI itemInventory = FindObjectOfType<ItemInventoryUI>();
                itemInventory.AddItem(item);
            }
            foreach (var effect in EffectList)
            {
                Instantiate(effect);
            }
            EventUI theEvent = FindObjectOfType<EventUI>();
            theEvent.StartPostEventSequence(Option.PostDescription + "\n<b>" + Option.PostTitle + "<b>");
        }

        public void Lose()
        {
            ChanceToWin += 10;
            PayCost();
            ShowMessage(LoseMessagePrefab);
            UpdateDescription();
        }

        public void PayCost()
        {
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            health.Health.DecreaseValue(LoseHealthAmount);
        }
        
        public void ShowMessage(GameObject message)
        {
            if (message == null)
                return;
            
            Instantiate(message, MessageContainer);
        }
        
        public override void UpdateDescription()
        {
            Description.text = "<b>Love Tester.</b>\n" + 
                               ChanceToWin +
                               "% chance to receive 1 Heart Stealer relic and " + 
                               (100 - ChanceToWin) + 
                               "% chance to lose 1 heart. Additional 10% chance to win each attempt.";
        }
    }
}