using Microsoft.AspNetCore.Mvc;
using ES2_TP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ES2_TP.Data;
using ES2_TP.Models;

namespace ES2_TP.Controllers
{
    public class Relatorio
    {
        public Relatorio()
        {
            precoHorasTalento = new();
            precoHorasTalento2 = new();

        }


        public float? PrecoRH { get; set; }
        public float? PrecoRRH { get; set; }

        public int NumeroHorasMensaisR { get; set; }
        public int NumeroHorasMensaisRR { get; set; }
        public float PrecoMedioMensalR { get; set; }
        public float PrecoMedioMensalRR { get; set; }

        public List<RelPrecoHora> precoHorasTalento { get; set; }
        public List<RelPrecoHora2> precoHorasTalento2 { get; set; }

      
    }
    public class RelPrecoHora
    {
        public string Talento { get; set; }
        public string Pais { get; set; }
        public float PrecoTotal { get; set; }
    }

    public class RelPrecoHora2
    {
        public string Skill { get; set; }
        public float PrecoTotal { get; set; }
    }

    public class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Relatorio()
        {
            Relatorio rel = new();

            var t = await _context.Talento.Include(e=>e.Skill).ToListAsync();
           

            foreach (var item in t)
            {
                if (item.Skill == null)
                {
                    rel.precoHorasTalento.Add(new RelPrecoHora() { Talento = item.Categoria.descricao, Pais = item.pais, PrecoTotal = item.precoHora * 176 });
                }
            }

            return View(rel.precoHorasTalento.ToList());
        }
        public async Task<IActionResult> RelatorioSkill()
        {
            Relatorio rel = new();

            var t = await _context.Talento.Include(e=>e.Skill).ToListAsync();

            foreach (var item in t)
            {
                rel.precoHorasTalento2.Add(new RelPrecoHora2() { Skill = item.Skill.descricao, PrecoTotal = item.precoHora * 176 });
            }
            return View(rel.precoHorasTalento2.ToList());
        }
    }
}
