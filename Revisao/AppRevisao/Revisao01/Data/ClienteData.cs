using AppRevisao.Revisao01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace AppRevisao.Revisao01.Data
{
    public class ClienteData
    {
        //Models
        ClienteModel clienteModel;
        //objs sqlClient
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        //strings
        string strCom;
        string strConn;

        public ClienteData()
        {
            this.strConn = ConfigurationManager.ConnectionStrings["connRevisao"].ConnectionString.ToString();
        }
    public bool CadastroCliente(ClienteModel clienteModel) 
        {
            bool bRet = true;
            strCom = string.Format("INSERT INTO CLIENTEA VALUES('{0}', '{1}', '{2}')",
            clienteModel.Name, clienteModel.Usuario, clienteModel.Senha);

            //strCom = string.Format("INSERT INTO CLIENTEB VALUES((SELECT CASE WHEN COUNT(Id) = 0 THEN (SELECT(1))ELSE (SELECT MAX(Id) + 1 FROM CLIENTEB) END FROM CLIENTEB),'{0}', '{1}', '{2}')",clienteModel.Name, clienteModel.Usuario, clienteModel.Senha);
           
            //carregar objs sqlClient
            sqlConnection = new SqlConnection(strConn);
            sqlCommand = new SqlCommand(strCom,sqlConnection);
            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlConnection = null;

            return bRet;
        }

        public List<ClienteModel> BuscarClientes(int TipoBusca)
        {
            if (TipoBusca == 0) //tipo de busca 0 retorna todos os campos da tabela clienteA
            {
                strCom = $"SELECT * FROM CLIENTEA";
            }
            else if (TipoBusca == 1)
            {
                strCom = $"SELECT * FROM CLIENTEA ORDER BY Id DESC";
            }

            sqlConnection = new SqlConnection(strConn);
            sqlCommand = new SqlCommand(strCom,sqlConnection);
            
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<ClienteModel> clienteModels = new List<ClienteModel>();

            if (sqlDataReader.HasRows)
            {
                
                while (sqlDataReader.Read())
                {
                    clienteModel = new ClienteModel();
                    clienteModel.Id = sqlDataReader.GetInt32(0);
                    clienteModel.Name = sqlDataReader.GetString(1);
                    clienteModel.Usuario = sqlDataReader.GetString(2);
                    clienteModel.Senha = sqlDataReader.GetString(3);
                    clienteModels.Add(clienteModel);
                }
               
            }
            return clienteModels;

        }

        internal bool AlterarCliente(ClienteModel clienteModel)
        {
            bool bRet = false;

            try
            {
                strCom = string.Format("UPDATE CLIENTEA SET " +
                        "Nome='{0}', Usuario='{1}', " +
                        "Senha='{2}' WHERE Id={3}",
                clienteModel.Name,
                clienteModel.Usuario,
                clienteModel.Senha,
                clienteModel.Id);

                sqlConnection = new SqlConnection(strConn);
                sqlCommand = new SqlCommand(strCom,sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                bRet = true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                sqlConnection = null;
            }
            return bRet;
        }
        public bool DeletarCliente(ClienteModel clienteModel)
        {
            bool bRet = false;

            strCom = string.Format("Delete Cliente  WHERE Id={4}",
                clienteModel.Usuario,
                clienteModel.Name,
                clienteModel.Senha,
                clienteModel.Id);

            sqlConnection = new SqlConnection(strConn);
            sqlCommand = new SqlCommand(strCom, sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            bRet = true;

            sqlConnection.Close();
            sqlCommand.Dispose();
            sqlConnection = null;

            return bRet;


        }
    }
}
