namespace ES2_TP.Models
{
    public class PropostasTrabalho
    {
        public Guid Id { get; set; }
        public Cliente cliente { get; set; }
        public Categoria categoria { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

    }
}
