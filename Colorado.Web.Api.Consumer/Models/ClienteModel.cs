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

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime DataCadastro { get; set; }

	}
}
