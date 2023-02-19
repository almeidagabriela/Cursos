namespace UsuariosAPI.Models
{
    public class Token
    {
        public Token(string value)
        {
            Value = value;
        }

        // Será iniciado apenas via construtor, portanto não será necessário o "set"
        public string Value { get; }
    }
}