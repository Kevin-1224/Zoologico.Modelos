using System.ComponentModel.DataAnnotations;

namespace Zoologico.Modelos
{
    public class Animal
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public int EspecieCodigo { get; set; }
        public int RazaId { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        //navegation 
        public Especie? Especie { get; set; }
        public Raza? Raza { get; set; }
    }
}
