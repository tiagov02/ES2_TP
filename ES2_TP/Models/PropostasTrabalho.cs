namespace ES2_TP.Models
{
    public class PropostasTrabalho
    {
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }
        public Guid IdSkill { get; set; }
        public Skills? Skill { get; set; }
        public float AnosExperiencia { get; set; }
        public float TotalHoras { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

    }
}
