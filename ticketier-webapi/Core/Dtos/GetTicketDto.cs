namespace ticketier_webapi.Core.Dtos
{
    public class GetTicketDto
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }

        public string PassengerName { get; set; }

        public long PassengerSSN { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
