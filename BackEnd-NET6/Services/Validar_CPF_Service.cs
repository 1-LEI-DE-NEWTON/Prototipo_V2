using BackEnd_NET6.Services.Interfaces;

namespace BackEnd_NET6.Services
{
    public class Validar_CPF_Service : I_Validar_CPF_Service
    {
        //Valida o CPF usando o 4devs
        public bool Validar_CPF(string cpf)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://www.4devs.com.br/");
                var payload = new Dictionary<string, string>
                {
                    { "acao", "validar_cpf" },
                    { "txt_cpf", cpf }
                };

                var content = new FormUrlEncodedContent(payload);
                var response = client.PostAsync("ferramentas_online.php", content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                if (responseString.Contains("Verdadeiro"))
                {
                    return true;
                }
                else
                {
                    return false;
                }                                
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar CPF: " + e.Message);
            }
        }                    
    }
}