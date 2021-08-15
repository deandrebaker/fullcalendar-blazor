using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FullCalendarBlazor.Services
{
    public class JSRuntimeService : IJSRuntimeService, IAsyncDisposable
    {
        public JsonSerializerSettings SerializerSettings => new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public JSRuntimeService(IJSRuntime jsRuntime)
        {
            _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/FullCalendarBlazor/fullCalendarJsInterop.js").AsTask());
        }

        public async ValueTask RenderAsync(object calendarData, IEnumerable<(string, string)> calendarMethods, DotNetObjectReference<IJSInvokableService> objRef)
        {
            var serializedData = JsonConvert.SerializeObject(calendarData, Formatting.Indented, SerializerSettings);
            var serializedMethods = JsonConvert.SerializeObject(calendarMethods, Formatting.Indented, SerializerSettings);
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("render", serializedData, serializedMethods, objRef);
        }

        public async ValueTask ExecuteVoidMethodAsync(string elementId, string methodName, params object[] args)
        {
            var serializedArgs = JsonConvert.SerializeObject(args, Formatting.Indented, SerializerSettings);
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("executeMethod", elementId, methodName, serializedArgs);
        }

        public async ValueTask<TValue> ExecuteMethodAsync<TValue>(string elementId, string methodName, params object[] args)
        {
            var serializedArgs = JsonConvert.SerializeObject(args, Formatting.Indented, SerializerSettings);
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<TValue>("executeMethod", elementId, methodName, serializedArgs);
        }

        public async ValueTask ExecuteVoidEventMethodAsync(string elementId, string eventId, string methodName, params object[] args) {
            var serializedArgs = JsonConvert.SerializeObject(args, Formatting.Indented, SerializerSettings);
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("executeEventMethod", elementId, eventId, methodName, serializedArgs);
        }

        public async ValueTask<TValue> ExecuteEventMethodAsync<TValue>(string elementId, string eventId, string methodName, params object[] args) {
            var serializedArgs = JsonConvert.SerializeObject(args, Formatting.Indented, SerializerSettings);
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<TValue>("executeEventMethod", elementId, eventId, methodName, serializedArgs);
        }

        public async ValueTask<TValue> GetPropertyAsync<TValue>(string elementId, string propName)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<TValue>("getProperty", elementId, propName);
        }

        public async ValueTask PrintAsync(object obj)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("print", obj);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}