﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Dto
{
    public class AutorRegistroDto
    {
        [Required(ErrorMessage = "O nome é obrigatório para o registro.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }     
        
    }
}