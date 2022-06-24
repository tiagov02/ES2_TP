namespace ES2_TP.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string telefone { get; set; }
        public string mail { get; set; }
        public Talento? Talento { get; set; }
        public Guid? IdTalento { get; set; }
    }
}
