import {FullCalendar} from "./fullcalendar/main.min.js";

export function render(elementId, serializedData, objRef) {
    var calendarData = JSON.parse(serializedData);

    // Calendar functions
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

    var calendarElement = document.getElementById(elementId);
    var calendar = new FullCalendar.Calendar(calendarElement, calendarData);
    calendar.render();
}
