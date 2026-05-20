using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Services;
using Restaurant.ValueObjects;

Console.WriteLine("проверка доменного слоя ресторана\n");

var service = new ReservationService();

//клиент
var client = new Client(
    new PersonName("Юлия Иванова"),
    new Phone("+79001234567"));

Console.WriteLine($"Клиент: {client.Name} | {client.Phone} | Id: {client.Id}\n");

//столик
var table = new Table(id: 1, number: 5, seats: 4);
Console.WriteLine($"Столик №{table.Number}, мест: {table.Seats}, доступен: {table.IsAvailable}\n");

//график работы (понедельник, 10:00–22:00)
var workingHours = new WorkingHours(
    id: 1,
    dayOfWeek: DayOfWeek.Monday,
    openTime: new TimeSpan(10, 0, 0),
    closeTime: new TimeSpan(22, 0, 0),
    isWorkingDay: true);

Console.WriteLine($"График: {workingHours.DayOfWeek}, {workingHours.OpenTime}–{workingHours.CloseTime}\n");

//создание брони
var start = DateTime.UtcNow.AddDays(1).Date.AddHours(13); //завтра в 13:00
var end = start.AddHours(2);                               //до 15:00

var reservation = service.CreateReservation(client, table, workingHours, start, end, guestsCount: 3);
Console.WriteLine($"Бронь создана: {reservation.Id}");
Console.WriteLine($"  Статус:  {reservation.Status}");
Console.WriteLine($"  Время:   {reservation.StartTime:HH:mm} – {reservation.EndTime:HH:mm}");
Console.WriteLine($"  Гостей:  {reservation.GuestsCount}\n");

//подтверждение
var adminId = Guid.NewGuid();
reservation.Confirm(adminId);
Console.WriteLine($"Бронь подтверждена администратором {adminId}\n");

//попытка подтвердить повторно
Console.Write("Повторное подтверждение: ");
try
{
    reservation.Confirm(adminId);
}
catch (ReservationNotPendingException ex)
{
    Console.WriteLine($"Ошибка — {ex.Message}");
}

//перенос брони
var newStart = start.AddDays(1);
var newEnd = newStart.AddHours(2);
service.TransferReservation(reservation, table, workingHours, newStart, newEnd, newGuestsCount: 2);
Console.WriteLine($"\nБронь перенесена: {reservation.StartTime:dd.MM HH:mm} – {reservation.EndTime:HH:mm}, гостей: {reservation.GuestsCount}, статус: {reservation.Status}");

//отмена
reservation.Cancel();
Console.WriteLine($"\nБронь отменена. Статус: {reservation.Status}");

//попытка отменить повторно
Console.Write("повторная отмена: ");
try
{
    reservation.Cancel();
}
catch (ReservationAlreadyCancelledException ex)
{
    Console.WriteLine($"Ошибка — {ex.Message}");
}

Console.WriteLine("\nготово");
