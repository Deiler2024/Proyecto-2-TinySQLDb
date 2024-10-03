using Entities;
using StoreDataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace QueryProcessor.Operations
{
    internal class Select
    {
        public OperationStatus Execute(string command)
        {
            try
            {
                // Extraer el nombre de la tabla del comando
                var tableName = command.Substring(command.IndexOf("FROM") + 5).Trim();

                // Lógica para seleccionar en la base de datos
                return Store.GetInstance().Select(tableName); // Llama al método Select en Store
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al seleccionar: {ex.Message}");
                return OperationStatus.Error; // Indica un error
            }
        }
    }
}
