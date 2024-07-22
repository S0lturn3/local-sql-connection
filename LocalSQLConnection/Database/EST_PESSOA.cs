using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalSQLConnection.Database
{
    [DataContract]
    public class EstPessoaRecord
    {
        [DataMember]
        public string ID_PESSOA { get; set; }

        [DataMember]
        public long TENANT_ID { get; set; }

        [DataMember]
        public string TX_NOMEPESSOA { get; set; }

        [DataMember]
        public string TIPOPESSOA_CD { get; set; }

        [DataMember]
        public string PAPEL_CD { get; set; }

        [DataMember]
        public string TX_DOCUMENTO { get; set; }

        [DataMember]
        public DateTime? DT_NASCIMENTO { get; set; }

        [DataMember]
        public DateTime? DT_FUNDACAO { get; set; }

        [DataMember]
        public DateTime DT_INICIOVINCULO { get; set; }

        [DataMember]
        public DateTime DT_CRIACAO { get; set; }

        [DataMember]
        public DateTime DT_ULTIMAALTERACAO { get; set; }

        [DataMember]
        public bool IS_ACTIVE { get; set; }

        public EstPessoaRecord()
        {
            ID_PESSOA = string.Empty;
            TENANT_ID = 0;
            TX_NOMEPESSOA = string.Empty;
            TIPOPESSOA_CD = string.Empty;
            PAPEL_CD = string.Empty;
            TX_DOCUMENTO = string.Empty;
            DT_NASCIMENTO = null;
            DT_FUNDACAO = null;
            DT_INICIOVINCULO = DateTime.Now;
            DT_CRIACAO = DateTime.Now;
            DT_ULTIMAALTERACAO = DateTime.Now;
            IS_ACTIVE = false;
        }
    }


    public class EstPessoa
    {

        #region Private Properties

        private SqlConnection _dbConn;

        #endregion Private Properties


        #region Constructor

        public EstPessoa(string connectionString)
        {
            this._dbConn = new SqlConnection(connectionString);
        }

        #endregion Constructor


        #region Public Methods

        public List<EstPessoaRecord> Get(string id)
        {
            List<EstPessoaRecord> recordsList = new List<EstPessoaRecord>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand($@"
                    SELECT      *
                    FROM        EST_PESSOA
                    
                ", _dbConn);

                _dbConn.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        EstPessoaRecord pessoa = new EstPessoaRecord
                        {
                            ID_PESSOA = sqlDataReader["ID_PESSOA"].ToString(),
                            TENANT_ID = Convert.ToInt64(sqlDataReader["TENANT_ID"]),
                            TX_NOMEPESSOA = sqlDataReader["TX_NOMEPESSOA"].ToString(),
                            TIPOPESSOA_CD = sqlDataReader["TIPOPESSOA_CD"].ToString(),
                            PAPEL_CD = sqlDataReader["PAPEL_CD"].ToString(),
                            TX_DOCUMENTO = sqlDataReader["TX_DOCUMENTO"].ToString(),
                            DT_NASCIMENTO = sqlDataReader["DT_NASCIMENTO"] as DateTime?,
                            DT_FUNDACAO = sqlDataReader["DT_FUNDACAO"] as DateTime?,
                            DT_INICIOVINCULO = Convert.ToDateTime(sqlDataReader["DT_INICIOVINCULO"]),
                            DT_CRIACAO = Convert.ToDateTime(sqlDataReader["DT_CRIACAO"]),
                            DT_ULTIMAALTERACAO = Convert.ToDateTime(sqlDataReader["DT_ULTIMAALTERACAO"]),
                            IS_ACTIVE = Convert.ToBoolean(sqlDataReader["IS_ACTIVE"])
                        };

                        recordsList.Add(pessoa);
                    }

                    return recordsList;
                }

                _dbConn.Close();
                return recordsList;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods

    }

}
