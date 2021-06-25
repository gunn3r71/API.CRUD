using System.ComponentModel.DataAnnotations;

namespace API.CRUD.Models.InputModels
{
    public class CreateFornecedorInputModel
    {
        [Required(ErrorMessage = "Nome de fornecedor não deve ser nulo!"), StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Nome de fornecedor não deve ser nulo!"), StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }
        public int TipoFornecedor { get; set; }
        public bool Ativo { get; set; }
    }
}