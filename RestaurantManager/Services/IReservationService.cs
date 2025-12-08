using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManager.Models;

namespace RestaurantManager.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservations();
        Task<Reservation?> GetReservationById(int id);
        Task CreateReservation(Reservation reservation);
        Task EditReservation(Reservation reservation);
        Task DeleteReservation(int id);

        SelectList GetCustomerSelectList();
        Task<List<Reservation>> spGetTop3NumberOfGuestsReservations();


    }
}
