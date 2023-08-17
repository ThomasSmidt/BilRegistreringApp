using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace VærkstedBilRegisteringApp
{
    internal class JSON
    {
        public static string _userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private const string _fileName = "køretøj.json";
        private static string _filePath = Path.Combine(_userFolder, _fileName);
        private static List<object> _alleKøretøjer = new List<object>();

        public static void WriteToJsonFile(List<object> alleKøretøjer)
        {
            string updatedData = System.Text.Json.JsonSerializer.Serialize(alleKøretøjer, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, updatedData);
        }
        public static List<object> ReadFromJsonFile()
        {
            if (File.Exists(_filePath))
            {
                string currentData = File.ReadAllText(_filePath);
                List<object> alleKøretøjerTemp = JsonConvert.DeserializeObject<List<object>>(currentData);

                foreach (var data in alleKøretøjerTemp)
                {
                    var jsonObject = JObject.Parse(data.ToString());

                    //Opretter variabler ud fra json filen
                    string mærke = jsonObject.Value<string>("Mærke");
                    string model = jsonObject.Value<string>("Model");
                    string størrelse = jsonObject.Value<string>("Størrelse");
                    string nummerplade = jsonObject.Value<string>("Nummerplade");
                    string årgang = jsonObject.Value<string>("Årgang");
                    DateTime førsteRegistrering = DateTime.Parse(jsonObject.Value<string>("FørsteRegistrering"));
                    DateTime sidsteSynsDato = DateTime.Parse(jsonObject.Value<string>("SidsteSynsDato"));

                    double dblStørrelse;
                    bool isDouble = double.TryParse(størrelse, out dblStørrelse);

                    string fornavn = jsonObject.SelectToken("KundeKontaktInfo.kundensFornavn").ToString();
                    string efternavn = jsonObject.SelectToken("KundeKontaktInfo.kundensEfternavn").ToString();
                    string tlf = jsonObject.SelectToken("KundeKontaktInfo.kundensTlf").ToString();
                    
                    //Opretter et Køretøj objekt af enten <double> eller <string>, afhængig af om "størrelse" kan parses til en double
                    if (isDouble)
                        _alleKøretøjer.Add(new Køretøj<double>(fornavn, efternavn, tlf, mærke, model, dblStørrelse, nummerplade, årgang, førsteRegistrering, sidsteSynsDato));
                    else
                        _alleKøretøjer.Add(new Køretøj<string>(fornavn, efternavn, tlf, mærke, model, størrelse, nummerplade, årgang, førsteRegistrering, sidsteSynsDato));
                }
            }
            return _alleKøretøjer;
        }
    }
}
