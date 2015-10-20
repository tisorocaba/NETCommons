using Sorocaba.Commons.Foundation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Comdata.Utils {
    public static class CertidaoNascimento {

        public static void Validar(string numeroCertidao, bool verificarDigitoServentia = true) {

            // Verifica o formato no número.
            if (string.IsNullOrWhiteSpace(numeroCertidao) || !Regex.IsMatch(numeroCertidao, @"^\d{32}$")) {
                throw new BusinessException("Formato de número de certidão incorreto.");
            }

            // Converte o número em um array de inteiros.
            int[] digitosCertidao = numeroCertidao.ToCharArray().Select(c => Int32.Parse(c.ToString())).ToArray();

            if (verificarDigitoServentia) {
                // Verifica o dígito da serventia.
                int digitoServentia = CalcularDigitoServentia(digitosCertidao);
                if (digitosCertidao[5] != digitoServentia) {
                    throw new BusinessException("Dígito da serventia incorreto.");
                }
            }

            // Verifica o tipo de serviço.
            if (digitosCertidao[8] != 5 || digitosCertidao[9] != 5) {
                throw new BusinessException("Tipo de serviço incorreto.");
            }

            // Verifica o ano.
            int ano = Int32.Parse(
                digitosCertidao[10].ToString() +
                digitosCertidao[11].ToString() +
                digitosCertidao[12].ToString() +
                digitosCertidao[13].ToString()
            );
            if (ano < 1900 || ano > DateTime.Now.Year) {
                throw new BusinessException("Ano incorreto.");
            }

            // Verifica o tipo de livro.
            if (digitosCertidao[14] != 1 && digitosCertidao[14] != 7) {
                throw new BusinessException("Tipo de livro incorreto.");
            }

            // Verifica os dígitos da certidão.
            int[] digitosVerificadores = CalcularDigitoCertidao(digitosCertidao);
            if (digitosVerificadores[0] != digitosCertidao[30] || digitosVerificadores[1] != digitosCertidao[31]) {
                throw new BusinessException("Dígitos verificadores incorretos.");
            }
        }

        private static int CalcularDigitoServentia(int[] numeroServentia) {

            int digitoServentia = 0;
            int digitoAtual = 0;

            digitoAtual = numeroServentia[0] * 2;
            digitoAtual = (digitoAtual <= 9) ? digitoAtual : digitoAtual - 9;
            digitoServentia += digitoAtual;

            digitoAtual = numeroServentia[1] * 1;
            digitoAtual = (digitoAtual <= 9) ? digitoAtual : digitoAtual - 9;
            digitoServentia += digitoAtual;

            digitoAtual = numeroServentia[2] * 2;
            digitoAtual = (digitoAtual <= 9) ? digitoAtual : digitoAtual - 9;
            digitoServentia += digitoAtual;

            digitoAtual = numeroServentia[3] * 1;
            digitoAtual = (digitoAtual <= 9) ? digitoAtual : digitoAtual - 9;
            digitoServentia += digitoAtual;

            digitoAtual = numeroServentia[4] * 2;
            digitoAtual = (digitoAtual <= 9) ? digitoAtual : digitoAtual - 9;
            digitoServentia += digitoAtual;

            digitoServentia = 10 - (digitoServentia % 10);
            digitoServentia = (digitoServentia != 10) ? digitoServentia : 0;

            return digitoServentia;
        }

        private static int[] CalcularDigitoCertidao(int[] numeroCertidao) {

            int verificador1 = 0;
            int[] matrizVerificador1 = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 30; i++) {
                verificador1 += numeroCertidao[i] * matrizVerificador1[i];
            }

            verificador1 = verificador1 % 11;
            verificador1 = (verificador1 == 10) ? 1 : verificador1;

            int verificador2 = 0;
            int[] matrizVerificador2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 30; i++) {
                verificador2 += numeroCertidao[i] * matrizVerificador2[i];
            }
            verificador2 += verificador1 * matrizVerificador2[30];

            verificador2 = verificador2 % 11;
            verificador2 = (verificador2 == 10) ? 1 : verificador2;

            return new int[] { verificador1, verificador2 };
        }

        public static string GerarNumeroCertidao(Random random = null) {

            if (random == null) {
                random = new Random((int) DateTime.Now.Ticks);
            }

            int[] novoNumero = new int[32];

            // Gera o número da serventia.
            GerarDigitosAleatorios(5, random).CopyTo(novoNumero, 0);
            novoNumero[5] = CalcularDigitoServentia(novoNumero);

            // Gera o acervo da serventia.
            GerarDigitosAleatorios(2, random).CopyTo(novoNumero, 6);

            // Preenche o tipo de serviço prestado (fixo para certidões de nascimento).
            novoNumero[8] = 5;
            novoNumero[9] = 5;

            // Gera o ano da certidão.
            GerarAnoAleatorio(random).CopyTo(novoNumero, 10);

            // Preenche o tipo do livro (fixo para certidões de nascimento).
            novoNumero[14] = 1;

            // Gera o livro, folha e termo.
            GerarDigitosAleatorios(5, random).CopyTo(novoNumero, 15);
            GerarDigitosAleatorios(3, random).CopyTo(novoNumero, 20);
            GerarDigitosAleatorios(7, random).CopyTo(novoNumero, 23);

            // Gera os dígitos verificadores.
            int[] verificadores = CalcularDigitoCertidao(novoNumero);
            novoNumero[30] = verificadores[0];
            novoNumero[31] = verificadores[1];

            return new String(novoNumero.Select(d => d.ToString()[0]).ToArray());
        }

        private static int[] GerarDigitosAleatorios(int quantidade, Random random = null) {

            if (random == null) {
                random = new Random((int) DateTime.Now.Ticks);
            }

            string mask = "";
            string max = "";

            for (int i = 0; i < quantidade; i++) {
                mask += "0";
                max += "9";
            }

            string number = String.Format("{0:" + mask + "}", random.Next(1, Int32.Parse(max) + 1));
            return number.ToCharArray().Select(c => Int32.Parse(c.ToString())).ToArray();
        }

        private static int[] GerarAnoAleatorio(Random random = null) {

            if (random == null) {
                random = new Random((int) DateTime.Now.Ticks);
            }

            string ano = String.Format("{0:0000}", random.Next(1900, DateTime.Now.Year + 1));
            return ano.ToCharArray().Select(a => Int32.Parse(a.ToString())).ToArray();
        }
    }
}
