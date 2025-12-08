using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManager.Migrations
{
    /// <inheritdoc />
    public partial class spGetCustomerById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string spGetCustomerById = @"
                CREATE PROCEDURE spGetCustomerById
                    @CustomerId INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT *
                    FROM Customer
                    WHERE Id = @CustomerId;
                END";







            migrationBuilder.Sql(spGetCustomerById);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string spGetCustomerById = @"
                Drop PROCEDURE spGetCustomerById";
            migrationBuilder.Sql(spGetCustomerById);
        }
    }
}
