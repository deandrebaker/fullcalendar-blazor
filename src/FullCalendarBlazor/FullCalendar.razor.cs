﻿using System;
using System.Collections;
using System.Threading.Tasks;
using FullCalendarBlazor.Models;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        [Inject] private IJSRuntimeService JsInterop { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public IEnumerable Events { get; set; }
        // Todo: Add EventDataTransform delegate for transforming events from a source (https://fullcalendar.io/docs/eventDataTransform)
        [Parameter] public bool? DefaultAllDay { get; set; }
        [Parameter] public TimeSpan? DefaultAllDayEventDuration { get; set; }
        [Parameter] public TimeSpan? DefaultTimedEventDuration { get; set; }
        [Parameter] public bool? ForceEventDuration { get; set; }
        [Parameter] public bool? Editable { get; set; }
        [Parameter] public bool? EventStartEditable { get; set; }
        [Parameter] public bool? EventResizableFromStart { get; set; }
        [Parameter] public bool? EventDurationEditable { get; set; }
        [Parameter] public bool? EventResourceEditable { get; set; }
        [Parameter] public bool? Droppable { get; set; }
        [Parameter] public Action<EventActionArgs> OnEventClick { get; set; }
        [Parameter] public Action<EventActionArgs> OnEventMouseEnter { get; set; }
        [Parameter] public Action<EventActionArgs> OnEventMouseLeave { get; set; }

        [JSInvokable]
        public void EventClick(EventActionArgs e)
        {
            OnEventClick?.Invoke(e);
        }

        [JSInvokable]
        public void EventMouseEnter(EventActionArgs e)
        {
            OnEventMouseEnter?.Invoke(e);
        }

        [JSInvokable]
        public void EventMouseLeave(EventActionArgs e)
        {
            OnEventMouseLeave?.Invoke(e);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var data = new FullCalendarData
            {
                Events = Events, 
                DefaultAllDay = DefaultAllDay,
                DefaultAllDayEventDuration = DefaultAllDayEventDuration,
                DefaultTimedEventDuration = DefaultTimedEventDuration,
                ForceEventDuration = ForceEventDuration,
                Editable = Editable,
                EventStartEditable = EventStartEditable,
                EventResizableFromStart = EventResizableFromStart,
                EventDurationEditable = EventDurationEditable,
                EventResourceEditable = EventResourceEditable,
                Droppable = Droppable,
            };
            await JsInterop.Render(Id, data, DotNetObjectReference.Create(this));
        }
    }
}