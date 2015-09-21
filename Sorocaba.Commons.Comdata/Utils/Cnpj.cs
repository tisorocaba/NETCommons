using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Comdata.Utils {
    public static class Cnpj {

        public static bool Validar(decimal cnpj) {
            return Validar(string.Format("{0:00000000000000}", cnpj));
        }

        public static bool Validar(string cnpj) {

            string _cnpj = "";
            for (int i = 0; i < cnpj.Length; i++) {
                if ("0123456789".Contains(cnpj[i])) {
                    _cnpj = _cnpj + cnpj[i];
                }
            }
            cnpj = _cnpj;

            if (cnpj.Length != 14) {
                return false;
            }

            Boolean digitosIguais = true;
            for (int i = 1; i < cnpj.Length; i++) {
                if (cnpj[i] != cnpj[0]) {
                    digitosIguais = false;
                    break;
                }
            }

            if (digitosIguais) {
                return false;
            }

            int[] numeros = new int[14];
            for (int i = 0; i < cnpj.Length; i++) {
                numeros[i] = Convert.ToInt32(cnpj[i].ToString());
            }

            // Cálculo do 1º dígito verificador.

            int digito1;
            int soma = 0;
            int fator = 5;
            for (int i = 0; i < numeros.Length - 2; i++) {
                soma += fator * numeros[i];
                fator--;
                if (fator < 2)
                    fator = 9;
            }

            int resto = soma % 11;

            if (resto < 2) {
                digito1 = 0;
            } else {
                digito1 = 11 - resto;
            }

            // Cálculo do 2º dígito verificador.

            int digito2;
            soma = 0;
            fator = 6;
            for (int i = 0; i < numeros.Length - 2; i++) {
                soma += fator * numeros[i];
                fator--;
                if (fator < 2)
                    fator = 9;
            }

            resto = (soma + 2 * digito1) % 11;

            if (resto < 2) {
                digito2 = 0;
            } else {
                digito2 = 11 - resto;
            }

            // Verificação final.
            if (numeros[12] != digito1 || numeros[13] != digito2) {
                return false;
            }

            return true;
        }
    }
}
