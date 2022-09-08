namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoEmAndamento,
        LeilaoFinalizado,
        LeilaoAntesDoPregao
    }
    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        private IModalidadeAvaliacao _avaliador;
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
        public Leilao(string peca, IModalidadeAvaliacao avaliador)

        {
            Peca = peca;
            _avaliador = avaliador;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;

        }
        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (Estado == EstadoLeilao.LeilaoEmAndamento)
            {
                if (cliente != _ultimoCliente)
                {
                    _lances.Add(new Lance(cliente, valor));
                    _ultimoCliente = cliente;
                }
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new System.InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método IniciaPregao().");
            }
            Ganhador= _avaliador.Avalia(this);
           
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
