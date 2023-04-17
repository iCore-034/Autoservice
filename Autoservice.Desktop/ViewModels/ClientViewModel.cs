using Autoservice.Desktop.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Autoservice.Desktop.ViewModels
{
    internal class ClientViewModel : TableViewModel<Client>
    {
        public ClientViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<Client>(dbContext.Client);
            }
        }
        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<Client>(dbContext.Client);
                }
            }
            else
            {
                var items = Items;
                Items = new ObservableCollection<Client>();
                foreach (var item in items)
                {
                    if (
                        item.ID.ToString().ToLower().Contains(value.ToLower()) ||
                        item.Name.ToString().ToLower().Contains(value.ToLower()) ||
                        item.SecondName.ToString().ToLower().Contains(value.ToLower()) ||
                        item.SurName.ToString().ToLower().Contains(value.ToLower())
                       )
                    {
                        Items.Add(item);
                    }
                }
            }
        }
    }
}
