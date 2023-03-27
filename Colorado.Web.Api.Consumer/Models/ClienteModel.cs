using System;
using System.ComponentModel.DataAnnotations;

namespace Colorado.Web.Api.Consumer.Models
{
	public class ClienteModel
	{
		public int Id { get; set; }

		public string CodigoCliente { get; set; }

		public string Nome { get; set; }

		public string Endereco { get; set; }

		public string Cidade { get; set; }

		public string UF { get; set; }

		public DateTime DataCadastro { get; set; }

	}
}
