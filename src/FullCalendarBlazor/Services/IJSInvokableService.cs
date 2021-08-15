using System;
using System.Collections.Generic;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Display;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.Views;

namespace FullCalendarBlazor.Services
{
    public interface IJSInvokableService
    {
        #region Overall Display

        void WindowResize(WindowResizeInfo windowResizeInfo);

        #endregion

        #region Views

        void AllDayDidMount(AllDayInfo allDayInfo);
        void AllDayWillUnmount(AllDayInfo allDayInfo);
        void NoEventsDidMount(NoEventsInfo noEventsInfo);
        void NoEventsWillUnmount(NoEventsInfo noEventsInfo);
        DateRange GetVisibleRange(DateTime currentDate);
        void ViewDidMount(ViewInfo viewInfo);
        void ViewWillUnmount(ViewInfo viewInfo);

        #endregion Views

        #region Date and Time

        void DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo);
        void DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo);
        void DayCellDidMount(DayCellRenderInfo dayCellRenderInfo);
        void DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo);
        void SlotLabelDidMount(SlotRenderInfo slotRenderInfo);
        void SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo);
        void SlotLaneDidMount(SlotRenderInfo slotRenderInfo);
        void SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo);
        void DatesSet(DateInfo dateInfo);
        void NavLinkDayClick(DateTime date, object jsEvent);
        void NavLinkWeekClick(DateTime weekStart, object jsEvent);
        int? GetWeekNumber(DateTime date);
        void WeekNumberDidMount(WeekNumberInfo weekNumberInfo);
        void WeekNumberWillUnmount(WeekNumberInfo weekNumberInfo);
        bool? GetSelectOverlap(Event overlappedEvent);
        bool? GetSelectAllow(SelectInfo selectInfo);
        void DateClick(DateClickInfo dateClickInfo);
        void Select(SelectionInfo selectionInfo);
        void Unselect(object jsEvent, View view);
        DateTime? GetNow();
        void NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo);
        void NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo);

        #endregion

        #region Event

        object GetEventDataTransform(object eventData);
        void EventAdd(EventAddInfo eventAddInfo);
        void EventRemove(EventChangeInfo eventChangeInfo);
        void EventChange(EventAddInfo eventRemoveInfo);
        void EventsSet(IEnumerable<Event> events);
        int? GetEventOrder(Event eventA, Event eventB);
        void EventDidMount(EventRenderInfo eventRenderInfo);
        void EventWillUnmount(EventRenderInfo eventRenderInfo);
        void EventClick(EventClickInfo eventClickInfo);
        void EventMouseEnter(EventClickInfo mouseEnterInfo);
        void EventMouseLeave(EventClickInfo mouseLeaveInfo);
        bool GetEventOverlap(Event stillEvent, Event movingEvent);
        bool GetEventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent);
        bool GetDropAccept(object draggableItem);
        void EventDragStart(EventDragInfo eventDragInfo);
        void EventDragStop(EventDragInfo eventDragInfo);
        void EventDrop(EventDropInfo eventDropInfo);
        void Drop(DropInfo dropInfo);
        void EventReceive(ExternalEventDropInfo eventReceiveInfo);
        void EventLeave(ExternalEventDropInfo eventLeaveInfo);
        void EventResizeStart(EventDragInfo eventResizeInfo);
        void EventResizeStop(EventDragInfo eventResizeInfo);
        void EventResize(EventResizeInfo eventResizeInfo);
        void MoreLinkClick(MoreLinkClickInfo info);
        void MoreLinkDidMount(MoreLinkInfo moreLinkInfo);
        void MoreLinkWillUnmount(MoreLinkInfo moreLinkInfo);

        #endregion
    }
}