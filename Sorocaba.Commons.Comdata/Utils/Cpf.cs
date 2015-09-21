using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Comdata.Utils {
    public static class Cpf {

        public static bool Validar(decimal cpf) {
            return Validar(string.Format("{0:00000000000}", cpf));
        }

        public static bool Validar(string cpf) {

            string _cpf = "";
            for (int i = 0; i < cpf.Length; i++) {
                if ("0123456789".Contains(cpf[i])) {
                    _cpf = _cpf + cpf[i];
                }
            }
            cpf = _cpf;

            if (cpf.Length != 11) {
                return false;
            }

            bool digitosIguais = true;
            for (int i = 1; i < 11; i++) {
                if (cpf[i] != cpf[0]) {
                    digitosIguais = false;
                    break;
                }
            }

            if (digitosIguais || cpf == "12345678909") {
                return false;
            }

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++) {
                numeros[i] = Int32.Parse(cpf[i].ToString());
            }

            int soma = 0;
            for (int i = 0; i < 9; i++) {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0) {
                if (numeros[9] != 0) {
                    return false;
                }
            } else if (numeros[9] != 11 - resultado) {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++) {
                soma += (11 - i) * numeros[i];
            }

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0) {
                if (numeros[10] != 0) {
                    return false;
                }
            } else if (numeros[10] != 11 - resultado) {
                return false;
            }

            return true;
        }
    }
}
