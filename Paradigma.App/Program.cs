using System;
using System.Text.RegularExpressions;
using Paradigma.App.Business;

namespace Paradigma.App
{
	class Program
	{
		static void Main(string[] args)
		{
			var PadraoValidacao = new Regex(@"^(\[[A-Z]\,[A-Z]\]\s?)+$");
			var PadraoLimpeza = new Regex(@"\]\s?\[");
			var arvoreHierarquica = new ArvoreHierarquica(PadraoValidacao, PadraoLimpeza);

			Console.Write("Informe um Array de entrada: ");
			string TextoDigitado = Console.ReadLine();
			//string TextoDigitado = "[A,B] [A,C] [B,G] [C,H] [E,F] [B,D] [C,E]";
			//string TextoDigitado = "[B,D] [D,E] [A,B] [C,F] [E,G] [A,C]";
			//string TextoDigitado = "[A,B] [A,C] [B,D] [D,C]";

			if (ArrayEhValido(TextoDigitado, arvoreHierarquica))
			{
				ExibirArvoreHierarquica(arvoreHierarquica);
			}
			Console.Write("Pressione a tecla entrar para sair... ");
			while (Console.ReadKey().Key != ConsoleKey.Enter) {}
		}

		private static void ExibirArvoreHierarquica(ArvoreHierarquica arvoreHierarquica)
		{
			arvoreHierarquica.Exibir();
		}

		private static bool ArrayEhValido(string textoDigitado, ArvoreHierarquica arvoreHierarquica)
		{
			var erroDeArray = arvoreHierarquica.Validar(textoDigitado);
			if (erroDeArray != null)
			{
				Console.WriteLine(string.Format("Erro Código: {0} Mensagem: {1} no array informado.", erroDeArray.Id, erroDeArray.Erro));
			}
			return (erroDeArray == null);
		}
	}
}
