using Cards.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    class CardsViewModel : BaseViewModel<Card>
    {
        
        private Card selectedItem;

        
        public CardsViewModel()
        {
            DataStore = new JSONCardsDataStore();

            Title = "Cards";
            Items = new ObservableCollection<Card>();

            Load(); 
        }

        public ObservableCollection<Card> Items { get; set; }

        public Card SelectedItem {
            get {
                if (selectedItem == null) 
                    selectedItem = new Card();

                return selectedItem;  
            }
            set {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public async Task Load()
        {
            try
            {
                IEnumerable<Card> itemList = await DataStore.GetAsync();
                Items = new ObservableCollection<Card>(itemList);
                OnPropertyChanged("Items");
            }
            catch
            {
                throw new ArgumentNullException("Connection error :(");
            }
        }

        public async Task<Card> AddAsync(Card card)
        {
            Card res;
            try
            {
                res = await DataStore.AddAsync(card);
                await Load(); //upd all elements
                SelectedItem = Items.Last();
            }
            catch
            {
                res = null;
            }
            return res;
        }

        public async Task<Card> DeleteAsync(int id)
        {
            Card res;
            try
            {
                res = await DataStore.DeleteAsync(id);
                await Load(); //upd all elements
                SelectedItem = Items.FirstOrDefault();
            }
            catch
            {
                res = null;
            }
            return res;
        }

        public async Task<Card> UpdateAsync(Card card)
        {
            Card res;
            try
            {
                int oldId = card.Id;
                res = await DataStore.UpdateAsync(card);
                await Load(); //upd all elements
                SelectedItem = Items.FirstOrDefault(i => i.Id == oldId);
            }
            catch
            {
                res = null;
            }
            return res;
        }

        public void RefreshSelectedData()
        {            
            var s = SelectedItem;
            SelectedItem = null;
            SelectedItem = s;
        }
    }
}
