using Autoservice.Desktop.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Autoservice.Desktop.ViewModels
{
    internal class ServiceViewModel : TableViewModel<Service>
    {
        public ServiceViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<Service>(dbContext.Service);
            }
        }
        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<Service>(dbContext.Service);
                }
            }
            else
            {
                var items = Items;
                Items = new ObservableCollection<Service>();
                foreach (var item in items)
                {
                    if (
                        item.ID.ToString().ToLower().Contains(value.ToLower()) ||
                        item.Name.ToString().ToLower().Contains(value.ToLower()) ||
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
