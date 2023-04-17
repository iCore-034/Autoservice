using Autoservice.Desktop.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Autoservice.Desktop.ViewModels
{
    internal class EmployeeViewModel : TableViewModel<Employee>
    {
        public EmployeeViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<Employee>(dbContext.Employee);
            }
        }

        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<Employee>(dbContext.Employee);
                }
            }
            else
            {
                var items = Items;
                Items = new ObservableCollection<Employee>();
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
