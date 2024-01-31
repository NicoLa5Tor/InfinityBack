namespace BackInfinity.Models.DTO
{
    public class AppointmentDTO
    {
        public string IdAppointment { get; set; }

        public string? NameUs { get; set; }

        public string? Tel { get; set; }

        public string? HorAppoint { get; set; }

        public string HorCreate { get; set; }

        public string? EstatePay { get; set; } = null!;

        public string? IdService { get; set; }
        public string? NameService { get; set;}
    }
}
