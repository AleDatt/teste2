using AppRevisao.Revisao01.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRevisao.Revisao01.Data
{
    public class ProdutoData
    {
        ProdutoModel produtoModel;
        //objs sqlClient
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        //strings
        string strCom;
        string strConn;

        public ProdutoData()
        {
            this.strConn = ConfigurationManager.ConnectionStrings["connProdutos"].ConnectionString.ToString();
        }
        public bool CadastroProduto(ProdutoModel ProdutoModel)
        {
            bool bRet = true;
            strCom = string.Format("INSERT INTO Produto VALUES('{0}', '{1}', '{2}', '{3}')",
                                    ProdutoModel.Produto, ProdutoModel.Marca, ProdutoModel.Quantidade, ProdutoModel.CodigoBarra);
            //carregar objs sqlClient
            sqlConnection = new SqlConnection(strConn);
            sqlCommand = new SqlCommand(strCom, sqlConnection);
            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            sqlConnection.Dispose();
            sqlConnection = null;

            return bRet;
        }

        public List<ProdutoModel> BuscarProdutos(int TipoBusca = 0)
        {
            {
                if (TipoBusca == 0) //tipo de busca 0 retorna todos os campos da tabela clienteA
                {
                    strCom = $"SELECT * FROM Produto";
                }

                else if (TipoBusca == 1)
                {
                    strCom = $"SELECT * FROM Produto ORDER BY Id DESC";
                }

                sqlConnection = new SqlConnection(strConn);
                sqlCommand = new SqlCommand(strCom, sqlConnection);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<ProdutoModel> produtoModels = new List<ProdutoModel>();

                if (sqlDataReader.HasRows)
                {

                    while (sqlDataReader.Read())
                    {
                        produtoModel = new ProdutoModel();
                        produtoModel.Id = sqlDataReader.GetInt32(0);
                        produtoModel.Produto = sqlDataReader.GetString(1);
                        produtoModel.Marca = sqlDataReader.GetString(2);
                        produtoModel.Quantidade = sqlDataReader.GetInt32(3);
                        produtoModel.CodigoBarra = sqlDataReader.GetString(4);
                        produtoModels.Add(produtoModel);
                    }

                }
                return produtoModels;

            }
        }

        public bool AlterarProduto(ProdutoModel produtoModel)
        {
            bool bRet = false;

            try
            {
                strCom = string.Format("UPDATE Produto SET " +
                        "Produto='{1}', Marca='{2}', " +
                        "Quantidade='{3}', CodigoBarra = {4}  WHERE Id={0}",
                produtoModel.Id,
                produtoModel.Produto,
                produtoModel.Marca,
                produtoModel.Quantidade,
                produtoModel.CodigoBarra
                );

                sqlConnection = new SqlConnection(strConn);
                sqlCommand = new SqlCommand(strCom, sqlConnection);
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

       public bool DeletarProduto(ProdutoModel produtoModel)
        {
            bool bRet = false;

            strCom = string.Format("Delete Produto  WHERE Id={4}",
                produtoModel.Produto,
                produtoModel.Marca,
                produtoModel.Quantidade,
                produtoModel.CodigoBarra,
                produtoModel.Id);

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
