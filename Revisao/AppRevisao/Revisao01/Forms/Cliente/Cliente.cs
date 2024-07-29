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

namespace AppRevisao.Revisao01.Forms
{
    public partial class frmCadastroCliente : Form
    {
        public frmCadastroCliente()
        {
            InitializeComponent();
        }

        private void Cadastrar(object sender, EventArgs e)
        {
            if (txtNome.TextLength > 3 && txtUsuario.TextLength > 3 && txtSenha.TextLength > 5)
            {
                ClienteData clienteData = new ClienteData();
                ClienteModel clienteModel = new ClienteModel();

                clienteModel.Usuario = txtUsuario.Text;
                clienteModel.Name = txtNome.Text;
                clienteModel.Senha = txtSenha.Text;

                clienteData.CadastroCliente(clienteModel);

                txtCodigo.Clear();//limpa o campo
                txtNome.Clear();
                txtSenha.Clear();
                txtUsuario.Clear();
                txtNome.Focus();//coloca o textbox para uso receber informações digitadas
                btnBuscarCliente_Click(sender,e);

                MessageBox.Show("Cadastro efetuado com sucesso!!!", "..::Cadastro::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Atenção ao inserir as informações!" + 
                    "\nMínimo três caracteres para Nome, Usuário e seis caracteres para Senha. " + "Muito Obrigado!!!",
                    "..::Atenção::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void VisualizarSenha(object sender, EventArgs e)
        {
            if(btnVisualizar.Text == "Visualizar")
            {
                btnVisualizar.Text = "Esconder";
                txtSenha.PasswordChar = '\0';
            }
            else
            {
                btnVisualizar.Text = "Visualizar";
                txtSenha.PasswordChar= '*';
            }
        }

        private void config(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ClienteData clienteData = new ClienteData();

            List<ClienteModel> clientes;
            if (btn.Name == "btnCadastrar")
            {
                clientes = clienteData.BuscarClientes(1);
            }
            else 
            {
                clientes = clienteData.BuscarClientes(0);
            }

            if (clientes != null)
            {
                dgvClientes.DataSource = clientes;
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
            dgvClientes.Dock = DockStyle.Fill;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.AllowUserToResizeColumns = false;
            dgvClientes.AllowUserToResizeRows = false;
            dgvClientes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //CORES
            dgvClientes.RowsDefaultCellStyle.BackColor = Color.DarkGreen; //plano de fundo padrão
            dgvClientes.RowsDefaultCellStyle.ForeColor = Color.White; //cor da fonte padrão
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.White; //plano de fundo alternando
            dgvClientes.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkGreen; //cor da fonte alternado alternando
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
            dgvClientes.RowsDefaultCellStyle.SelectionBackColor = Color.YellowGreen; //plano de fundo linha selecionada
            dgvClientes.RowsDefaultCellStyle.SelectionForeColor = Color.Red; //cor da fonte linha
            dgvClientes.BackgroundColor = Color.LightGray;
            dgvClientes.BorderStyle = BorderStyle.Fixed3D;
            dgvClientes.MultiSelect = false;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize; //tamanho automatico da largura cabeçalho
            dgvClientes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders; //tamanho automatico largura cabeçalho
            dgvClientes.AllowUserToOrderColumns = true;
        }

        private void SelecionaCliente(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvClientes.SelectedCells[1].Value.ToString();
            txtUsuario.Text = dgvClientes.SelectedCells[2].Value.ToString();
            txtSenha.Text = dgvClientes.SelectedCells[3].Value.ToString();

        }

        private void AlterarCliente(object sender, EventArgs e)
        {
            if (txtNome.TextLength > 3 && txtUsuario.TextLength > 3 && txtSenha.TextLength > 5)
            {
                ClienteData clienteData = new ClienteData();
                ClienteModel clienteModel = new ClienteModel();
                clienteModel.Id = int.Parse(txtCodigo.Text);
                clienteModel.Usuario = txtUsuario.Text;
                clienteModel.Name = txtNome.Text;
                clienteModel.Senha = txtSenha.Text;

                clienteData.AlterarCliente(clienteModel);

                txtCodigo.Clear();//limpa o campo
                txtNome.Clear();
                txtSenha.Clear();
                txtUsuario.Clear();
                txtNome.Focus();//coloca o textbox para uso receber informações digitadas
                btnBuscarCliente_Click(sender, e);

                MessageBox.Show("Alteração efetuada com sucesso!!!", "..::Alteração::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Atenção ao inserir as informações!" +
                    "\nMínimo três caracteres para Nome, Usuário e seis caracteres para Senha. " + "Muito Obrigado!!!",
                    "..::Atenção::..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            ClienteData clienteData = new ClienteData();
            ClienteModel clienteModel = new ClienteModel();
            clienteModel.Id = int.Parse(txtCodigo.Text);
            clienteModel.Usuario = txtUsuario.Text;
            clienteModel.Name = txtNome.Text;
            clienteModel.Senha = txtSenha.Text;
           
            clienteData.DeletarCliente(clienteModel);

            txtCodigo.Clear();//limpa o campo
            txtUsuario.Clear();
            txtNome.Clear();
            txtSenha.Clear();
            txtNome.Focus();//coloca o textbox para uso receber informações digitadas
            btnBuscarCliente_Click(sender, e);

            MessageBox.Show("Produto excluido!!!", "..::Delete::..", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}