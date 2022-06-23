﻿namespace ES2_TP.Models
{
    public class Talento
    { 
        public Guid Id { get; set; }
        public float precoHora { get; set; }
        public float horasExperiencia { get; set; }
        public string nome { get; set; }
        public string pais { get; set; }
        public string email { get; set; }
        public Categoria Categoria { get; set; }
    }
}
