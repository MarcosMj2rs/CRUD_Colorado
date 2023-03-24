using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Colorado.Core.Entities
{
	public  class Cliente
	{
		[Required]
		public int Id { get; set; }

		public string CodigoCliente { get; set; }

		public string Nome { get; set; }

		public string Endereco { get; set; }

		public string Cidade { get; set; }

		[MaxLength(2)]
		public string UF { get; set; }

		public DateTime DataCadastro { get; set; }
	}
}
