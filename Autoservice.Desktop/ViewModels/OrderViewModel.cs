using Autoservice.Desktop.MVVM.Navigation;
using Autoservice.Desktop.ViewModels.Base;
using Autoservice.Persistance.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Autoservice.Desktop.ViewModels
{
    internal class OrderViewModel : TableViewModel<Order>
    {
        private delegate void CommandClickButton();
        
        public OrderViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<Order>(dbContext.Order);
            }
            
        }
        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<Order>(dbContext.Order);
                }
            }
            else
            {
                var items = Items;
                Items = new ObservableCollection<Order>();
                foreach (var item in items)
                {
                    if (
                        item.ID.ToString().ToLower().Contains(value.ToLower()) ||
                        item.Date.ToString().ToLower().Contains(value.ToLower()) ||
                        item.Cost.ToString().ToLower().Contains(value.ToLower())
                       )
                    {
                        Items.Add(item);
                    }
                }
            }
        }
        
    }
}
