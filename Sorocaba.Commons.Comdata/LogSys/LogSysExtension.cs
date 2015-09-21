using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Comdata.LogSys {
    public static class LogSysExtension {

        public static void GerarEventoAuditoria(this DbContext context, int codigoTipoEventoAuditoria, Func<int> idUsuarioFunc, params ParametroEvento[] parametrosEvento) {

            string siglaSistemaPermisys = ConfigurationManager.AppSettings["Permisys.SiglaSistema"];
            string siglaModuloPermisys = ConfigurationManager.AppSettings["Permisys.SiglaModulo"];

            if (string.IsNullOrWhiteSpace("Permisys.SiglaSistema") || string.IsNullOrWhiteSpace("Permisys.SiglaModulo")) {
                throw new Exception("As configurações \"Permisys.SiglaSistema\" e \"Permisys.SiglaModulo\" são obrigatórias.");
            }

            if (idUsuarioFunc == null) {
                throw new Exception("A função de obtenção do ID do usuário é obrigatória.");
            }

            int idUsuarioPermisys = idUsuarioFunc();

            List<SqlDataRecord> parametros = null;

            if (parametrosEvento != null && parametrosEvento.Count() > 0) {

                parametros = new List<SqlDataRecord>();

                SqlMetaData[] rowMetadata = new SqlMetaData[] {
                    new SqlMetaData("ORDEM", SqlDbType.Int),
                    new SqlMetaData("NOME", SqlDbType.VarChar, 20),
                    new SqlMetaData("VALOR", SqlDbType.VarChar, 100)
                };

                foreach (var parametro in parametrosEvento) {

                    if (string.IsNullOrWhiteSpace(parametro.Nome) || string.IsNullOrWhiteSpace(parametro.Valor)) {
                        throw new Exception("O \"Nome\" e \"Valor\" são obrigatórios para todos os parâmetros.");
                    }

                    SqlDataRecord row = new SqlDataRecord(rowMetadata);
                    row.SetInt32(0, parametro.Ordem);
                    row.SetString(1, parametro.Nome);
                    row.SetString(2, parametro.Valor);
                    parametros.Add(row);
                }
            }

            context.Database.ExecuteSqlCommand("LOGSYS.SP_GERAR_EVENTO_AUDITORIA @SIGLA_SISTEMA_PERMISYS, @SIGLA_MODULO_PERMISYS, @ID_USUARIO_PERMISYS, @CODIGO_TIPO_EVENTO_AUDITORIA, @PARAMETROS_EVENTO",
                new object[] {
                    new SqlParameter("SIGLA_SISTEMA_PERMISYS", SqlDbType.VarChar, 30) { Value = siglaSistemaPermisys },
                    new SqlParameter("SIGLA_MODULO_PERMISYS", SqlDbType.VarChar, 30) { Value = siglaModuloPermisys },
                    new SqlParameter("ID_USUARIO_PERMISYS", SqlDbType.Int) { Value = idUsuarioPermisys },
                    new SqlParameter("CODIGO_TIPO_EVENTO_AUDITORIA", SqlDbType.Int) { Value = codigoTipoEventoAuditoria },
                    new SqlParameter("PARAMETROS_EVENTO", SqlDbType.Structured) { TypeName = "LOGSYS.LOGSYS_LISTA_PARAMETROS", Value = parametros }
                }
            );
        }
    }
}
