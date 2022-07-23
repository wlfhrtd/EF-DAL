using Microsoft.EntityFrameworkCore.Migrations;


namespace Dal.EfStructures
{
    public static class MigrationsHelper
    {
        public static void CreateStoredProcedure(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                exec (N'
                CREATE PROCEDURE [dbo].[GetPetName]
                    @carID int,
                    @petName nvarchar(50) output
                AS
                SELECT @petName = PetName FROM dbo.Inventory WHERE Id = @carID
                ')");
        }

        public static void DropStoredProcedure(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPetName]");

        public static void CreateCustomerOrderView(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                exec (N'
                CREATE VIEW [dbo].[CustomerOrderView]
                AS
                SELECT dbo.Customers.FirstName, dbo.Customers.LastName,
                       dbo.Inventory.Color, dbo.Inventory.PetName, dbo.Inventory.IsDrivable,
                       dbo.Makes.Name AS Make
                FROM dbo.Orders
                INNER JOIN dbo.Customers ON dbo.Orders.CustomerId = dbo.Customers.Id
                INNER JOIN dbo.Inventory ON dbo.Orders.CarId = dbo.Inventory.Id
                INNER JOIN dbo.Makes ON dbo.Makes.Id = dbo.Inventory.MakeId
                ')");
        }

        public static void DropCustomerOrderView(MigrationBuilder migrationBuilder)
            => migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[CustomerOrderView] ')");
    }
}
