namespace SulpakTest.Services
{
    //For QuickGrid
    public class FootballWorldCupData
    {
        public string Country { get; set; }
        public int Place { get; set; }
    }

    public class FootballWorldCupDataService
    {
        public IEnumerable<FootballWorldCupData> GetData()
        {
            return new List<FootballWorldCupData>
            {
                new FootballWorldCupData { Country = "Argentina", Place = 1},
                new FootballWorldCupData { Country = "France", Place = 2},
            };
        }
    }
}
