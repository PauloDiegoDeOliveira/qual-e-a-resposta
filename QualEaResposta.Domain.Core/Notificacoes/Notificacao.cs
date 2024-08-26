namespace QualEaResposta.Domain.Notificacoes
{
    public class Notificacao(string mensagem, ETipoNotificacao tipo = ETipoNotificacao.Erro)
    {
        public string Mensagem { get; } = mensagem;
        public ETipoNotificacao Tipo { get; } = tipo;
    }
}