using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace produtos
{
    public class Connection
    {
        public static void conectar(DataGridView dgv)
        {
            string connectionString = "Data Source=Localhost;Initial Catalog=Master;User ID=sa;Password=Carioca123";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                            select [PRODU].Codigo,[PRODU].descricao,[BIONEXO_ESTOQUE].ESTOQUE 
                            from [DMD].[DBO].[PRODU] 
                            inner join [DMD].[DBO].BIONEXO_ESTOQUE on PRODU.Codigo = BIONEXO_ESTOQUE.CODPRODUTO";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigo = reader.GetInt32(0);
                            string descricao = reader.GetString(1);
                            int qtd_estoque = reader.GetInt32(2);

                            dgv.Rows.Add(codigo, descricao, qtd_estoque);

                        }
                    }
                }
                connection.Close();
            }
        }


        public static void Query(string codigo, DataGridView dgv)
        {
            string connectionString = "Data Source=Localhost;Initial Catalog=Master;User ID=sa;Password=Carioca123";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $@"
                            select [PRODU].Codigo,[PRODU].descricao,[BIONEXO_ESTOQUE].ESTOQUE 
                            from [DMD].[DBO].[PRODU] 
                            inner join [DMD].[DBO].BIONEXO_ESTOQUE on PRODU.Codigo = BIONEXO_ESTOQUE.CODPRODUTO
                            where codigo = {codigo}
                            ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int codigo_produto = reader.GetInt32(0);
                                string descricao = reader.GetString(1);
                                int qtd_estoque = reader.GetInt32(2);

                                dgv.Rows.Add(codigo_produto, descricao, qtd_estoque);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Ops! parece que algo deu errado: " + err.Message);
                    }
                }
                connection.Close();
            }
        }


        public static void Comprar(int codigo, int qtd_itens)
        {
            string connectionString = "Data Source=Localhost;Initial Catalog=Master;User ID=sa;Password=Carioca123";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sql = $@"
                            update [DMD].[DBO].BIONEXO_ESTOQUE
                            set ESTOQUE = ESTOQUE + {qtd_itens}
                            where CODPRODUTO = {codigo}
                            ";

                if (qtd_itens <= 0)
                {
                    MessageBox.Show($"você não pode comprar {qtd_itens} itens!");
                }
                else
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"Compra autorizada!");
                        MessageBox.Show($"{qtd_itens} itens foram adicionados ao estoque");
                    }
                    connection.Close();
                }
            }
        }


        public static void Vender(int codigo, int qtd_itens)
        {
            string connectionString = "Data Source=Localhost;Initial Catalog=Master;User ID=sa;Password=Carioca123";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sql = $@"
                            update [DMD].[DBO].BIONEXO_ESTOQUE
                            set ESTOQUE = ESTOQUE - {qtd_itens}
                            where CODPRODUTO = {codigo}
                            ";

                if (qtd_itens <= 0)
                {
                    MessageBox.Show($"Sintaxe incorreta: você não pode vender {qtd_itens} itens!");
                }
                else
                {

                    DialogResult result = MessageBox.Show(@$"Deseja autorizar a venda de {qtd_itens} itens?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            MessageBox.Show($"Venda autorizada!");
                            MessageBox.Show($"{qtd_itens} itens foram retirados do estoque");
                        }
                        connection.Close();
                    }
                }
            }
        }

        
        public static void Consulta(string descricao,DataGridView dgv)
        {
            string connectionString = "Data Source=Localhost;Initial Catalog=Master;User ID=sa;Password=Carioca123";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $@"
                            select [PRODU].Codigo,[PRODU].descricao,[BIONEXO_ESTOQUE].ESTOQUE 
                            from [DMD].[DBO].[PRODU] 
                            inner join [DMD].[DBO].BIONEXO_ESTOQUE on PRODU.Codigo = BIONEXO_ESTOQUE.CODPRODUTO
                            where Descricao like '%{descricao}%'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigo = reader.GetInt32(0);
                            string produtoName = reader.GetString(1);
                            int qtd_estoque = reader.GetInt32(2);

                            dgv.Rows.Add(codigo,produtoName, qtd_estoque);

                        }
                    }
                }
                connection.Close();
            }
        }

    }
}

