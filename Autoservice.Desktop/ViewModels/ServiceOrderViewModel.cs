using Autoservice.Desktop.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Autoservice.Desktop.ViewModels
{
    internal class ServiceOrderViewModel : TableViewModel<ServiceOrder>
    {
        public ServiceOrderViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<ServiceOrder>(dbContext.ServiceOrder);
            }
        }
        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<ServiceOrder>(dbContext.ServiceOrder);
                }
            }
            else
            {
                var items = Items;
                Items = new ObservableCollection<ServiceOrder>();
                foreach (var item in items)
                {
                    if (
                        item.ID.ToString().ToLower().Contains(value.ToLower()) ||
                        item.OrderID.ToString().ToLower().Contains(value.ToLower()) ||
                        item.ServiceID.ToString().ToLower().Contains(value.ToLower())
                       )
                    {
                        Items.Add(item);
                    }
                }
            }
        }
    }
}
