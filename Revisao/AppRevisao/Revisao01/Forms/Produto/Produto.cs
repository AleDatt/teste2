using AppRevisao.Revisao01.Data;
using AppRevisao.Revisao01.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AppRevisao.Revisao01.Forms.Produto
{
    public partial class frmProduto : Form
    {
        private object dgvProdutos;

        public frmProduto()
        {
            InitializeComponent();
        }

        private void BuscarProduto(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ProdutoData produtoData = new ProdutoData();

            List<ProdutoModel> produtos;
            if (btn.Name == "btnCadastrar")
            {
                produtos = produtoData.BuscarProdutos(1);
            }
            else
            {
                produtos = produtoData.BuscarProdutos(0);
            }

            if (produtos != null)
            {
                dgvProduto.DataSource = produtos;
                FormataGrid();
            }
            else
            {
                MessageBox.Show("Busca sem retorno", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public void FormataGrid()
        {
            //FORMATA GRID
            //INICIANDO DATAGRID
            dgvProduto.Dock = DockStyle.Fill;
            dgvProduto.ReadOnly = true;
            dgvProduto.AllowUserToAddRows = false;
            dgvProduto.RowHeadersVisible = false;
            dgvProduto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduto.AllowUserToResizeColumns = false;
            dgvProduto.AllowUserToResizeRows = false;
            dgvProduto.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvProduto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //CORES
            dgvProduto.RowsDefaultCellStyle.BackColor = Color.DarkGreen; //plano de fundo padrão
            dgvProduto.RowsDefaultCellStyle.ForeColor = Color.White; //cor da fonte padrão
            dgvProduto.AlternatingRowsDefaultCellStyle.BackColor = Color.White; //plano de fundo alternando
            dgvProduto.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkGreen; //cor da fonte alternado alternando
            dgvProduto.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvProduto.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvProduto.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
            dgvProduto.RowsDefaultCellStyle.SelectionBackColor = Color.YellowGreen; //plano de fundo linha selecionada
            dgvProduto.RowsDefaultCellStyle.SelectionForeColor = Color.Red; //cor da fonte linha
            dgvProduto.BackgroundColor = Color.LightGray;
            dgvProduto.BorderStyle = BorderStyle.Fixed3D;
            dgvProduto.MultiSelect = false;
            dgvProduto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize; //tamanho automatico da largura cabeçalho
            dgvProduto.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders; //tamanho automatico largura cabeçalho
            dgvProduto.AllowUserToOrderColumns = true;
        }

        private void SelecionaProduto(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgvProduto.CurrentRow.Cells[0].Value.ToString();
            txtProduto.Text = dgvProduto.SelectedCells[1].Value.ToString();
            txtMarca.Text = dgvProduto.SelectedCells[2].Value.ToString();
            txtQuantidade.Text = dgvProduto.SelectedCells[3].Value.ToString();
            txtCodigoBarra.Text = dgvProduto.SelectedCells[4].Value.ToString();
        }


        private void CadastrarProduto(object sender, EventArgs e)
        {
            if (txtProduto.TextLength > 0 && txtMarca.TextLength > 0 && txtQuantidade.TextLength > 0 && txtCodigoBarra.TextLength > 0)
            {
                ProdutoData produtoData = new ProdutoData();
                ProdutoModel produtoModel = new ProdutoModel();

                produtoModel.Produto = txtProduto.Text;
                produtoModel.Marca = txtMarca.Text;
                produtoModel.Quantidade = int.Parse(txtQuantidade.Text);
                produtoModel.CodigoBarra = txtCodigoBarra.Text;
                produtoData.CadastroProduto(produtoModel);

                txtProduto.Clear();//limpa o campo
                txtMarca.Clear();
                txtQuantidade.Clear();
                txtCodigoBarra.Clear();
                txtProduto.Focus();//coloca o textbox para uso receber informações digitadas
                BuscarProduto(sender, e);

                MessageBox.Show("Cadastro efetuado com sucesso!!!", "..::Cadastro::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Atenção ao inserir as informações!" +
                    "\nMínimo três caracteres para Nome, Usuário e seis caracteres para Senha. " + "Muito Obrigado!!!",
                    "..::Atenção::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void AlterarProduto(object sender, EventArgs e)
        {
            if (txtProduto.TextLength > 0 && txtMarca.TextLength > 0 && txtQuantidade.TextLength > 0 && txtCodigoBarra.TextLength > 0)
            {
                ProdutoData produtoData = new ProdutoData();
                ProdutoModel produtoModel = new ProdutoModel();
                produtoModel.Id = int.Parse(txtCodigo.Text);
                produtoModel.Produto = txtProduto.Text;
                produtoModel.Marca = txtMarca.Text;
                produtoModel.Quantidade = int.Parse(txtQuantidade.Text);
                produtoModel.CodigoBarra = txtCodigoBarra.Text;

               produtoData.AlterarProduto(produtoModel);

                txtCodigo.Clear();//limpa o campo
                txtProduto.Clear();
                txtMarca.Clear();
                txtQuantidade.Clear();
                txtCodigoBarra.Clear();
                txtProduto.Focus();//coloca o textbox para uso receber informações digitadas
                BuscarProduto(sender, e);

                MessageBox.Show("Alteração efetuada com sucesso!!!", "..::Alteração::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Atenção ao inserir as informações!" +
                    "\nMínimo três caracteres para Nome, Usuário e seis caracteres para Senha. " + "Muito Obrigado!!!",
                    "..::Atenção::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void DeletarProduto(object sender, EventArgs e)
        {
            ProdutoData produtoData = new ProdutoData();
            ProdutoModel produtoModel = new ProdutoModel();
            produtoModel.Id = int.Parse(txtCodigo.Text);
            produtoModel.Produto = txtProduto.Text;
            produtoModel.Marca = txtMarca.Text;
            produtoModel.Quantidade = int.Parse(txtQuantidade.Text);
            produtoModel.CodigoBarra = txtCodigoBarra.Text;

            produtoData.DeletarProduto(produtoModel);

            txtCodigo.Clear();//limpa o campo
            txtProduto.Clear();
            txtMarca.Clear();
            txtQuantidade.Clear();
            txtCodigoBarra.Clear();
            txtProduto.Focus();//coloca o textbox para uso receber informações digitadas
            BuscarProduto(sender, e);

            MessageBox.Show("Produto excluido!!!", "..::Delete::..", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Inicializar(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;  
        }
    }
}
