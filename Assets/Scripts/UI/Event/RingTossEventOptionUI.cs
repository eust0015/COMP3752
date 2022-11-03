using Audio;
using Player;
using UI.AbilityInventory;
using UI.ItemInventory;
using UI.RelicInventory;
using UI.Statistics;
using UnityEngine;
using Random = System.Random;

namespace UI.Event
{
    public class RingTossEventOptionUI : EventOptionUI
    {
        [SerializeField] private int chanceToWin;
        [SerializeField] private int pollenCost;
        [SerializeField] private Transform messageContainer;
        [SerializeField] private GameObject insufficientPaymentMessagePrefab;
        [SerializeField] private GameObject loseMessagePrefab;
        [SerializeField] private FMODAudioSource winSound;
        public int ChanceToWin
        {
            get => chanceToWin;
            private set => chanceToWin = value;
        }

        public int PollenCost
        {
            get => pollenCost;
            set => pollenCost = value;
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
            
            PayCost();
            
            Random rnd = new Random();
            
            if (ChanceToWin >= rnd.Next(1, 101))
                Win();
            else
                Lose();
        }

        public bool InsufficientPayment()
        {
            Currency currency = FindObjectOfType<CurrencyHUD>().Currency;

            if (currency == null)
                return true;

            return currency.Value < PollenCost;
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
            ShowMessage(LoseMessagePrefab);
            UpdateDescription();
        }

        public void PayCost()
        {
            Currency currency = FindObjectOfType<CurrencyHUD>().Currency;
            currency.DecreaseValue(PollenCost);
        }
        
        public void ShowMessage(GameObject message)
        {
            if (message == null)
                return;
            
            Instantiate(message, MessageContainer);
        }
        
        public override void UpdateDescription()
        {
            Description.text = "<b>Ring Toss.</b>\nExchange 2 pollen for a " + 
                               ChanceToWin +
                               "% chance to win a Critter relic. Additional 10% chance to win each attempt.";
        }
    }
}