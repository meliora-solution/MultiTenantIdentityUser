using Microsoft.JSInterop;


namespace AuthpExample3.Services
{
    public class NavigationService
    {
        private readonly IJSRuntime _jsRuntime;

        public NavigationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task GoBack()
        {
            await _jsRuntime.InvokeVoidAsync("history.back");
        }
    }

}
