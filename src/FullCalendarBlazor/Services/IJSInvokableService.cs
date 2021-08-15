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

        void _WindowResize(WindowResizeInfo windowResizeInfo);

        #endregion

        #region Views

        void _AllDayDidMount(AllDayInfo allDayInfo);
        void _AllDayWillUnmount(AllDayInfo allDayInfo);
        void _NoEventsDidMount(NoEventsInfo noEventsInfo);
        void _NoEventsWillUnmount(NoEventsInfo noEventsInfo);
        DateRange _GetVisibleRange(DateTime currentDate);
        void _ViewDidMount(ViewInfo viewInfo);
        void _ViewWillUnmount(ViewInfo viewInfo);

        #endregion Views

        #region Date and Time

        void _DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo);
        void _DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo);
        void _DayCellDidMount(DayCellRenderInfo dayCellRenderInfo);
        void _DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo);
        void _SlotLabelDidMount(SlotRenderInfo slotRenderInfo);
        void _SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo);
        void _SlotLaneDidMount(SlotRenderInfo slotRenderInfo);
        void _SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo);
        void _DatesSet(DateInfo dateInfo);
        void _NavLinkDayClick(DateTime date, object jsEvent);
        void _NavLinkWeekClick(DateTime weekStart, object jsEvent);
        int? _GetWeekNumber(DateTime date);
        void _WeekNumberDidMount(WeekNumberInfo weekNumberInfo);
        void _WeekNumberWillUnmount(WeekNumberInfo weekNumberInfo);
        bool? _GetSelectOverlap(Event overlappedEvent);
        bool? _GetSelectAllow(SelectInfo selectInfo);
        void _DateClick(DateClickInfo dateClickInfo);
        void _Select(SelectionInfo selectionInfo);
        void _Unselect(object jsEvent, View view);
        DateTime? _GetNow();
        void _NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo);
        void _NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo);

        #endregion

        #region Event

        object _GetEventDataTransform(object eventData);
        void _EventAdd(EventAddInfo eventAddInfo);
        void _EventRemove(EventChangeInfo eventChangeInfo);
        void _EventChange(EventAddInfo eventRemoveInfo);
        void _EventsSet(IEnumerable<Event> events);
        int? _GetEventOrder(Event eventA, Event eventB);
        void _EventDidMount(EventRenderInfo eventRenderInfo);
        void _EventWillUnmount(EventRenderInfo eventRenderInfo);
        void _EventClick(EventClickInfo eventClickInfo);
        void _EventMouseEnter(EventClickInfo mouseEnterInfo);
        void _EventMouseLeave(EventClickInfo mouseLeaveInfo);
        bool _GetEventOverlap(Event stillEvent, Event movingEvent);
        bool _GetEventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent);
        bool _GetDropAccept(object draggableItem);
        void _EventDragStart(EventDragInfo eventDragInfo);
        void _EventDragStop(EventDragInfo eventDragInfo);
        void _EventDrop(EventDropInfo eventDropInfo);
        void _Drop(DropInfo dropInfo);
        void _EventReceive(ExternalEventDropInfo eventReceiveInfo);
        void _EventLeave(ExternalEventDropInfo eventLeaveInfo);
        void _EventResizeStart(EventDragInfo eventResizeInfo);
        void _EventResizeStop(EventDragInfo eventResizeInfo);
        void _EventResize(EventResizeInfo eventResizeInfo);
        void _MoreLinkClick(MoreLinkClickInfo info);
        void _MoreLinkDidMount(MoreLinkInfo moreLinkInfo);
        void _MoreLinkWillUnmount(MoreLinkInfo moreLinkInfo);

        #endregion
    }
}