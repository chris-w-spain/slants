using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Slants.Core.Pages;
using Slants.Core.Shared;
using Slants.Services;

namespace Slants.Core.Shared
{
    public partial class CreateSlantInlineForm
    {
        [Inject]
        private ISlantsService _slantsService { get; set; } = null!;
        private string _slantText = "";
        private List<string> _topics = new List<string>();

        [Parameter]
        public bool Opened { get; set; } = false;

        [Parameter]
        public EventCallback OnSlantCreated { get; set; }

        // create delegate for the button click
        private void AddSlant()
        {
            Opened = !Opened;
        }

        private async Task CreateSlantAsync()
        {
            // create CreateSlantRequest object first
            var request = new CreateSlantRequest
            {
                Text = _slantText,
                Topics = _topics
            };

            var response = await _slantsService.CreateSlantAsync(request);
            if(!response?.Success ?? false)
                throw new Exception("Slant creation failed");

            // invoke OnSlantCreated event
            if(OnSlantCreated.HasDelegate)
                await OnSlantCreated.InvokeAsync();

            Opened = false;
            StateHasChanged();
        }
    }
}