import {FullCalendar} from "./fullcalendar/main.min.js";

// Globals

const calendars = {};

export function render(elementId, serializedData, objRef) {
    const calendarData = JSON.parse(serializedData);

    // Calendar functions

    // region Overall Display

    calendarData.windowResize = (arg) => objRef.invokeMethod('WindowResize', arg.view);

    // endregion

    // region Views

    calendarData.allDayDidMount = (arg) => objRef.invokeMethod('AllDayDidMount', arg.text);
    calendarData.allDayWillUnmount = (arg) => objRef.invokeMethod('AllDayWillUnmount', arg.text);
    calendarData.noEventsDidMount = (arg) => objRef.invokeMethod('NoEventsDidMount', arg.el);
    calendarData.noEventsWillUnmount = (arg) => objRef.invokeMethod('NoEventsWillUnmount', arg.el);
    calendarData.viewDidMount = (arg) => objRef.invokeMethod('ViewDidMount', arg.view, arg.el);
    calendarData.viewWillUnmount = (arg) => objRef.invokeMethod('ViewWillUnmount', arg.view, arg.el);

    // endregion

    // region Date and Time

    calendarData.dayHeaderDidMount = (dayHeaderRenderInfo) => objRef.invokeMethod('DayHeaderDidMount', dayHeaderRenderInfo);
    calendarData.dayHeaderWillUnmount = (dayHeaderRenderInfo) => objRef.invokeMethod('DayHeaderWillUnmount', dayHeaderRenderInfo);
    calendarData.dayCellDidMount = (dayCellRenderInfo) => objRef.invokeMethod('DayCellDidMount', dayCellRenderInfo);
    calendarData.dayCellWillUnmount = (dayCellRenderInfo) => objRef.invokeMethod('DayCellWillUnmount', dayCellRenderInfo);
    calendarData.slotLabelDidMount = (slotRenderInfo) => objRef.invokeMethod('SlotLabelDidMount', slotRenderInfo);
    calendarData.slotLabelWillUnmount = (slotRenderInfo) => objRef.invokeMethod('SlotLabelWillUnmount', slotRenderInfo);
    calendarData.slotLaneDidMount = (slotRenderInfo) => objRef.invokeMethod('SlotLaneDidMount', slotRenderInfo);
    calendarData.slotLaneWillUnmount = (slotRenderInfo) => objRef.invokeMethod('SlotLaneWillUnmount', slotRenderInfo);
    calendarData.datesSet = (dateInfo) => objRef.invokeMethod('DatesSet', dateInfo);
    calendarData.weekNumberDidMount = (num, text, date) => objRef.invokeMethod('WeekNumberDidMount', num, text, date);
    calendarData.weekNumberWillUnmount = (num, text, date) => objRef.invokeMethod('WeekNumberWillUnmount', num, text, date);
    calendarData.selectAllow = (selectInfo) => objRef.invokeMethod('SelectAllow', selectInfo);
    calendarData.nowIndicatorDidMount = (nowIndicatorInfo) => objRef.invokeMethod('NowIndicatorDidMount', nowIndicatorInfo);
    calendarData.nowIndicatorWillUnmount = (nowIndicatorInfo) => objRef.invokeMethod('NowIndicatorWillUnmount', nowIndicatorInfo);

    // endregion

    // region Events

    calendarData.eventAdd = (eventAddInfo) => objRef.invokeMethod('EventAdd', eventAddInfo);
    calendarData.eventChange = (eventChangeInfo) => objRef.invokeMethod('EventChange', eventChangeInfo);
    calendarData.eventRemove = (eventRemoveInfo) => objRef.invokeMethod('EventRemove', eventRemoveInfo);
    calendarData.eventsSet = (events) => objRef.invokeMethod('EventsSet', events);
    calendarData.eventDidMount = (eventRenderInfo) => objRef.invokeMethod('EventDidMount', eventRenderInfo);
    calendarData.eventWillUnmount = (eventRenderInfo) => objRef.invokeMethod('EventWillUnmount', eventRenderInfo);
    calendarData.eventClick = (eventClickInfo) => objRef.invokeMethod('EventClick', eventClickInfo);
    calendarData.eventMouseEnter = (mouseEnterInfo) => objRef.invokeMethod('EventMouseEnter', mouseEnterInfo);
    calendarData.eventMouseLeave = (mouseLeaveInfo) => objRef.invokeMethod('EventMouseLeave', mouseLeaveInfo);
    calendarData.eventOverlap = (stillEvent, movingEvent) => objRef.invokeMethod('EventOverlap', stillEvent, movingEvent);
    calendarData.eventAllow = (eventAllowInfo, draggedEvent) => objRef.invokeMethod('EventAllow', eventAllowInfo, draggedEvent);
    calendarData.dropAccept = (draggableEl) => objRef.invokeMethod('DropAccept', draggableEl);
    calendarData.eventDragStart = (eventDragInfo) => objRef.invokeMethod('EventDragStart', eventDragInfo);
    calendarData.eventDragStop = (eventDragInfo) => objRef.invokeMethod('EventDragStop', eventDragInfo);
    calendarData.eventDrop = (eventDropInfo) => objRef.invokeMethod('EventDrop', eventDropInfo);
    calendarData.drop = (dropInfo) => objRef.invokeMethod('Drop', dropInfo);
    calendarData.eventReceive = (eventReceiveInfo) => objRef.invokeMethod('EventReceive', eventReceiveInfo);
    calendarData.eventLeave = (eventLeaveInfo) => objRef.invokeMethod('EventLeave', eventLeaveInfo);
    calendarData.eventResizeStart = (eventResizeInfo) => objRef.invokeMethod('EventResizeStart', eventResizeInfo);
    calendarData.eventResizeStop = (eventResizeInfo) => objRef.invokeMethod('EventResizeStop', eventResizeInfo);
    calendarData.eventResize = (eventResizeInfo) => objRef.invokeMethod('EventResize', eventResizeInfo);
    calendarData.moreLinkDidMount = (num, text) => objRef.invokeMethod('MoreLinkDidMount', num, text);
    calendarData.moreLinkWillUnmount = (num, text) => objRef.invokeMethod('MoreLinkWillUnmount', num, text);

    // endregion

    const calendarElement = document.getElementById(elementId);
    calendars[elementId] = new FullCalendar.Calendar(calendarElement, calendarData);
    calendars[elementId].render();
}
