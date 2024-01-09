using Microsoft.AspNetCore.Identity;

namespace DevexpressDemoVue.Service
{
    public class Service : IService
    {
        public string Base64File(string path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            String file = Convert.ToBase64String(bytes);

            return file;
        }

        public string ConvertStr(object obj)
        {
            string str = "";
            if (obj == null)
                return str;
            return obj.ToString();
        }
        public DateTime ConvertStrToDateTime(string date)
        {
            string day = date.Substring(0, 2);
            string mount = date.Substring(3, 2);
            string year = date.Substring(6, 4);
            string dateT = year + "-" + mount + "-" + day;

            return Convert.ToDateTime(dateT);
        }
        public string ReplaceSpesific(string str)
        {
            return str.Replace("(mövcud olduğu halda)","")
                .Replace(".","");
        }
    }
}
