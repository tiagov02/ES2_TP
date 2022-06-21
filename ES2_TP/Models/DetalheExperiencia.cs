namespace ES2_TP.Models
{
    public class DetalheExperiencia
    {
        public Guid Id { get; set; }
        public Talento? talento { get; set; }
        public string descricao { get; set; }
        public DateTime? dt_ini { get; set; }
        public DateTime? dt_fim { get; set; }
    }
}
