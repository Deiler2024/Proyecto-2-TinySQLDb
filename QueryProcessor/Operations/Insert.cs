using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using StoreDataManager;



namespace QueryProcessor.Operations
{
    public class Insert
    {
        public OperationStatus Execute(string command)
        {
            try
            {
                // Obtener el nombre de la tabla y la parte de valores
                // Asumimos que el comando tiene el formato "INSERT INTO <nombre_tabla> VALUES (valores)"
                var tableName = command.Substring(command.IndexOf("INTO") + 5, command.IndexOf("VALUES") - command.IndexOf("INTO") - 6).Trim();

                var valuesPart = command.Substring(command.IndexOf("VALUES") + 6).Trim(); // Obtiene la parte de valores
                var values = valuesPart.Trim('(', ')').Split(',').Select(v => v.Trim()).ToArray(); // Separa los valores y elimina espacios

                // Lógica para insertar en la base de datos
                // Aquí asumimos que Store tiene un método InsertIntoTable que maneja la inserción
                Store.GetInstance().InsertIntoTable(tableName, values); // Cambiado para usar tableName

                return OperationStatus.Success; // Indica que la operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar: {ex.Message}");
                return OperationStatus.Error; // Indica un error
            }
        }
    }
}


