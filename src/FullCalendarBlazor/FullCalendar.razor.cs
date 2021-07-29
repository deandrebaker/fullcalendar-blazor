using System;
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
        // Injected Dependencies
        [Inject] private IJSRuntimeService JsInterop { get; set; }

        // Parameters
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
        [Parameter] public int? EventDragMinDistance { get; set; }
        [Parameter] public int? DragRevertDuration { get; set; }
        [Parameter] public bool? DragScroll { get; set; }
        [Parameter] public TimeSpan? SnapDuration  { get; set; }
        [Parameter] public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent parameter (https://fullcalendar.io/docs/fixedMirrorParent)
        [Parameter] public Func<Event, Event, bool> OnEventOverlap { get; set; }
        [Parameter] public object EventConstraint { get; set; }
        [Parameter] public Func<EventDropInfo, Event, bool> OnEventAllow { get; set; }
        [Parameter] public Func<object, bool> OnDropAccept { get; set; }

        // JSInvokable methods
        [JSInvokable] public void EventClick(EventActionArgs e) => OnEventClick?.Invoke(e);
        [JSInvokable] public void EventMouseEnter(EventActionArgs e) => OnEventMouseEnter?.Invoke(e);
        [JSInvokable] public void EventMouseLeave(EventActionArgs e) =>  OnEventMouseLeave?.Invoke(e);
        [JSInvokable] public bool EventOverlap(Event stillEvent, Event movingEvent) => OnEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool EventAllow(EventDropInfo dropInfo, Event draggedEvent) => OnEventAllow?.Invoke(dropInfo, draggedEvent) ?? true;
        [JSInvokable] public bool DropAccept(object draggableItem) => OnDropAccept?.Invoke(draggableItem) ?? true; // Todo: Replace object with DraggableItem type.

        // Lifecycle methods
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
                EventDragMinDistance = EventDragMinDistance,
                DragRevertDuration = DragRevertDuration,
                DragScroll = DragScroll,
                SnapDuration = SnapDuration,
                AllDayMaintainDuration = AllDayMaintainDuration,
                EventConstraint = EventConstraint,
            };
            await JsInterop.Render(Id, data, DotNetObjectReference.Create(this));
        }
    }
}