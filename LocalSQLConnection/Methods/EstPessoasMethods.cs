using LocalSQLConnection.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSQLConnection.Methods
{
    public class EstPessoasMethods
    {

        #region Private Properties

        public SqlConnection _dbConn;

        #endregion Private Properties


        #region Constructor

        public EstPessoasMethods(string connectionString)
        {
            this._dbConn = new SqlConnection(connectionString);
        }

        #endregion Constructor


        #region Public Methods

        public List<EstPessoaRecord> GetPessoa(string id = "")
        {
            try
            {
                EstPessoa estPessoaDb = new EstPessoa(_dbConn.ConnectionString);
                return estPessoaDb.Get(id);
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Ocorreu um erro de SQL. Mensagem a seguir: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods


        #region Private Methods

        #endregion Private Methods

    }
}
