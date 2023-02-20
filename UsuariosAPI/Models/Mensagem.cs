using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        // Tipo que identifica um endereço de email
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatarios, string assunto, int usuarioId, string codigo)
        {
            Destinatario = new List<MailboxAddress>();
            
            // Adiciona os destinatarios
            Destinatario.AddRange(destinatarios.Select(destinatario => new MailboxAddress(string.Empty, destinatario)));

            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativar?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}