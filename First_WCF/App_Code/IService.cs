using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
	[OperationContract]
	IEnumerable<Service> GetAll();

	[OperationContract]
	Service GetOne(int id);

	[OperationContract]
	string Inserir(Service livro);

	[OperationContract]
	string Update(int id,Service livro);

	[OperationContract]
	string Delete(int id);

	[OperationContract]
	IEnumerable<Categorias_> GetCategorias();
	// TODO: Add your service operations here
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.