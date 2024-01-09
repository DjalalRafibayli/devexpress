namespace DevexpressDemoVue.Service
{
    public interface IService
    {
        string Base64File(string path);
        string ConvertStr(object obj);
        string ReplaceSpesific(string str);
        DateTime ConvertStrToDateTime(string date);
    }
}
