import {FullCalendar} from "./fullcalendar/main.min.js";

export function render(elementId, serializedData, objRef) {
    var calendarData = JSON.parse(serializedData);

    // Calendar functions
    calendarData.eventClick = (eventClickInfo) => objRef.invokeMethod('EventClick', eventClickInfo);
    calendarData.eventMouseEnter = (mouseEnterInfo) => objRef.invokeMethod('EventMouseEnter', mouseEnterInfo);
    calendarData.eventMouseLeave = (mouseLeaveInfo) => objRef.invokeMethod('EventMouseLeave', mouseLeaveInfo);
    calendarData.eventOverlap = (stillEvent, movingEvent) => objRef.invokeMethod('EventOverlap', stillEvent, movingEvent);
    calendarData.eventAllow = (dropInfo, draggedEvent) => objRef.invokeMethod('EventAllow', dropInfo, draggedEvent);
    calendarData.dropAccept = (draggableEl) => objRef.invokeMethod('DropAccept', draggableEl);

    var calendarElement = document.getElementById(elementId);
    var calendar = new FullCalendar.Calendar(calendarElement, calendarData);
    calendar.render();
}
