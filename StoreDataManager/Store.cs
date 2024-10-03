using Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace StoreDataManager
{
    public sealed class Store
    {
        private static Store? instance = null;
        private static readonly object _lock = new object();
               
        public static Store GetInstance()
        {
            lock(_lock)
            {
                if (instance == null) 
                {
                    instance = new Store();
                }
                return instance;
            }
        }

        private const string DatabaseBasePath = @"C:\Users\Deiler\Pictures\TinySQLDb-main\TinySQLDb-main\Client";
        private const string DataPath = $@"{DatabaseBasePath}\Tablas";
        private const string SystemCatalogPath = $@"{DataPath}\SystemCatalog";
        private const string SystemDatabasesFile = $@"{SystemCatalogPath}\SystemDatabases.table";
        private const string SystemTablesFile = $@"{SystemCatalogPath}\SystemTables.table";


        public Store()
        {
            this.InitializeSystemCatalog();
            
        }

        private void InitializeSystemCatalog()
        {
            // Always make sure that the system catalog and above folder
            // exist when initializing
            Directory.CreateDirectory(SystemCatalogPath);
        }

        public OperationStatus CreateTable()
        {
            // Creates a default DB called TESTDB
            Directory.CreateDirectory($@"{DataPath}\TESTDB");

            // Creates a default Table called ESTUDIANTES
            var tablePath = $@"{DataPath}\TESTDB\ESTUDIANTES.Table";

            using (FileStream stream = File.Open(tablePath, FileMode.OpenOrCreate))
            using (BinaryWriter writer = new (stream))
            {
                // Create an object with a hardcoded.
                // First field is an int, second field is a string of size 30,
                // third is a string of 50
                int id = 1;
                string nombre = "Isaac".PadRight(30); // Pad to make the size of the string fixed
                string apellido = "Ramirez".PadRight(50);

                writer.Write(id);
                writer.Write(nombre);
                writer.Write(apellido);
            }
            return OperationStatus.Success;
        }



        public OperationStatus InsertIntoTable(string tableName, string[] values)
        {
            var tablePath = $@"{DataPath}\{tableName}.Table";

            // Verifica si el directorio existe, si no, lo crea
            string directoryPath = Path.GetDirectoryName(tablePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Ahora abre el archivo para añadir usando StreamWriter
            using (FileStream stream = File.Open(tablePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                // Escribe los datos en formato CSV
                writer.WriteLine($"{int.Parse(values[0])},{values[1]},{values[2]}");
            }

            return OperationStatus.Success;
        }






        public OperationStatus Select(string tableName)
        {
            var tablePath = $@"{DataPath}\{tableName}.Table"; // Asegúrate de que la ruta sea correcta
            if (!File.Exists(tablePath))
            {
                Console.WriteLine($"La tabla {tableName} no existe.");
                return OperationStatus.Error; // Retorna un error si la tabla no existe
            }

            using (FileStream stream = File.Open(tablePath, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(','); // Divide la línea por comas
                    Console.WriteLine($"ID: {values[0]}, Nombre: {values[1]}, Apellido: {values[2]}");
                }
            }
            return OperationStatus.Success;
        }



    }
}
