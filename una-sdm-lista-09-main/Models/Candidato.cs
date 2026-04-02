namespace EleicaoBrasilApi.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Partido { get; set; } = string.Empty;

        // NOVO CAMPO
        public string ViceNome { get; set; } = string.Empty;
    }
}