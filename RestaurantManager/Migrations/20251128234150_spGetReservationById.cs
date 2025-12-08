using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManager.Migrations
{
    /// <inheritdoc />
    public partial class spGetReservationById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"
         CREATE PROCEDURE spGetReservationById
             @ReservationId INT
            AS
            BEGIN
                SET NOCOUNT ON;
                SELECT * FROM Reservation WHERE Id = @ReservationId
            END
        ";
                
            migrationBuilder.Sql(procedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spGetReservationById";
            migrationBuilder.Sql(procedure);



        }
    }
}