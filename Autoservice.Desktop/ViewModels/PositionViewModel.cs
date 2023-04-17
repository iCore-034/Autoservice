using Autoservice.Desktop.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Autoservice.Desktop.ViewModels
{
    internal class PositionViewModel : TableViewModel<Position>
    {
        public PositionViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<Position>(dbContext.Position);
            }
        }
        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<Position>(dbContext.Position);
                }
            }
            else
            {
                var items = Items;
                Items = new ObservableCollection<Position>();
                foreach (var item in items)
                {
                    if (
                        item.ID.ToString().ToLower().Contains(value.ToLower()) ||
                        item.Name.ToString().ToLower().Contains(value.ToLower()) ||
                        item.Salary.ToString().ToLower().Contains(value.ToLower())
                       )
                    {
                        Items.Add(item);
                    }
                }
            }
        }
    }
}
