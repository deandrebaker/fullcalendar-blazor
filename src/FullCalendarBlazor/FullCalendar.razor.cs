﻿using System;
using System.Collections;
using System.Collections.Generic;
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
        [Parameter] public Action<EventAddInfo> OnEventAdd { get; set; }
        [Parameter] public Action<EventChangeInfo> OnEventChange { get; set; }
        [Parameter] public Action<EventAddInfo> OnEventRemove { get; set; }
        [Parameter] public Action<IEnumerable<Event>> OnEventsSet { get; set; }
        [Parameter] public bool? Editable { get; set; }
        [Parameter] public bool? EventStartEditable { get; set; }
        [Parameter] public bool? EventResizableFromStart { get; set; }
        [Parameter] public bool? EventDurationEditable { get; set; }
        [Parameter] public bool? EventResourceEditable { get; set; }
        [Parameter] public bool? Droppable { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventClick { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseEnter { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseLeave { get; set; }
        [Parameter] public int? EventDragMinDistance { get; set; }
        [Parameter] public int? DragRevertDuration { get; set; }
        [Parameter] public bool? DragScroll { get; set; }
        [Parameter] public TimeSpan? SnapDuration { get; set; }
        [Parameter] public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent parameter (https://fullcalendar.io/docs/fixedMirrorParent)
        [Parameter] public Func<Event, Event, bool> OnEventOverlap { get; set; }
        [Parameter] public object EventConstraint { get; set; }
        [Parameter] public Func<EventAllowInfo, Event, bool> OnEventAllow { get; set; }
        [Parameter] public Func<object, bool> OnDropAccept { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventDragStart { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventDragStop { get; set; }
        [Parameter] public Action<EventDropInfo> OnEventDrop { get; set; }
        [Parameter] public Action<DropInfo> OnDrop { get; set; }
        [Parameter] public Action<ExternalEventDropInfo> OnEventReceive { get; set; }
        [Parameter] public Action<ExternalEventDropInfo> OnEventLeave { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventResizeStart { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventResizeStop { get; set; }
        [Parameter] public Action<EventResizeInfo> OnEventResize { get; set; }

        // JSInvokable methods
        [JSInvokable] public void EventAdd(EventAddInfo eventAddInfo) => OnEventAdd?.Invoke(eventAddInfo);
        [JSInvokable] public void EventRemove(EventChangeInfo eventChangeInfo) => OnEventChange?.Invoke(eventChangeInfo);
        [JSInvokable] public void EventChange(EventAddInfo eventRemoveInfo) => OnEventRemove?.Invoke(eventRemoveInfo);
        [JSInvokable] public void EventsSet(IEnumerable<Event> events) => OnEventsSet?.Invoke(events);
        [JSInvokable] public void EventClick(EventClickInfo eventClickInfo) => OnEventClick?.Invoke(eventClickInfo);
        [JSInvokable] public void EventMouseEnter(EventClickInfo mouseEnterInfo) => OnEventMouseEnter?.Invoke(mouseEnterInfo);
        [JSInvokable] public void EventMouseLeave(EventClickInfo mouseLeaveInfo) => OnEventMouseLeave?.Invoke(mouseLeaveInfo);
        [JSInvokable] public bool EventOverlap(Event stillEvent, Event movingEvent) => OnEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool EventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent) => OnEventAllow?.Invoke(eventAllowInfo, draggedEvent) ?? true;
        [JSInvokable] public bool DropAccept(object draggableItem) => OnDropAccept?.Invoke(draggableItem) ?? true; // Todo: Replace object with DraggableItem type.
        [JSInvokable] public void EventDragStart(EventDragInfo eventDragInfo) => OnEventDragStart?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDragStop(EventDragInfo eventDragInfo) => OnEventDragStop?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDrop(EventDropInfo eventDropInfo) => OnEventDrop?.Invoke(eventDropInfo);
        [JSInvokable] public void Drop(DropInfo dropInfo) => OnDrop?.Invoke(dropInfo);
        [JSInvokable] public void EventReceive(ExternalEventDropInfo eventReceiveInfo) => OnEventReceive?.Invoke(eventReceiveInfo);
        [JSInvokable] public void EventLeave(ExternalEventDropInfo eventLeaveInfo) => OnEventLeave?.Invoke(eventLeaveInfo);
        [JSInvokable] public void EventResizeStart(EventDragInfo eventResizeInfo) => OnEventResizeStart?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResizeStop(EventDragInfo eventResizeInfo) => OnEventResizeStop?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResizeStop(EventResizeInfo eventResizeInfo) => OnEventResize?.Invoke(eventResizeInfo);

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
                EventConstraint = EventConstraint
            };
            await JsInterop.Render(Id, data, DotNetObjectReference.Create(this));
        }
    }
}