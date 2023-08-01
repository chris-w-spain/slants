using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Slants.Services;

namespace Slants.Core.Shared
{
    public partial class CurrentUserSlantsList
    {
        [Inject] private ISlantsService _slantService { get; set; } = default!;
        private readonly IList<Slant> _slants = new List<Slant>();

        public async Task RefreshDataAsync()
        {
            await _getAndSetDataAsync();
            this.StateHasChanged();
        }

        protected async override Task OnInitializedAsync()
        {
            await _getAndSetDataAsync();
        }

        private async Task _getAndSetDataAsync()
        {
            _slants.Clear();

            // getting all slants
            var slants = await _slantService.GetSlantsAsync();
            foreach (var slant in slants.OrderByDescending(s => s.Created))
            {
                _slants.Add(slant);
            }
        }
    }
}