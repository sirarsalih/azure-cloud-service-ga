using System.ComponentModel.DataAnnotations;

namespace WebRole.ViewModels
{
    public class MainViewModel
    {
        [Display(Name = "Number of cities:")]
        public string NumberOfCities { get; set; }
        [Display(Name = "Number of generations:")]
        public string NumberOfGenerations { get; set; }
        [Display(Name = "Number of chromosomes:")]
        public string NumberOfChromosomes { get; set; }
        [Display(Name = "Random or circle plotting:")]
        public string RandomOrCirclePlotting { get; set; }
        [Display(Name = "Mutation method:")]
        public string MutationMethod { get; set; }
        [Display(Name = "Cross over method:")]
        public string CrossOverMethod { get; set; }
        [Display(Name = "Selection method:")]
        public string SelectionMethod { get; set; }
        [Display(Name = "Results:")]
        public string Results { get; set; }
        
        public string SetProcessParamteresFromProperties()
        {
            return NumberOfCities + " " + NumberOfGenerations + " " + NumberOfChromosomes + " " + RandomOrCirclePlotting +
                   " " + MutationMethod + " " + CrossOverMethod + " " + SelectionMethod;
        }
    }
}