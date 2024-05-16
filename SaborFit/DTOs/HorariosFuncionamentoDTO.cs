namespace SaborFit.DTOs
{
    public class HorariosFuncionamentoDTO
    {
        public int ID { get; set; }
        public int IdRestaurante { get; set; }
        public int DiaSemana { get; set; } // 0 (Domingo) a 6 (Sábado)
        public TimeSpan HorarioAbertura { get; set; }
        public TimeSpan HorarioFechamento { get; set; }
    }
}
