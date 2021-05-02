using Paradigma.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Paradigma.App.Business
{
	public class ArvoreHierarquica
	{
		private readonly Regex _padraoValidacao;
		private readonly Regex _padraoLimpeza;
		public string[,] TabelaDeArray { get; set; }
		public bool EhValido { get; set; }

		public ArvoreHierarquica(Regex padraoValidadao, Regex padraoLimpeza)
		{
			_padraoValidacao = padraoValidadao;
			_padraoLimpeza = padraoLimpeza;
		}

		public ErroDeHierarquia Validar(string textoDigitado)
		{
			EhValido = false;
			if (_padraoValidacao.IsMatch(textoDigitado))
			{
				var Valores = _padraoLimpeza.Split(textoDigitado);
				string separador = ",";
				int UltimoIndice = Valores.Length;
				TabelaDeArray = new string[UltimoIndice, 2];
				UltimoIndice -= 1;
				for (int i = 0; i <= UltimoIndice; i++)
				{
					if (i == 0)
					{
						Valores[i] = Valores[i].Replace("[", "");
					}
					if (i == UltimoIndice)
					{
						Valores[i] = Valores[i].Replace("]", "");
					}
					var Pares = Valores[i].Split(separador.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					TabelaDeArray[i, 0] = Pares[0];
					TabelaDeArray[i, 1] = Pares[1];
				}
				if (VerificarSeNaTabelaDeArrayContemMaisDeDoisFilhos(TabelaDeArray))
				{
					return new ErroDeHierarquia() { Id = "E1", Erro = "Mais de 2 filhos" };
				}
				if (VerificarSeNaTabelaDeArrayContemCiclo(TabelaDeArray))
				{
					return new ErroDeHierarquia() { Id = "E2", Erro = "Clico presente" };
				}
				if (VerificarSeNaTabelaDeArrayContemRaizesMultiplas(TabelaDeArray))
				{
					return new ErroDeHierarquia() { Id = "E3", Erro = "Raízes múltiplas" };
				}
			}
			else
			{
				return new ErroDeHierarquia() { Id = "E4", Erro = "Padrão irregular" };
			}
			EhValido = true;
			return null;
		}

		private static bool VerificarSeNaTabelaDeArrayContemRaizesMultiplas(string[,] tabelaDeArray)
		{
			int UltimoIndice = (tabelaDeArray.Length / 2) - 1;
			for (int p1 = 0; p1 <= UltimoIndice; p1++)
			{
				var filho = tabelaDeArray[p1, 1];
				for (int p2 = 0; p2 <= UltimoIndice; p2++)
				{
					if (p1 == p2)
					{
						continue;
					}
					if (tabelaDeArray[p2, 1] == filho)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool VerificarSeNaTabelaDeArrayContemCiclo(string[,] tabelaDeArray)
		{
			int UltimoIndice = (tabelaDeArray.Length / 2) - 1;
			for (int p1 = 0; p1 <= UltimoIndice; p1++)
			{
				var pai = tabelaDeArray[p1, 0];
				var filho = tabelaDeArray[p1, 1];
				for (int p2 = 0; p2 <= UltimoIndice; p2++)
				{
					if (p1 == p2)
					{
						continue;
					}
					if (pai == tabelaDeArray[p2, 1] && tabelaDeArray[p2, 0] == filho)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool VerificarSeNaTabelaDeArrayContemMaisDeDoisFilhos(string[,] tabelaDeArray)
		{
			int UltimoIndice = (tabelaDeArray.Length / 2) - 1;
			for (int p1 = 0; p1 <= UltimoIndice; p1++)
			{
				var pai = tabelaDeArray[p1, 0];
				int QtdFilhos = 1;
				for (int p2 = 0; p2 <= UltimoIndice; p2++)
				{
					if (p1 == p2)
					{
						continue;
					}
					if (pai == tabelaDeArray[p2, 0])
					{
						QtdFilhos += 1;
					}
					if (QtdFilhos > 2)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void Exibir()
		{
			if (EhValido)
			{
				int UltimoIndice = (TabelaDeArray.Length / 2) - 1;
				// Identificar o maior pai
				string maiorPai = string.Empty;
				for (int i = 0; i <= UltimoIndice; i++)
				{
					if (i == 0)
					{
						maiorPai = TabelaDeArray[i, 0];
					}
					else
					{
						var proximo = TabelaDeArray[i, 0];
						if (string.CompareOrdinal(maiorPai, proximo) > 0)
						{
							maiorPai = proximo;
						}
					}
				}
				// Imprime pai
				Console.WriteLine(string.Concat(maiorPai, "["));
				// Busca filhos em recursividade
				IList<string> filhos = RetornarFilhosOrdenados(UltimoIndice, maiorPai);
				PercorrerFilhosOrdenadosEIdenficarNetos(UltimoIndice, filhos, true);

				Console.WriteLine("]");
			}
		}

		private void PercorrerFilhosOrdenadosEIdenficarNetos(int UltimoIndice, IList<string> filhos, bool pularLinha = false)
		{
			// Percorrer filhos ordenados e identificar netos.
			foreach (var filho in filhos)
			{
				string colchete = (pularLinha) ? " [" : "[";
				// Imprimir filhos ordenados
				Console.Write(string.Concat(colchete, filho));
				// Busca netos e se existir chama o método em recursividade
				var netos = RetornarFilhosOrdenados(UltimoIndice, filho);
				if (netos.Count > 0)
				{
					PercorrerFilhosOrdenadosEIdenficarNetos(UltimoIndice, netos);
				}
				if (pularLinha)
				{
					Console.WriteLine("]");
				}
				else
				{
					Console.Write("]");
				}
			}
		}

		private IList<string> RetornarFilhosOrdenados(int UltimoIndice, string pai)
		{
			//Identificar os filhos
			IList<string> filhos = new List<string>();
			for (int i = 0; i <= UltimoIndice; i++)
			{
				if (pai == TabelaDeArray[i, 0])
				{
					filhos.Add(TabelaDeArray[i, 1]);
				}
			}
			//Ordernar os filhos
			return filhos.OrderBy(q => q).ToList(); ;
		}
	}
}
