using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Autoservice.Desktop.MVVM.Navigation
{
    class SelectedOrderBingings
    {
        public static int orderID;

        private string orderClient;

        private string orderClientPhone;

        private string orderCarBrand;

        private string orderModel;

        private string orderRegNumber;

        private string orderVin;

        private string orderServiceName;

        private int orderCost;

        public SelectedOrderBingings(AutoserviceDbContext context)
        {
            // Order ID  && Cost
            List<Order> elem = context.Order.ToList();
            var selectedElem = elem.Where(x => x.ID == orderID).ElementAt(0);
            var orderAsClass = selectedElem as Order;
            this.orderCost = orderAsClass.Cost;

            // Car Brand && Model && RegNum && VIN
            var listCars = context.Car.Where(x => x.ID == orderAsClass.CarID);
            var exactCar = listCars.First() as Car;
            this.orderCarBrand = exactCar.Brand;
            this.orderModel = exactCar.Model;
            this.orderRegNumber = exactCar.RegNum;
            this.orderVin = exactCar.VinNum;

            // CliendName && SurName && Phone && SecondName
            var listClient = context.Client.Where(x => x.ID == orderAsClass.ClientID);
            var exactClient = listClient.First() as Client;
            this.orderClient = exactClient.Name + " " + exactClient.SecondName + " " + exactClient.SurName;
            this.orderClientPhone = exactClient.Phone;
            // Service Name
            List<ServiceOrder> serviceOrderList = context.ServiceOrder.ToList();
            var serviceOrder = serviceOrderList.Where(x => x.OrderID == orderID).ElementAt(0);
            serviceOrder = serviceOrder as ServiceOrder;
            var serviceTemp = context.Service.ToList();
            var service = serviceTemp.Where(x => x.ID == serviceOrder.ServiceID).ElementAt(0);
            service = service as Service;
            this.orderServiceName = service.Name;



        }
        public void outputOrderToFile()
        {
            MessageBoxResult res = MessageBox.Show(
                $"OrderID: {orderID}\n" +
                $"Client: {this.orderClient}\n"+
                $"Client Phone: {this.orderClientPhone}\n" +
                $"Car: {this.orderCarBrand} {this.orderModel}\n"+
                $"RegNum and Vin: {this.orderRegNumber} {this.orderVin}\n"+
                $"Service: {this.orderServiceName}\n"+
                $"Const: {this.orderCost}",
                "Are you shure you want to generate file with this order?",
                MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                List<object> Elements = new List<object> { 
                    orderID, 
                    orderClient, 
                    orderClientPhone, 
                    orderCarBrand,
                    orderModel,
                    orderRegNumber,
                    orderVin,
                    orderServiceName,
                    orderCost
                };
                string filePath = "C:\\Users\\Romanticore\\source\\repos\\Autoservice-main\\src\\Autoservice.Desktop\\FileTemplate\\";
                string textFromPath = File.ReadAllText(filePath + "index.html");
                string[] splitFileText = textFromPath.Split('*');
                string outputTextFile = "";
                for(int i = 0; i < Elements.Count; i++)
                {
                    outputTextFile += splitFileText[i] + Elements[i].ToString();
                }
                outputTextFile += splitFileText.Last();
                string outputFilePath = "C:\\Users\\Romanticore\\source\\repos\\Autoservice-main\\src\\Autoservice.Desktop\\FileTemplate\\";
                File.WriteAllText(outputFilePath + $"Order #{orderID}.html",outputTextFile);
                MessageBox.Show($"File with the order was created in directory:\n{filePath}");
            }
        }
    }
}
