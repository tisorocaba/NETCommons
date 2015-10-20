using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.EntityModel {
    public static class DbContextExtensions {
        public static void ExecuteWithTransaction(this DbContext context, Action action) {
            using (var tx = context.Database.BeginTransaction()) {
                try {
                    action();
                    tx.Commit();
                } catch {
                    try { tx.Rollback(); } catch { }
                    throw;
                }
            }
        }
    }
}
