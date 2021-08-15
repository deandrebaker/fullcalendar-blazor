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
        public Action<NoEventsInfo> OnNoEventsDidMount { get; set; }
        public Action<NoEventsInfo> OnNoEventsWillUnmount { get; set; }
        public Func<DateTime, DateRange> OnGetVisibleRange { get; set; }
        public Action<ViewInfo> OnViewDidMount { get; set; }
        public Action<ViewInfo> OnViewWillUnmount { get; set; }

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
        public Action<object, View> OnUnselect { get; set; }
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

        [JSInvokable] public void WindowResize(WindowResizeInfo windowResizeInfo) => OnWindowResize?.Invoke(windowResizeInfo);

        #endregion

        #region Views

        [JSInvokable] public void AllDayDidMount(AllDayInfo allDayInfo) => OnAllDayDidMount?.Invoke(allDayInfo);
        [JSInvokable] public void AllDayWillUnmount(AllDayInfo allDayInfo) => OnAllDayWillUnmount?.Invoke(allDayInfo);
        [JSInvokable] public void NoEventsDidMount(NoEventsInfo noEventsInfo) => OnNoEventsDidMount?.Invoke(noEventsInfo);
        [JSInvokable] public void NoEventsWillUnmount(NoEventsInfo noEventsInfo) => OnNoEventsWillUnmount?.Invoke(noEventsInfo);
        [JSInvokable] public DateRange GetVisibleRange(DateTime currentDate) => OnGetVisibleRange?.Invoke(currentDate);
        [JSInvokable] public void ViewDidMount(ViewInfo viewInfo) => OnViewDidMount?.Invoke(viewInfo);
        [JSInvokable] public void ViewWillUnmount(ViewInfo viewInfo) => OnViewWillUnmount?.Invoke(viewInfo);

        #endregion Views

        #region Date and Time

        [JSInvokable] public void DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderDidMount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderWillUnmount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void DayCellDidMount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellDidMount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellWillUnmount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void SlotLabelDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLabelDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLabelWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLaneDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLaneDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLaneWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void DatesSet(DateInfo dateInfo) => OnDatesSet?.Invoke(dateInfo);
        [JSInvokable] public void NavLinkDayClick(DateTime date, object jsEvent) => OnNavLinkDayClick?.Invoke(date, jsEvent);
        [JSInvokable] public void NavLinkWeekClick(DateTime weekStart, object jsEvent) => OnNavLinkWeekClick?.Invoke(weekStart, jsEvent);
        [JSInvokable] public int? GetWeekNumber(DateTime date) => OnGetWeekNumber?.Invoke(date);
        [JSInvokable] public void WeekNumberDidMount(WeekNumberInfo weekNumberInfo) => OnWeekNumberDidMount?.Invoke(weekNumberInfo);
        [JSInvokable] public void WeekNumberWillUnmount(WeekNumberInfo weekNumberInfo) => OnWeekNumberWillUnmount?.Invoke(weekNumberInfo);
        [JSInvokable] public bool? GetSelectOverlap(Event overlappedEvent) => OnGetSelectOverlap?.Invoke(overlappedEvent);
        [JSInvokable] public bool? GetSelectAllow(SelectInfo selectInfo) => OnGetSelectAllow?.Invoke(selectInfo);
        [JSInvokable] public void DateClick(DateClickInfo dateClickInfo) => OnDateClick?.Invoke(dateClickInfo);
        [JSInvokable] public void Select(SelectionInfo selectionInfo) => OnSelect?.Invoke(selectionInfo);
        [JSInvokable] public void Unselect(object jsEvent, View view) => OnUnselect?.Invoke(jsEvent, view);
        [JSInvokable] public DateTime? GetNow() => OnGetNow?.Invoke();
        [JSInvokable] public void NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorDidMount?.Invoke(nowIndicatorInfo);
        [JSInvokable] public void NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorWillUnmount?.Invoke(nowIndicatorInfo);

        #endregion

        #region Event

        [JSInvokable] public object GetEventDataTransform(object eventData) => (object) OnGetEventDataTransform?.Invoke(eventData) ?? false;
        [JSInvokable] public void EventAdd(EventAddInfo eventAddInfo) => OnEventAdd?.Invoke(eventAddInfo);
        [JSInvokable] public void EventRemove(EventChangeInfo eventChangeInfo) => OnEventChange?.Invoke(eventChangeInfo);
        [JSInvokable] public void EventChange(EventAddInfo eventRemoveInfo) => OnEventRemove?.Invoke(eventRemoveInfo);
        [JSInvokable] public void EventsSet(IEnumerable<Event> events) => OnEventsSet?.Invoke(events);
        [JSInvokable] public int? GetEventOrder(Event eventA, Event eventB) => OnGetEventOrder?.Invoke(eventA, eventB);
        [JSInvokable] public void EventDidMount(EventRenderInfo eventRenderInfo) => OnEventDidMount?.Invoke(eventRenderInfo);
        [JSInvokable] public void EventWillUnmount(EventRenderInfo eventRenderInfo) => OnEventWillUnmount?.Invoke(eventRenderInfo);
        [JSInvokable] public void EventClick(EventClickInfo eventClickInfo) => OnEventClick?.Invoke(eventClickInfo);
        [JSInvokable] public void EventMouseEnter(EventClickInfo mouseEnterInfo) => OnEventMouseEnter?.Invoke(mouseEnterInfo);
        [JSInvokable] public void EventMouseLeave(EventClickInfo mouseLeaveInfo) => OnEventMouseLeave?.Invoke(mouseLeaveInfo);
        [JSInvokable] public bool GetEventOverlap(Event stillEvent, Event movingEvent) => OnGetEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool GetEventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent) => OnGetEventAllow?.Invoke(eventAllowInfo, draggedEvent) ?? true;
        [JSInvokable] public bool GetDropAccept(object draggableItem) => OnGetDropAccept?.Invoke(draggableItem) ?? true;
        [JSInvokable] public void EventDragStart(EventDragInfo eventDragInfo) => OnEventDragStart?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDragStop(EventDragInfo eventDragInfo) => OnEventDragStop?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDrop(EventDropInfo eventDropInfo) => OnEventDrop?.Invoke(eventDropInfo);
        [JSInvokable] public void Drop(DropInfo dropInfo) => OnDrop?.Invoke(dropInfo);
        [JSInvokable] public void EventReceive(ExternalEventDropInfo eventReceiveInfo) => OnEventReceive?.Invoke(eventReceiveInfo);
        [JSInvokable] public void EventLeave(ExternalEventDropInfo eventLeaveInfo) => OnEventLeave?.Invoke(eventLeaveInfo);
        [JSInvokable] public void EventResizeStart(EventDragInfo eventResizeInfo) => OnEventResizeStart?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResizeStop(EventDragInfo eventResizeInfo) => OnEventResizeStop?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResize(EventResizeInfo eventResizeInfo) => OnEventResize?.Invoke(eventResizeInfo);
        [JSInvokable] public void MoreLinkClick(MoreLinkClickInfo info) => OnMoreLinkClick?.Invoke(info);
        [JSInvokable] public void MoreLinkDidMount(MoreLinkInfo moreLinkInfo) => OnMoreLinkDidMount?.Invoke(moreLinkInfo);
        [JSInvokable] public void MoreLinkWillUnmount(MoreLinkInfo moreLinkInfo) => OnMoreLinkWillUnmount?.Invoke(moreLinkInfo);

        #endregion
    }
}