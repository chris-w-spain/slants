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
    public partial class TopicsSelector
    {
        [Inject]
        private ITopicsService _topicsService { get; set; } = null!;

        // public parameter for selected topics
        [Parameter]
        public List<string> SelectedTopics { get; set; } = new List<string>();

        [Parameter]
        public EventCallback<List<string>> SelectedTopicsChanged { get; set; }

        List<string> _topics = new();
        List<string> _filteredTopics = new List<string>();
        string _inputValue = "";

        protected override async Task OnInitializedAsync()
        {
            _topics = (await _topicsService.GetTopicsAsync()).ToList();
        }

        void OnInputChange(ChangeEventArgs e)
        {
            _filteredTopics.Clear();
            var filter = e.Value?.ToString()?.ToLower() ?? "";
            if (filter.Length > 0)
            {
                var filteredTopics = _topics.Where(t => t.ToLower().Contains(filter));
                _filteredTopics.AddRange(filteredTopics);
            }
        }

        void OnMenuItemKeyboardPress(KeyboardEventArgs e, string topic)
        {
            if (e.Key == "Enter" || e.Key == " ")
            {
                SelectTopic(topic);
            }
        }

        void SelectTopic(string topic)
        {
            if (!SelectedTopics.Contains(topic))
            {
                SelectedTopics.Add(topic);
                _filteredTopics.Clear();
                _inputValue = "";
                if(SelectedTopicsChanged.HasDelegate)
                    SelectedTopicsChanged.InvokeAsync(SelectedTopics);
            }
        }

        void RemoveTopic(string topic)
        {
            if (SelectedTopics.Contains(topic))
            {
                SelectedTopics.Remove(topic);
                if(SelectedTopicsChanged.HasDelegate)
                    SelectedTopicsChanged.InvokeAsync(SelectedTopics);
            }
        }
    }
}