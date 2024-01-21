namespace ticketier_webapi.Core.Dtos
{
    public class UpdateTicketDto
    {
        public DateTime Time { get; set; }

        public string PassengerName { get; set; }

        public long PassengerSSN { get; set; }

        public int Price { get; set; }
    }
}
