using System;
using System.Collections.Generic;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Display;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.Views;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.Services
{
    public class JSInvokableService : IJSInvokableService
    {
        // Properties

        #region Overall Display

        public Action<WindowResizeInfo> OnWindowResize { get; set; }

        #endregion

        #region View

        public Action<AllDayInfo> OnAllDayDidMount { get; set; }
        public Action<AllDayInfo> OnAllDayWillUnmount { get; set; }
        public Action<NoEventsInfo> OnNoEventsDidMount { get; set; } // Todo: Replace object with proper type
        public Action<NoEventsInfo> OnNoEventsWillUnmount { get; set; } // Todo: Replace object with proper type
        public Func<DateTime, DateRange> OnGetVisibleRange { get; set; }
        public Action<ViewInfo> OnViewDidMount { get; set; } // Todo: Replace object with proper type
        public Action<ViewInfo> OnViewWillUnmount { get; set; } // Todo: Replace object with proper type

        #endregion

        #region Date and Time

        public Action<DayHeaderRenderInfo> OnDayHeaderDidMount { get; set; }
        public Action<DayHeaderRenderInfo> OnDayHeaderWillUnmount { get; set; }
        public Action<DayCellRenderInfo> OnDayCellDidMount { get; set; }
        public Action<DayCellRenderInfo> OnDayCellWillUnmount { get; set; }
        public Action<SlotRenderInfo> OnSlotLabelDidMount { get; set; }
        public Action<SlotRenderInfo> OnSlotLabelWillUnmount { get; set; }
        public Action<SlotRenderInfo> OnSlotLaneDidMount { get; set; }
        public Action<SlotRenderInfo> OnSlotLaneWillUnmount { get; set; }
        public Action<DateInfo> OnDatesSet { get; set; }
        public Action<DateTime, object> OnNavLinkDayClick { get; set; }
        public Action<DateTime, object> OnNavLinkWeekClick { get; set; }
        public Func<DateTime, int> OnGetWeekNumber { get; set; }
        public Action<WeekNumberInfo> OnWeekNumberDidMount { get; set; }
        public Action<WeekNumberInfo> OnWeekNumberWillUnmount { get; set; }
        public Func<Event, bool> OnGetSelectOverlap { get; set; }
        public Func<SelectInfo, bool> OnGetSelectAllow { get; set; }
        public Action<DateClickInfo> OnDateClick { get; set; }
        public Action<SelectionInfo> OnSelect { get; set; }
        public Action<object, View> OnUnselect { get; set; } // Todo
        public Func<DateTime> OnGetNow { get; set; }
        public Action<NowIndicatorInfo> OnNowIndicatorDidMount { get; set; }
        public Action<NowIndicatorInfo> OnNowIndicatorWillUnmount { get; set; }

        #endregion

        #region Events

        public Func<object, Event> OnGetEventDataTransform { get; set; }
        public Action<EventAddInfo> OnEventAdd { get; set; }
        public Action<EventChangeInfo> OnEventChange { get; set; }
        public Action<EventAddInfo> OnEventRemove { get; set; }
        public Action<IEnumerable<Event>> OnEventsSet { get; set; }
        public Func<Event, Event, int> OnGetEventOrder { get; set; }
        public Action<EventRenderInfo> OnEventDidMount { get; set; }
        public Action<EventRenderInfo> OnEventWillUnmount { get; set; }
        public Action<EventClickInfo> OnEventClick { get; set; }
        public Action<EventClickInfo> OnEventMouseEnter { get; set; }
        public Action<EventClickInfo> OnEventMouseLeave { get; set; }
        public Func<Event, Event, bool> OnGetEventOverlap { get; set; }
        public Func<EventAllowInfo, Event, bool> OnGetEventAllow { get; set; }
        public Func<object, bool> OnGetDropAccept { get; set; }
        public Action<EventDragInfo> OnEventDragStart { get; set; }
        public Action<EventDragInfo> OnEventDragStop { get; set; }
        public Action<EventDropInfo> OnEventDrop { get; set; }
        public Action<DropInfo> OnDrop { get; set; }
        public Action<ExternalEventDropInfo> OnEventReceive { get; set; }
        public Action<ExternalEventDropInfo> OnEventLeave { get; set; }
        public Action<EventDragInfo> OnEventResizeStart { get; set; }
        public Action<EventDragInfo> OnEventResizeStop { get; set; }
        public Action<EventResizeInfo> OnEventResize { get; set; }
        public Action<MoreLinkClickInfo> OnMoreLinkClick { get; set; }
        public Action<MoreLinkInfo> OnMoreLinkDidMount { get; set; }
        public Action<MoreLinkInfo> OnMoreLinkWillUnmount { get; set; }

        #endregion

        // Methods

        #region Overall Display

        [JSInvokable] public void _WindowResize(WindowResizeInfo windowResizeInfo) => OnWindowResize?.Invoke(windowResizeInfo);

        #endregion

        #region Views

        [JSInvokable] public void _AllDayDidMount(AllDayInfo allDayInfo) => OnAllDayDidMount?.Invoke(allDayInfo);
        [JSInvokable] public void _AllDayWillUnmount(AllDayInfo allDayInfo) => OnAllDayWillUnmount?.Invoke(allDayInfo);
        [JSInvokable] public void _NoEventsDidMount(NoEventsInfo noEventsInfo) => OnNoEventsDidMount?.Invoke(noEventsInfo);
        [JSInvokable] public void _NoEventsWillUnmount(NoEventsInfo noEventsInfo) => OnNoEventsWillUnmount?.Invoke(noEventsInfo);
        [JSInvokable] public DateRange _GetVisibleRange(DateTime currentDate) => OnGetVisibleRange?.Invoke(currentDate);
        [JSInvokable] public void _ViewDidMount(ViewInfo viewInfo) => OnViewDidMount?.Invoke(viewInfo);
        [JSInvokable] public void _ViewWillUnmount(ViewInfo viewInfo) => OnViewWillUnmount?.Invoke(viewInfo);

        #endregion Views

        #region Date and Time

        [JSInvokable] public void _DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderDidMount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void _DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderWillUnmount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void _DayCellDidMount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellDidMount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void _DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellWillUnmount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void _SlotLabelDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLabelDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLabelWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _SlotLaneDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLaneDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLaneWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _DatesSet(DateInfo dateInfo) => OnDatesSet?.Invoke(dateInfo);
        [JSInvokable] public void _NavLinkDayClick(DateTime date, object jsEvent) => OnNavLinkDayClick?.Invoke(date, jsEvent);
        [JSInvokable] public void _NavLinkWeekClick(DateTime weekStart, object jsEvent) => OnNavLinkWeekClick?.Invoke(weekStart, jsEvent);
        [JSInvokable] public int? _GetWeekNumber(DateTime date) => OnGetWeekNumber?.Invoke(date);
        [JSInvokable] public void _WeekNumberDidMount(WeekNumberInfo weekNumberInfo) => OnWeekNumberDidMount?.Invoke(weekNumberInfo);
        [JSInvokable] public void _WeekNumberWillUnmount(WeekNumberInfo weekNumberInfo) => OnWeekNumberWillUnmount?.Invoke(weekNumberInfo);
        [JSInvokable] public bool? _GetSelectOverlap(Event overlappedEvent) => OnGetSelectOverlap?.Invoke(overlappedEvent);
        [JSInvokable] public bool? _GetSelectAllow(SelectInfo selectInfo) => OnGetSelectAllow?.Invoke(selectInfo);
        [JSInvokable] public void _DateClick(DateClickInfo dateClickInfo) => OnDateClick?.Invoke(dateClickInfo);
        [JSInvokable] public void _Select(SelectionInfo selectionInfo) => OnSelect?.Invoke(selectionInfo);
        [JSInvokable] public void _Unselect(object jsEvent, View view) => OnUnselect?.Invoke(jsEvent, view);
        [JSInvokable] public DateTime? _GetNow() => OnGetNow?.Invoke();
        [JSInvokable] public void _NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorDidMount?.Invoke(nowIndicatorInfo);
        [JSInvokable] public void _NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorWillUnmount?.Invoke(nowIndicatorInfo);

        #endregion

        #region Event

        [JSInvokable] public object _GetEventDataTransform(object eventData) => (object) OnGetEventDataTransform?.Invoke(eventData) ?? false;
        [JSInvokable] public void _EventAdd(EventAddInfo eventAddInfo) => OnEventAdd?.Invoke(eventAddInfo);
        [JSInvokable] public void _EventRemove(EventChangeInfo eventChangeInfo) => OnEventChange?.Invoke(eventChangeInfo);
        [JSInvokable] public void _EventChange(EventAddInfo eventRemoveInfo) => OnEventRemove?.Invoke(eventRemoveInfo);
        [JSInvokable] public void _EventsSet(IEnumerable<Event> events) => OnEventsSet?.Invoke(events);
        [JSInvokable] public int? _GetEventOrder(Event eventA, Event eventB) => OnGetEventOrder?.Invoke(eventA, eventB);
        [JSInvokable] public void _EventDidMount(EventRenderInfo eventRenderInfo) => OnEventDidMount?.Invoke(eventRenderInfo);
        [JSInvokable] public void _EventWillUnmount(EventRenderInfo eventRenderInfo) => OnEventWillUnmount?.Invoke(eventRenderInfo);
        [JSInvokable] public void _EventClick(EventClickInfo eventClickInfo) => OnEventClick?.Invoke(eventClickInfo);
        [JSInvokable] public void _EventMouseEnter(EventClickInfo mouseEnterInfo) => OnEventMouseEnter?.Invoke(mouseEnterInfo);
        [JSInvokable] public void _EventMouseLeave(EventClickInfo mouseLeaveInfo) => OnEventMouseLeave?.Invoke(mouseLeaveInfo);
        [JSInvokable] public bool _GetEventOverlap(Event stillEvent, Event movingEvent) => OnGetEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool _GetEventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent) => OnGetEventAllow?.Invoke(eventAllowInfo, draggedEvent) ?? true;
        [JSInvokable] public bool _GetDropAccept(object draggableItem) => OnGetDropAccept?.Invoke(draggableItem) ?? true; // Todo: Replace object with DraggableItem type.
        [JSInvokable] public void _EventDragStart(EventDragInfo eventDragInfo) => OnEventDragStart?.Invoke(eventDragInfo);
        [JSInvokable] public void _EventDragStop(EventDragInfo eventDragInfo) => OnEventDragStop?.Invoke(eventDragInfo);
        [JSInvokable] public void _EventDrop(EventDropInfo eventDropInfo) => OnEventDrop?.Invoke(eventDropInfo);
        [JSInvokable] public void _Drop(DropInfo dropInfo) => OnDrop?.Invoke(dropInfo);
        [JSInvokable] public void _EventReceive(ExternalEventDropInfo eventReceiveInfo) => OnEventReceive?.Invoke(eventReceiveInfo);
        [JSInvokable] public void _EventLeave(ExternalEventDropInfo eventLeaveInfo) => OnEventLeave?.Invoke(eventLeaveInfo);
        [JSInvokable] public void _EventResizeStart(EventDragInfo eventResizeInfo) => OnEventResizeStart?.Invoke(eventResizeInfo);
        [JSInvokable] public void _EventResizeStop(EventDragInfo eventResizeInfo) => OnEventResizeStop?.Invoke(eventResizeInfo);
        [JSInvokable] public void _EventResize(EventResizeInfo eventResizeInfo) => OnEventResize?.Invoke(eventResizeInfo);
        [JSInvokable] public void _MoreLinkClick(MoreLinkClickInfo info) => OnMoreLinkClick?.Invoke(info);
        [JSInvokable] public void _MoreLinkDidMount(MoreLinkInfo moreLinkInfo) => OnMoreLinkDidMount?.Invoke(moreLinkInfo);
        [JSInvokable] public void _MoreLinkWillUnmount(MoreLinkInfo moreLinkInfo) => OnMoreLinkWillUnmount?.Invoke(moreLinkInfo);

        #endregion
    }
}