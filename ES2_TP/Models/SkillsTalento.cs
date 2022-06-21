namespace ES2_TP.Models
{
    public class SkillsTalento
    {
        public Guid Id { get; set; }
        public Talento? talento { get; set; }
        public Skills? skill { get; set; }
        public float? preco { get; set; }
        public float numHoras { get; set; }
    }
}
