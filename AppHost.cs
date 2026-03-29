namespace FuelFlow_Server.AppHost
{
    public static class AppHost
    {
        public static void Main(string[] args)
        {
            var builder = DistributedApplication.CreateBuilder(args);

            builder.AddProject<Projects.FuelFlow_Server>("fuelflow-server");
            builder.AddProject<Projects.Authentication_Service>("authentication-service");
            builder.AddProject<Projects.Inventory_Service>("inventory-service");
            builder.AddProject<Projects.Sales_Service>("sales-service");

            builder.Build().Run();
        }
    }
}