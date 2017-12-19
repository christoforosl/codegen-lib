using org.model.lib.db;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace org.model.lib.db.ora {

    public class OracleDBUtils : DBUtils {

        protected override IDbConnection ConnectionInternal {
            get {
                return new OracleConnection(this.ConnString.Trim());
            }
        }

        public override IDbDataAdapter getAdapter() {
            return new OracleDataAdapter();
        }

        public override IDbCommand getCommand() {
            IDbCommand ret = new OracleCommand();
            return ret;
        }

        public override string getIdentitySQL() {
            return null;
        }

        public override IDataParameter getParameter() {
            return new OracleParameter();
        }

        protected override void setSpecialChars() {
            base.p_dbNow = "sysdate";
            base.p_datePattern = "'{0}'";
            base.p_likeChar = "%";
            base.p_leftQuoteChar = "\"";
            base.p_rightQuoteChar = "\"";
            base.paramPrefix = ":";
        }
    }

}