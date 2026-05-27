using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Repositories.Abstractions;

public interface IReservationRepository : IRepository<Reservation, Guid>
{
    Task<IReadOnlyList<Reservation>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Reservation>> GetByDateAsync(DateTime date, CancellationToken cancellationToken);

    Task<IReadOnlyList<Reservation>> GetByStatusAsync(ReservationStatus status, CancellationToken cancellationToken);

    //для проверки 15-минутного правила
    Task<IReadOnlyList<Reservation>> GetExpiredConfirmedAsync(DateTime olderThan, CancellationToken cancellationToken);
}
