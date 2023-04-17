using Autoservice.Desktop.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Autoservice.Desktop.ViewModels
{
    internal class CarViewModel : TableViewModel<Car>
    {
        public CarViewModel()
        {
            using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
            {
                Items = new System.Collections.ObjectModel.ObservableCollection<Car>(dbContext.Car);
            }
        }
        protected override void FilterAction(string value)
        {
            if (string.IsNullOrEmpty(_filter))
            {
                using (var dbContext = _autoserviceDbContextFactory.CreateDbContext())
                {
                    Items = new System.Collections.ObjectModel.ObservableCollection<Car>(dbContext.Car);
                }
            }
            else
            {
                var cars = Items; 
                Items = new ObservableCollection<Car>();
                foreach (var car in cars)
                {
                    if (
                        car.ID.ToString().ToLower().Contains(value.ToLower()) ||
                        car.Brand.ToString().ToLower().Contains(value.ToLower()) ||
                        car.Model.ToString().ToLower().Contains(value.ToLower()) ||
                        car.RegNum.ToString().ToLower().Contains(value.ToLower()) ||
                        car.VinNum.ToString().ToLower().Contains(value.ToLower())
                       )
                    {
                        Items.Add(car);
                    }
                }
            }
        }
    }
}
