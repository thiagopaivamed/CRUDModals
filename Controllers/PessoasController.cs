using System.Threading.Tasks;
using CRUDModals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDModals.Controllers
{
    public class PessoasController : Controller
    {
        private readonly Contexto _contexto;

        public PessoasController(Contexto contexto)
        {
            _contexto=contexto;
        }

        public ActionResult Index ()
        {
            return View();
        }  

        [HttpGet]
        public async Task<JsonResult> PegarTodos(){
            return Json(await _contexto.Pessoas.ToListAsync());
        
        }

        [HttpPost]
        public async Task<JsonResult> NovaPessoa(Pessoa pessoa){
            if(ModelState.IsValid){
                await _contexto.Pessoas.AddAsync(pessoa);
                await _contexto.SaveChangesAsync();
                return Json(pessoa);
            }

            return Json(ModelState);
            
        }

        [HttpGet]
        public async Task<JsonResult> PegarPessoaPeloId(int pessoaId){
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);
            return Json(pessoa);
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarPessoa(Pessoa pessoa){
            if(ModelState.IsValid)
            {
                _contexto.Pessoas.Update(pessoa);
                await _contexto.SaveChangesAsync();
                return Json(pessoa);
            }

            return Json(ModelState);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirPessoa(int pessoaId){
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);

            if(pessoa != null){
                _contexto.Pessoas.Remove(pessoa);
                await _contexto.SaveChangesAsync();
                return Json(true);
            }

            return Json(new {
                mensagem = "Pessoa não encontrada"
            });
        }       
    }
}