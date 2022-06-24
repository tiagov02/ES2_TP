namespace ES2_TP.Models
{
    public class Skills
    {
        public Guid Id { get; set; }
        public Categoria? categoria { get; set; } 
        public string? descricao { get; set; }
        public Guid IdCategoria { get; set; }
    }
}
