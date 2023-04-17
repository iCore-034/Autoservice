using Autoservice.Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Autoservice.Desktop.Controls;
using Autoservice.Desktop.MVVM.Navigation;

namespace Autoservice.Desktop.ViewModels.Base
{
    internal class TableViewModel<TModel> : ViewModel
        where TModel : BaseEntity, new()
    {
        //конструктор
        public TableViewModel()
        {
            _autoserviceDbContextFactory = new AutoserviceDbContextFactory();
            _updatedItemsIds = new List<int>();
            DeleteSelectedItem = new RelayCommand(DeleteItem, (obj) => Configuration.IsAdmin && String.IsNullOrEmpty(_filter));
            AddNewRecord = new RelayCommand(AddRecord, (obj) => Configuration.IsAdmin && String.IsNullOrEmpty(_filter));
            CommitChanges = new RelayCommand(Commit, (obj) => Configuration.IsAdmin && String.IsNullOrEmpty(_filter));
            hehe = new RelayCommand(ButtonIsClicked, (obj) => Configuration.IsAdmin && String.IsNullOrEmpty(_filter));
        }

        protected readonly AutoserviceDbContextFactory _autoserviceDbContextFactory;

        private ObservableCollection<TModel> _items;
        private TModel _selectedItem;
        protected List<int> _updatedItemsIds;
        protected string _filter;

        public TModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (Configuration.IsAdmin)
                {
                    _selectedItem = value;
                    if (_selectedItem != null)
                    {
                        _updatedItemsIds.Add(_selectedItem.ID);
                    }
                    OnPropertyChanged(nameof(SelectedItem));
                }
                else
                {
                    _selectedItem = value;
                    //OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public ObservableCollection<TModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        #region Фильтр
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                FilterAction(value);
                OnPropertyChanged(nameof(Filter));
            }
        }

        protected virtual void FilterAction(string value) { }
        #endregion

        #region Операции с данными

        #region Команды
        public Command hehe { get; set; }
        public Command AddNewRecord { get; }
        public Command DeleteSelectedItem { get; }
        public Command CommitChanges { get; set; }

        #endregion

        #region Методы
        public void ButtonIsClicked(object obj)
        {
            
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                try
                {
                    SelectedOrderBingings sob = new SelectedOrderBingings(dbContext);
                    sob.outputOrderToFile();
                }
                catch (Exception)
                {
                    MessageBox.Show("Chose the order!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
        private void Commit(object obj)
        {
            try
            {
                List<TModel> dbData;
                var itemsIds = Items.Select(x => x.ID);

                // deleting removed items
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    dbContext.Set<TModel>().RemoveRange(dbContext.Set<TModel>().Where(x => !itemsIds.Contains(x.ID)));
                    dbData = dbContext.Set<TModel>().ToList();
                    dbContext.SaveChanges();
                }

                // add and update items
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    foreach (var item in Items)
                    {
                        // add item if it is not exists in DB
                        if (!dbData.Any(x => x.ID == item.ID))
                        {
                            //item.ID = 0;
                            dbContext.Set<TModel>().Add(item);
                        }
                    }

                    // update selected items
                    dbContext.UpdateRange(Items.Where(x => _updatedItemsIds.Contains(x.ID)));
                    _updatedItemsIds.Clear();

                    dbContext.SaveChanges();
                }
                MessageBox.Show("Successfully!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AddRecord(object obj)
        {
            if (Items.Count != 0)
            {
                Items.Add(new TModel() { ID = MaxId() + 1 });
            }
            else
            {
                Items.Add(new TModel() { ID = 1 });
            }
        }

        private int MaxId()
        {
            int max = Items[0].ID;
            foreach (var item in Items)
            {
                if (item.ID > max)
                {
                    max = item.ID;
                }
            }
            return max;
        }

        private void DeleteItem(object obj)
        {
            if (new DialogWindow().ShowDialog() == true)
            {
                if (Items.Count != 0 && SelectedItem != null)
                {
                    var previousItemIndex = Items.IndexOf(SelectedItem) - 1;
                    if (previousItemIndex >= 0 && previousItemIndex < Items.Count)
                    {
                        var beforeSelectedItem = Items[previousItemIndex];
                        Items.Remove(SelectedItem);
                        SelectedItem = beforeSelectedItem;
                    }
                    else
                    {
                        Items.Remove(SelectedItem);
                    }
                }
            }
        }

        #endregion


        #endregion


    }
}
