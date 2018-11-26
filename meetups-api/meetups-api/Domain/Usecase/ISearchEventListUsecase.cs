namespace meetupsApi.Domain.Usecase
{
    public interface ISearchEventListUsecase
    {
        void Execute(string searchKeyword = "");
    }
}