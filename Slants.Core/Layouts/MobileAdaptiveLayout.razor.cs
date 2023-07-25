using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Slants.Core.Settings;

namespace Slants.Core.Layouts
{

    public partial class MobileAdaptiveLayout : IAsyncDisposable
    {
        private DotNetObjectReference<MobileAdaptiveLayout>? _objectReference;
        private int _currentWidth = 1920;

        [Inject] public IJSRuntime? JSRuntime { get; set; }
        [CascadingParameter] public MobileAdaptiveLayoutSettings Settings { get; set; } = new MobileAdaptiveLayoutSettings();
        public bool IsMobile { get; private set; } = false;

        public void ToggleMenu()
        {
            this.Settings.ManuallyForceMobile = !IsMobile;
        }

        [JSInvokable]
        public async Task UpdateWindowWidth(int windowWidth)
        {
            _currentWidth = windowWidth;

            if (Settings.ManuallyForceMobile)
                return;


            if (IsMobile && _currentWidth >= Settings.MobileBreakPointWidth)
                _mobileChanged(false);
            else if (!IsMobile && _currentWidth < Settings.MobileBreakPointWidth)
                _mobileChanged(true);

            await InvokeAsync(StateHasChanged);
        }

        protected override void OnInitialized()
        {
            _objectReference = DotNetObjectReference.Create(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await _initWindowWidthListener();
        }

        private void _mobileChanged(bool isMobile)
        {
            IsMobile = isMobile;
        }

        private async Task _initWindowWidthListener()
        {
            if (JSRuntime == null)
                throw new Exception("JSRuntime is required to process component.");

            await JSRuntime.InvokeVoidAsync("AddWindowWidthListener", _objectReference);
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (JSRuntime == null)
                throw new Exception("JSRuntime is required to process component.");

            await JSRuntime.InvokeVoidAsync("RemoveWindowWidthListener", _objectReference);
            _objectReference?.Dispose();

            GC.SuppressFinalize(this);
        }

    }
}