using System;
using MailKit.Net.Smtp;
using MimeKit;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        public void EnviarEmail(string[] destinatarios, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(destinatarios, assunto, usuarioId, code);

            // Conversão para mensagem de email
            var mensagemDeEmail = CriarCorpoDoEmail(mensagem);

            // Realiza o envio
            Enviar(mensagemDeEmail);
        }

        private MimeMessage CriarCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();

            // Remetente
            mensagemDeEmail.From.Add(new MailboxAddress("ADICIONAR REMETENTE"));
            // Destinatario
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            // Assunto
            mensagemDeEmail.Subject = mensagem.Assunto;
            // Corpo
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text) {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            // Utilizando o client de SMTP
            using(var client = new SmtpClient())
            {
                // Tenta conectar o cliente no servidor de email
                try
                {
                    client.Connect("Conexão a fazer");
                    // TODO Auth de e-mail
                    client.Send(mensagemDeEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    // Disconecta do servidor de email
                    client.Disconnect(true);
                    // Libera os recursos do cliente (memória)
                    client.Dispose();
                }
            }
        }
    }
}