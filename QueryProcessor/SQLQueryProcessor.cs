using Entities;
using QueryProcessor.Exceptions;
using QueryProcessor.Operations;
using StoreDataManager;

namespace QueryProcessor
{
    public class SQLQueryProcessor
    {
        public static OperationStatus Execute(string sentence)
        {
            if (sentence.StartsWith("CREATE TABLE"))
            {
                return new CreateTable().Execute();
            }
            if (sentence.StartsWith("SELECT"))
            {
                // Crear una instancia de Select y ejecutar con la sentencia
                return new Select().Execute(sentence);
            }
            if (sentence.StartsWith("INSERT INTO"))
            {
                // Llama a la clase Insert para manejar el comando, pasando la sentencia completa
                return new Insert().Execute(sentence);
            }
            else
            {
                throw new UnknownSQLSentenceException();
            }
        }
    }
}


