using ForApplication.PresentationLogic.Views;

namespace ForApplication;

public class Program
{
    static async Task Main(string[] args)
    {
        ProductView productView = new ProductView();
        await productView.Menu();
    }
}