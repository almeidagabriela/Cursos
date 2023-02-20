using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        // Utilizado para consulta de informações presentes no appsettings
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
            mensagemDeEmail.From.Add(new MailboxAddress(string.Empty, _configuration.GetValue<string>("EmailSettings:From")));
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
                    // EmailSettings está configurado em appsettings.json
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        _configuration.GetValue<int>("EmailSettings:Port"), true);
                    
                    // Definindo o mecanismo de autenticação
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                        _configuration.GetValue<string>("EmailSettings:Password"));

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