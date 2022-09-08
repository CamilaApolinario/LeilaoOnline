using Alura.LeilaoOnline.Core;

class Program
{
    static void Main()
    {
        var modalidade = new MaiorValor();
        var leilao = new Leilao("Van Gogh", modalidade);
        var fulano = new Interessada("Fulano", leilao);
        var maria = new Interessada("Maria", leilao);

        leilao.RecebeLance(fulano, 800);
        leilao.RecebeLance(maria, 900);
        leilao.RecebeLance(fulano, 1000);
        leilao.RecebeLance(maria, 990);

        leilao.TerminaPregao();

        Console.WriteLine(leilao.Ganhador.Valor);
    }
}
