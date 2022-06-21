namespace ES2_TP.Models
{
    public class PropostasTalento
    {
        public Guid Id { get; set; }
        public Talento? talento { get; set; }
        public PropostasTrabalho? proposta { get; set; }
        public float? tempoEstimado { get; set; }
        public float? valor { get; set; }
    }
}
