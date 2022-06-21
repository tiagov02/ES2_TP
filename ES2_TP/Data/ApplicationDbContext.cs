﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ES2_TP.Models;

namespace ES2_TP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ES2_TP.Models.Cliente>? Cliente { get; set; }
        public DbSet<ES2_TP.Models.Categoria>? Categoria { get; set; }
        public DbSet<ES2_TP.Models.PropostasTrabalho>? PropostasTrabalho { get; set; }
        public DbSet<ES2_TP.Models.Skills>? Skills { get; set; }
        public DbSet<ES2_TP.Models.Talento>? Talento { get; set; }
        public DbSet<ES2_TP.Models.PropostasTalento>? PropostasTalento { get; set; }
        public DbSet<ES2_TP.Models.SkillsTalento>? SkillsTalento { get; set; }
        public DbSet<ES2_TP.Models.DetalheExperiencia>? DetalheExperiencia { get; set; }
    }
}