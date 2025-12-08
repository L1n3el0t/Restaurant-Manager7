using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data;
using RestaurantManager.Models;

namespace RestaurantManager.Services
{
    public class ReservationService : IReservationService
    {

        private readonly ILogger<ReservationService> _logger;
        private readonly RestaurantManagerContext _context;


        public ReservationService(RestaurantManagerContext context, ILogger<ReservationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(GetAllReservations);
            var restaurantManagerContext = _context.Reservation.Include(r => r.Customer);
            _logger.LogInformation($"All reservations retrieved using {action} by request {correlationId} on {DateTime.UtcNow}.");
            return await restaurantManagerContext.ToListAsync();
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(GetReservationById);

            try
            {
                var reservations = await _context.Reservation
               
                    .FromSqlRaw("EXEC spGetReservationById {0}", id)
                    .ToListAsync();

                var reservation = reservations.FirstOrDefault();
                if (reservation == null)
                {
                    _logger.LogWarning($"Reservation with ID {id} not found using {action} by request {correlationId} on {DateTime.UtcNow}).");
                }
                else
                {
                   
                    await _context.Entry(reservation)
                        .Reference(r => r.Customer)
                        .LoadAsync();

                    _logger.LogInformation($"Reservation with ID {id} retrieved and Customer loaded using {action} by request {correlationId} on {DateTime.UtcNow}).");
                }
                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving reservation with ID {id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
        }
        public async Task CreateReservation(Reservation reservation)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(CreateReservation);

            try
            {
                _context.Reservation.Add(reservation);
                _logger.LogInformation($"Reservation {reservation.Id} created using {action} by request {correlationId} on {DateTime.UtcNow}).");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating reservation {reservation.Id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
        }
        public async Task EditReservation(Reservation reservation)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(EditReservation);

            try
            {
                _context.Reservation.Update(reservation);
                _logger.LogInformation($"Reservation with ID {reservation.Id} updated using {action} by request {correlationId} on {DateTime.UtcNow}).");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating reservation with ID {reservation.Id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteReservation(int id)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(DeleteReservation);
            var reservation = await _context.Reservation.FindAsync(id);
            try
            {
                if (reservation != null)
                {
                    _context.Reservation.Remove(reservation);
                    _logger.LogInformation($"Reservation with ID {id} deleted using {action} by request {correlationId} on {DateTime.UtcNow}).");
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning($"Reservation with ID {id} not found for deletion using {action} by request {correlationId} on {DateTime.UtcNow}).");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting reservation with ID {id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
        }
        public SelectList GetCustomerSelectList()
            {

            var data = new SelectList(_context.Customer, "Id", "Id", "Name");
            return data;

        }
        public async Task<List<Reservation>> spGetTop3NumberOfGuestsReservations()
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(spGetTop3NumberOfGuestsReservations);
            try
            {
                // Get reservations from stored procedure
                var reservations = _context.Reservation
                    .FromSqlRaw("EXEC spGetTop3NumberOfGuestsReservations")
                    .AsEnumerable().ToList();

                foreach (var reservation in reservations)
                {
                    await _context.Entry(reservation)
                        .Reference(r => r.Customer)
                        .LoadAsync();
                }
                _logger.LogInformation($"Top 3 reservations by number of guests retrieved using {action} by request {correlationId} on {DateTime.UtcNow}).");
                return reservations;
                }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving top 3 reservations by number of guests using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
            }
        }
    }
