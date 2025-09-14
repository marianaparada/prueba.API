using FxResources.System.ComponentModel.DataAnnotations;
namespace prueba.API.Model;

public class Meals
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Entry_date { get; set; } = null!;
    public string Food_name { get; set; } = null!;
    public int Calories { get; set; } = null!;
    public int Protein { get; set; } = null!;
    public int Carbs { get; set; } = null!;
    public int Fat { get; set; } = null!;
}
    