using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; //This if just for MYSQL Server
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	MySqlConnection connection = new MySqlConnection("DATA SOURCE=127.0.0.1;UID=root;PWD=;DATABASE=livraria;SSL Mode=none;");
	MySqlCommand command;
	MySqlDataReader reader; //Para a leitura dos dados

	List<Service> dados = new List<Service>();
	public int Id { get; set; }
	public string Titulo { get; set; }
	public string Autor { get; set; }
	public string Categoria { get; set; } 
	public decimal Preco { get; set; }

	# region Mostrando todos os Livros
    public IEnumerable<Service> GetAll()
	{
		try
		{
			connection.Open();
			command = new MySqlCommand("select * from getlivros", connection);
			reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					dados.Add(new Service { Id = int.Parse(reader.GetString(0)), Titulo = reader.GetString(1).ToString(), Autor = reader.GetString(2), Categoria = reader.GetString(3), Preco = decimal.Parse(reader.GetString(4)) });
				}
				return dados;
			}
			return dados;
		}
		catch (Exception error)
		{
			connection.Close();
			throw error;
		}
	}
    #endregion
    #region Monstrando apenas um Livro
	public Service GetOne(int id)
    {
        try
        {
			connection.Open();
			command = new MySqlCommand("Select * from getlivros where id_livro=" + id, connection);
			reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					dados.Add(new Service { Id = int.Parse(reader.GetString(0)), Titulo = reader.GetString(1).ToString(), Autor = reader.GetString(2), Categoria = reader.GetString(3), Preco = decimal.Parse(reader.GetString(4)) });
				}
				return dados[0];
			}
			return new Service{};
			
        }
        catch (Exception)
        {
			connection.Close();
			throw;
        }
       
    }
    #endregion
    #region Inserindo um Livro
	public string Inserir(Service livro)
    {
        try
        {
			connection.Open();
			command = new MySqlCommand("CALL `Inserir`('" + livro.Titulo + "','" + livro.Autor + "','" + livro.Categoria + "','" + livro.Preco + "');",connection);
			command.ExecuteNonQuery();
			return "'" + livro.Titulo + "' registrado banco de dados.";
        }
        catch (Exception)
        {
			connection.Close();
			return "Não foi possível inserir o Livro '" + livro.Titulo + "'";
        }
		
    }
    #endregion
    #region Actualizando um Livro
	public string Update(int id,Service livro)
    {
        try
        {
			connection.Open();
			command = new MySqlCommand("CALL `Update`("+id+",'" + livro.Titulo + "','" + livro.Autor + "','" + livro.Categoria + "','" + livro.Preco + "');",connection);
			command.ExecuteNonQuery();
			return "O Livro '" + livro.Titulo + "' foi actualizado com sucesso."; 
        }
        catch (Exception)
        {
			connection.Close();
			return "Não foi possível actualizar";
        }
    }
    #endregion
    #region Deletando um Livro
	public string Delete(int id)
    {
		try
		{
			connection.Open();
			command = new MySqlCommand("CALL `Delete`("+id+");",connection);
			command.ExecuteReader();
			return "O Livro já não faz parte da Base de dados";
		}
		catch (Exception)
		{
			connection.Close();
			throw;
		}
	}
    #endregion

    #region Listando as categorias
	public IEnumerable<Categorias_> GetCategorias()
    {
        try
        {
			connection.Open();
			command = new MySqlCommand("select * from categoria", connection);
			reader = command.ExecuteReader();
			List<Categorias_> categorias = new List<Categorias_>();

			while (reader.Read())
            {
				categorias.Add(new Categorias_ { ID_categoria = int.Parse(reader.GetString(0)), Categoria = reader.GetString(1) });
            }
			return categorias;
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

}
public class Categorias_
{
	public int ID_categoria { get; set; }
	public string Categoria { get; set; }
}
